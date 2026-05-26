using BLL_65RD;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            txtContraseña.PasswordChar = '•';
        }
        public bool EsReLogin { get; set; } = false;


        private void frmLogin_Load(object sender, EventArgs e)
        {
            
           

            
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();

            
            if (gestorUsuario.ObtenerUsuarios().Count == 0)
            {
                Usuario_65RD adminInicial = new Usuario_65RD
                {
                    Nombre = "Admin",
                    Apellido = "Sistema",
                    DNI = "1234",
                    Contraseña = Seguridad_65RD.Encriptar("1234"), // Hash 
                    Perfil = new Perfil_65RD { Id = 2, Nombre = "Administrador" }, // El ID 2 es Admin en SQL
                    Activo = true,
                    PrimerLogin = false 
                };

                gestorUsuario.RegistrarUsuario(adminInicial);
                MessageBox.Show("Usuario administrador creado con éxito.\n\nUsuario: Sistema1234\nClave: 1234", "Admin Inicial");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Por favor, ingrese sus datos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
            ResultadoLogin resultado = gestorUsuario.IniciarSesion(txtUsuario.Text, txtContraseña.Text);

            switch (resultado)
            {
                case ResultadoLogin.Exitoso:
                    Usuario_65RD usuarioLogueado = SessionManager_65RD.Instancia.UsuarioLogueado;

                    if (Application.OpenForms.OfType<MainForm>().Any() &&
                    SessionManager_65RD.Instancia.UsuarioLogueado != null &&
                    SessionManager_65RD.Instancia.UsuarioLogueado.NombreUsuario == txtUsuario.Text && this.EsReLogin) // 🚨 solo bloquea si es relogin
                    {
                        MessageBox.Show("Este usuario ya tiene una sesión activa. No puede iniciar sesión nuevamente.",
                                        "Sesión activa",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                        // Mantener frmLogin abierto y MainForm detrás
                        this.BringToFront();
                        return;
                    }
                    if (usuarioLogueado.Perfil != null)
                    {
                        MainForm menuPrincipal = new MainForm(
                            usuarioLogueado.Id,
                            usuarioLogueado.Nombre,
                            usuarioLogueado.Perfil.Nombre
                        );

                        menuPrincipal.Show();
                        this.Hide();

                        // solo cerramos login junto con MainForm si es login inicial
                        if (!this.EsReLogin)
                            menuPrincipal.FormClosed += (s, args) => this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El usuario no tiene un perfil asignado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                case ResultadoLogin.RequiereCambioContrasena:
                    MessageBox.Show("Por seguridad, debe cambiar su contraseña en su primer ingreso.", "Cambio requerido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmCambioContraseña frmCambio = new frmCambioContraseña();
                    frmCambio.Show();
                    this.Hide();
                    frmCambio.FormClosed += (s, args) => this.Show();
                    break;

                case ResultadoLogin.CredencialesInvalidas:
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Text = "";
                    txtContraseña.Focus();
                    break;

                case ResultadoLogin.CuentaBloqueada:
                    MessageBox.Show("Su cuenta ha sido bloqueada por seguridad tras múltiples intentos fallidos. Por favor, comuníquese con el administrador para desbloquearla.", "Cuenta Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtContraseña.Text = "";
                    txtContraseña.Focus();
                    break;

                case ResultadoLogin.CuentaDeshabilitada:
                    MessageBox.Show("Su cuenta se encuentra deshabilitada. Por favor, comuníquese con la administración.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContraseña.Text = "";
                    txtContraseña.Focus();
                    break;
            }
        }
        
        

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtContraseña.PasswordChar = cbShowPassword.Checked ? '\0' : '•';
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!this.EsReLogin)
            {
                // 🚨 Solo cerrar la aplicación si era el login inicial
                Application.Exit();
            }
            else
            {
                // 🚨 Si era relogin, simplemente mostrar el MainForm que ya estaba abierto
                var main = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                if (main != null)
                {
                    main.Show();
                }
            }
        }
    }
}
