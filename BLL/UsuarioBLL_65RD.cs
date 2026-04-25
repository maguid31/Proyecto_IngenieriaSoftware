using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_65RD;


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

        public ResultadoLogin IniciarSesion(string nombreUsuarioIngresado, string contraseñaIngresada)
        {
            string hashIngresado = Seguridad_65RD.Encriptar(contraseñaIngresada);
            Usuario_65RD usuarioEncontrado = _usuarioDAL.Login(nombreUsuarioIngresado, hashIngresado);

            if (usuarioEncontrado != null && usuarioEncontrado.Activo)
            {
                // Guardamos en el Singleton
                SessionManager_65RD.Instancia.IniciarSesion(usuarioEncontrado);

                // Validamos si es el primer ingreso (Contraseña original encriptada vs DNI ingresado)
                if (contraseñaIngresada == usuarioEncontrado.DNI)
                {
                    return ResultadoLogin.RequiereCambioContrasena;
                }

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
                Perfil = RolUsuario.Basico
            };

            return _usuarioDAL.RegistrarUsuario(nuevoUsuario);
        }

        public bool CambiarContraseña(int usuarioId, string nuevaContraseña)
        {
            string nuevaContraseñaHash = Seguridad_65RD.Encriptar(nuevaContraseña);
            return _usuarioDAL.ActualizarContraseña(usuarioId, nuevaContraseñaHash);
        }
    }
}
