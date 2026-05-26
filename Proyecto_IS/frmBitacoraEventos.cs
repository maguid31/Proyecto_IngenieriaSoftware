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
            dtpFechaIni.Value = DateTime.Now.AddDays(-7); 
            dtpFechaFin.Value = DateTime.Now;

            BuscarEventos();
        }

        private void CargarCombos()
        {
            cmbModulo.Items.AddRange(new string[] { "Todos", "Usuarios" });
            cmbModulo.SelectedIndex = 0;

            
            cmbEvento.Items.AddRange(new string[] {
                "Todos",
                "Login",
                "Login Fallido", 
                "Logout",
                "Alta Usuario",
                "Modificar Usuario",
                "Bloquear Usuario",
                "Bloqueo por Intentos",
                "Desbloquear Usuario",
                "Cambio Contraseña"
            });
            cmbEvento.SelectedIndex = 0;

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
            dtpFechaIni.Value = DateTime.Now.AddDays(-7);
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
                            MessageBox.Show("No se puede sobreescribir el archivo. Asegurate de que no esté abierto en otro programa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                                
                                Paragraph titulo = new Paragraph("Reporte de Bitácora de Eventos\n\n");
                                titulo.Alignment = Element.ALIGN_CENTER;
                                pdfDoc.Add(titulo);

                                pdfDoc.Add(tablaPdf);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("El reporte PDF se generó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay eventos en la grilla para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
