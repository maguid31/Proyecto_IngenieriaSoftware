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
    public partial class frmRegistro : Form
    {
        public frmRegistro()
        {
            InitializeComponent();
        }

        private void frmRegistro_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtDNI.Text))
            {
                MessageBox.Show("Por favor, ingrese su Apellido y DNI.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
            bool registrado = gestorUsuario.RegistrarNuevoUsuario(txtApellido.Text, txtDNI.Text);

            if (registrado)
            {
                MessageBox.Show($"¡Cuenta creada!\nTu usuario es: {txtApellido.Text}{txtDNI.Text}\nTu contraseña inicial es: {txtDNI.Text}", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al guardar en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            
             frmLogin login = new frmLogin();
             login.Show();

            // Ocultar o cerrar el formulario de registro actual
            this.Hide();
        }
    }
}
