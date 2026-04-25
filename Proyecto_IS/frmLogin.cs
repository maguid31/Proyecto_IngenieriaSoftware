using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_65RD;

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
                    mainForm principal = new mainForm();
                    principal.Show();
                    this.Hide();
                    principal.FormClosed += (s, args) => this.Close();
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

        private void btnRegistrarme_Click(object sender, EventArgs e)
        {
            frmRegistro registro = new frmRegistro();
            registro.Show();
            this.Hide();
            registro.FormClosed += (s, args) => this.Show();
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtContraseña.PasswordChar = cbShowPassword.Checked ? '\0' : '•';
        }
    }
}
