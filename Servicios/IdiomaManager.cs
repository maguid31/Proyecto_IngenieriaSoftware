using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Servicios
{
    public class IdiomaManager
    {
        private static IdiomaManager _instance;
        private List<IidiomaObserver> _observers = new List<IidiomaObserver>();

        // Este diccionario doble guardará: [NombreFormulario][NombreControl] -> TextoTraducido
        private Dictionary<string, Dictionary<string, string>> _traduccionesActuales;

        public string IdiomaActual { get; private set; } = "es"; // Idioma por defecto al arrancar

        // Constructor privado (Patrón Singleton)
        private IdiomaManager()
        {
            CargarArchivoIdioma(IdiomaActual);
        }

        public static IdiomaManager GetInstance()
        {
            if (_instance == null) _instance = new IdiomaManager();
            return _instance;
        }

        // Método para registrar los formularios que quieren ser avisados
        public void RegisterObserver(IidiomaObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        // Método para desregistrar los formularios cuando se cierran
        public void RemoveObserver(IidiomaObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        // Cambiar el idioma de toda la aplicación
        public void CambiarIdioma(string nuevoIdioma)
        {
            IdiomaActual = nuevoIdioma;
            CargarArchivoIdioma(nuevoIdioma);

            // Avisar a todos los formularios abiertos que se actualicen
            NotifyObservers();
        }

        // Lee el archivo JSON desde la carpeta Idiomas
        private void CargarArchivoIdioma(string idioma)
        {
            // Buscamos el archivo en la carpeta "Idiomas" donde los creaste
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas", $"idioma.{idioma}.json");

            if (File.Exists(path))
            {
                string jsonContent = File.ReadAllText(path);
                // Convertimos el texto del JSON en el diccionario de C#
                _traduccionesActuales = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonContent);
            }
        }

        // Este método lo usará cada formulario para pedir el texto de sus controles
        public string GetTexto(string nombreFormulario, string claveControl)
        {
            if (_traduccionesActuales != null &&
                _traduccionesActuales.ContainsKey(nombreFormulario) &&
                _traduccionesActuales[nombreFormulario].ContainsKey(claveControl))
            {
                return _traduccionesActuales[nombreFormulario][claveControl];
            }
            return $"[{claveControl}]"; // Si no lo encuentra, te muestra el nombre entre corchetes para que te des cuenta
        }

        // Notificar a todos los observadores registrados
        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.UpdateIdioma(IdiomaActual);
            }
        }
    }

}

