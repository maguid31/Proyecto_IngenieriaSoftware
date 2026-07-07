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
    public class FamiliaBLL_65RD
    {


        private FamiliaDAL_65RD _familiaDAL = new FamiliaDAL_65RD();
        private BitacoraBLL_65RD _bitacoraBLL = new BitacoraBLL_65RD();

        public bool GuardarFamilia(Familia_65RD familia)
        {
            if (string.IsNullOrWhiteSpace(familia.Nombre))
                throw new ArgumentException("El nombre de la familia no puede estar vacío.");

            var existentes = _familiaDAL.ObtenerTodasLasFamilias();
            if (existentes.Any(f => f.Nombre.Equals(familia.Nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"Ya existe una familia con el nombre '{familia.Nombre}'.");

            bool resultado = _familiaDAL.GuardarFamilia(familia);
            if (resultado)
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Familia");
            return resultado;
        }

        public bool ModificarFamilia(Familia_65RD familia)
        {
            if (string.IsNullOrWhiteSpace(familia.Nombre))
                throw new ArgumentException("El nombre de la familia no puede estar vacío.");

            bool resultado = _familiaDAL.ActualizarFamilia(familia);
            if (resultado)
            {
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Familia");
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Familia_Permiso");
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Familia_Familia");
            }
            return resultado;
        }

        public bool EliminarFamilia(int idFamilia)
        {
            int perfilesQueUsan = _familiaDAL.ContarPerfilesQueUsanFamilia(idFamilia);
            if (perfilesQueUsan > 0)
                throw new Exception($"No se puede eliminar la familia porque está asignada a {perfilesQueUsan} perfil(es). Quitala de los perfiles primero.");

            int familiasQueLaContienen = _familiaDAL.ContarFamiliasQueContienenEstaFamilia(idFamilia);
            if (familiasQueLaContienen > 0)
                throw new Exception($"No se puede eliminar la familia porque está dentro de {familiasQueLaContienen} familia(s). Quitala primero.");

            bool resultado = _familiaDAL.EliminarFamilia(idFamilia);
            if (resultado)
            {
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Familia");
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Familia_Permiso");
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Familia_Familia");
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Perfil_Familia");
            }
            return resultado;

        }

        public List<Familia_65RD> ObtenerTodasLasFamilias()
        {
            return _familiaDAL.ObtenerTodasLasFamilias();
        }

        public void ValidarAsignacionSinRepetidos(ComponentePermiso_65RD contenedorPadre, ComponentePermiso_65RD elementoAAgregar)
        {

            List<int> idsActuales = ObtenerTodosLosIdsRecursivo(contenedorPadre);
            List<int> idsNuevos = ObtenerTodosLosIdsRecursivo(elementoAAgregar);

            if (idsActuales.Contains(elementoAAgregar.Id))
                throw new Exception($"'{elementoAAgregar.Nombre}' ya forma parte de este elemento.");

            foreach (int id in idsNuevos)
            {
                if (idsActuales.Contains(id))
                    throw new Exception($"El permiso o sub-familia contiene un elemento con ID {id} que ya forma parte de este elemento.");
            }
        }

        private List<int> ObtenerTodosLosIdsRecursivo(ComponentePermiso_65RD componente)
        {
            List<int> ids = new List<int>();
            ids.Add(componente.Id); 

            if (componente is Familia_65RD familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                {
                    ids.AddRange(ObtenerTodosLosIdsRecursivo(hijo));
                }
            }

            return ids.Distinct().ToList();
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
                {
                    ids.AddRange(ObtenerTodasLasPatentesDeFormaRecursiva(hijo));
                }
            }

            return ids.Distinct().ToList();
        }



    }
}
