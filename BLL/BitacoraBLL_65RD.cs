using DAL;
using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BitacoraBLL_65RD
    {
        private BitacoraDAL_65RD _bitacoraDAL = new BitacoraDAL_65RD();

        public void RegistrarEvento(int usuarioId, string modulo, string accion, int criticidad, string descripcion)
        {
            _bitacoraDAL.RegistrarEvento(usuarioId, modulo, accion, criticidad, descripcion);
        }

        public List<Bitacora_65RD> ConsultarBitacora(DateTime fechaDesde, DateTime fechaHasta, string modulo, string evento, int criticidad)
        {
            // Validaciones de negocio simples
            if (fechaDesde > fechaHasta)
            {
                throw new Exception("La fecha de inicio no puede ser mayor a la fecha de fin.");
            }

            return _bitacoraDAL.ObtenerEventos(fechaDesde, fechaHasta, modulo, evento, criticidad);
        }

    }
}
