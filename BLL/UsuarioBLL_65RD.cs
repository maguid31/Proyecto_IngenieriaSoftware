using BLL;
using DAL_65RD;
using Servicios;
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
        CredencialesInvalidas,
        CuentaBloqueada,
        CuentaDeshabilitada
    }
    public class UsuarioBLL_65RD
    {
        private UsuarioDAL_65RD _usuarioDAL = new UsuarioDAL_65RD();
        private BitacoraBLL_65RD _bitacoraBLL = new BitacoraBLL_65RD();

        public ResultadoLogin IniciarSesion(string nombreUsuarioIngresado, string contraseñaIngresada)
        {
            Usuario_65RD usuarioEncontrado = _usuarioDAL.ObtenerUsuarioPorLogin(nombreUsuarioIngresado);

            if (usuarioEncontrado == null)
            {
                return ResultadoLogin.CredencialesInvalidas;
            }

            
            if (!usuarioEncontrado.Activo)
            {
                return ResultadoLogin.CuentaDeshabilitada;
            }

            
            if (usuarioEncontrado.Bloqueado)
            {
                return ResultadoLogin.CuentaBloqueada;
            }

            string hashIngresado = Seguridad_65RD.Encriptar(contraseñaIngresada);

            if (usuarioEncontrado.Contraseña == hashIngresado)
            {

                SessionManager_65RD.Instancia.IniciarSesion(usuarioEncontrado);
                _bitacoraBLL.RegistrarEvento(usuarioEncontrado.Id, "Usuarios", "Login", 1, "Usuario inició sesión");

                if (usuarioEncontrado.PrimerLogin)
                    return ResultadoLogin.RequiereCambioContrasena;

                return ResultadoLogin.Exitoso;
            }
            else
            {
                _bitacoraBLL.RegistrarEvento(usuarioEncontrado.Id, "Usuarios", "Login Fallido", 2, "Intento de inicio de sesión fallido");

                int intentosFallidos = _bitacoraBLL.ContarIntentosFallidos(usuarioEncontrado.Id);

                
                if (intentosFallidos >= 3)
                {
                    _usuarioDAL.ActualizarBloqueo(usuarioEncontrado.Id, true);
                    _bitacoraBLL.RegistrarEvento(usuarioEncontrado.Id, "Usuarios", "Bloqueo por Intentos", 4, "Cuenta bloqueada por superar intentos fallidos");

                    return ResultadoLogin.CuentaBloqueada;
                }

                return ResultadoLogin.CredencialesInvalidas;
            }
        }

        public bool RegistrarUsuario(Usuario_65RD nuevoUsuario)
        {
            bool resultado = _usuarioDAL.RegistrarUsuario(nuevoUsuario);
            if (resultado)
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Usuarios"); // ← agregar
            return resultado;

        }

        public bool ActualizarUsuario(Usuario_65RD usuario)
        {
            bool resultado = _usuarioDAL.ActualizarUsuario(usuario);
            if (resultado)
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Usuarios"); // ← agregar
            return resultado;

        }
        public void DeshabilitarUsuario(int id)
        {
            _usuarioDAL.ActualizarEstado(id, false);
            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Usuarios");
        }
        public void ActualizarEstado(int id, bool activo)
        {
            _usuarioDAL.ActualizarEstado(id, activo);
            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Usuarios");
        }

        public void ActualizarBloqueo(int id, bool bloqueado)
        {
            _usuarioDAL.ActualizarBloqueo(id, bloqueado);
            new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Usuarios");
        }

        public bool CambiarContraseña(int usuarioId, string nuevaContraseña)
        {
            string hash = Seguridad_65RD.Encriptar(nuevaContraseña);
            bool resultado = _usuarioDAL.ActualizarContraseña(usuarioId, hash);

            if (resultado)
            {
                
                new BitacoraBLL_65RD().RegistrarEvento(usuarioId, "Usuarios", "Cambio Contraseña", 2, "El usuario cambió su contraseña");
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Usuarios");
            }

            return resultado;

        }

        public List<Usuario_65RD> ObtenerUsuarios()
        {
            return _usuarioDAL.ObtenerUsuarios();
        }

        
        public List<Perfil_65RD> ObtenerPerfiles()
        {
            
            return _usuarioDAL.ObtenerPerfiles();
        }

        public bool ActualizarIdiomaUsuario(int idUsuario, string nuevoIdioma)
        {
            UsuarioDAL_65RD usuarioDAL = new UsuarioDAL_65RD();
            bool resultado = usuarioDAL.ActualizarIdiomaUsuario(idUsuario, nuevoIdioma);
            if (resultado)
                new DigitoVerificadorBLL_65RD().GenerarYGuardarDV("Usuarios"); // ← agregar
            return resultado;
        }

        public bool EsAdministrador(string nombreUsuarioIngresado)
        {
            try
            {
                Usuario_65RD usuario = _usuarioDAL.ObtenerUsuarioPorLogin(nombreUsuarioIngresado);
                return usuario.Perfil?.TienePermiso("Gestión de Usuarios") ?? false;
            }
            catch
            {
                return false; // si hay error, tratar como usuario normal (más seguro)
            }
        }
    }
}
