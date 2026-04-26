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
            string apellido = txtApellido.Text.Trim();
            string dni = txtDNI.Text.Trim();

            // 1. Validar el Apellido: Que no esté vacío y que contenga SOLO letras (permitiendo espacios y apóstrofes)
            if (string.IsNullOrWhiteSpace(apellido) || !apellido.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '\''))
            {
                MessageBox.Show("El Apellido solo debe contener letras (se permiten espacios o apóstrofes).", "Apellido Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar el DNI: Que no esté vacío, que tenga exactamente 8 caracteres y que TODOS sean números
            if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8 || !dni.All(char.IsDigit))
            {
                MessageBox.Show("El DNI debe contener exactamente 8 números (sin puntos ni letras).", "DNI Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Si pasó las validaciones, procedemos a registrar
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
            bool registrado = gestorUsuario.RegistrarNuevoUsuario(apellido, dni);

            if (registrado)
            {
                MessageBox.Show($"¡Cuenta creada!\nTu usuario es: {apellido}{dni}\nTu contraseña inicial es: {dni}", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
