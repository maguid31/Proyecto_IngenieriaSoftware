using BLL;
using DAL_65RD;
using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL_65RD
{
    public enum ResultadoLogin
    {
        Exitoso,
        RequiereCambioContrasena,
        CredencialesInvalidas
    }
    public class UsuarioBLL_65RD
    {
        private UsuarioDAL_65RD _usuarioDAL = new UsuarioDAL_65RD();
        private BitacoraBLL_65RD _bitacoraBLL = new BitacoraBLL_65RD();

        public ResultadoLogin IniciarSesion(string nombreUsuarioIngresado, string contraseñaIngresada)
        {
            string hashIngresado = Seguridad_65RD.Encriptar(contraseñaIngresada);
            Usuario_65RD usuarioEncontrado = _usuarioDAL.Login(nombreUsuarioIngresado, hashIngresado);

            if (usuarioEncontrado != null && usuarioEncontrado.Activo)
            {
                if (usuarioEncontrado.Contraseña == hashIngresado)
                {
                    _usuarioDAL.ActualizarIntentos(usuarioEncontrado.Id, 0); // reset
                    SessionManager_65RD.Instancia.IniciarSesion(usuarioEncontrado);
                    _bitacoraBLL.RegistrarEvento(usuarioEncontrado.Id, "Login", "Usuario inició sesión");

                    if (contraseñaIngresada == usuarioEncontrado.DNI)
                        return ResultadoLogin.RequiereCambioContrasena;

                    return ResultadoLogin.Exitoso;
                }
                else
                {
                    usuarioEncontrado.IntentosFallidos++;
                    _usuarioDAL.ActualizarIntentos(usuarioEncontrado.Id, usuarioEncontrado.IntentosFallidos);
                    _bitacoraBLL.RegistrarEvento(usuarioEncontrado.Id, "Login fallido", "Contraseña incorrecta");

                    if (usuarioEncontrado.IntentosFallidos >= 3)
                    {
                        _usuarioDAL.BloquearUsuario(usuarioEncontrado.Id);
                        _bitacoraBLL.RegistrarEvento(usuarioEncontrado.Id, "Usuario bloqueado", "Se bloqueó por 3 intentos fallidos");
                    }
                }
            }
            return ResultadoLogin.CredencialesInvalidas;
        }

        public bool RegistrarNuevoUsuario(string apellido, string dni)
        {
            Usuario_65RD nuevoUsuario = new Usuario_65RD
            {
                Apellido = apellido,
                DNI = dni,
                Contraseña = Seguridad_65RD.Encriptar(dni), // Se guarda encriptada
                Perfil = RolUsuario.Basico
            };

            return _usuarioDAL.RegistrarUsuario(nuevoUsuario);
        }

        public bool CambiarContraseña(int usuarioId, string nuevaContraseña)
        {
            string nuevaContraseñaHash = Seguridad_65RD.Encriptar(nuevaContraseña);
            bool resultado = _usuarioDAL.ActualizarContraseña(usuarioId, nuevaContraseñaHash);

            if (resultado)
            {
                // Registrar en bitácora
                new BitacoraBLL_65RD().RegistrarEvento(usuarioId, "Cambio Contraseña", "El usuario cambió su contraseña");
            }

            return resultado;

        }
    }
}
