using DAL;
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

            public void RegistrarEvento(int usuarioId, string accion, string descripcion)
            {
                _bitacoraDAL.RegistrarAccion(usuarioId, accion, descripcion);
            }
        

    }
}
