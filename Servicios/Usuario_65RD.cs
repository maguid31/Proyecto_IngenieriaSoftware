using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_65RD
{
    public enum RolUsuario
    {
        SinAsignar = 0,
        Basico = 1,
        Administrador = 2
    }

    public class Usuario_65RD
    {
       
        public int Id { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Contraseña { get; set; }
        public RolUsuario Perfil { get; set; }
        public bool Activo { get; set; }
        public string Email { get; set; }
        public int IntentosFallidos { get; set; }
        public bool PrimerLogin { get; set; }


        // Propiedad de solo lectura: Une Apellido y DNI dinámicamente
        public string NombreUsuario
        {
            get { return $"{Apellido}{DNI}"; }
        }
    }
}
