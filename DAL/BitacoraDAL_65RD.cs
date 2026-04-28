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

        private string _connectionString = "Data Source=DESKTOP-UOCRKUM;Initial Catalog=proyecto_ingenieria;Integrated Security=True";

        public void RegistrarEvento(int usuarioId, string accion, string descripcion)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Bitacora (UsuarioId, FechaHora, Accion, Descripcion) " +
                               "VALUES (@usuarioId, GETDATE(), @accion, @descripcion)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@accion", accion);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
