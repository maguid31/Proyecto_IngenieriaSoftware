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
            //"Data Source=DESKTOP-UOCRKUM;Initial Catalog=proyecto_ingenieria;Integrated Security=True";

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
        // Método para llenar la grilla aplicando filtros
        public List<Bitacora_65RD> ObtenerEventos(DateTime fechaDesde, DateTime fechaHasta, string modulo, string evento, int criticidad)
        {
            List<Bitacora_65RD> lista = new List<Bitacora_65RD>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Hacemos un JOIN para traer el Apellido+DNI del usuario (Login)
                string query = @"
                    SELECT b.Id, b.UsuarioId, CONCAT(u.Apellido, u.DNI) AS LoginUsuario, 
                           b.FechaHora, b.Modulo, b.Accion, b.Criticidad, b.Descripcion 
                    FROM Bitacora b
                    INNER JOIN Usuarios u ON b.UsuarioId = u.Id
                    WHERE b.FechaHora >= @fechaDesde AND b.FechaHora <= @fechaHasta ";

                if (!string.IsNullOrEmpty(modulo) && modulo != "Todos")
                    query += " AND b.Modulo = @modulo ";

                if (!string.IsNullOrEmpty(evento) && evento != "Todos")
                    query += " AND b.Accion = @evento ";

                if (criticidad > 0)
                    query += " AND b.Criticidad = @criticidad ";

                query += " ORDER BY b.FechaHora DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Ajustamos las fechas para que cubran todo el día
                    cmd.Parameters.AddWithValue("@fechaDesde", fechaDesde.Date);
                    cmd.Parameters.AddWithValue("@fechaHasta", fechaHasta.Date.AddDays(1).AddTicks(-1));

                    if (!string.IsNullOrEmpty(modulo) && modulo != "Todos") cmd.Parameters.AddWithValue("@modulo", modulo);
                    if (!string.IsNullOrEmpty(evento) && evento != "Todos") cmd.Parameters.AddWithValue("@evento", evento);
                    if (criticidad > 0) cmd.Parameters.AddWithValue("@criticidad", criticidad);

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
    }
}
