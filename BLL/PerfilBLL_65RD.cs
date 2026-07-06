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
        private BitacoraBLL_65RD _bitacoraBLL = new BitacoraBLL_65RD();


        public bool IntentarEliminarPerfil(int idPerfil, out string mensajeError)
        {
            mensajeError = string.Empty;

            int usuariosActivos = _perfilDAL.ContarUsuariosPorPerfil(idPerfil);
            if (usuariosActivos > 0)
            {
                mensajeError = $"No se puede eliminar el rol. Actualmente hay {usuariosActivos} " +
                               $"usuario(s) asignado(s) a este perfil. Modifique los usuarios primero.";
                return false;
            }

            string nombrePerfil = _perfilDAL.ObtenerTodosLosPerfiles()
                                             .FirstOrDefault(p => p.Id == idPerfil)?.Nombre
                                  ?? $"ID {idPerfil}";

            bool resultado = _perfilDAL.EliminarPerfil(idPerfil);

            if (resultado)
                _bitacoraBLL.RegistrarEvento(
                    SessionManager_65RD.Instancia.UsuarioLogueado.Id,
                    "Perfiles",
                    "Eliminar Perfil",
                    4,
                    $"Se eliminó el perfil '{nombrePerfil}'"
                );

            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Perfiles");
            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Perfil_Permiso");
            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Perfil_Familia");

            return resultado;
        }



        public void ValidarAsignacionSinRepetidos(Perfil_65RD rolActual, ComponentePermiso_65RD nuevoPermiso)
        {
            List<int> patentesActuales = new List<int>();

            foreach (var permisoExistente in rolActual.PermisosAsignados)
                patentesActuales.AddRange(ExtraerIdsPatentes(permisoExistente));

            patentesActuales = patentesActuales.Distinct().ToList();

            List<int> patentesNuevas = ExtraerIdsPatentes(nuevoPermiso);

            if (rolActual.PermisosAsignados.Any(p => p.Id == nuevoPermiso.Id && p.GetType() == nuevoPermiso.GetType()))
                throw new Exception($"El componente '{nuevoPermiso.Nombre}' ya está asignado directamente en la lista de este rol.");

            foreach (int idPatente in patentesNuevas)
            {
                if (patentesActuales.Contains(idPatente))
                    throw new Exception($"La patente con ID {idPatente} ya se encuentra asignada en este rol " +
                                        $"(de forma directa o heredada por una familia).");
            }
        }

 


        private bool ExisteElementoRec(ComponentePermiso_65RD nodo, int idBuscar)
        {
            if (nodo.Id == idBuscar) return true;

            if (nodo is Familia_65RD familia)
                foreach (var hijo in familia.ObtenerHijos())
                    if (ExisteElementoRec(hijo, idBuscar)) return true;

            return false;
        }

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



        public List<Perfil_65RD> ObtenerTodosLosPerfiles()
        {
            return _perfilDAL.ObtenerTodosLosPerfiles();
        }

        public bool CrearPerfil(Perfil_65RD perfil)
        {
            if (string.IsNullOrWhiteSpace(perfil.Nombre))
                throw new ArgumentException("El nombre del perfil no puede estar vacío.");

            var existentes = _perfilDAL.ObtenerTodosLosPerfiles();
            if (existentes.Any(p => p.Nombre.Equals(perfil.Nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"Ya existe un perfil con el nombre '{perfil.Nombre}'.");

            bool resultado = _perfilDAL.InsertarPerfil(perfil);

            if (resultado)
                _bitacoraBLL.RegistrarEvento(
                    SessionManager_65RD.Instancia.UsuarioLogueado.Id,
                    "Perfiles",
                    "Crear Perfil",
                    2,
                    $"Se creó el perfil '{perfil.Nombre.Trim()}'"
                );
            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Perfiles");
            return resultado;
        }

        public bool GuardarAsignacionPermisos(Perfil_65RD perfil)
        {
            bool resultado = _perfilDAL.ActualizarPermisosDelPerfil(perfil);

            if (resultado)
                _bitacoraBLL.RegistrarEvento(
                    SessionManager_65RD.Instancia.UsuarioLogueado.Id,
                    "Perfiles",
                    "Modificar Perfil",
                    3,
                    $"Se actualizaron los permisos del perfil '{perfil.Nombre}'"
                );
            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Perfiles");
            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Perfil_Permiso");
            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Perfil_Familia");

            return resultado;


        }
    }
}