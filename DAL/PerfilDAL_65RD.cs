using DAL_65RD;
using Servicios;
using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PerfilDAL_65RD
    {
        private string connectionString = @"Data Source=.;Initial Catalog=proyecto_ingenieria;Integrated Security=True";

        // ──────────────────────────────────────────────
        //  MÉTODOS EXISTENTES
        // ──────────────────────────────────────────────

        public int ContarUsuariosPorPerfil(int idPerfil)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE PerfilId = @idPerfil";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPerfil", idPerfil);
                    con.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public bool EliminarPerfil(int idPerfil)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Perfil_Permiso WHERE IdPerfil = @id", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idPerfil);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Perfiles WHERE Id = @id", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idPerfil);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        return true;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Perfil_65RD> ObtenerTodosLosPerfiles()
        {
            var lista = new List<Perfil_65RD>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, Descripcion FROM Perfiles ORDER BY Nombre";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            lista.Add(new Perfil_65RD
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                Nombre = r["Nombre"].ToString(),
                                Descripcion = r["Descripcion"] == DBNull.Value ? string.Empty : r["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }

            // Cargamos los permisos asignados a cada perfil
            foreach (var perfil in lista)
                perfil.PermisosAsignados = ObtenerPermisosDePerfil(perfil.Id);

            return lista;
        }

        public bool InsertarPerfil(Perfil_65RD perfil)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = @"
                INSERT INTO Perfiles (Nombre, Descripcion) 
                OUTPUT INSERTED.Id 
                VALUES (@nombre, @desc)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", perfil.Nombre);
                    cmd.Parameters.AddWithValue("@desc", (object)perfil.Descripcion ?? DBNull.Value);

                    con.Open();
                    
                    perfil.Id = (int)cmd.ExecuteScalar();
                    return perfil.Id > 0;
                }
            }
        }

      
        public bool ActualizarPermisosDelPerfil(Perfil_65RD perfil)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        // 1. Borramos asignaciones anteriores en ambas tablas intermedias
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Perfil_Permiso WHERE IdPerfil = @id", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", perfil.Id);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Perfil_Familia WHERE IdPerfil = @id", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", perfil.Id);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Recorremos e insertamos discriminando el tipo de componente en la tabla correcta
                        foreach (var permiso in perfil.PermisosAsignados)
                        {
                            if (permiso is Familia_65RD)
                            {
                                string queryInsFam = "INSERT INTO Perfil_Familia (IdPerfil, IdFamilia) VALUES (@idPerfil, @idFamilia)";
                                using (SqlCommand cmd = new SqlCommand(queryInsFam, con, tx))
                                {
                                    cmd.Parameters.AddWithValue("@idPerfil", perfil.Id);
                                    cmd.Parameters.AddWithValue("@idFamilia", permiso.Id);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else // Es Patente_65RD
                            {
                                string queryInsPat = "INSERT INTO Perfil_Permiso (IdPerfil, IdPermiso) VALUES (@idPerfil, @idPermiso)";
                                using (SqlCommand cmd = new SqlCommand(queryInsPat, con, tx))
                                {
                                    cmd.Parameters.AddWithValue("@idPerfil", perfil.Id);
                                    cmd.Parameters.AddWithValue("@idPermiso", permiso.Id);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        tx.Commit();
                        return true;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        /// Recupera los permisos (Patentes o Familias) asignados a un perfil usando la recursividad de PermisoDAL.
        private List<ComponentePermiso_65RD> ObtenerPermisosDePerfil(int idPerfil)
        {
            var lista = new List<ComponentePermiso_65RD>();
            var permDAL = new PermisoDAL_65RD();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // 🌟 CORRECCIÓN: Usamos UNION para traer los IDs tanto de Patentes como de Familias asociadas al Perfil
                string query = @"
                         SELECT IdPermiso AS PermisoId FROM Perfil_Permiso WHERE IdPerfil = @id
                         UNION
                         SELECT IdFamilia AS PermisoId FROM Perfil_Familia WHERE IdPerfil = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idPerfil);
                    con.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            int idPermiso = Convert.ToInt32(r["PermisoId"]);

                            // Tu método recursivo se encarga de determinar inteligentemente si es Patente o Familia
                            var comp = permDAL.ObtenerPermisoRecursivo(idPermiso);
                            if (comp != null) lista.Add(comp);
                        }
                    }
                }
            }
            return lista;
        }
    }
}
