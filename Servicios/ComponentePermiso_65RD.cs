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
        
        public abstract void AgregarHijo(ComponentePermiso_65RD c);
        public abstract void EliminarHijo(ComponentePermiso_65RD c);
        public abstract void VaciarHijos();
        public abstract IList<ComponentePermiso_65RD> ObtenerHijos();
        public abstract bool TienePermiso(string nombrePermiso);
    }
}
