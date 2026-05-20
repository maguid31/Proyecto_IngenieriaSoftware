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

        // Aplicamos composición
        public Perfil_65RD Perfil { get; set; }

        public bool Activo { get; set; }
        public string Email { get; set; }
        public bool PrimerLogin { get; set; }

        // Propiedad de ayuda para que el DataGridView muestre el nombre del rol fácilmente
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

        // Propiedad de solo lectura: Une Apellido y DNI dinámicamente
        public string NombreUsuario
        {
            get { return $"{Apellido}{DNI}"; }
        }

        // Propiedad de solo lectura: Devuelve si o no según el estado del usuario
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
