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
        CuentaBloqueada //  para avisarle a la UI
    }
    public class UsuarioBLL_65RD
    {
        private UsuarioDAL_65RD _usuarioDAL = new UsuarioDAL_65RD();
        private BitacoraBLL_65RD _bitacoraBLL = new BitacoraBLL_65RD();

        public ResultadoLogin IniciarSesion(string nombreUsuarioIngresado, string contraseñaIngresada)
        {
            // 1. Buscamos al usuario en la BD SOLO por su nombre (Apellido+DNI)
            Usuario_65RD usuarioEncontrado = _usuarioDAL.ObtenerUsuarioPorLogin(nombreUsuarioIngresado);

            // 2. Si no existe, devolvemos error genérico (no le decimos al atacante que el usuario no existe)
            if (usuarioEncontrado == null)
            {
                return ResultadoLogin.CredencialesInvalidas;
            }

            // 3. Verificamos si la cuenta ya está inactiva/bloqueada ANTES de probar la clave
            if (!usuarioEncontrado.Activo)
            {
                return ResultadoLogin.CuentaBloqueada;
            }

            // 4. Recién ahora aplicamos el hash irreversible a lo que escribió el usuario en la UI
            string hashIngresado = Seguridad_65RD.Encriptar(contraseñaIngresada);

            // 5. Comparamos el hash generado contra el que trajimos de la BD
            if (usuarioEncontrado.Contraseña == hashIngresado)
            {
                // === LOGIN EXITOSO ===
                _usuarioDAL.ActualizarIntentos(usuarioEncontrado.Id, 0); // Limpiamos el historial de errores
                SessionManager_65RD.Instancia.IniciarSesion(usuarioEncontrado);
                _bitacoraBLL.RegistrarEvento(usuarioEncontrado.Id, "Usuarios", "Login", 1, "Usuario inició sesión");

                if (usuarioEncontrado.PrimerLogin)
                    return ResultadoLogin.RequiereCambioContrasena;

                return ResultadoLogin.Exitoso;
            }
            else
            {
                // === CONTRASEÑA INCORRECTA ===
                int nuevosIntentos = usuarioEncontrado.IntentosFallidos + 1;

                if (nuevosIntentos >= 3)
                {
                    // Superó el límite: Lo bloqueamos y lo dejamos en 0 para cuando un Admin lo habilite de nuevo
                    _usuarioDAL.ActualizarEstado(usuarioEncontrado.Id, false);
                    _usuarioDAL.ActualizarIntentos(usuarioEncontrado.Id, 0);

                    _bitacoraBLL.RegistrarEvento(usuarioEncontrado.Id, "Usuarios", "Bloqueo por Intentos", 4, "Cuenta bloqueada por superar intentos fallidos");

                    return ResultadoLogin.CuentaBloqueada;
                }
                else
                {
                    // Aún le quedan intentos, solo actualizamos el contador en la BD
                    _usuarioDAL.ActualizarIntentos(usuarioEncontrado.Id, nuevosIntentos);
                    return ResultadoLogin.CredencialesInvalidas;
                }
            }
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
                new BitacoraBLL_65RD().RegistrarEvento(usuarioId, "Usuarios", "Cambio Contraseña", 2, "El usuario cambió su contraseña");
            }

            return resultado;

        }

        public List<Usuario_65RD> ObtenerUsuarios()
        {
            return _usuarioDAL.ObtenerUsuarios();
        }

        // Método nuevo para llenar el ComboBox de la UI
        public List<Perfil_65RD> ObtenerPerfiles()
        {
            // Llama al método que creamos en la capa DAL
            return _usuarioDAL.ObtenerPerfiles();
        }
    }
}
