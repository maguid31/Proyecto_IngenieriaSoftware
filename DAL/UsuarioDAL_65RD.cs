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
    public class UsuarioDAL_65RD
    {
        private string connectionString = @"Data Source=.;Initial Catalog=proyecto_ingenieria;Integrated Security=True";
        //@"Data Source=DESKTOP-UOCRKUM;Initial Catalog=proyecto_ingenieria;Integrated Security=True";

        public Usuario_65RD ObtenerUsuarioPorLogin(string usuarioConcatenado)
        {
            Usuario_65RD usuarioEncontrado = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = @"
                    SELECT u.Id, u.Nombre, u.Apellido, u.DNI, u.Contraseña, u.Activo, u.PrimerLogin, u.Bloqueado, u.Idioma,
                    p.Id AS PerfilId, p.Nombre AS PerfilNombre
                    FROM Usuarios u
                    INNER JOIN Perfiles p ON u.PerfilId = p.Id
                    WHERE CONCAT(u.Apellido, u.DNI) = @usuario";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuarioConcatenado);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuarioEncontrado = new Usuario_65RD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                DNI = reader["DNI"].ToString(),
                                Contraseña = reader["Contraseña"].ToString(),
                                Activo = Convert.ToBoolean(reader["Activo"]),
                                PrimerLogin = Convert.ToBoolean(reader["PrimerLogin"]),
                                Bloqueado = Convert.ToBoolean(reader["Bloqueado"]),
                                Idioma = reader["Idioma"].ToString() ?? "es",


                                Perfil = new Perfil_65RD
                                {
                                    Id = Convert.ToInt32(reader["PerfilId"]),
                                    Nombre = reader["PerfilNombre"].ToString()
                                }
                            };
                        }
                    }
                }
            }

            if (usuarioEncontrado != null && usuarioEncontrado.Perfil != null)
            {
                PermisoDAL_65RD permisoDAL = new PermisoDAL_65RD();

                // Buscamos los permisos iniciales de su rol
                var componentesRaiz = permisoDAL.ObtenerPermisosPorPerfil(usuarioEncontrado.Perfil.Id);

                foreach (var comp in componentesRaiz)
                {
                    // Si el permiso es una familia, llamamos a la función mágica para que busque sus hijos
                    if (comp is Familia_65RD)
                    {
                        CargarHijosRecursivos(comp, permisoDAL);
                    }

                    // Agregamos el permiso completo con sus hijos al usuario logueado
                    usuarioEncontrado.Perfil.PermisosAsignados.Add(comp);
                }
            }
            return usuarioEncontrado;
        }

        private void CargarHijosRecursivos(ComponentePermiso_65RD padre, PermisoDAL_65RD dal)
        {
           
            var hijos = dal.ObtenerHijosDeFamilia(padre.Id);
            foreach (var hijo in hijos)
            {
                padre.AgregarHijo(hijo); 

                if (hijo is Familia_65RD) 
                {
                    CargarHijosRecursivos(hijo, dal);
                }
            }
        }
        public bool RegistrarUsuario(Usuario_65RD nuevoUsuario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string query = "INSERT INTO Usuarios (Nombre, Apellido, DNI, Contraseña, PerfilId, Activo, Email, Idioma, PrimerLogin) " +
               "VALUES (@nombre, @apellido, @dni, @contraseña, @perfilId, @activo, @email, @idioma, @primerLogin)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", (object)nuevoUsuario.Nombre ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@apellido", nuevoUsuario.Apellido);
                    cmd.Parameters.AddWithValue("@dni", nuevoUsuario.DNI);
                    cmd.Parameters.AddWithValue("@contraseña", nuevoUsuario.Contraseña);
                    cmd.Parameters.AddWithValue("@perfilId", nuevoUsuario.Perfil.Id);
                    cmd.Parameters.AddWithValue("@activo", nuevoUsuario.Activo);
                    cmd.Parameters.AddWithValue("@email", (object)nuevoUsuario.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@idioma", !string.IsNullOrEmpty(nuevoUsuario.Idioma) ? nuevoUsuario.Idioma : "es");
                    cmd.Parameters.AddWithValue("@primerLogin", nuevoUsuario.PrimerLogin ? 1 : 0);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool ActualizarUsuario(Usuario_65RD usuario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuarios SET Email=@email, PerfilId=@perfilId WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@email", (object)usuario.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@perfilId", usuario.Perfil.Id);
                    cmd.Parameters.AddWithValue("@id", usuario.Id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


        public bool ActualizarContraseña(int idUsuario, string nuevaContraseñaHash)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string query = "UPDATE Usuarios SET Contraseña = @nueva, PrimerLogin = 0 WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nueva", nuevaContraseñaHash);
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Usuario_65RD> ObtenerUsuarios()
        {
            List<Usuario_65RD> usuarios = new List<Usuario_65RD>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT u.Id, u.Nombre, u.Apellido, u.DNI, u.Email, u.Activo, u.PrimerLogin,u.Bloqueado, u.Idioma,
                    p.Id AS PerfilId, p.Nombre AS PerfilNombre
                    FROM Usuarios u
                    INNER JOIN Perfiles p ON u.PerfilId = p.Id";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuarios.Add(new Usuario_65RD
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        DNI = reader["DNI"].ToString(),
                        Email = reader["Email"].ToString(),
                        Activo = Convert.ToBoolean(reader["Activo"]),
                        PrimerLogin = Convert.ToBoolean(reader["PrimerLogin"]),
                        Bloqueado = Convert.ToBoolean(reader["Bloqueado"]),
                        Idioma = reader["Idioma"].ToString() ?? "es",

                        Perfil = new Perfil_65RD
                        {
                            Id = Convert.ToInt32(reader["PerfilId"]),
                            Nombre = reader["PerfilNombre"].ToString()
                        }
                    });
                }
            }
            return usuarios;
        }
        public void ActualizarEstado(int idUsuario, bool activo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Usuarios 
                         SET Activo = @activo, 
                             PrimerLogin = CASE WHEN @activo = 1 THEN 1 ELSE PrimerLogin END
                         WHERE Id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@activo", activo);
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Perfil_65RD> ObtenerPerfiles()
        {
            List<Perfil_65RD> perfiles = new List<Perfil_65RD>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre FROM Perfiles";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    perfiles.Add(new Perfil_65RD
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString()
                    });
                }
            }
            return perfiles;
        }

        public void ActualizarBloqueo(int idUsuario, bool estaBloqueado)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
               
                string query = @"UPDATE Usuarios 
                         SET Bloqueado = @bloqueado, 
                             PrimerLogin = CASE WHEN @bloqueado = 0 THEN 1 ELSE PrimerLogin END
                         WHERE Id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@bloqueado", estaBloqueado);
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool ActualizarIdiomaUsuario(int idUsuario, string nuevoIdioma)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuarios SET Idioma = @idioma WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idioma", nuevoIdioma);
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

    }
}
