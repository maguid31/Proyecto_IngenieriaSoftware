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
    public class FamiliaDAL_65RD
    {

        private string connectionString = @"Data Source=.;Initial Catalog=proyecto_ingenieria;Integrated Security=True";


        public bool GuardarFamilia(ComponentePermiso_65RD familia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        string queryFam = "INSERT INTO Familia (Nombre, Descripcion) OUTPUT INSERTED.Id VALUES (@nom, @desc)";
                        using (SqlCommand cmd = new SqlCommand(queryFam, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@nom", familia.Nombre);
                            cmd.Parameters.AddWithValue("@desc", (object)familia.Descripcion ?? DBNull.Value);
                            familia.Id = (int)cmd.ExecuteScalar();
                        }

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
                            else 
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
                        string queryUpdate = "UPDATE Familia SET Nombre = @nom, Descripcion = @desc WHERE Id = @id";
                        using (SqlCommand cmd = new SqlCommand(queryUpdate, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@nom", familia.Nombre);
                            cmd.Parameters.AddWithValue("@desc", (object)familia.Descripcion ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@id", familia.Id);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia_Permiso WHERE IdPadre = @id", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", familia.Id);
                            cmd.ExecuteNonQuery();
                        }
                 
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia_Familia WHERE IdFamiliaPadre = @id", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", familia.Id);
                            cmd.ExecuteNonQuery();
                        }
                    
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
                            else 
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


        public List<ComponentePermiso_65RD> ObtenerHijosDeFamilia(int familiaPadreId)
        {
            List<ComponentePermiso_65RD> hijos = new List<ComponentePermiso_65RD>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

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

        public bool EliminarFamilia(int idFamilia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia_Permiso WHERE IdPadre = @id", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", idFamilia);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Perfil_Familia WHERE IdFamilia = @id", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", idFamilia);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia_Familia WHERE IdFamiliaPadre = @id OR IdFamiliaHijo = @id", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", idFamilia);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Familia WHERE Id = @id", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@id", idFamilia);
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
        public int ContarPerfilesQueUsanFamilia(int idFamilia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Perfil_Familia WHERE IdFamilia = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idFamilia);
                    con.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
