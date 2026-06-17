using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_65RD
{
    public abstract class ComponentePermiso_65RD
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual List<ComponentePermiso_65RD> Hijos { get; set; } = new List<ComponentePermiso_65RD>();
        // Métodos que los hijos deberán implementar
        public abstract void AgregarHijo(ComponentePermiso_65RD c);
        public abstract void VaciarHijos();
        public abstract IList<ComponentePermiso_65RD> ObtenerHijos();
        public abstract bool TienePermiso(string nombrePermiso);
    }
}
