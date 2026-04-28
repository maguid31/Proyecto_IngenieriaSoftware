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
                _usuarioDAL.ActualizarIntentos(usuarioEncontrado.Id, 0);
                SessionManager_65RD.Instancia.IniciarSesion(usuarioEncontrado);
                _bitacoraBLL.RegistrarEvento(usuarioEncontrado.Id, "Login", "Usuario inició sesión");

                // Usamos la propiedad de la base de datos
                if (usuarioEncontrado.PrimerLogin)
                    return ResultadoLogin.RequiereCambioContrasena;

                return ResultadoLogin.Exitoso;
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
                Perfil = RolUsuario.Basico,
                Activo=true
            };

            return _usuarioDAL.RegistrarUsuario(nuevoUsuario);
        }

        public bool RegistrarUsuario(Usuario_65RD nuevoUsuario)
        {
            return _usuarioDAL.RegistrarUsuario(nuevoUsuario);
        }

        public bool ActualizarUsuario(Usuario_65RD usuario)
        {
            return _usuarioDAL.ActualizarUsuario(usuario);
            
        }
        public void DeshabilitarUsuario(int id)
        {
            _usuarioDAL.ActualizarEstado(id, false);
        }
        public void ActualizarEstado(int id, bool activo)
        {
            _usuarioDAL.ActualizarEstado(id, activo);
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

        public List<Usuario_65RD> ObtenerUsuarios()
        {
            return _usuarioDAL.ObtenerUsuarios();
        }
    }
}
