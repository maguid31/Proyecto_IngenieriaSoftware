using DAL;
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
        private FamiliaDAL_65RD _familiaDAL = new FamiliaDAL_65RD();

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

        public ComponentePermiso_65RD ObtenerPermisoRecursivo(ComponentePermiso_65RD padre)
        {
            if (padre == null) return null;

            var hijos = _familiaDAL.ObtenerHijosDeFamilia(padre.Id);
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

        public ComponentePermiso_65RD ObtenerPermisoRecursivo(int permisoId)
        {
            ComponentePermiso_65RD componente = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

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

        public List<ComponentePermiso_65RD> ObtenerTodosLosPermisos()
        {
            List<ComponentePermiso_65RD> lista = new List<ComponentePermiso_65RD>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

               
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

    }
    
}




