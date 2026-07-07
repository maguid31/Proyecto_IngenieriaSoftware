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
        private readonly string _connectionString = @"Data Source=.;Initial Catalog=proyecto_ingenieria;Integrated Security=True";
        private readonly DigitoVerificadorBLL_65RD _dvBLL = new DigitoVerificadorBLL_65RD();

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
                // Usamos una conexión a master para poder ejecutar el BACKUP
                string connMaster = _connectionString.Replace(
                    "Initial Catalog=proyecto_ingenieria",
                    "Initial Catalog=master");

                string query = $@"
                    BACKUP DATABASE [proyecto_ingenieria] 
                    TO DISK = N'{txtRutaBackup.Text}' 
                    WITH FORMAT, MEDIANAME = 'ProyectoBackup', 
                    NAME = 'Backup completo proyecto_ingenieria'";

                using (var con = new SqlConnection(connMaster))
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.CommandTimeout = 300; // 5 minutos máximo
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // Registrar en bitácora
                int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;
                new BitacoraBLL_65RD().RegistrarEvento(
                    idUsuario, "Respaldo", "Backup", 3,
                    $"Se realizó un backup en: {txtRutaBackup.Text}");

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

        // ─────────────────────────────────────────────────────────────
        //  RESTORE — El admin elige un .bak y el sistema restaura la BD
        // ─────────────────────────────────────────────────────────────
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
                // Conectamos a master para poder restaurar proyecto_ingenieria
                string connMaster = _connectionString.Replace(
                    "Initial Catalog=proyecto_ingenieria",
                    "Initial Catalog=master");

                // Primero ponemos la BD en modo single user para forzar desconexión
                string querySingleUser = @"
                    ALTER DATABASE [proyecto_ingenieria] 
                    SET SINGLE_USER WITH ROLLBACK IMMEDIATE";

                string queryRestore = $@"
                    RESTORE DATABASE [proyecto_ingenieria] 
                    FROM DISK = N'{txtRutaRestore.Text}' 
                    WITH REPLACE, RECOVERY";

                string queryMultiUser = @"
                    ALTER DATABASE [proyecto_ingenieria] 
                    SET MULTI_USER";

                using (var con = new SqlConnection(connMaster))
                {
                    con.Open();
                    cmd_ejecutar(querySingleUser, con);
                    cmd_ejecutar(queryRestore, con);
                    cmd_ejecutar(queryMultiUser, con);
                }

                // Después del restore, recalculamos el DV con los datos restaurados
                _dvBLL.Recalcular();

                // Registrar en bitácora
                int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;
                new BitacoraBLL_65RD().RegistrarEvento(
                    idUsuario, "Respaldo", "Restore", 4,
                    $"Se realizó un restore desde: {txtRutaRestore.Text}");

                MessageBox.Show(
                    "✔ Restore realizado exitosamente.\n\n" +
                    "El Dígito Verificador fue recalculado.\n" +
                    "El sistema se cerrará para que vuelva a iniciar sesión.",
                    "Restore exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cerrar sesión y volver al login
                SessionManager_65RD.Instancia.CerrarSesion();
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al realizar el restore:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmd_ejecutar(string query, SqlConnection con)
        {
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
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
            this.Text = "Gestión de Respaldo";
        }

        
    }
}

