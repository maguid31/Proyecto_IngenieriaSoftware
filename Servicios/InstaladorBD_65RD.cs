using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class InstaladorBD_65RD
    {

        public static List<string> ObtenerInstanciasSQL()
        {
            List<string> instancias = new List<string>();

            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable table = instance.GetDataSources();

            foreach (DataRow row in table.Rows)
            {
                string servidor = row["ServerName"].ToString();
                string instanceName = row["InstanceName"].ToString();

                string nombreCompleto = string.IsNullOrEmpty(instanceName)
                    ? servidor
                    : $"{servidor}\\{instanceName}";

                instancias.Add(nombreCompleto);
            }

            if (!instancias.Contains("."))
                instancias.Add(".");

            return instancias;
        }

        public static bool ExisteBaseDeDatos(string instancia)
        {
            string connMaster = $@"Data Source={instancia};Initial Catalog=master;Integrated Security=True";

            using (var con = new SqlConnection(connMaster))
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM sys.databases WHERE name = 'proyecto_ingenieria'";
                using (var cmd = new SqlCommand(query, con))
                {
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static void CrearBaseDeDatos(string instancia, string rutaScriptSql)
        {
            if (!File.Exists(rutaScriptSql))
                throw new FileNotFoundException("No se encontró el script de instalación de la base de datos.");

            string scriptCompleto = File.ReadAllText(rutaScriptSql);

            string[] lotes = scriptCompleto.Split(
                new[] { "\r\nGO", "\nGO", "\rGO" },
                StringSplitOptions.RemoveEmptyEntries);

            string connMaster = $@"Data Source={instancia};Initial Catalog=master;Integrated Security=True";

            using (var con = new SqlConnection(connMaster))
            {
                con.Open();
                foreach (string lote in lotes)
                {
                    string comandoLimpio = lote.Trim();
                    if (string.IsNullOrWhiteSpace(comandoLimpio))
                        continue;

                    using (var cmd = new SqlCommand(comandoLimpio, con))
                    {
                        cmd.CommandTimeout = 120;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
