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
        private Dictionary<string, Dictionary<string, string>> _traduccionesActuales;

        public string IdiomaActual { get; private set; } = "es"; 

        private IdiomaManager()
        {
            CargarArchivoIdioma(IdiomaActual);
        }

        public static IdiomaManager GetInstance()
        {
            if (_instance == null) _instance = new IdiomaManager();
            return _instance;
        }

        public void RegisterObserver(IidiomaObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void RemoveObserver(IidiomaObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void CambiarIdioma(string nuevoIdioma)
        {
            IdiomaActual = nuevoIdioma;
            CargarArchivoIdioma(nuevoIdioma);

            NotifyObservers();
        }

        private void CargarArchivoIdioma(string idioma)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas", $"idioma.{idioma}.json");

            if (File.Exists(path))
            {
                string jsonContent = File.ReadAllText(path);
               
                _traduccionesActuales = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonContent);
            }
        }

        public string GetTexto(string nombreFormulario, string claveControl)
        {
            if (_traduccionesActuales != null &&
                _traduccionesActuales.ContainsKey(nombreFormulario) &&
                _traduccionesActuales[nombreFormulario].ContainsKey(claveControl))
            {
                return _traduccionesActuales[nombreFormulario][claveControl];
            }
            return $"[{claveControl}]"; 
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.UpdateIdioma(IdiomaActual);
            }
        }
    }

}

