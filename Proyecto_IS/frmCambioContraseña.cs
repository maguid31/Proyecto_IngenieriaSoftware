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
using Servicios_65RD;

namespace Proyecto_IS
{
    public partial class frmCambioContraseña : Form
    {
        public frmCambioContraseña()
        {
            InitializeComponent();
        }

        private void frmCambioContraseña_Load(object sender, EventArgs e)
        {
            // Ocultamos los caracteres de las contraseñas
            txtContraseña.PasswordChar = '•';
            txtConfirmContraseña.PasswordChar = '•';

            // Mostramos el nombre de usuario de la sesión actual y bloqueamos el campo
            if (SessionManager_65RD.Instancia.UsuarioLogueado != null)
            {
                txtUsuario.Text = SessionManager_65RD.Instancia.UsuarioLogueado.NombreUsuario;
                txtUsuario.ReadOnly = true; // Para que no lo puedan modificar por error
                txtUsuario.BackColor = System.Drawing.SystemColors.Control; 
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraseña.Text) || string.IsNullOrWhiteSpace(txtConfirmContraseña.Text))
            {
                MessageBox.Show("Por favor, ingrese y confirme su nueva contraseña.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valida que las contraseñas coincidan
            if (txtContraseña.Text != txtConfirmContraseña.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden. Inténtelo de nuevo.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que la nueva contraseña NO sea igual al DNI 
            if (txtContraseña.Text == SessionManager_65RD.Instancia.UsuarioLogueado.DNI)
            {
                MessageBox.Show("La nueva contraseña no puede ser igual a su DNI. Por favor, elija una diferente.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Text = "";
                txtConfirmContraseña.Text = "";
                txtContraseña.Focus();
                return;
            }

            // Llamar a la BLL para actualizar la contraseña en la base de datos
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
            bool actualizado = gestorUsuario.CambiarContraseña(SessionManager_65RD.Instancia.UsuarioLogueado.Id, txtContraseña.Text);

            if (actualizado)
            {
                MessageBox.Show("¡Contraseña actualizada correctamente!\nPor favor, inicie sesión con sus nuevas credenciales.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cerramos la sesión temporal que habíamos abierto en el login
                SessionManager_65RD.Instancia.CerrarSesion();

                // Cerramos este formulario 
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar actualizar la contraseña en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                txtContraseña.PasswordChar = '\0';
                txtConfirmContraseña.PasswordChar = '\0';
            }
            else
            {
                txtContraseña.PasswordChar = '•';
                txtConfirmContraseña.PasswordChar = '•';
            }
        }
    }
}
