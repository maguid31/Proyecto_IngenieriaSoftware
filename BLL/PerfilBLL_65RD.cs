using DAL;
using Servicios;
using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PerfilBLL_65RD
    {
        private PerfilDAL_65RD _perfilDAL = new PerfilDAL_65RD();

        // ──────────────────────────────────────────────
        //  MÉTODOS EXISTENTES
        // ──────────────────────────────────────────────

        // REGLA 1: PROTECCIÓN CONTRA ELIMINACIÓN
        public bool IntentarEliminarPerfil(int idPerfil, out string mensajeError)
        {
            mensajeError = string.Empty;
            int usuariosActivos = _perfilDAL.ContarUsuariosPorPerfil(idPerfil);
            if (usuariosActivos > 0)
            {
                mensajeError = $"No se puede eliminar el rol. Actualmente hay {usuariosActivos} usuario(s) asignado(s) a este perfil. Modifique los usuarios primero.";
                return false;
            }
            return _perfilDAL.EliminarPerfil(idPerfil);
        }

        // REGLA 2: VALIDACIÓN ANTI-REPETICIÓN EN ROLES
        public void ValidarAsignacionSinRepetidos(Perfil_65RD rolActual, ComponentePermiso_65RD nuevoPermiso)
        {
            // 1. Extraemos todas las IDs de patentes que YA tiene el rol actual
            List<int> patentesActuales = new List<int>();

            foreach (var permisoExistente in rolActual.PermisosAsignados)
            {
                // Usamos el aplanador para desglosar todo lo que ya tiene el rol adentro
                patentesActuales.AddRange(ExtraerIdsPatentes(permisoExistente));
            }
            // Eliminamos duplicados por las dudas
            patentesActuales = patentesActuales.Distinct().ToList();

            // 2. Extraemos las IDs de lo que queremos agregar ahora
            List<int> patentesNuevas = ExtraerIdsPatentes(nuevoPermiso);

            // 3. VALIDACIÓN DIRECTA: Si el usuario intenta agregar EXACTAMENTE el mismo componente 
            // (sea una patente suelta o la misma familia) que ya está en la lista principal del rol:
            if (rolActual.PermisosAsignados.Any(p => p.Id == nuevoPermiso.Id && p.GetType() == nuevoPermiso.GetType()))
            {
                throw new Exception($"El componente '{nuevoPermiso.Nombre}' ya está asignado directamente en la lista de este rol.");
            }

            // 4. VALIDACIÓN RECURSIVA TRADICIONAL (Cruzar las patentes internas)
            foreach (int idPatente in patentesNuevas)
            {
                if (patentesActuales.Contains(idPatente))
                {
                    throw new Exception($"La patente con ID {idPatente} ya se encuentra asignada en este rol (de forma directa o heredada por una familia).");
                }
            }
        }

        // Función auxiliar interna recursiva para PerfilBLL (La dejamos por si la usan en otro lado)
        private bool ExisteElementoRec(ComponentePermiso_65RD nodo, int idBuscar)
        {
            if (nodo.Id == idBuscar) return true;

            if (nodo is Familia_65RD familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                {
                    if (ExisteElementoRec(hijo, idBuscar)) return true;
                }
            }
            return false;
        }

        // MÉTODO AUXILIAR RECURSIVO: APLANADOR DE ÁRBOLES
        private List<int> ExtraerIdsPatentes(ComponentePermiso_65RD componente)
        {
            List<int> ids = new List<int>();
            if (componente is Patente_65RD)
            {
                ids.Add(componente.Id);
            }
            else if (componente is Familia_65RD familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                    ids.AddRange(ExtraerIdsPatentes(hijo));
            }
            return ids;
        }

        // ──────────────────────────────────────────────
        //  MÉTODOS NUEVOS (requeridos por frmGestionRoles)
        // ──────────────────────────────────────────────

        public List<Perfil_65RD> ObtenerTodosLosPerfiles()
        {
            return _perfilDAL.ObtenerTodosLosPerfiles();
        }

        public bool CrearPerfil(Perfil_65RD perfil)
        {
            if (string.IsNullOrWhiteSpace(perfil.Nombre))
                throw new ArgumentException("El nombre del perfil no puede estar vacío.");

            // Validar nombre duplicado
            var existentes = _perfilDAL.ObtenerTodosLosPerfiles();
            if (existentes.Any(p => p.Nombre.Equals(perfil.Nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"Ya existe un perfil con el nombre '{perfil.Nombre}'.");

            return _perfilDAL.InsertarPerfil(perfil);
        }

        public bool GuardarAsignacionPermisos(Perfil_65RD perfil)
        {
            return _perfilDAL.ActualizarPermisosDelPerfil(perfil);
        }


    }
}
