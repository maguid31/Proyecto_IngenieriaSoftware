using System;
using BLL;
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
    public partial class frmReparacionDV : Form
    {

        public frmReparacionDV(List<TablaInconsistente> tablasConError)
        {
            _tablasConError = tablasConError;
            InitializeComponent();
            ConstruirInterfaz();
        }

        private void frmReparacionDV_Load(object sender, EventArgs e)
        {

        }

        private readonly DigitoVerificadorBLL_65RD _dvBLL = new DigitoVerificadorBLL_65RD();
        private readonly List<TablaInconsistente> _tablasConError;

        // Resultado que le devolvemos al login para saber qué pasó
        public enum AccionReparacion { Ninguna, Recalculado, Restaurado, Salio }
        public AccionReparacion AccionElegida { get; private set; } = AccionReparacion.Ninguna;



        private void ConstruirInterfaz()
        {
            this.Text = "Inconsistencia detectada — Asistente de Reparación";
            this.Size = new Size(620, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            var lblTitulo = new Label
            {
                Text = "⚠  INCONSISTENCIA EN LA BASE DE DATOS",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.FromArgb(123, 97, 255),
                Location = new Point(20, 20),
                Size = new Size(570, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblExplicacion = new Label
            {
                Text = "Se detectaron diferencias entre los datos actuales de la base de datos\n" +
                       "y los dígitos verificadores guardados. Esto puede indicar una modificación\n" +
                       "directa a la base de datos fuera del sistema.",
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(20, 65),
                Size = new Size(570, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblTablas = new Label
            {
                Text = "Tablas con inconsistencias detectadas:",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(20, 140),
                Size = new Size(300, 20)
            };

            var lstTablas = new ListBox
            {
                Location = new Point(20, 165),
                Size = new Size(570, 100),
                Font = new Font("Consolas", 9f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(235, 238, 245)
            };

            foreach (var t in _tablasConError)
                lstTablas.Items.Add($"    ✗  {t.NombreTabla}");

            var lblElegir = new Label
            {
                Text = "Elija una acción para continuar:",
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(20, 285),
                Size = new Size(300, 22)
            };

            var btnRecalcular = new Button
            {
                Text = "1.  RECALCULAR el Dígito Verificador",
                Location = new Point(20, 315),
                Size = new Size(570, 42),
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                BackColor = Color.FromArgb(123, 97, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRecalcular.FlatAppearance.BorderSize = 0;
            btnRecalcular.Click += BtnRecalcular_Click;

            var btnRestore = new Button
            {
                Text = "2.  RESTORE — Restaurar desde Backup",
                Location = new Point(20, 365),
                Size = new Size(570, 42),
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 215, 255),
                ForeColor = Color.FromArgb(123, 97, 255),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRestore.FlatAppearance.BorderSize = 1;
            btnRestore.FlatAppearance.BorderColor = Color.FromArgb(123, 97, 255);
            btnRestore.Click += BtnRestore_Click;

            var btnSalir = new Button
            {
                Text = "3.  SALIR — No resolver ahora",
                Location = new Point(20, 415),
                Size = new Size(570, 35),
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(150, 150, 150),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSalir.FlatAppearance.BorderSize = 1;
            btnSalir.FlatAppearance.BorderColor = Color.LightGray;
            btnSalir.Click += BtnSalir_Click;

            this.Controls.AddRange(new Control[]
            {
        lblTitulo, lblExplicacion, lblTablas,
        lstTablas, lblElegir,
        btnRecalcular, btnRestore, btnSalir
            });
        }

        // ─────────────────────────────────────────────────────────────
        //  BOTÓN 1 — RECALCULAR
        //  El profesor: "se fuerza la GENERACIÓN del DV tal como ocurre
        //  cada vez que se ejecuta una persistencia. Después se limpia
        //  la pantalla y se vuelve al login para hacer un nuevo acceso.
        //  No se resuelve la inconsistencia, solo se acepta, se acoge,
        //  se normaliza."
        // ─────────────────────────────────────────────────────────────
        private void BtnRecalcular_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(
                "¿Confirmar RECALCULAR?\n\n" +
                "Esto igualará el Dígito Verificador con el estado actual de la base de datos.\n" +
                "La inconsistencia física en los datos NO se resuelve, solo se normaliza el DV.\n\n" +
                "Deberá reiniciar el sistema y loguearse nuevamente.",
                "Confirmar Recalcular",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _dvBLL.Recalcular();

                AccionElegida = AccionReparacion.Recalculado;

                MessageBox.Show(
                    "Dígito Verificador recalculado correctamente.\n\n" +
                    "El sistema se cerrará. Por favor, vuelva a iniciar sesión.",
                    "Recalculado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al recalcular: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  BOTÓN 2 — RESTORE
        //  El profesor: "mostrar el GUI asociado al restore de algún
        //  backup. Debería elegirse el más reciente para perder la menor
        //  cantidad de datos. Después se limpia la pantalla y se vuelve
        //  al login."
        //  El restore real es a nivel SQL Server (fuera del sistema),
        //  nosotros mostramos las instrucciones y recalculamos el DV
        //  una vez que el admin indica que ya restauró.
        // ─────────────────────────────────────────────────────────────
        private void BtnRestore_Click(object sender, EventArgs e)
        {
            using (var frmRestore = new frmGestionRespaldo())
            {
                frmRestore.ShowDialog();
            }
            AccionElegida = AccionReparacion.Restaurado;
            this.Close();
        }

        // ─────────────────────────────────────────────────────────────
        //  BOTÓN 3 — SALIR
        //  El profesor: "se limpia la pantalla, se sale del sistema,
        //  no se resuelve el problema de la inconsistencia."
        // ─────────────────────────────────────────────────────────────
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            AccionElegida = AccionReparacion.Salio;
            this.Close();
        }

    }
}
