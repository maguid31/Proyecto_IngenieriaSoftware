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

        // 1. Carga los componentes iniciales asignados a un Perfil
        // 1. Carga los componentes iniciales asignados a un Perfil
        public List<ComponentePermiso_65RD> ObtenerPermisosPorPerfil(int perfilId)
        {
            List<ComponentePermiso_65RD> lista = new List<ComponentePermiso_65RD>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string queryPatentes = @"
            SELECT p.Id, p.Nombre, p.Descripcion 
            FROM Permisos p
            INNER JOIN Perfil_Permiso pp ON p.Id = pp.IdPermiso
            WHERE pp.IdPerfil = @perfilId";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryPatentes, con))
                {
                    cmd.Parameters.AddWithValue("@perfilId", perfilId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Patente_65RD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString()
                            });
                        }
                    }
                }

                string queryFamilias = @"
            SELECT f.Id, f.Nombre, f.Descripcion 
            FROM Familia f
            INNER JOIN Perfil_Familia pf ON f.Id = pf.IdFamilia
            WHERE pf.IdPerfil = @perfilId";

                using (SqlCommand cmd = new SqlCommand(queryFamilias, con))
                {
                    cmd.Parameters.AddWithValue("@perfilId", perfilId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Familia_65RD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 2. Mantiene la recursividad en memoria
        public ComponentePermiso_65RD ObtenerPermisoRecursivo(ComponentePermiso_65RD padre)
        {
            if (padre == null) return null;

            var hijos = ObtenerHijosDeFamilia(padre.Id);
            foreach (var hijo in hijos)
            {
                padre.AgregarHijo(hijo);
                if (hijo is Familia_65RD)
                {
                    ObtenerPermisoRecursivo(hijo);
                }
            }
            return padre;
        }

        // Sobrecarga por Id
        public ComponentePermiso_65RD ObtenerPermisoRecursivo(int permisoId)
        {
            ComponentePermiso_65RD componente = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Buscamos primero en la tabla de Familias
                string qFam = "SELECT Id, Nombre, Descripcion FROM Familia WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(qFam, con))
                {
                    cmd.Parameters.AddWithValue("@id", permisoId);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            componente = new Familia_65RD
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                Nombre = r["Nombre"].ToString(),
                                Descripcion = r["Descripcion"].ToString()
                            };
                        }
                    }
                }

                // Si no era familia, buscamos en la tabla de Permisos (Patentes)
                if (componente == null)
                {
                    string qPat = "SELECT Id, Nombre, Descripcion FROM Permisos WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(qPat, con))
                    {
                        cmd.Parameters.AddWithValue("@id", permisoId);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                componente = new Patente_65RD
                                {
                                    Id = Convert.ToInt32(r["Id"]),
                                    Nombre = r["Nombre"].ToString(),
                                    Descripcion = r["Descripcion"].ToString()
                                };
                            }
                        }
                    }
                }
            }

            return componente is Familia_65RD ? ObtenerPermisoRecursivo(componente) : componente;
        }

        public List<ComponentePermiso_65RD> ObtenerHijosDeFamilia(int familiaPadreId)
        {
            List<ComponentePermiso_65RD> hijos = new List<ComponentePermiso_65RD>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Hijos que son Patentes (Familia_Permiso)
                string queryPatentes = @"
            SELECT p.Id, p.Nombre, p.Descripcion 
            FROM Permisos p
            INNER JOIN Familia_Permiso fp ON p.Id = fp.IdHijo
            WHERE fp.IdPadre = @padreId";

                using (SqlCommand cmd = new SqlCommand(queryPatentes, con))
                {
                    cmd.Parameters.AddWithValue("@padreId", familiaPadreId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hijos.Add(new Patente_65RD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString()
                            });
                        }
                    }
                }

                // Hijos que son Familias (Familia_Familia) ← ESTO FALTABA
                string queryFamilias = @"
            SELECT f.Id, f.Nombre, f.Descripcion 
            FROM Familia f
            INNER JOIN Familia_Familia ff ON f.Id = ff.IdFamiliaHijo
            WHERE ff.IdFamiliaPadre = @padreId";

                using (SqlCommand cmd = new SqlCommand(queryFamilias, con))
                {
                    cmd.Parameters.AddWithValue("@padreId", familiaPadreId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hijos.Add(new Familia_65RD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            return hijos;
        }

        // 4. Obtiene de manera unificada todo lo que existe (Une tablas Familia y Permisos)
        public List<ComponentePermiso_65RD> ObtenerTodosLosPermisos()
        {
            List<ComponentePermiso_65RD> lista = new List<ComponentePermiso_65RD>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Solo familias que NO son hijas de otra familia
                string qFamilias = @"
            SELECT Id, Nombre, Descripcion FROM Familia
            WHERE Id NOT IN (SELECT IdFamiliaHijo FROM Familia_Familia)";

                using (SqlCommand cmd = new SqlCommand(qFamilias, con))
                using (SqlDataReader r = cmd.ExecuteReader())
                    while (r.Read())
                        lista.Add(new Familia_65RD
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            Nombre = r["Nombre"].ToString(),
                            Descripcion = r["Descripcion"].ToString()
                        });

                // Solo patentes que NO son hijas de ninguna familia
                string qPatentes = @"
            SELECT Id, Nombre, Descripcion FROM Permisos
            WHERE Id NOT IN (SELECT IdHijo FROM Familia_Permiso)";

                using (SqlCommand cmd = new SqlCommand(qPatentes, con))
                using (SqlDataReader r = cmd.ExecuteReader())
                    while (r.Read())
                        lista.Add(new Patente_65RD
                        {
                            Id = Convert.ToInt32(r["Id"]),
                            Nombre = r["Nombre"].ToString(),
                            Descripcion = r["Descripcion"].ToString()
                        });
            }
            return lista;
        }

        // 5. Trae las patentes limpias de la tabla Permisos
        public List<Patente_65RD> ObtenerTodasLasPatentes()
        {
            List<Patente_65RD> lista = new List<Patente_65RD>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, Descripcion FROM Permisos";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Patente_65RD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<Familia_65RD> ObtenerTodasLasFamilias()
        {
            var lista = new List<Familia_65RD>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Nombre, Descripcion FROM Familia", con))
                {
                    con.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                        while (r.Read())
                            lista.Add(new Familia_65RD
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                Nombre = r["Nombre"].ToString(),
                                Descripcion = r["Descripcion"].ToString()
                            });
                }
            }
            return lista;
        }

        // 6. ¡COMPLETO!: Ahora sí guarda de verdad en la base de datos relacional
        public bool GuardarFamilia(ComponentePermiso_65RD familia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        // A. Insertamos la cabecera en la tabla Familia sacando el ID autogenerado
                        string queryFam = "INSERT INTO Familia (Nombre, Descripcion) OUTPUT INSERTED.Id VALUES (@nom, @desc)";
                        using (SqlCommand cmd = new SqlCommand(queryFam, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@nom", familia.Nombre);
                            cmd.Parameters.AddWithValue("@desc", (object)familia.Descripcion ?? DBNull.Value);
                            familia.Id = (int)cmd.ExecuteScalar();
                        }

                        // B. Insertamos las relaciones de los componentes hijos en Familia_Permiso
                        foreach (var hijo in familia.ObtenerHijos())
                        {
                            if (hijo is Familia_65RD)
                            {
                                string querySubFam = "INSERT INTO Familia_Familia (IdFamiliaPadre, IdFamiliaHijo) VALUES (@padre, @hijo)";
                                using (SqlCommand cmd = new SqlCommand(querySubFam, con, tx))
                                {
                                    cmd.Parameters.AddWithValue("@padre", familia.Id);
                                    cmd.Parameters.AddWithValue("@hijo", hijo.Id);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else // Patente_65RD
                            {
                                string queryPatente = "INSERT INTO Familia_Permiso (IdPadre, IdHijo) VALUES (@padre, @hijo)";
                                using (SqlCommand cmd = new SqlCommand(queryPatente, con, tx))
                                {
                                    cmd.Parameters.AddWithValue("@padre", familia.Id);
                                    cmd.Parameters.AddWithValue("@hijo", hijo.Id);
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

        public bool ActualizarFamilia(ComponentePermiso_65RD familia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        // A. Actualizamos la descripción o nombre de la Familia cabecera
                        string queryUpdate = "UPDATE Familia SET Nombre = @nom, Descripcion = @desc WHERE Id = @id";
                        using (SqlCommand cmd = new SqlCommand(queryUpdate, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@nom", familia.Nombre);
                            cmd.Parameters.AddWithValue("@desc", (object)familia.Descripcion ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@id", familia.Id);
                            cmd.ExecuteNonQuery();
                        }

                        // B. Limpiamos relaciones viejas
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia_Permiso WHERE IdPadre = @id", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", familia.Id);
                            cmd.ExecuteNonQuery();
                        }
                        // Después del DELETE de Familia_Permiso, agregá:
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia_Familia WHERE IdFamiliaPadre = @id", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", familia.Id);
                            cmd.ExecuteNonQuery();
                        }
                        // C. Re-insertamos los componentes hijos actuales
                        foreach (var hijo in familia.ObtenerHijos())
                        {
                            if (hijo is Familia_65RD)
                            {
                                string querySubFam = "INSERT INTO Familia_Familia (IdFamiliaPadre, IdFamiliaHijo) VALUES (@padre, @hijo)";
                                using (SqlCommand cmd = new SqlCommand(querySubFam, con, tx))
                                {
                                    cmd.Parameters.AddWithValue("@padre", familia.Id);
                                    cmd.Parameters.AddWithValue("@hijo", hijo.Id);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else // Patente_65RD
                            {
                                string queryPatente = "INSERT INTO Familia_Permiso (IdPadre, IdHijo) VALUES (@padre, @hijo)";
                                using (SqlCommand cmd = new SqlCommand(queryPatente, con, tx))
                                {
                                    cmd.Parameters.AddWithValue("@padre", familia.Id);
                                    cmd.Parameters.AddWithValue("@hijo", hijo.Id);
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

        public bool EliminarPermiso(int idPermiso)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        // 1. Borramos relaciones en Familia_Permiso
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia_Permiso WHERE IdPadre = @id", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", idPermiso);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Borramos relaciones de perfiles asociados a la familia
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Perfil_Familia WHERE IdFamilia = @id", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", idPermiso);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia_Familia WHERE IdFamiliaPadre = @id OR IdFamiliaHijo = @id", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", idPermiso);
                            cmd.ExecuteNonQuery();
                        }

                        // 3. Borramos la cabecera definitiva de la tabla Familia
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia WHERE Id = @id", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", idPermiso);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
    
}




