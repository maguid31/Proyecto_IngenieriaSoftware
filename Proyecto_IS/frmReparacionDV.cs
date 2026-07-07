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



        // ─────────────────────────────────────────────────────────────
        //  Construye la UI programáticamente (sin diseñador .designer.cs)
        //  así no necesitás crear el form en el diseñador de Visual Studio
        // ─────────────────────────────────────────────────────────────
        private void ConstruirInterfaz()
        {
            this.Text = "⚠ Inconsistencia detectada — Asistente de Reparación";
            this.Size = new Size(620, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // --- Título ---
            var lblTitulo = new Label
            {
                Text = "⚠  INCONSISTENCIA EN LA BASE DE DATOS",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 40, 40),
                Location = new Point(20, 20),
                Size = new Size(570, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // --- Explicación ---
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

            // --- Detalle de tablas inconsistentes ---
            var lblTablas = new Label
            {
                Text = "Tablas con inconsistencias detectadas:",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(20, 140),
                Size = new Size(300, 20)
            };

            var lstTablas = new ListBox
            {
                Location = new Point(20, 165),
                Size = new Size(570, 100),
                Font = new Font("Consolas", 9f)
            };

            // Mostramos cada tabla inconsistente con su detalle
            foreach (var t in _tablasConError)
            {
                lstTablas.Items.Add($"  ✗  {t.NombreTabla} — {t.DVFinalCalculado}");
            }

            // --- Separador ---
            var lblElegir = new Label
            {
                Text = "Elija una acción para continuar:",
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Location = new Point(20, 285),
                Size = new Size(300, 22)
            };

            // --- Botón RECALCULAR ---
            var btnRecalcular = new Button
            {
                Text = "1.  RECALCULAR el Dígito Verificador",
                Location = new Point(20, 315),
                Size = new Size(570, 42),
                Font = new Font("Segoe UI", 10f),
                BackColor = Color.FromArgb(255, 200, 0),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRecalcular.FlatAppearance.BorderSize = 0;
            btnRecalcular.Click += BtnRecalcular_Click;

            // Tooltip explicativo
            var tooltip = new ToolTip();
            tooltip.SetToolTip(btnRecalcular,
                "Acepta el estado actual de la BD como válido.\n" +
                "El error físico sigue existiendo pero el sistema podrá arrancar.\n" +
                "Deberá volver a loguearse.");

            // --- Botón RESTORE ---
            var btnRestore = new Button
            {
                Text = "2.  RESTORE — Restaurar desde Backup",
                Location = new Point(20, 365),
                Size = new Size(570, 42),
                Font = new Font("Segoe UI", 10f),
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRestore.FlatAppearance.BorderSize = 0;
            btnRestore.Click += BtnRestore_Click;

            tooltip.SetToolTip(btnRestore,
                "Muestra las instrucciones para restaurar el último backup.\n" +
                "ATENCIÓN: se perderán los datos desde el último backup hasta ahora.");

            // --- Botón SALIR ---
            var btnSalir = new Button
            {
                Text = "3.  SALIR — No resolver ahora",
                Location = new Point(20, 415),
                Size = new Size(570, 35),
                Font = new Font("Segoe UI", 9f),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSalir.FlatAppearance.BorderSize = 0;
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
                    "✔ Dígito Verificador recalculado correctamente.\n\n" +
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
            // Mostramos el formulario de instrucciones de Restore
            using (var frmRestore = new frmRestoreInstrucciones_65RD(_dvBLL))
            {
                frmRestore.ShowDialog();
                if (frmRestore.RestoreConfirmado)
                {
                    AccionElegida = AccionReparacion.Restaurado;
                    this.Close();
                }
            }
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

        // ─────────────────────────────────────────────────────────────
        //  Necesario para que el form funcione sin el diseñador visual
        // ─────────────────────────────────────────────────────────────
    }

    // ─────────────────────────────────────────────────────────────────
    //  FORMULARIO AUXILIAR — Instrucciones de Restore
    //  Le muestra al admin los pasos para restaurar el backup en SQL Server
    //  y luego recalcula el DV para que el sistema quede consistente
    // ─────────────────────────────────────────────────────────────────
    public partial class frmRestoreInstrucciones_65RD : Form
    {
        private readonly DigitoVerificadorBLL_65RD _dvBLL;
        public bool RestoreConfirmado { get; private set; } = false;

        public frmRestoreInstrucciones_65RD(DigitoVerificadorBLL_65RD dvBLL)
        {
            _dvBLL = dvBLL;
            InicializarUI();
        }

        private void InicializarUI()
        {
            this.Text = "Instrucciones de Restore";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.White;

            var lblTitulo = new Label
            {
                Text = "RESTORE — Restauración desde Backup",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 130, 180),
                Location = new Point(20, 15),
                Size = new Size(550, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var txtInstrucciones = new RichTextBox
            {
                Location = new Point(20, 55),
                Size = new Size(550, 270),
                ReadOnly = true,
                BackColor = Color.FromArgb(245, 245, 245),
                Font = new Font("Segoe UI", 9.5f),
                Text =
                    "Siga estos pasos para restaurar el backup en SQL Server Management Studio:\n\n" +
                    "1. Abra SQL Server Management Studio (SSMS).\n\n" +
                    "2. Haga clic derecho sobre la base de datos 'proyecto_ingenieria'\n" +
                    "   y elija: Tareas → Restaurar → Base de Datos.\n\n" +
                    "3. Seleccione el backup más reciente disponible.\n" +
                    "   RECOMENDACIÓN: elija el más reciente para minimizar la pérdida de datos.\n\n" +
                    "4. En 'Opciones', active 'Sobrescribir la base de datos existente'.\n\n" +
                    "5. Haga clic en Aceptar y espere que termine el restore.\n\n" +
                    "6. Una vez completado, vuelva aquí y haga clic en\n" +
                    "   'Ya restauré el backup' para que el sistema recalcule el DV\n" +
                    "   y pueda volver a iniciar sesión.\n\n" +
                    "⚠ ADVERTENCIA: Los datos ingresados desde el último backup se perderán."
            };

            var btnConfirmarRestore = new Button
            {
                Text = "✔  Ya restauré el backup — Recalcular DV y continuar",
                Location = new Point(20, 340),
                Size = new Size(550, 45),
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirmarRestore.FlatAppearance.BorderSize = 0;
            btnConfirmarRestore.Click += (s, e) =>
            {
                try
                {
                    _dvBLL.Recalcular(); // recalcula el DV sobre los datos restaurados
                    RestoreConfirmado = true;
                    MessageBox.Show(
                        "✔ DV recalculado sobre los datos restaurados.\n" +
                        "El sistema se cerrará. Vuelva a iniciar sesión.",
                        "Restore completado",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al recalcular tras restore: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(20, 393),
                Size = new Size(550, 30),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[]
            {
                lblTitulo, txtInstrucciones, btnConfirmarRestore, btnCancelar
            });
        }

    }
}
