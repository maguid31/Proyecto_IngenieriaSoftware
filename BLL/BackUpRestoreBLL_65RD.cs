using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BackUpRestoreBLL_65RD
    {
        private readonly BackUpRestoreDAL_65RD _backUpRestoreDAL = new BackUpRestoreDAL_65RD();
        private readonly BitacoraBLL_65RD _bitacoraBLL = new BitacoraBLL_65RD();
        private readonly DigitoVerificadorBLL_65RD _dvBLL = new DigitoVerificadorBLL_65RD();

        public void RealizarBackup(string rutaCompleta, int idUsuario)
        {
            if (string.IsNullOrWhiteSpace(rutaCompleta))
                throw new ArgumentException("Debe seleccionar una ruta para guardar el backup.");

            _backUpRestoreDAL.EjecutarBackup(rutaCompleta);

            _bitacoraBLL.RegistrarEvento(idUsuario, "Respaldo", "Backup", 3,
                $"Se realizó un backup en: {rutaCompleta}");
        }

        public void RealizarRestore(string rutaCompleta, int idUsuario)
        {
            if (string.IsNullOrWhiteSpace(rutaCompleta))
                throw new ArgumentException("Debe seleccionar el archivo .bak para restaurar.");

            if (!File.Exists(rutaCompleta))
                throw new FileNotFoundException("El archivo seleccionado no existe.");

            _backUpRestoreDAL.EjecutarRestore(rutaCompleta);
            _dvBLL.Recalcular();
            try
            {
                _bitacoraBLL.RegistrarEvento(idUsuario, "Respaldo", "Restore", 4,
                    $"Se realizó un restore desde: {rutaCompleta}");
            }
            catch
            {
               
            }
        }
    }
}
