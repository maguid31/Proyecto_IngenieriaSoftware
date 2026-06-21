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
    public partial class frmIdioma : Form ,IidiomaObserver
    {
        public frmIdioma()
        {
            InitializeComponent();
        }

        private void frmIdioma_Load(object sender, EventArgs e)
        {
            IdiomaManager.GetInstance().RegisterObserver(this);

            cmbIdiomas.Items.Clear();
            cmbIdiomas.Items.Add("Español");
            cmbIdiomas.Items.Add("English");

            if (IdiomaManager.GetInstance().IdiomaActual == "en")
            {
                cmbIdiomas.SelectedIndex = 1; 
            }
            else
            {
                cmbIdiomas.SelectedIndex = 0; 
            }
            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);
        }

        private void btnGuardarIdioma_Click(object sender, EventArgs e)
        {
            string idiomaSeleccionado = "es";

            if (cmbIdiomas.SelectedIndex == 1)
            {
                idiomaSeleccionado = "en";
            }
            IdiomaManager.GetInstance().CambiarIdioma(idiomaSeleccionado);

            if (SessionManager_65RD.Instancia.UsuarioLogueado != null)
            {
                SessionManager_65RD.Instancia.UsuarioLogueado.Idioma = idiomaSeleccionado;

                try
                {
                    UsuarioBLL_65RD usuarioBLL = new UsuarioBLL_65RD();
                    usuarioBLL.ActualizarIdiomaUsuario(SessionManager_65RD.Instancia.UsuarioLogueado.Id, idiomaSeleccionado);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la preferencia en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgExitoCuerpo");
            string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgExitoTitulo");

            MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void UpdateIdioma(string idioma)
        {
            this.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "titleForm");
            lblTituloPantalla.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloPantalla");
            lblSeleccionarIdioma.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblSeleccionarIdioma");
            btnGuardarIdioma.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnGuardarIdioma");
        }

        private void frmIdioma_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().RemoveObserver(this);
        }
    }
}
