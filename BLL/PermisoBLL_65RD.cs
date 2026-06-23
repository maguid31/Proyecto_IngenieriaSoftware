using DAL;
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
        private BitacoraBLL_65RD _bitacoraBLL = new BitacoraBLL_65RD();
        private FamiliaDAL_65RD _familiaDAL = new FamiliaDAL_65RD();

        public bool ExistePermisoEnFamilia(ComponentePermiso_65RD componenteRaiz, int idPermisoBuscado)
        {
            if (componenteRaiz.Id == idPermisoBuscado)
                return true;

            if (componenteRaiz is Familia_65RD familia)
                foreach (var hijo in familia.ObtenerHijos())
                    if (ExistePermisoEnFamilia(hijo, idPermisoBuscado))
                        return true;

            return false;
        }

        public bool ExisteComponenteEnEstructura(ComponentePermiso_65RD nodoRaiz, int idComponenteBuscar)
        {
            if (nodoRaiz.Id == idComponenteBuscar)
                return true;

            if (nodoRaiz is Familia_65RD familia)
                foreach (var hijo in familia.ObtenerHijos())
                    if (ExisteComponenteEnEstructura(hijo, idComponenteBuscar))
                        return true;

            return false;
        }

        public bool AsignarPermisoAFamilia(Familia_65RD familiaDestino, ComponentePermiso_65RD nuevoPermiso)
        {
            if (ExistePermisoEnFamilia(familiaDestino, nuevoPermiso.Id))
                return false;

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

        public List<Familia_65RD> ObtenerTodasLasFamilias()
        {
            return _familiaDAL.ObtenerTodasLasFamilias();
        }



        public bool GuardarFamilia(Familia_65RD familia)
        {
            if (string.IsNullOrWhiteSpace(familia.Nombre))
                throw new ArgumentException("El nombre de la familia no puede estar vacío.");

            bool resultado = _familiaDAL.GuardarFamilia(familia);

            if (resultado)
                _bitacoraBLL.RegistrarEvento(
                    SessionManager_65RD.Instancia.UsuarioLogueado.Id,
                    "Familias",
                    "Crear Familia",
                    2,
                    $"Se creó la familia '{familia.Nombre.Trim()}'"
                );

            return resultado;
        }

        public bool ModificarFamilia(Familia_65RD familia)
        {
            if (string.IsNullOrWhiteSpace(familia.Nombre))
                throw new ArgumentException("El nombre de la familia no puede estar vacío.");

            bool resultado = _familiaDAL.ActualizarFamilia(familia);

            if (resultado)
                _bitacoraBLL.RegistrarEvento(
                    SessionManager_65RD.Instancia.UsuarioLogueado.Id,
                    "Familias",
                    "Modificar Familia",
                    3,
                    $"Se modificó la familia '{familia.Nombre.Trim()}'"
                );

            return resultado;
        }

        public bool EliminarFamiliaBLL(int idFamilia)
        {
            // Guardamos el nombre antes de eliminar para loguearlo con sentido
            string nombreFamilia = (_permisoDAL.ObtenerPermisoRecursivo(idFamilia) as Familia_65RD)?.Nombre
                                   ?? $"ID {idFamilia}";

            bool resultado = _familiaDAL.EliminarFamilia(idFamilia);

            if (resultado)
                _bitacoraBLL.RegistrarEvento(
                    SessionManager_65RD.Instancia.UsuarioLogueado.Id,
                    "Familias",
                    "Eliminar Familia",
                    4,
                    $"Se eliminó la familia '{nombreFamilia}'"
                );

            return resultado;
        }

        // ──────────────────────────────────────────────
        //  VALIDACIÓN ANTI-REPETICIÓN EN FAMILIAS
        // ──────────────────────────────────────────────

        public void ValidarAsignacionSinRepetidos(ComponentePermiso_65RD contenedorPadre, ComponentePermiso_65RD elementoAAgregar)
        {
            List<int> patentesActuales = ObtenerTodasLasPatentesDeFormaRecursiva(contenedorPadre);
            List<int> patentesNuevas = ObtenerTodasLasPatentesDeFormaRecursiva(elementoAAgregar);

            foreach (int idPatente in patentesNuevas)
                if (patentesActuales.Contains(idPatente))
                    throw new Exception($"El permiso o sub-familia contiene la patente con ID {idPatente}, " +
                                        $"la cual ya forma parte de este elemento.");
        }

        private List<int> ObtenerTodasLasPatentesDeFormaRecursiva(ComponentePermiso_65RD componente)
        {
            List<int> ids = new List<int>();

            if (componente is Patente_65RD)
            {
                ids.Add(componente.Id);
            }
            else if (componente is Familia_65RD familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                    ids.AddRange(ObtenerTodasLasPatentesDeFormaRecursiva(hijo));
            }

            return ids.Distinct().ToList();
        }
    }
}