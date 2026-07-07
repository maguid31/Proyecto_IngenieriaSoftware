using BLL;
using Servicios;
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
    public partial class MainForm : Form , IidiomaObserver
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IdiomaManager.GetInstance().RegisterObserver(this);
            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);

        }

        
        private readonly string _rol;          
        private readonly string _nombre;
        private readonly int _usuarioId;

         
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

            IdiomaManager.GetInstance().RegisterObserver(this);
            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);
        }

        
        private void ConfigurarVentana()
        {
            this.Size = new Size(1000, 750);
            this.MinimumSize = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            
            panelHeader.BackColor = ColorHeader;
            panelMenu.BackColor = ColorMenu;
            panelContenido.BackColor = ColorFondoContenido;

            
            lblAppNombre.ForeColor = ColorAccent;
        }

        
        private void ConstruirMenu()
        {
            panelMenu.Controls.Clear();

            panelMenu.Controls.Add(lblRol);
            panelMenu.Controls.Add(lblBienvenida);

            int y = 100; 

            string segAdmin = IdiomaManager.GetInstance().GetTexto(this.Name, "segAdministracion");

            
            bool canGestionUsuarios = SessionManager_65RD.Instancia.TienePermiso("Gestión de Usuarios");
            bool canBitacora = SessionManager_65RD.Instancia.TienePermiso("Ver Bitácora");
            bool canGestionRoles = SessionManager_65RD.Instancia.TienePermiso("Gestión de Perfiles");
            bool canGestionFamilias = SessionManager_65RD.Instancia.TienePermiso("Gestión de Familias");
            bool canRespaldo = SessionManager_65RD.Instancia.TienePermiso("Gestión de Respaldo");


            if (canGestionUsuarios || canBitacora || canGestionRoles || canGestionFamilias)
            {
                AgregarSeparador(segAdmin, ref y);
            }

            
            if (canGestionUsuarios)
            {
                string btnGestion = IdiomaManager.GetInstance().GetTexto(this.Name, "btnGestionUsuarios");
                AgregarBoton("👤  " + btnGestion, ref y, AbrirGestionUsuarios);
            }

            if (canBitacora)
            {
                string btnBitacora = IdiomaManager.GetInstance().GetTexto(this.Name, "btnBitacoraEventos");
                AgregarBoton("📋  " + btnBitacora, ref y, AbrirBitacora);
            }

            if (canGestionRoles)
            {
                string btnGestionPerfiles = IdiomaManager.GetInstance().GetTexto(this.Name, "btnMenuPerfiles");
                AgregarBoton("⚙️  " + btnGestionPerfiles, ref y, AbrirGestionRoles);
            }

            if (canGestionFamilias)
            {
                string btnFamilias = IdiomaManager.GetInstance().GetTexto(this.Name, "btnMenuFamilias");
                AgregarBoton("📁  " + btnFamilias, ref y, AbrirGestionFamilias);
            }


            if (canRespaldo)
            {
                string btnMenuRespaldo = IdiomaManager.GetInstance().GetTexto(this.Name, "btnMenuRespaldo");
                AgregarBoton("📁" + btnMenuRespaldo, ref y, AbrirGestionRespaldo);
            }

            string segConfig = IdiomaManager.GetInstance().GetTexto(this.Name, "segConfiguracion");
            string btnIdioma = IdiomaManager.GetInstance().GetTexto(this.Name, "btnCambiarIdioma");
            string btnContrasena = IdiomaManager.GetInstance().GetTexto(this.Name, "btnCambiarContrasena");
            string btnPerfiles = IdiomaManager.GetInstance().GetTexto(this.Name, "btnMenuPerfiles");
            string btnGestionFamilia = IdiomaManager.GetInstance().GetTexto(this.Name, "btnMenuFamilias");
            string btnRespaldo = IdiomaManager.GetInstance().GetTexto(this.Name, "btnMenuRespaldo");



            AgregarSeparador(segConfig, ref y);
            AgregarBoton("🌐  " + btnIdioma, ref y, AbrirCambiarIdioma);
            AgregarBoton("🔑 " + btnContrasena, ref y,  AbrirCambiarContraseña);


            
            string segSesion = IdiomaManager.GetInstance().GetTexto(this.Name, "segSesion");
            string btnLogOut = IdiomaManager.GetInstance().GetTexto(this.Name, "btnCerrarSesion");
            string btnReLog = IdiomaManager.GetInstance().GetTexto(this.Name, "btnIniciarSesion");

            AgregarSeparador(segSesion, ref y);
            AgregarBoton(btnLogOut, ref y, CerrarSesion, esLogout: true);
            AgregarBoton("🔄  " + btnReLog, ref y, ReLogin);

            

        }

        private void AbrirGestionRespaldo(object sender, EventArgs e)
        {
            frmGestionRespaldo frm = new frmGestionRespaldo();
            frm.ShowDialog();
        }

        private void ReLogin(object sender, EventArgs e)
        {
            
            if (SessionManager_65RD.Instancia.UsuarioLogueado != null)
            {
                
                string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgSesionActivaCuerpo");
                string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgSesionActivaTitulo");

                
                DialogResult advertencia = MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                
                if (advertencia == DialogResult.No)
                {
                    return;
                }

                
                int idUsuarioActual = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                new BitacoraBLL_65RD().RegistrarEvento(idUsuarioActual, "Usuarios", "Logout", 1, "Cierre de sesión por cambio de usuario (ReLogin)");

                SessionManager_65RD.Instancia.CerrarSesion();
            }

            
            frmLogin login = new frmLogin();
            login.EsReLogin = true;
            login.Show();
            this.Hide();

            
            login.FormClosed += (s, args) =>
            {
                
                if (SessionManager_65RD.Instancia.UsuarioLogueado != null)
                {
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            };
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
            string saludo = IdiomaManager.GetInstance().GetTexto(this.Name, "lblHola");
            lblBienvenida.Text = $"{saludo}, {_nombre}";

            string rolAdmin = IdiomaManager.GetInstance().GetTexto(this.Name, "lblRolAdmin");
            string rolBasico = IdiomaManager.GetInstance().GetTexto(this.Name, "lblRolBasico");

            lblRol.Text = _rol == "Administrador" ? rolAdmin : rolBasico;
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

            panelContenido.Controls.Clear();

            frmIdioma frmIdioma = new frmIdioma();
            frmIdioma.TopLevel = false;
            frmIdioma.FormBorderStyle = FormBorderStyle.None;
            frmIdioma.Dock = DockStyle.Fill;

            panelContenido.Controls.Add(frmIdioma);
            panelContenido.Tag = frmIdioma;
            frmIdioma.Show();
        }
        

        private void CerrarSesion(object sender, EventArgs e)
        {
            string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgCerrarSesionCuerpo");
            string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgCerrarSesionTitulo");

            var res = MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                
                if (SessionManager_65RD.Instancia.UsuarioLogueado != null)
                {
                    int idUsuarioActual = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                    BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                    bitacora.RegistrarEvento(idUsuarioActual, "Usuarios", "Logout", 1, "El usuario cerró sesión desde el menú principal");
                }

                SessionManager_65RD.Instancia.CerrarSesion();
                IdiomaManager.GetInstance().CambiarIdioma("es");
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
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().RemoveObserver(this);
        }

        public void UpdateIdioma(string idioma)
        {
            this.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloVentana");
            if (lblAppNombre != null) lblAppNombre.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblMenuHeader");

            if (lblContenidoTitulo != null) lblContenidoTitulo.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblPanelInicioTitulo");
            if (lblContenidoDetalle != null) lblContenidoDetalle.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblPanelInicioDetalle");

            ActualizarBienvenida();
            ConstruirMenu();
        }

        private void AbrirGestionRoles(object sender, EventArgs e)
        {
            frmGestionPerfiles frm = new frmGestionPerfiles();
            frm.ShowDialog();
        }

        private void AbrirGestionFamilias(object sender, EventArgs e)
        {
            frmGestionFamilias frm = new frmGestionFamilias();
            frm.ShowDialog();
        }
        private void AbrirCambiarContraseña(object sender, EventArgs e)
        {
            frmCambioContraseña frm = new frmCambioContraseña();
            frm.ShowDialog();

           
        }
        private void panelContenido_Paint(object sender, PaintEventArgs e)
        {

        }
    }


}
