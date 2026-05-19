using BLL;
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
    public partial class frmBitacoraEventos : Form
    {
        private BitacoraBLL_65RD _bitacoraBLL;
        public frmBitacoraEventos()
        {
            InitializeComponent();
            _bitacoraBLL = new BitacoraBLL_65RD();
        }

        private void frmBitacoraEventos_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarGrilla();
            dtpFechaIni.Value = DateTime.Now.AddDays(-7); // Por defecto busca la última semana
            dtpFechaFin.Value = DateTime.Now;
        }

        private void CargarCombos()
        {
            // Módulos actuales del sistema
            cmbModulo.Items.AddRange(new string[] { "Todos", "Usuarios" });
            cmbModulo.SelectedIndex = 0;

            // Eventos reales que tenés hasta ahora en tu BLL
            cmbEvento.Items.AddRange(new string[] {
                "Todos",
                "Login",
                "Logout",
                "Alta Usuario",
                "Modificar Usuario",
                "Bloquear Usuario",
                "Cambio Contraseña"
            });
            cmbEvento.SelectedIndex = 0;

            // Criticidad: 0 = Todos, 1 a 5 = Niveles
            cmbCriticidad.Items.AddRange(new string[] { "0", "1", "2", "3", "4", "5" });
            cmbCriticidad.SelectedIndex = 0;
        }

        private void ConfigurarGrilla()
        {
            dgvBitacora.AutoGenerateColumns = false;
            dgvBitacora.Columns.Clear();

            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LoginUsuario", HeaderText = "Login" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Fecha", HeaderText = "Fecha" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Hora", HeaderText = "Hora" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Modulo", HeaderText = "Modulo" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Evento", HeaderText = "Evento" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Criticidad", HeaderText = "Criticidad" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Descripcion", HeaderText = "Descripción del Evento", Width = 300 });
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime desde = dtpFechaIni.Value;
                DateTime hasta = dtpFechaFin.Value;
                string modulo = cmbModulo.SelectedItem.ToString();
                string evento = cmbEvento.SelectedItem.ToString();
                int criticidad = int.Parse(cmbCriticidad.SelectedItem.ToString());

                List<Bitacora_65RD> eventos = _bitacoraBLL.ConsultarBitacora(desde, hasta, modulo, evento, criticidad);
                dgvBitacora.DataSource = eventos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpFechaIni.Value = DateTime.Now.AddDays(-7);
            dtpFechaFin.Value = DateTime.Now;
            cmbModulo.SelectedIndex = 0;
            cmbEvento.SelectedIndex = 0;
            cmbCriticidad.SelectedIndex = 0;
            dgvBitacora.DataSource = null;
        }
    }
}
