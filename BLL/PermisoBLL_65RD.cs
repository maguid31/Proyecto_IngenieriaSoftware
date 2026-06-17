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

        // 1. VALIDACIÓN RECURSIVA DE DUPLICADOS EN FAMILIAS

        /// Recorre recursivamente un componente (Familia) para verificar si un permiso específico ya existe en su estructura.
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

        // 2. MÉTODOS DE PASARELA HACIA LA DAL

        public bool GuardarFamilia(Familia_65RD familia)
        {
            if (string.IsNullOrWhiteSpace(familia.Nombre))
            {
                throw new ArgumentException("El nombre de la familia no puede estar vacío.");
            }

            return _permisoDAL.GuardarFamilia(familia);
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

        public bool ModificarFamilia(Familia_65RD familia)
        {
            if (string.IsNullOrWhiteSpace(familia.Nombre))
            {
                throw new ArgumentException("El nombre de la familia no puede estar vacío.");
            }
            return _permisoDAL.ActualizarFamilia(familia);
        }

        public bool EliminarFamiliaBLL(int idFamilia)
        {
            return _permisoDAL.EliminarPermiso(idFamilia);
        }

        // 3. 🌟 CORRECCIÓN Y UNIFICACIÓN DE MÉTODOS DE VALIDACIÓN

        public void ValidarAsignacionSinRepetidos(ComponentePermiso_65RD contenedorPadre, ComponentePermiso_65RD elementoAAgregar)
        {
            // Buscamos todas las patentes reales que ya existen en el contenedor (Perfil o Familia)
            List<int> patentesActuales = ObtenerTodasLasPatentesDeFormaRecursiva(contenedorPadre);

            // Buscamos las patentes que trae el elemento que queremos meter
            List<int> patentesNuevas = ObtenerTodasLasPatentesDeFormaRecursiva(elementoAAgregar);

            // Cruzamos las listas para ver si hay un choque de IDs
            foreach (int idPatente in patentesNuevas)
            {
                if (patentesActuales.Contains(idPatente))
                {
                    throw new Exception($"El permiso o sub-familia contiene la patente con ID {idPatente}, la cual ya forma parte de este elemento.");
                }
            }
        }

        // 🛠 CORREGIDO: Cambiamos 'familia.Hijos' por 'familia.ObtenerHijos()' para mantener tu diseño
        private List<int> ObtenerTodasLasPatentesDeFormaRecursiva(ComponentePermiso_65RD componente)
        {
            List<int> ids = new List<int>();

            if (componente is Patente_65RD)
            {
                ids.Add(componente.Id);
            }
            else if (componente is Familia_65RD familia)
            {
                // 👈 Cambiado acá para usar tu método nativo de la entidad
                foreach (var hijo in familia.ObtenerHijos())
                {
                    ids.AddRange(ObtenerTodasLasPatentesDeFormaRecursiva(hijo));
                }
            }

            return ids.Distinct().ToList();
        }
    }
}
