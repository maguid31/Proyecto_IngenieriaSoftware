using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Familia_65RD: ComponentePermiso_65RD
    {
        private List<ComponentePermiso_65RD> _hijos = new List<ComponentePermiso_65RD>();

        public override void AgregarHijo(ComponentePermiso_65RD c)
        {
            if (!_hijos.Contains(c))
            {
                _hijos.Add(c);
            }
        }

        public override void EliminarHijo(ComponentePermiso_65RD c)
        {
            _hijos.Remove(c);
        }

        public override void VaciarHijos()
        {
            _hijos.Clear();
        }

        public override IList<ComponentePermiso_65RD> ObtenerHijos()
        {
            return _hijos;
        }

        public override bool TienePermiso(string nombrePermiso)
        {
  
            if (this.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            var hijos = ObtenerHijos();
            if (hijos != null)
            {
                foreach (var hijo in hijos)
                {
                    if (hijo.TienePermiso(nombrePermiso))
                    {
                        return true; 
                    }
                }
            }

            return false;
        }

    }
}
