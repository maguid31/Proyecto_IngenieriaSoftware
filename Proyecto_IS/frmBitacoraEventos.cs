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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Servicios;

namespace Proyecto_IS
{
    public partial class frmBitacoraEventos : Form, IidiomaObserver
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
            dtpFechaIni.Value = DateTime.Now.AddDays(-3); 
            dtpFechaFin.Value = DateTime.Now;

            BuscarEventos();

            IdiomaManager.GetInstance().RegisterObserver(this);
            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);
        }

        private void CargarCombos()
        {
   
            cmbModulo.Items.Clear();
            cmbModulo.Items.AddRange(new string[] { "Todos", "Usuarios Básicos", "Administradores" });
            cmbModulo.SelectedIndex = 0;
            ActualizarEventosPorModulo();
            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.AddRange(new string[] { "0", "1", "2", "3", "4", "5" });
            cmbCriticidad.SelectedIndex = 0;

            cmbModulo.SelectedIndexChanged += (s, e) => ActualizarEventosPorModulo();
        }

        private void ActualizarEventosPorModulo()
        {
            cmbEvento.Items.Clear();

            string moduloSeleccionado = cmbModulo.SelectedItem.ToString();

            if (moduloSeleccionado == "Usuarios Básicos")
            {
                cmbEvento.Items.AddRange(new string[] {
              "Todos",
              "Login",
              "Login Fallido",
              "Logout",
              "Bloqueo por Intentos",
              "Cambio Contraseña"});

            }
            else if (moduloSeleccionado == "Administradores")
            {
                cmbEvento.Items.AddRange(new string[] {
              "Todos",
              "Login",
              "Login Fallido",
              "Logout",
              "Bloqueo por Intentos",
              "Cambio Contraseña",
              "Alta Usuario",
              "Modificar Usuario",
              "Bloquear Usuario",
              "Desbloquear Usuario"});

            }
            else 
            {
                cmbEvento.Items.AddRange(new string[] {
              "Todos",
              "Login",
              "Login Fallido",
              "Logout",
              "Bloqueo por Intentos",
              "Cambio Contraseña",
              "Alta Usuario",
              "Modificar Usuario",
              "Bloquear Usuario",
              "Desbloquear Usuario"});

            }

            cmbEvento.SelectedIndex = 0;
        }


        private void ConfigurarGrilla()
        {
            dgvBitacora.AutoGenerateColumns = false;
            dgvBitacora.Columns.Clear();

            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUsuario", DataPropertyName = "LoginUsuario", HeaderText = "Nombre usuario" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { Name = "colApellido", DataPropertyName = "Apellido", HeaderText = "Apellido" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFecha", DataPropertyName = "Fecha", HeaderText = "Fecha" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHora", DataPropertyName = "Hora", HeaderText = "Hora" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { Name = "colModulo", DataPropertyName = "Modulo", HeaderText = "Modulo" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEvento", DataPropertyName = "Evento", HeaderText = "Evento" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCriticidad", DataPropertyName = "Criticidad", HeaderText = "Criticidad" });
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescripcion", DataPropertyName = "Descripcion", HeaderText = "Descripción del Evento", Width = 300 });

            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);
        }

        private void BuscarEventos()
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

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            BuscarEventos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpFechaIni.Value = DateTime.Now.AddDays(-3);
            dtpFechaFin.Value = DateTime.Now;
            cmbModulo.SelectedIndex = 0;
            cmbEvento.SelectedIndex = 0;
            cmbCriticidad.SelectedIndex = 0;
            dgvBitacora.DataSource = null;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvBitacora.Rows.Count > 0)
            {
                SaveFileDialog guardar = new SaveFileDialog();
                guardar.Filter = "Archivo PDF (*.pdf)|*.pdf";
                guardar.FileName = "Reporte_Bitacora.pdf";

                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    bool errorArchivo = false;

                    if (File.Exists(guardar.FileName))
                    {
                        try
                        {
                            File.Delete(guardar.FileName);
                        }
                        catch (IOException)
                        {
                            errorArchivo = true;
                            string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgArchivoAbiertoCuerpo");
                            string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgErrorTitulo");
                            MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (!errorArchivo)
                    {
                        try
                        {
                            PdfPTable tablaPdf = new PdfPTable(dgvBitacora.Columns.Count);
                            tablaPdf.DefaultCell.Padding = 3;
                            tablaPdf.WidthPercentage = 100;
                            tablaPdf.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn columna in dgvBitacora.Columns)
                            {
                                PdfPCell celda = new PdfPCell(new Phrase(columna.HeaderText));
                                celda.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                                tablaPdf.AddCell(celda);
                            }

                            foreach (DataGridViewRow fila in dgvBitacora.Rows)
                            {
                                foreach (DataGridViewCell celda in fila.Cells)
                                {
                                    tablaPdf.AddCell(celda.Value?.ToString() ?? "");
                                }
                            }

                            using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();

                                string tituloPdfTexto = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloReportePdf");
                                Paragraph titulo = new Paragraph(tituloPdfTexto + "\n\n");
                                titulo.Alignment = Element.ALIGN_CENTER;
                                pdfDoc.Add(titulo);

                                pdfDoc.Add(tablaPdf);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            string msgExitoCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgPdfExitoCuerpo");
                            string msgExitoTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgExitoTitulo");
                            MessageBox.Show(msgExitoCuerpo, msgExitoTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            string msgErrorGenerar = IdiomaManager.GetInstance().GetTexto(this.Name, "msgErrorGenerarPdfCuerpo");
                            string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgErrorTitulo");
                            MessageBox.Show(msgErrorGenerar + " " + ex.Message, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                string msgAdvertenciaCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgSinEventosExportarCuerpo");
                string msgAdvertenciaTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgAdvertenciaTitulo");
                MessageBox.Show(msgAdvertenciaCuerpo, msgAdvertenciaTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void frmBitacoraEventos_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().RemoveObserver(this);
        }

        

        public void UpdateIdioma(string idioma)
        {
            this.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloVentana");

            if (label6 != null) label6.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloPantalla");
            if (lblModulo != null) lblModulo.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblModulo");
            if (lblEvento != null) lblEvento.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblEvento");
            if (lblCriticidad != null) lblCriticidad.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblCriticidad");
            if (lblInicio != null) lblInicio.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblInicio");
            if (lblFin != null) lblFin.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblFin");

            if (btnAplicar != null) btnAplicar.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnAplicar");
            if (btnLimpiar != null) btnLimpiar.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnLimpiar");
            if (btnImprimir != null) btnImprimir.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnImprimir");

            if (dgvBitacora != null && dgvBitacora.Columns.Count > 0)
            {
                if (dgvBitacora.Columns.Contains("colUsuario")) dgvBitacora.Columns["colUsuario"].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "colUsuario");
                if (dgvBitacora.Columns.Contains("colNombre")) dgvBitacora.Columns["colNombre"].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "colNombre");
                if (dgvBitacora.Columns.Contains("colApellido")) dgvBitacora.Columns["colApellido"].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "colApellido");
                if (dgvBitacora.Columns.Contains("colFecha")) dgvBitacora.Columns["colFecha"].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "colFecha");
                if (dgvBitacora.Columns.Contains("colHora")) dgvBitacora.Columns["colHora"].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "colHora");
                if (dgvBitacora.Columns.Contains("colModulo")) dgvBitacora.Columns["colModulo"].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "colModulo");
                if (dgvBitacora.Columns.Contains("colEvento")) dgvBitacora.Columns["colEvento"].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "colEvento");
                if (dgvBitacora.Columns.Contains("colCriticidad")) dgvBitacora.Columns["colCriticidad"].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "colCriticidad");
                if (dgvBitacora.Columns.Contains("colDescripcion")) dgvBitacora.Columns["colDescripcion"].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "colDescripcion");
            }
        }
    }
}
