using BLL_65RD;
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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            frmRegistro formularioRegistro = new frmRegistro();

            
            formularioRegistro.ShowDialog();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
            var listaUsuarios = gestorUsuario.ObtenerUsuarios(); // Método que devuelve todos los usuarios desde la DAL

            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = listaUsuarios;

            dgvUsuarios.Columns["Contraseña"].Visible = false;
            dgvUsuarios.Columns["Email"].Visible = false;
            dgvUsuarios.Columns["IntentosFallidos"].Visible = false;
            dgvUsuarios.Columns["PrimerLogin"].Visible = false;
        }

        private void S_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }
    }
}
