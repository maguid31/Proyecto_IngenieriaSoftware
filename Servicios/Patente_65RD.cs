using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Patente_65RD : ComponentePermiso_65RD
    {
        public override void AgregarHijo(ComponentePermiso_65RD c)
        {
            throw new Exception("No se le pueden asignar permisos hijos a una Patente.");
        }
        public override void EliminarHijo(ComponentePermiso_65RD c)
        { }
        public override void VaciarHijos()
        {  }

        public override IList<ComponentePermiso_65RD> ObtenerHijos()
        {
            return new List<ComponentePermiso_65RD>(); 
        }
        public override bool TienePermiso(string nombrePermiso)
        {
            return this.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase);
        }
    }
}
