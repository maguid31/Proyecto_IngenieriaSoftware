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
        public bool TienePermiso(string nombrePermiso)
        {
            if (PermisosAsignados == null) return false;

            foreach (var componente in PermisosAsignados)
            {
                // Si el componente actual (sea Patente o Familia) coincide con el nombre, da true
                if (componente.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                // Si es una Familia, el Composite delega la búsqueda de manera recursiva hacia adentro
                if (componente.TienePermiso(nombrePermiso))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
