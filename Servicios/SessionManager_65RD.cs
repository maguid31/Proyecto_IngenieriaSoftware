using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_65RD
{
    public class SessionManager_65RD
    {
        private static SessionManager_65RD _instancia;
        private static readonly object _lock = new object();

        public Usuario_65RD UsuarioLogueado { get; private set; }
        public string IdiomaUsuario => UsuarioLogueado?.Idioma ?? "es";

        private SessionManager_65RD() { }

        public static SessionManager_65RD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                            _instancia = new SessionManager_65RD();
                    }
                }
                return _instancia;
            }
        }

        public void IniciarSesion(Usuario_65RD usuario)
        {
            UsuarioLogueado = usuario;
            if (usuario != null && !string.IsNullOrEmpty(usuario.Idioma))
            {
                IdiomaManager.GetInstance().CambiarIdioma(usuario.Idioma);
            }
        }

        public void CerrarSesion()
        {
            UsuarioLogueado = null;
        }
    }
}
