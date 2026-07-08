using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Servicios
{
    public static class ConfiguracionApp_65RD
    {

        private static readonly string RutaConfig =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

        public static bool ExisteConfiguracion()
        {
            return File.Exists(RutaConfig);
        }

        public static void GuardarInstancia(string instancia)
        {
            File.WriteAllText(RutaConfig, instancia);
        }

        public static string ObtenerInstancia()
        {
            if (!ExisteConfiguracion())
                return null;
            return File.ReadAllText(RutaConfig).Trim();
        }

        public static string ObtenerConnectionString()
        {
            string instancia = ObtenerInstancia();
            if (string.IsNullOrEmpty(instancia))
                throw new InvalidOperationException("No hay una instancia SQL configurada.");

            return $@"Data Source={instancia};Initial Catalog=proyecto_ingenieria;Integrated Security=True";
        }
    }
}

