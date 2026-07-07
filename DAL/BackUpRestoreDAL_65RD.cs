using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BackUpRestoreDAL_65RD
    {
        private readonly string _connectionString = @"Data Source=.;Initial Catalog=proyecto_ingenieria;Integrated Security=True";

        public void EjecutarBackup(string rutaCompleta)
        {
            string connMaster = _connectionString.Replace(
                "Initial Catalog=proyecto_ingenieria",
                "Initial Catalog=master");

            string query = $@"
                BACKUP DATABASE [proyecto_ingenieria] 
                TO DISK = N'{rutaCompleta}' 
                WITH FORMAT, MEDIANAME = 'ProyectoBackup', 
                NAME = 'Backup completo proyecto_ingenieria'";

            using (var con = new SqlConnection(connMaster))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.CommandTimeout = 300;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EjecutarRestore(string rutaCompleta)
        {
            string connMaster = _connectionString.Replace(
                "Initial Catalog=proyecto_ingenieria",
                "Initial Catalog=master");

            using (var con = new SqlConnection(connMaster))
            {
                con.Open();

                EjecutarComando(@"ALTER DATABASE [proyecto_ingenieria] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", con);
                EjecutarComando($@"RESTORE DATABASE [proyecto_ingenieria] FROM DISK = N'{rutaCompleta}' WITH REPLACE, RECOVERY", con);
                EjecutarComando(@"ALTER DATABASE [proyecto_ingenieria] SET MULTI_USER", con);
            }
        }

        private void EjecutarComando(string query, SqlConnection con)
        {
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
            }
        }
    }
}

