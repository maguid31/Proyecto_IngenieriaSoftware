using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Perfil_65RD
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // lista que contendrá las familias y patentes asignadas a este rol
        public List<Servicios_65RD.ComponentePermiso_65RD> PermisosAsignados { get; set; }

        public Perfil_65RD()
        {
            PermisosAsignados = new List<Servicios_65RD.ComponentePermiso_65RD>();
        }
    }
}
