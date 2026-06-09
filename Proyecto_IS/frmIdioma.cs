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
            // 1. Registrar el formulario como observador del patrón
            IdiomaManager.GetInstance().RegisterObserver(this);

            // 2. Limpiar y configurar las opciones del ComboBox
            cmbIdiomas.Items.Clear();
            cmbIdiomas.Items.Add("Español");
            cmbIdiomas.Items.Add("English");

            // 3. Seleccionar el idioma que esté activo en el sistema actualmente
            if (IdiomaManager.GetInstance().IdiomaActual == "en")
            {
                cmbIdiomas.SelectedIndex = 1; // English
            }
            else
            {
                cmbIdiomas.SelectedIndex = 0; // Español
            }

            // 4. Ejecutar la traducción inicial de los textos de esta pantalla
            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);
        }

        private void btnGuardarIdioma_Click(object sender, EventArgs e)
        {
            string idiomaSeleccionado = "es";

            if (cmbIdiomas.SelectedIndex == 1)
            {
                idiomaSeleccionado = "en";
            }

            // 1. Cambiar el idioma de forma global (Notifica a todos los formularios abiertos)
            IdiomaManager.GetInstance().CambiarIdioma(idiomaSeleccionado);

            // 2. Guardar la elección en la sesión actual en memoria
            if (SessionManager_65RD.Instancia.UsuarioLogueado != null)
            {
                SessionManager_65RD.Instancia.UsuarioLogueado.Idioma = idiomaSeleccionado;

                // 3. Persistencia en Base de Datos usando la BLL
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

            // 4. Mostrar mensaje de éxito traducido dinámicamente
            string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgExitoCuerpo");
            string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgExitoTitulo");

            MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void UpdateIdioma(string idioma)
        {
            // Traduce el título principal del Formulario o sus grupobox/labels
            this.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "titleForm");
            lblTituloPantalla.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloPantalla");
            lblSeleccionarIdioma.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblSeleccionarIdioma");
            btnGuardarIdioma.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnGuardarIdioma");
        }

        // Si cerrás el formulario, es buena práctica desuscribirlo del Observer para liberar memoria
        private void frmIdioma_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().RemoveObserver(this);
        }
    }
}
