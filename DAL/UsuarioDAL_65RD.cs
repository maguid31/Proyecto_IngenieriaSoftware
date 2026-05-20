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
                    SELECT u.Id, u.Nombre, u.Apellido, u.DNI, u.Contraseña, u.Activo, u.PrimerLogin,
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
                               
                                // Instanciamos el objeto Perfil
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
            return usuarioEncontrado;
        }

        public bool RegistrarUsuario(Usuario_65RD nuevoUsuario)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Cambiamos Rol por PerfilId
                string query = "INSERT INTO Usuarios (Nombre, Apellido, DNI, Contraseña, PerfilId, Activo, Email) " +
                               "VALUES (@nombre, @apellido, @dni, @contraseña, @perfilId, @activo, @email)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", (object)nuevoUsuario.Nombre ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@apellido", nuevoUsuario.Apellido);
                    cmd.Parameters.AddWithValue("@dni", nuevoUsuario.DNI);
                    cmd.Parameters.AddWithValue("@contraseña", nuevoUsuario.Contraseña); // Sigue siendo hash
                    cmd.Parameters.AddWithValue("@perfilId", nuevoUsuario.Perfil.Id); // Obtenemos el Id del objeto Perfil
                    cmd.Parameters.AddWithValue("@activo", nuevoUsuario.Activo);
                    cmd.Parameters.AddWithValue("@email", (object)nuevoUsuario.Email ?? DBNull.Value);
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
                    SELECT u.Id, u.Nombre, u.Apellido, u.DNI, u.Email, u.Activo, u.PrimerLogin,
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
                string query = "UPDATE Usuarios SET Activo=@activo WHERE Id=@id";
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

    }
}
