using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public enum ResultadoRevisionDV
    {
        Consistente,

        Inconsistente
    }


    public class TablaInconsistente
    {
        public string NombreTabla { get; set; }
        public string DVFinalCalculado { get; set; }
        public string DVFinalGuardado { get; set; }
    }

    public class DigitoVerificadorBLL_65RD
    {
        private readonly DigitoVerificadorDAL_65RD _dvDAL = new DigitoVerificadorDAL_65RD();

        private static readonly string[] TABLAS_AUDITADAS =
        {
            "Usuarios",
            "Perfiles",
            "Permisos",
            "Familia",
            "Perfil_Permiso",
            "Perfil_Familia",
            "Familia_Permiso",
            "Familia_Familia"
        };


        public void GenerarYGuardarDV(string nombreTabla = null)
        {

            _dvDAL.AsegurarTablaExiste();

            if (nombreTabla != null)
            {

                var resultado = CalcularDVDeTabla(nombreTabla);
                if (resultado != null)
                    _dvDAL.GuardarDV(resultado);
            }
            else
            {
 
                foreach (string tabla in TABLAS_AUDITADAS)
                {
                    var resultado = CalcularDVDeTabla(tabla);
                    if (resultado != null)
                        _dvDAL.GuardarDV(resultado);
                }
            }
        }

        public ResultadoRevisionDV RevisarConsistencia(out List<TablaInconsistente> tablasConError)
        {
            tablasConError = new List<TablaInconsistente>();

            try
            {

                var calculadosAhora = new Dictionary<string, ResultadoDV_65RD>();
                foreach (string tabla in TABLAS_AUDITADAS)
                {
                    var resultado = CalcularDVDeTabla(tabla);
                    if (resultado != null)
                        calculadosAhora[tabla] = resultado;
                }

                var guardadosEnBD = _dvDAL.ObtenerDVsGuardados();

                if (guardadosEnBD.Count == 0)
                {
                    GenerarYGuardarDV(); 
                    return ResultadoRevisionDV.Consistente;
                }

                foreach (string tabla in TABLAS_AUDITADAS)
                {
                    bool calculadoExiste = calculadosAhora.ContainsKey(tabla);
                    bool guardadoExiste = guardadosEnBD.ContainsKey(tabla);

                    if (!calculadoExiste && !guardadoExiste)
                        continue; 

                    string dvCalculado = calculadoExiste ? calculadosAhora[tabla].DVFinal : "NO_EXISTE";
                    string dvGuardado = guardadoExiste ? guardadosEnBD[tabla].DVFinal : "NO_EXISTE";

                    if (dvCalculado != dvGuardado)
                    {

                        tablasConError.Add(new TablaInconsistente
                        {
                            NombreTabla = tabla,
                            DVFinalCalculado = dvCalculado,
                            DVFinalGuardado = dvGuardado
                        });
                    }
                }

                return tablasConError.Count == 0
                    ? ResultadoRevisionDV.Consistente
                    : ResultadoRevisionDV.Inconsistente;
            }
            catch (Exception ex)
            {

                tablasConError.Add(new TablaInconsistente
                {
                    NombreTabla = "ERROR_SISTEMA",
                    DVFinalCalculado = ex.Message,
                    DVFinalGuardado = "N/A"
                });
                return ResultadoRevisionDV.Inconsistente;
            }
        }

        public void Recalcular()
        {
            GenerarYGuardarDV();
        }

        private ResultadoDV_65RD CalcularDVDeTabla(string nombreTabla)
        {
            IList<IList<string>> filas;
            int cantidadColumnas;


            switch (nombreTabla)
            {
                case "Usuarios":
                    filas = _dvDAL.ObtenerDatosTablaUsuarios();
                    cantidadColumnas = 6; // Id, Apellido, DNI, Activo, Bloqueado, PerfilId
                    break;

                case "Perfiles":
                    filas = _dvDAL.ObtenerDatosTablaPerfiles();
                    cantidadColumnas = 2; // Id, Nombre
                    break;

                case "Permisos":
                    filas = _dvDAL.ObtenerDatosTablaPermisos();
                    cantidadColumnas = 2; // Id, Nombre
                    break;

                case "Familia":
                    filas = _dvDAL.ObtenerDatosTablaFamilias();
                    cantidadColumnas = 2; // Id, Nombre
                    break;

                case "Perfil_Permiso":
                    filas = _dvDAL.ObtenerDatosTablaPerfilPermiso();
                    cantidadColumnas = 2; // IdPerfil, IdPermiso
                    break;

                case "Perfil_Familia":
                    filas = _dvDAL.ObtenerDatosTablaPerfilFamilia();
                    cantidadColumnas = 2; // IdPerfil, IdFamilia
                    break;

                case "Familia_Permiso":
                    filas = _dvDAL.ObtenerDatosTablaFamiliaPermiso();
                    cantidadColumnas = 2; // IdPadre, IdHijo
                    break;

                case "Familia_Familia":
                    filas = _dvDAL.ObtenerDatosTablaFamiliaFamilia();
                    cantidadColumnas = 2; // IdFamiliaPadre, IdFamiliaHijo
                    break;

                default:
                    return null; 
            }

            return CalculadorDV_65RD.Calcular(nombreTabla, filas, cantidadColumnas);
        }
    }
}
