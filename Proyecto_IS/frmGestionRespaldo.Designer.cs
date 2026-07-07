namespace Proyecto_IS
{
    partial class frmGestionRespaldo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtRutaBackup = new System.Windows.Forms.TextBox();
            this.txtRutaRestore = new System.Windows.Forms.TextBox();
            this.btnSeleccionarRutaBackup = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnSeleccionarArchivoRestore = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblBackup = new System.Windows.Forms.Label();
            this.lblRestore = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.txtRutaBackup.Location = new System.Drawing.Point(20, 100);
            this.txtRutaBackup.Size = new System.Drawing.Size(370, 25);
            this.txtRutaBackup.ReadOnly = true;
            this.txtRutaBackup.BackColor = System.Drawing.Color.White;
            this.txtRutaBackup.Name = "txtRutaBackup";

            this.txtRutaRestore.Location = new System.Drawing.Point(20, 230);
            this.txtRutaRestore.Size = new System.Drawing.Size(370, 25);
            this.txtRutaRestore.ReadOnly = true;
            this.txtRutaRestore.BackColor = System.Drawing.Color.White;
            this.txtRutaRestore.Name = "txtRutaRestore";

            this.btnSeleccionarRutaBackup.Text = "📁";
            this.btnSeleccionarRutaBackup.Location = new System.Drawing.Point(400, 98);
            this.btnSeleccionarRutaBackup.Size = new System.Drawing.Size(80, 28);
            this.btnSeleccionarRutaBackup.BackColor = System.Drawing.Color.FromArgb(123, 97, 255);
            this.btnSeleccionarRutaBackup.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarRutaBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarRutaBackup.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarRutaBackup.Name = "btnSeleccionarRutaBackup";
            this.btnSeleccionarRutaBackup.Click += new System.EventHandler(this.btnSeleccionarRutaBackup_Click);

            this.btnBackup.Text = "Back Up";
            this.btnBackup.Location = new System.Drawing.Point(20, 135);
            this.btnBackup.Size = new System.Drawing.Size(120, 32);
            this.btnBackup.BackColor = System.Drawing.Color.FromArgb(123, 97, 255);
            this.btnBackup.ForeColor = System.Drawing.Color.White;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.FlatAppearance.BorderSize = 0;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);

            this.btnSeleccionarArchivoRestore.Text = "📁";
            this.btnSeleccionarArchivoRestore.Location = new System.Drawing.Point(400, 228);
            this.btnSeleccionarArchivoRestore.Size = new System.Drawing.Size(80, 28);
            this.btnSeleccionarArchivoRestore.BackColor = System.Drawing.Color.FromArgb(123, 97, 255);
            this.btnSeleccionarArchivoRestore.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarArchivoRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarArchivoRestore.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarArchivoRestore.Name = "btnSeleccionarArchivoRestore";
            this.btnSeleccionarArchivoRestore.Click += new System.EventHandler(this.btnSeleccionarArchivoRestore_Click);

            this.btnRestore.Text = "Restore";
            this.btnRestore.Location = new System.Drawing.Point(20, 265);
            this.btnRestore.Size = new System.Drawing.Size(120, 32);
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(123, 97, 255);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.FlatAppearance.BorderSize = 0;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);

            this.btnVolver.Text = "Volver al menú";
            this.btnVolver.Location = new System.Drawing.Point(330, 300);
            this.btnVolver.Size = new System.Drawing.Size(150, 32);
            this.btnVolver.BackColor = System.Drawing.Color.FromArgb(123, 97, 255);
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);

            this.lblTitulo.Text = "Respaldo";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16f, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(123, 97, 255);
            this.lblTitulo.Location = new System.Drawing.Point(180, 20);
            this.lblTitulo.Size = new System.Drawing.Size(200, 35);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitulo.Name = "lblTitulo";

            this.lblBackup.Text = "Ruta de Backup:";
            this.lblBackup.Location = new System.Drawing.Point(20, 75);
            this.lblBackup.Size = new System.Drawing.Size(120, 20);
            this.lblBackup.Name = "lblBackup";

            this.lblRestore.Text = "Archivo de Restore (.bak):";
            this.lblRestore.Location = new System.Drawing.Point(20, 205);
            this.lblRestore.Size = new System.Drawing.Size(200, 20);
            this.lblRestore.Name = "lblRestore";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 360);
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmGestionRespaldo";
            this.Text = "Gestión de Respaldo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGestionRespaldo_FormClosed);
            this.Load += new System.EventHandler(this.frmGestionRespaldo_Load);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitulo,
                this.lblBackup, this.txtRutaBackup, this.btnSeleccionarRutaBackup, this.btnBackup,
                this.lblRestore, this.txtRutaRestore, this.btnSeleccionarArchivoRestore, this.btnRestore,
                this.btnVolver

            });

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox txtRutaBackup;
        private System.Windows.Forms.TextBox txtRutaRestore;
        private System.Windows.Forms.Button btnSeleccionarRutaBackup;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnSeleccionarArchivoRestore;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBackup;
        private System.Windows.Forms.Label lblRestore;
    
    }
}