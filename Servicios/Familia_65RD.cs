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
            // Verificación simple en memoria para que no se duplique el mismo objeto exacto
            if (!_hijos.Contains(c))
            {
                _hijos.Add(c);
            }
        }

        public override void VaciarHijos()
        {
            _hijos.Clear();
        }

        public override IList<ComponentePermiso_65RD> ObtenerHijos()
        {
            return _hijos;
        }
    }
}
