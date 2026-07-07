using BLL;
using Servicios;
using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Proyecto_IS
{
    public partial class frmGestionRespaldo : Form, IidiomaObserver
    {
        private readonly BackUpRestoreBLL_65RD _backUpRestoreBLL = new BackUpRestoreBLL_65RD();

        public frmGestionRespaldo()
        {
            InitializeComponent();
        }

        private void frmGestionRespaldo_Load(object sender, EventArgs e)
        {
            IdiomaManager.GetInstance().RegisterObserver(this);
            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);
        }


        private void btnSeleccionarRutaBackup_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Guardar Backup";
                dialog.Filter = "Archivo de Backup (*.bak)|*.bak";
                dialog.FileName = $"proyecto_ingenieria_{DateTime.Now:yyyyMMdd_HHmmss}.bak";

                if (dialog.ShowDialog() == DialogResult.OK)
                    txtRutaBackup.Text = dialog.FileName;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaBackup.Text))
            {
                MessageBox.Show("Por favor, seleccioná una ruta para guardar el backup.",
                    "Ruta requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;
                _backUpRestoreBLL.RealizarBackup(txtRutaBackup.Text, idUsuario);

                MessageBox.Show(
                    $"✔ Backup realizado exitosamente.\n\nArchivo guardado en:\n{txtRutaBackup.Text}",
                    "Backup exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al realizar el backup:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnSeleccionarArchivoRestore_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Seleccionar archivo de Backup";
                dialog.Filter = "Archivo de Backup (*.bak)|*.bak";

                if (dialog.ShowDialog() == DialogResult.OK)
                    txtRutaRestore.Text = dialog.FileName;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaRestore.Text))
            {
                MessageBox.Show("Por favor, seleccioná el archivo .bak para restaurar.",
                    "Archivo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(txtRutaRestore.Text))
            {
                MessageBox.Show("El archivo seleccionado no existe.",
                    "Archivo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmacion = MessageBox.Show(
                 "⚠ ATENCIÓN: El Restore reemplazará TODA la base de datos actual.\n\n" +
                 "Se perderán todos los datos ingresados desde el último backup.\n\n" +
                 "¿Confirmar el Restore?",
                 "Confirmar Restore",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;
                _backUpRestoreBLL.RealizarRestore(txtRutaRestore.Text, idUsuario);

                MessageBox.Show(
                    "✔ Restore realizado exitosamente.\n\n" +
                    "El Dígito Verificador fue recalculado.\n" +
                    "El sistema se cerrará para que vuelva a iniciar sesión.",
                    "Restore exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SessionManager_65RD.Instancia.CerrarSesion();
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al realizar el restore:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmGestionRespaldo_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().RemoveObserver(this);
        }

        public void UpdateIdioma(string idioma)
        {
          
            this.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloVentana");

            if (lblRespaldo != null) lblRespaldo.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblRespaldo");
            if (lblBackup != null) lblBackup.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblBackup");
            if (lblRestore != null) lblRestore.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblRestore");

            if (btnBackup != null) btnBackup.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnBackup");
            if (btnRestore != null) btnRestore.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnRestore");
            if (btnVolver != null) btnVolver.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnVolver");
        }
    }

        
    
}

