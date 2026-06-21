using DAL_65RD;
using Servicios;
using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PermisoBLL_65RD
    {
        private PermisoDAL_65RD _permisoDAL = new PermisoDAL_65RD();

        public bool ExistePermisoEnFamilia(ComponentePermiso_65RD componenteRaiz, int idPermisoBuscado)
        {
            if (componenteRaiz.Id == idPermisoBuscado)
            {
                return true;
            }

            if (componenteRaiz is Familia_65RD familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                {
                    if (ExistePermisoEnFamilia(hijo, idPermisoBuscado))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool ExisteComponenteEnEstructura(ComponentePermiso_65RD nodoRaiz, int idComponenteBuscar)
        {
            if (nodoRaiz.Id == idComponenteBuscar)
            {
                return true;
            }

            if (nodoRaiz is Familia_65RD familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                {
                    if (ExisteComponenteEnEstructura(hijo, idComponenteBuscar))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool AsignarPermisoAFamilia(Familia_65RD familiaDestino, ComponentePermiso_65RD nuevoPermiso)
        {
            if (ExistePermisoEnFamilia(familiaDestino, nuevoPermiso.Id))
            {
                return false;
            }

            familiaDestino.AgregarHijo(nuevoPermiso);
            return true;
        }

        public ComponentePermiso_65RD ObtenerFamiliaOPatente(int idPermiso)
        {
            return _permisoDAL.ObtenerPermisoRecursivo(idPermiso);
        }

        public List<Patente_65RD> ObtenerTodasLasPatentes()
        {
            return _permisoDAL.ObtenerTodasLasPatentes();
        }

        public List<ComponentePermiso_65RD> ObtenerTodosLosPermisos()
        {
            return _permisoDAL.ObtenerTodosLosPermisos();
        }

            
    }
}
