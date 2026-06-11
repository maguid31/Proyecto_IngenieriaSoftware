using Servicios;
using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_65RD
{
    public class PermisoDAL_65RD
    {
        private string connectionString = @"Data Source=.;Initial Catalog=proyecto_ingenieria;Integrated Security=True";

        // 1. LÓGICA RECURSIVA PARA RECUPERAR FAMILIAS Y PATENTES

        /// Recupera un árbol completo de permisos (Patente o Familia con sus hijos) a partir de su ID.

        public ComponentePermiso_65RD ObtenerPermisoRecursivo(int idPermiso)
        {
            ComponentePermiso_65RD componente = null;
            bool esFamilia = false;

            // 1. Buscamos el permiso base
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, EsFamilia, Descripcion FROM Permisos WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idPermiso);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            esFamilia = Convert.ToBoolean(reader["EsFamilia"]);

                            if (esFamilia)
                                componente = new Familia_65RD();
                            else
                                componente = new Patente_65RD();

                            componente.Id = Convert.ToInt32(reader["Id"]);
                            componente.Nombre = reader["Nombre"].ToString();
                            componente.Descripcion = reader["Descripcion"].ToString();
                        }
                    }
                }
            }

            // 2. Si es una familia, aplicamos RECURSIVIDAD para buscar a sus hijos
            if (componente != null && esFamilia)
            {
                List<int> idHijos = new List<int>();

                // Obtenemos los IDs de los hijos desde la tabla intermedia
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string queryRelaciones = "SELECT IdHijo FROM Familia_Permiso WHERE IdPadre = @idPadre";
                    using (SqlCommand cmdRel = new SqlCommand(queryRelaciones, con))
                    {
                        cmdRel.Parameters.AddWithValue("@idPadre", idPermiso);
                        con.Open();
                        using (SqlDataReader readerRel = cmdRel.ExecuteReader())
                        {
                            while (readerRel.Read())
                            {
                                idHijos.Add(Convert.ToInt32(readerRel["IdHijo"]));
                            }
                        }
                    }
                }

                // Llamada recursiva por cada hijo encontrado
                foreach (int idHijo in idHijos)
                {
                    ComponentePermiso_65RD hijo = ObtenerPermisoRecursivo(idHijo);
                    if (hijo != null)
                    {
                        componente.AgregarHijo(hijo);
                    }
                }
            }

            return componente;
        }


        // 2. LÓGICA RECURSIVA PARA GUARDAR NUEVAS FAMILIAS

        /// Guarda una nueva familia en la BD y sus relaciones. 

        public bool GuardarFamilia(Familia_65RD familia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction transaccion = con.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertamos la cabecera de la Familia
                        string queryInsert = "INSERT INTO Permisos (Nombre, EsFamilia, Descripcion) OUTPUT INSERTED.Id VALUES (@nombre, 1, @desc)";
                        using (SqlCommand cmd = new SqlCommand(queryInsert, con, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@nombre", familia.Nombre);
                            cmd.Parameters.AddWithValue("@desc", (object)familia.Descripcion ?? DBNull.Value);

                            // Obtenemos el ID autogenerado
                            familia.Id = (int)cmd.ExecuteScalar();
                        }

                        // 2. Guardamos las relaciones recursivamente
                        foreach (var hijo in familia.ObtenerHijos())
                        {
                            GuardarRelacionRecursiva(familia.Id, hijo, con, transaccion);
                        }

                        transaccion.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                        throw; // Dejamos que la BLL capture el error
                    }
                }
            }
        }

        
        /// Inserta el registro en la tabla intermedia. Si el hijo es una Familia que aún no existe en BD (Id=0), 
    
        private void GuardarRelacionRecursiva(int idPadre, ComponentePermiso_65RD hijo, SqlConnection con, SqlTransaction transaccion)
        {
            // Insertamos la relación Padre-Hijo en la tabla intermedia
            string query = "INSERT INTO Familia_Permiso (IdPadre, IdHijo) VALUES (@padre, @hijo)";
            using (SqlCommand cmd = new SqlCommand(query, con, transaccion))
            {
                cmd.Parameters.AddWithValue("@padre", idPadre);
                cmd.Parameters.AddWithValue("@hijo", hijo.Id);
                cmd.ExecuteNonQuery();
            }

            // NOTA: Si quisieras permitir la creación de un árbol entero desde cero 
            // (donde los hijos también son nuevos y tienen Id=0), deberías insertar al hijo aquí
            // y luego hacer una llamada recursiva a GuardarRelacionRecursiva con los hijos del hijo.
            // Según las reglas del profesor, agrupamos permisos EXISTENTES, por lo que insertar la relación basta.
        }

        public List<ComponentePermiso_65RD> ObtenerTodosLosPermisos()
        {
            List<ComponentePermiso_65RD> listaPermisos = new List<ComponentePermiso_65RD>();
            List<int> idsPermisos = new List<int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id FROM Permisos";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idsPermisos.Add(Convert.ToInt32(reader["Id"]));
                        }
                    }
                }
            }

            foreach (int id in idsPermisos)
            {
                var permiso = ObtenerPermisoRecursivo(id);
                if (permiso != null)
                {
                    listaPermisos.Add(permiso);
                }
            }

            return listaPermisos;
        }

        public List<Patente_65RD> ObtenerTodasLasPatentes()
        {
            List<Patente_65RD> patentes = new List<Patente_65RD>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, Descripcion FROM Permisos WHERE EsFamilia = 0";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patentes.Add(new Patente_65RD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            return patentes;
        }

        public bool ActualizarFamilia(Familia_65RD familia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction transaccion = con.BeginTransaction())
                {
                    try
                    {
                        // 1. Actualizamos el nombre y descripción
                        string queryUpdate = "UPDATE Permisos SET Nombre = @nombre, Descripcion = @desc WHERE Id = @id";
                        using (SqlCommand cmd = new SqlCommand(queryUpdate, con, transaccion))
                        {
                            cmd.Parameters.AddWithValue("@nombre", familia.Nombre);
                            cmd.Parameters.AddWithValue("@desc", (object)familia.Descripcion ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@id", familia.Id);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Borramos las relaciones viejas de esta familia
                        string queryDelete = "DELETE FROM Familia_Permiso WHERE IdPadre = @idPadre";
                        using (SqlCommand cmdDel = new SqlCommand(queryDelete, con, transaccion))
                        {
                            cmdDel.Parameters.AddWithValue("@idPadre", familia.Id);
                            cmdDel.ExecuteNonQuery();
                        }

                        // 3. Insertamos las relaciones nuevas (recursivamente)
                        foreach (var hijo in familia.ObtenerHijos())
                        {
                            GuardarRelacionRecursiva(familia.Id, hijo, con, transaccion);
                        }

                        transaccion.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
