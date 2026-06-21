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
                if (componente.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (componente.TienePermiso(nombrePermiso))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
