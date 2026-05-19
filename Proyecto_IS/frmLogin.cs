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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Cargar roles en el ComboBox
            comboBoxRol.Items.Clear();
            comboBoxRol.Items.Add("Administrador");
            comboBoxRol.Items.Add("Usuario");
            comboBoxRol.SelectedIndex = 0; // por defecto Administrador

            // --- CÓDIGO TEMPORAL PARA CREAR ADMIN INICIAL ---
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();

            // Si no hay usuarios en la base de datos, creamos uno
            if (gestorUsuario.ObtenerUsuarios().Count == 0)
            {
                Usuario_65RD adminInicial = new Usuario_65RD
                {
                    Nombre = "Admin",
                    Apellido = "Sistema",
                    DNI = "1234",
                    Contraseña = Seguridad_65RD.Encriptar("1234"), // Tu sistema genera el Hash acá
                    Perfil = new Perfil_65RD { Id = 2, Nombre = "Administrador" }, // El ID 2 es Admin en SQL
                    Activo = true,
                    IntentosFallidos = 0,
                    PrimerLogin = false // Lo ponemos en false para que no te pida cambiarla de entrada
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
                    string rolSeleccionado = comboBoxRol.SelectedItem.ToString();

                    if ((usuarioLogueado.Perfil != null && usuarioLogueado.Perfil.Nombre == "Administrador" && rolSeleccionado == "Administrador") ||
                        (usuarioLogueado.Perfil != null && usuarioLogueado.Perfil.Nombre == "Basico" && rolSeleccionado == "Usuario"))
                    {
                        string stringRolParaMenu = usuarioLogueado.Perfil.Nombre;

                        MainForm menuPrincipal = new MainForm(
                            usuarioLogueado.Id,
                            usuarioLogueado.Nombre,
                            stringRolParaMenu
                        );

                        menuPrincipal.Show();
                        this.Hide();

                        menuPrincipal.FormClosed += (s, args) => this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El rol seleccionado no coincide con el rol asignado al usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    // Mensaje actualizado
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Text = "";
                    txtContraseña.Focus();
                    break;

                // AGREGAMOS EL NUEVO CASO ACÁ
                case ResultadoLogin.CuentaBloqueada:
                    MessageBox.Show("Su cuenta ha sido bloqueada por seguridad tras múltiples intentos fallidos. Por favor, comuníquese con el administrador.", "Cuenta Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            Application.Exit();
        }
    }
}
