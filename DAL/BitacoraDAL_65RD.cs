using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BitacoraDAL_65RD
    {
        private string _connectionString = @"Data Source=.;Initial Catalog=proyecto_ingenieria;Integrated Security=True";

        public void RegistrarEvento(int usuarioId, string modulo, string accion, int criticidad, string descripcion)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Bitacora (UsuarioId, FechaHora, Modulo, Accion, Criticidad, Descripcion) 
                                 VALUES (@usuarioId, GETDATE(), @modulo, @accion, @criticidad, @descripcion)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@modulo", modulo);
                    cmd.Parameters.AddWithValue("@accion", accion);
                    cmd.Parameters.AddWithValue("@criticidad", criticidad);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Bitacora_65RD> ObtenerEventos(DateTime fechaDesde, DateTime fechaHasta, string modulo, string evento, int criticidad)
        {
            List<Bitacora_65RD> lista = new List<Bitacora_65RD>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // ── CORRECCIÓN PRINCIPAL ──────────────────────────────────────
                // Antes: filtraba por p.Nombre (perfil del usuario que generó el evento),
                //        lo cual hacía que "Usuarios Básicos" / "Administradores" no
                //        encontraran nada porque en BD el campo Modulo dice "Usuarios",
                //        "Perfiles" o "Familias".
                // Ahora: filtra directamente por b.Modulo, que es lo que se grabó.
                // ─────────────────────────────────────────────────────────────
                string query = @"
                    SELECT b.Id, b.UsuarioId,
                           CONCAT(u.Apellido, u.DNI) AS LoginUsuario,
                           u.Nombre, u.Apellido,
                           p.Nombre AS Rol,
                           b.FechaHora, b.Modulo, b.Accion, b.Criticidad, b.Descripcion
                    FROM Bitacora b
                    INNER JOIN Usuarios u ON b.UsuarioId = u.Id
                    INNER JOIN Perfiles p ON u.PerfilId = p.Id
                    WHERE b.FechaHora >= @fechaDesde
                      AND b.FechaHora <= @fechaHasta
                      AND (@modulo     = '' OR b.Modulo = @modulo)
                      AND (@evento     = '' OR b.Accion = @evento)
                      AND (@criticidad = 0  OR b.Criticidad = @criticidad)
                    ORDER BY b.FechaHora DESC";

                // ── VENTAJA DE ESTE ENFOQUE ───────────────────────────────────
                // Todos los parámetros siempre se pasan (no se construye SQL dinámico
                // con concatenación de strings), lo que es más seguro y legible.
                // El valor "" para modulo/evento y 0 para criticidad actúa como "sin filtro".
                // ─────────────────────────────────────────────────────────────

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fechaDesde", fechaDesde.Date);
                    cmd.Parameters.AddWithValue("@fechaHasta", fechaHasta.Date.AddDays(1).AddTicks(-1));
                    cmd.Parameters.AddWithValue("@modulo", modulo ?? "");
                    cmd.Parameters.AddWithValue("@evento", evento ?? "");
                    cmd.Parameters.AddWithValue("@criticidad", criticidad);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Bitacora_65RD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                                LoginUsuario = reader["LoginUsuario"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Rol = reader["Rol"].ToString(),
                                FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                                Modulo = reader["Modulo"].ToString(),
                                Evento = reader["Accion"].ToString(),
                                Criticidad = Convert.ToInt32(reader["Criticidad"]),
                                Descripcion = reader["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public int ContarIntentosFallidos(int usuarioId)
        {
            int cantidad = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Bitacora 
                    WHERE UsuarioId = @usuarioId 
                      AND Accion = 'Login Fallido'
                      AND FechaHora > ISNULL(
                          (SELECT MAX(FechaHora) 
                           FROM Bitacora 
                           WHERE UsuarioId = @usuarioId 
                             AND Accion IN ('Login', 'Alta Usuario', 'Modificar Usuario', 'Cambio Contraseña')), 
                          '1900-01-01')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                    conn.Open();
                    cantidad = (int)cmd.ExecuteScalar();
                }
            }
            return cantidad;
        }


    }
}
