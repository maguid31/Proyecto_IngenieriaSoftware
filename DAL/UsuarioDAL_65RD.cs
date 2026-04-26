using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios_65RD;

namespace DAL_65RD
{
    public class UsuarioDAL_65RD
    {
        private string connectionString = @"Data Source=DESKTOP-UOCRKUM;Initial Catalog=proyecto_ingenieria;Integrated Security=True";

        public Usuario_65RD Login(string usuarioConcatenado, string hashContraseña)
        {
            Usuario_65RD usuarioEncontrado = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // CORRECCIÓN: Se cambió PerfilId por Rol para que coincida con tu nueva tabla
                string query = "SELECT Id, Apellido, DNI, Contraseña, Rol, Activo, PrimerLogin FROM Usuarios WHERE CONCAT(Apellido, DNI) = @usuario AND Contraseña = @contraseña";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuarioConcatenado);
                    cmd.Parameters.AddWithValue("@contraseña", hashContraseña);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuarioEncontrado = new Usuario_65RD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Apellido = reader["Apellido"].ToString(),
                                DNI = reader["DNI"].ToString(),
                                Contraseña = reader["Contraseña"].ToString(),
                                // CORRECCIÓN: Leemos de la columna "Rol" y convertimos al Enum
                                Perfil = (RolUsuario)Enum.Parse(typeof(RolUsuario), reader["Rol"].ToString()),
                                Activo = Convert.ToBoolean(reader["Activo"]),
                                PrimerLogin = Convert.ToBoolean(reader["PrimerLogin"])
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
                string query = "INSERT INTO Usuarios (Apellido, DNI, Contraseña, Rol, Activo, Email) " +
                               "VALUES (@apellido, @dni, @contraseña, @rol, @activo, @email)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@apellido", nuevoUsuario.Apellido);
                    cmd.Parameters.AddWithValue("@dni", nuevoUsuario.DNI);
                    cmd.Parameters.AddWithValue("@contraseña", nuevoUsuario.Contraseña);
                    cmd.Parameters.AddWithValue("@rol", nuevoUsuario.Perfil.ToString());
                    cmd.Parameters.AddWithValue("@activo", nuevoUsuario.Activo);
                    cmd.Parameters.AddWithValue("@email", (object)nuevoUsuario.Email ?? DBNull.Value);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public void ActualizarIntentos(int usuarioId, int intentos)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuarios SET IntentosFallidos = @i WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@i", intentos);
                    cmd.Parameters.AddWithValue("@id", usuarioId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void BloquearUsuario(int usuarioId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuarios SET Activo = 0 WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", usuarioId);
                    con.Open();
                    cmd.ExecuteNonQuery();
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
    }
}
