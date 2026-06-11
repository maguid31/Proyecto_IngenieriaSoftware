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
            // Caso base 1: El nodo actual es el que estamos buscando
            if (componenteRaiz.Id == idPermisoBuscado)
            {
                return true;
            }

            // Caso base 2: Si el nodo actual es una Familia, iteramos sobre sus hijos
            if (componenteRaiz is Familia_65RD familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                {
                    // Llamada recursiva: si el hijo (o alguno de los sub-hijos) lo encuentra, cortamos y devolvemos true
                    if (ExistePermisoEnFamilia(hijo, idPermisoBuscado))
                    {
                        return true;
                    }
                }
            }

            // Si recorrió todo el árbol y no lo encontró, entonces no existe
            return false;
        }


        /// Intenta agregar un nuevo permiso a una familia destino, aplicando la regla estricta de validación.
        /// Devuelve true si se agregó correctamente, o false si se detectó una repetición.

        public bool AsignarPermisoAFamilia(Familia_65RD familiaDestino, ComponentePermiso_65RD nuevoPermiso)
        {
            // Validamos que el permiso no exista ya dentro del árbol de la familia destino
            if (ExistePermisoEnFamilia(familiaDestino, nuevoPermiso.Id))
            {
                // Regla de negocio: El permiso ya existe, se rechaza la operación.
                // En la UI, evaluaremos este 'false' para lanzar el MessageBox de alerta.
                return false;
            }

            // Pasa la validación, lo agregamos en memoria
            familiaDestino.AgregarHijo(nuevoPermiso);
            return true;
        }

        // 2. MÉTODOS DE PASARELA HACIA LA DAL

        public bool GuardarFamilia(Familia_65RD familia)
        {
            // Antes de guardar en la base de datos, podrías agregar validaciones extra 
            // (ej. que la familia tenga al menos un permiso, que el nombre no esté vacío, etc.)
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
    }
}
