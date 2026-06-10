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
            // Las patentes no pueden tener hijos, lanzamos excepción o simplemente no hacemos nada
            throw new Exception("No se le pueden asignar permisos hijos a una Patente.");
        }

        public override void VaciarHijos()
        {
            // No hace nada
        }

        public override IList<ComponentePermiso_65RD> ObtenerHijos()
        {
            return new List<ComponentePermiso_65RD>(); // Retorna lista vacía
        }
    }
}
