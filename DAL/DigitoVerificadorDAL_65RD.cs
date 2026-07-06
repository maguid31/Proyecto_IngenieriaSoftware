using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DigitoVerificadorDAL_65RD
    {
        private readonly string _connectionString = @"Data Source=.;Initial Catalog=proyecto_ingenieria;Integrated Security=True";


        public IList<IList<string>> ObtenerDatosTablaUsuarios()
        {

            const string query = @"
                SELECT CAST(Id AS NVARCHAR), Apellido, DNI,
                       CAST(Activo AS NVARCHAR), CAST(Bloqueado AS NVARCHAR),
                       CAST(PerfilId AS NVARCHAR)
                FROM Usuarios
                ORDER BY Id";

            return EjecutarConsultaComoMatriz(query);
        }

        public IList<IList<string>> ObtenerDatosTablaPerfiles()
        {
            const string query = @"
                SELECT CAST(Id AS NVARCHAR), Nombre
                FROM Perfiles
                ORDER BY Id";

            return EjecutarConsultaComoMatriz(query);
        }

        public IList<IList<string>> ObtenerDatosTablaPermisos()
        {
            const string query = @"
                SELECT CAST(Id AS NVARCHAR), Nombre
                FROM Permisos
                ORDER BY Id";

            return EjecutarConsultaComoMatriz(query);
        }

        public IList<IList<string>> ObtenerDatosTablaFamilias()
        {
            const string query = @"
                SELECT CAST(Id AS NVARCHAR), Nombre
                FROM Familia
                ORDER BY Id";

            return EjecutarConsultaComoMatriz(query);
        }

        public IList<IList<string>> ObtenerDatosTablaPerfilPermiso()
        {
            const string query = @"
                SELECT CAST(IdPerfil AS NVARCHAR), CAST(IdPermiso AS NVARCHAR)
                FROM Perfil_Permiso
                ORDER BY IdPerfil, IdPermiso";

            return EjecutarConsultaComoMatriz(query);
        }

        public IList<IList<string>> ObtenerDatosTablaPerfilFamilia()
        {
            const string query = @"
                SELECT CAST(IdPerfil AS NVARCHAR), CAST(IdFamilia AS NVARCHAR)
                FROM Perfil_Familia
                ORDER BY IdPerfil, IdFamilia";

            return EjecutarConsultaComoMatriz(query);
        }

        public IList<IList<string>> ObtenerDatosTablaFamiliaPermiso()
        {
            const string query = @"
                SELECT CAST(IdPadre AS NVARCHAR), CAST(IdHijo AS NVARCHAR)
                FROM Familia_Permiso
                ORDER BY IdPadre, IdHijo";

            return EjecutarConsultaComoMatriz(query);
        }

        public IList<IList<string>> ObtenerDatosTablaFamiliaFamilia()
        {
            const string query = @"
                SELECT CAST(IdFamiliaPadre AS NVARCHAR), CAST(IdFamiliaHijo AS NVARCHAR)
                FROM Familia_Familia
                ORDER BY IdFamiliaPadre, IdFamiliaHijo";

            return EjecutarConsultaComoMatriz(query);
        }


        private IList<IList<string>> EjecutarConsultaComoMatriz(string query)
        {
            var resultado = new List<IList<string>>();

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var fila = new List<string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            
                            fila.Add(reader.IsDBNull(i) ? "NULL" : reader.GetValue(i).ToString());
                        }
                        resultado.Add(fila);
                    }
                }
            }

            return resultado;
        }


        public void GuardarDV(ResultadoDV_65RD resultado)
        {
            const string query = @"
                MERGE DigitoVerificador AS target
                USING (SELECT @tabla AS NombreTabla) AS source
                ON target.NombreTabla = source.NombreTabla
                WHEN MATCHED THEN
                    UPDATE SET DVH = @dvh, DVV = @dvv, DVFinal = @dvFinal,
                               UltimaActualizacion = GETDATE()
                WHEN NOT MATCHED THEN
                    INSERT (NombreTabla, DVH, DVV, DVFinal, UltimaActualizacion)
                    VALUES (@tabla, @dvh, @dvv, @dvFinal, GETDATE());";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@tabla", resultado.NombreTabla);
                cmd.Parameters.AddWithValue("@dvh", resultado.DVH_Tabla);
                cmd.Parameters.AddWithValue("@dvv", resultado.DVV_Tabla);
                cmd.Parameters.AddWithValue("@dvFinal", resultado.DVFinal);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public Dictionary<string, ResultadoDV_65RD> ObtenerDVsGuardados()
        {
            var resultado = new Dictionary<string, ResultadoDV_65RD>();

            const string query = @"
                SELECT NombreTabla, DVH, DVV, DVFinal
                FROM DigitoVerificador";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var r = new ResultadoDV_65RD
                        {
                            NombreTabla = reader["NombreTabla"].ToString(),
                            DVH_Tabla = reader["DVH"].ToString(),
                            DVV_Tabla = reader["DVV"].ToString(),
                            DVFinal = reader["DVFinal"].ToString()
                        };
                        resultado[r.NombreTabla] = r;
                    }
                }
            }

            return resultado;
        }

        //  Método para verificar/crear la tabla si no existe

        public void AsegurarTablaExiste()
        {
            const string query = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DigitoVerificador' AND xtype='U')
                BEGIN
                    CREATE TABLE DigitoVerificador (
                        Id                  INT           PRIMARY KEY IDENTITY,
                        NombreTabla         NVARCHAR(100) NOT NULL UNIQUE,
                        DVH                 NVARCHAR(500) NOT NULL,
                        DVV                 NVARCHAR(500) NOT NULL,
                        DVFinal             NVARCHAR(500) NOT NULL,
                        UltimaActualizacion DATETIME      NOT NULL DEFAULT GETDATE()
                    )
                END";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
