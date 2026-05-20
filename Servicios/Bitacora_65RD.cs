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
        public string LoginUsuario { get; set; } 
        public DateTime FechaHora { get; set; }
        public string Modulo { get; set; }
        public string Evento { get; set; } 
        public int Criticidad { get; set; }
        public string Descripcion { get; set; }

        
        public string Fecha => FechaHora.ToString("dd/MM/yyyy");
        public string Hora => FechaHora.ToString("HH:mm");
    }
}
