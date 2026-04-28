using BLL_65RD;
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

         /*   // CÓDIGO TEMPORAL PARA CREAR ADMIN 
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();

            // Verificamos si la base de datos está vacía
            if (gestorUsuario.ObtenerUsuarios().Count == 0)
            {
                Usuario_65RD adminDefault = new Usuario_65RD
                {
                    Apellido = "Admin",
                    DNI = "12345678",
                    Contraseña = Seguridad_65RD.Encriptar("12345678"),
                    Perfil = RolUsuario.Administrador,
                    Activo = true,
                    Email = "admin@autopremium.com",
                    PrimerLogin = true
                };

                if (gestorUsuario.RegistrarUsuario(adminDefault))
                {
                    MessageBox.Show("Se autogeneró un administrador.\n\nUsuario: Admin12345678\nContraseña: 12345678", "Rescate de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } */
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

                    if (usuarioLogueado.Perfil == RolUsuario.Administrador && rolSeleccionado == "Administrador")
                    {
                        frmGestionUsuarios gestion = new frmGestionUsuarios();
                        gestion.Show();
                        this.Hide();
                        gestion.FormClosed += (s, args) => this.Close();
                    }
                    else if (usuarioLogueado.Perfil == RolUsuario.Basico && rolSeleccionado == "Usuario")
                    {
                        frmCambioContraseña cambio = new frmCambioContraseña();
                        cambio.Show();
                        this.Hide();
                        cambio.FormClosed += (s, args) => this.Close();
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
                    MessageBox.Show("Credenciales incorrectas o cuenta inactiva.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Text = "";
                    txtContraseña.Focus();
                    break;
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtContraseña.PasswordChar = cbShowPassword.Checked ? '\0' : '•';
        }
    }
}
