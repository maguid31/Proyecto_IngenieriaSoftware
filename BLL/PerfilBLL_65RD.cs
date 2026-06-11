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
        public bool ValidarAsignacionARol(Perfil_65RD rolActual, ComponentePermiso_65RD nuevoPermiso)
        {
            List<int> patentesExistentes = new List<int>();
            foreach (var permiso in rolActual.PermisosAsignados)
                patentesExistentes.AddRange(ExtraerIdsPatentes(permiso));

            List<int> patentesNuevas = ExtraerIdsPatentes(nuevoPermiso);
            foreach (int idNuevo in patentesNuevas)
            {
                if (patentesExistentes.Contains(idNuevo))
                    return false;
            }
            return true;
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
            return _perfilDAL.InsertarPerfil(perfil);
        }

        public bool GuardarAsignacionPermisos(Perfil_65RD perfil)
        {
            return _perfilDAL.ActualizarPermisosDelPerfil(perfil);
        }


    }
}
