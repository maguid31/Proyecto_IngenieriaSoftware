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
        private FamiliaDAL_65RD _familiaDAL = new FamiliaDAL_65RD();

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
                            else 
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

        private List<ComponentePermiso_65RD> ObtenerPermisosDePerfil(int idPerfil)
        {
            var lista = new List<ComponentePermiso_65RD>();
            var permDAL = new PermisoDAL_65RD();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string queryPatentes = @"
            SELECT p.Id, p.Nombre, p.Descripcion
            FROM Permisos p
            INNER JOIN Perfil_Permiso pp ON p.Id = pp.IdPermiso
            WHERE pp.IdPerfil = @id";

                using (SqlCommand cmd = new SqlCommand(queryPatentes, con))
                {
                    cmd.Parameters.AddWithValue("@id", idPerfil);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            lista.Add(new Patente_65RD
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                Nombre = r["Nombre"].ToString(),
                                Descripcion = r["Descripcion"].ToString()
                            });
                        }
                    }
                }

                string queryFamilias = @"
            SELECT f.Id, f.Nombre, f.Descripcion
            FROM Familia f
            INNER JOIN Perfil_Familia pf ON f.Id = pf.IdFamilia
            WHERE pf.IdPerfil = @id";

                using (SqlCommand cmd = new SqlCommand(queryFamilias, con))
                {
                    cmd.Parameters.AddWithValue("@id", idPerfil);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var familia = new Familia_65RD
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                Nombre = r["Nombre"].ToString(),
                                Descripcion = r["Descripcion"].ToString()
                            };
                            CargarHijosRecursivos(familia); // carga los hijos en memoria
                            lista.Add(familia);
                        }
                    }
                }
            }

            return lista;
        }

        private void CargarHijosRecursivos(ComponentePermiso_65RD padre)
        {
            var hijos = _familiaDAL.ObtenerHijosDeFamilia(padre.Id);
            foreach (var hijo in hijos)
            {
                padre.AgregarHijo(hijo);
                if (hijo is Familia_65RD)
                    CargarHijosRecursivos(hijo);
            }
        }
    }
}
