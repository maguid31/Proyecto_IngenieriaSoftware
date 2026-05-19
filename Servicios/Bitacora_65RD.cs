using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_65RD
{
    public class Bitacora_65RD
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string LoginUsuario { get; set; } // Para mostrar en la grilla
        public DateTime FechaHora { get; set; }
        public string Modulo { get; set; }
        public string Evento { get; set; } // Es la "Accion"
        public int Criticidad { get; set; }
        public string Descripcion { get; set; }

        // Propiedades calculadas para separar Fecha y Hora en la grilla como pide la imagen
        public string Fecha => FechaHora.ToString("dd/MM/yyyy");
        public string Hora => FechaHora.ToString("HH:mm");
    }
}
