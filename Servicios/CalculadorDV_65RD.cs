using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class CalculadorDV_65RD
    {
        private static long ConvertirCeldaANumero(string valorCelda)
        {
            if (string.IsNullOrEmpty(valorCelda))
                valorCelda = "NULL"; 

            
            string hash = Seguridad_65RD.Encriptar(valorCelda);


            string subHash = hash.Substring(0, 8);
            return Convert.ToInt64(subHash, 16); // base 16 = hexadecimal
        }


        //  Cálculo Horizontal - columna a columna dentro de un registro

        private static long CalcularDVH_Registro(IList<string> valoresFila)
        {
            long suma = 0;
            foreach (string celda in valoresFila)
                suma += ConvertirCeldaANumero(celda);
            return suma;
        }


        //  "Cálculo Vertical de todos los DVH"

        private static long CalcularDVH_Tabla(IList<IList<string>> filas)
        {
            long suma = 0;
            foreach (var fila in filas)
                suma += CalcularDVH_Registro(fila);
            return suma;
        }


        private static long CalcularDVV_Columna(IList<IList<string>> filas, int indiceColumna)
        {
            long suma = 0;
            foreach (var fila in filas)
            {
                if (indiceColumna < fila.Count)
                    suma += ConvertirCeldaANumero(fila[indiceColumna]);
            }
            return suma;
        }


        private static long CalcularDVV_Tabla(IList<IList<string>> filas, int cantidadColumnas)
        {
            long suma = 0;
            for (int col = 0; col < cantidadColumnas; col++)
                suma += CalcularDVV_Columna(filas, col);
            return suma;
        }


        public static ResultadoDV_65RD Calcular(string nombreTabla, IList<IList<string>> filas, int cantidadColumnas)
        {
            if (filas == null || filas.Count == 0)
            {
                string dvVacio = Seguridad_65RD.Encriptar($"{nombreTabla}_EMPTY");
                return new ResultadoDV_65RD
                {
                    NombreTabla = nombreTabla,
                    DVH_Tabla = dvVacio,
                    DVV_Tabla = dvVacio,
                    DVFinal = dvVacio
                };
            }

            long dvhNumerico = CalcularDVH_Tabla(filas);
            long dvvNumerico = CalcularDVV_Tabla(filas, cantidadColumnas);

            string dvhStr = dvhNumerico.ToString();
            string dvvStr = dvvNumerico.ToString();

            string dvFinal = Seguridad_65RD.Encriptar(nombreTabla + dvhStr + dvvStr);

            return new ResultadoDV_65RD
            {
                NombreTabla = nombreTabla,
                DVH_Tabla = dvhStr,
                DVV_Tabla = dvvStr,
                DVFinal = dvFinal
            };
        }
    }
}
