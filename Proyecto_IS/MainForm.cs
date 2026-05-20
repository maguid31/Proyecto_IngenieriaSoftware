using BLL;
using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_IS
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        
        private readonly string _rol;          
        private readonly string _nombre;
        private readonly int _usuarioId;

        // ── Colores del tema (Blanco y Violeta) 
        private static readonly Color ColorHeader = Color.FromArgb(255, 255, 255);      
        private static readonly Color ColorMenu = Color.FromArgb(248, 249, 250);        
        private static readonly Color ColorFondoContenido = Color.FromArgb(235, 238, 245); 
        private static readonly Color ColorAccent = Color.FromArgb(123, 97, 255);       
        private static readonly Color ColorHover = Color.FromArgb(220, 215, 255);       
        private static readonly Color ColorTextoPrincipal = Color.FromArgb(40, 40, 40); 

        
        public MainForm(int usuarioId, string nombre, string rol)
        {
            _usuarioId = usuarioId;
            _nombre = nombre;
            _rol = rol;

            InitializeComponent();
            ConfigurarVentana();
            ConstruirMenu();
            ActualizarBienvenida();
        }

        
        private void ConfigurarVentana()
        {
            this.Text = "Menú Principal";
            this.Size = new Size(1000, 650);
            this.MinimumSize = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            
            panelHeader.BackColor = ColorHeader;
            panelMenu.BackColor = ColorMenu;
            panelContenido.BackColor = ColorFondoContenido;

            lblAppNombre.Text = "MENÚ";
            lblAppNombre.ForeColor = ColorAccent;
        }

        
        //  CONSTRUCCIÓN DEL MENÚ LATERAL (Control de Roles)
        
        private void ConstruirMenu()
        {
            panelMenu.Controls.Clear();

            
            panelMenu.Controls.Add(lblRol);
            panelMenu.Controls.Add(lblBienvenida);

            int y = 20; 

            if (_rol == "Administrador")
            {
                AgregarSeparador("ADMINISTRACIÓN", ref y);
                AgregarBoton("👤  Gestión de Usuarios", ref y, AbrirGestionUsuarios);
                AgregarBoton("📋  Bitácora de Eventos", ref y, AbrirBitacora);
            }
            else if (_rol == "Basico")
            {
                AgregarSeparador("CONFIGURACIÓN", ref y);
                AgregarBoton("🌐  Cambiar Idioma", ref y, AbrirCambiarIdioma);
            }

            
            AgregarSeparador("SESIÓN", ref y);
            AgregarBoton("CERRAR SESIÓN", ref y, CerrarSesion, esLogout: true);
        }

        
        private void AgregarSeparador(string titulo, ref int y)
        {
            y += 10;
            var lbl = new Label
            {
                Text = titulo,
                Left = 15,
                Top = y,
                Width = panelMenu.Width - 30,
                Height = 20,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.Gray,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.BottomLeft,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            panelMenu.Controls.Add(lbl);
            y += lbl.Height + 5;
        }

        private void AgregarBoton(string texto, ref int y, EventHandler accion, bool esLogout = false)
        {
            var btn = new Button
            {
                Text = texto,
                Left = 10,
                Top = y,
                Width = panelMenu.Width - 20,
                Height = 42,
                FlatStyle = FlatStyle.Flat,
                BackColor = esLogout ? ColorAccent : ColorMenu,
                ForeColor = esLogout ? Color.White : ColorTextoPrincipal,
                Font = esLogout ? new Font("Segoe UI", 9.5f, FontStyle.Bold) : new Font("Segoe UI", 10f, FontStyle.Regular),
                TextAlign = esLogout ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft,
                Padding = esLogout ? new Padding(0) : new Padding(10, 0, 0, 0),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            btn.FlatAppearance.BorderSize = esLogout ? 0 : 1;
            btn.FlatAppearance.BorderColor = esLogout ? ColorAccent : Color.LightGray;

            
            btn.MouseEnter += (s, e) => btn.BackColor = esLogout ? Color.FromArgb(100, 75, 230) : ColorHover;
            btn.MouseLeave += (s, e) => btn.BackColor = esLogout ? ColorAccent : ColorMenu;

            btn.Click += accion;
            panelMenu.Controls.Add(btn);
            y += btn.Height + 8;
        }

        
        private void ActualizarBienvenida()
        {
            lblBienvenida.Text = $"Hola, {_nombre}";
            lblRol.Text = _rol == "Administrador" ? "🛡  Administrador" : "👤  Usuario Básico";
        }

        
        private void AbrirGestionUsuarios(object sender, EventArgs e)
        {
            frmGestionUsuarios frmGestion = new frmGestionUsuarios();
            frmGestion.ShowDialog(); 
        }

        private void AbrirBitacora(object sender, EventArgs e)
        {
            
            frmBitacoraEventos formBitacora = new frmBitacoraEventos();

            
            formBitacora.ShowDialog();
        }

        private void AbrirCambiarIdioma(object sender, EventArgs e)
        {
            MostrarContenido("Cambiar Idioma", "El formulario para cambiar el idioma todavía no está desarrollado.");
        }

        private void CerrarSesion(object sender, EventArgs e)
        {
            var res = MessageBox.Show("¿Estás seguro que querés cerrar sesión?", "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                // REGISTRAMOS LOGOUT EN BITÁCORA ANTES DE BORRAR LA SESIÓN
                if (SessionManager_65RD.Instancia.UsuarioLogueado != null)
                {
                    int idUsuarioActual = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                    BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                    bitacora.RegistrarEvento(idUsuarioActual, "Usuarios", "Logout", 1, "El usuario cerró sesión desde el menú principal");
                }

                
                SessionManager_65RD.Instancia.CerrarSesion();
                this.Hide();
                frmLogin login = new frmLogin();
                login.Show();
            }
        }

        
        private void MostrarContenido(string titulo, string descripcion)
        {
            lblContenidoTitulo.Text = titulo;
            lblContenidoDetalle.Text = descripcion;
        }
    }


}
