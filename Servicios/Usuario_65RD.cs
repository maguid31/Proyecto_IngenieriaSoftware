using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_65RD
{

    public class Usuario_65RD
    {
        public int Id { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Contraseña { get; set; }

        
        public Perfil_65RD Perfil { get; set; }

        public bool Activo { get; set; }
        public string Email { get; set; }
        public bool PrimerLogin { get; set; }

        
        public string NombrePerfil
        {
            get
            {
                if (Perfil != null)
                    return Perfil.Nombre;
                else
                    return "Sin Asignar";
            }
        }

        
        public string NombreUsuario
        {
            get { return $"{Apellido}{DNI}"; }
        }

        
        public string EstadoBloqueado
        {
            get
            {
                if (Activo == true)
                    return "No";
                else
                    return "Sí";
            }
        }
    }
}
