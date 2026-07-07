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
            this.lblRespaldo = new System.Windows.Forms.Label();
            this.lblBackup = new System.Windows.Forms.Label();
            this.lblRestore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtRutaBackup
            // 
            this.txtRutaBackup.BackColor = System.Drawing.Color.White;
            this.txtRutaBackup.Location = new System.Drawing.Point(27, 123);
            this.txtRutaBackup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRutaBackup.Name = "txtRutaBackup";
            this.txtRutaBackup.ReadOnly = true;
            this.txtRutaBackup.Size = new System.Drawing.Size(492, 22);
            this.txtRutaBackup.TabIndex = 2;
            // 
            // txtRutaRestore
            // 
            this.txtRutaRestore.BackColor = System.Drawing.Color.White;
            this.txtRutaRestore.Location = new System.Drawing.Point(27, 283);
            this.txtRutaRestore.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRutaRestore.Name = "txtRutaRestore";
            this.txtRutaRestore.ReadOnly = true;
            this.txtRutaRestore.Size = new System.Drawing.Size(492, 22);
            this.txtRutaRestore.TabIndex = 6;
            // 
            // btnSeleccionarRutaBackup
            // 
            this.btnSeleccionarRutaBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(97)))), ((int)(((byte)(255)))));
            this.btnSeleccionarRutaBackup.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarRutaBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarRutaBackup.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarRutaBackup.Location = new System.Drawing.Point(533, 121);
            this.btnSeleccionarRutaBackup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSeleccionarRutaBackup.Name = "btnSeleccionarRutaBackup";
            this.btnSeleccionarRutaBackup.Size = new System.Drawing.Size(107, 34);
            this.btnSeleccionarRutaBackup.TabIndex = 3;
            this.btnSeleccionarRutaBackup.Text = "📁";
            this.btnSeleccionarRutaBackup.UseVisualStyleBackColor = false;
            this.btnSeleccionarRutaBackup.Click += new System.EventHandler(this.btnSeleccionarRutaBackup_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(97)))), ((int)(((byte)(255)))));
            this.btnBackup.FlatAppearance.BorderSize = 0;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackup.ForeColor = System.Drawing.Color.White;
            this.btnBackup.Location = new System.Drawing.Point(27, 166);
            this.btnBackup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(160, 39);
            this.btnBackup.TabIndex = 4;
            this.btnBackup.Text = "Back Up";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnSeleccionarArchivoRestore
            // 
            this.btnSeleccionarArchivoRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(97)))), ((int)(((byte)(255)))));
            this.btnSeleccionarArchivoRestore.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarArchivoRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarArchivoRestore.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarArchivoRestore.Location = new System.Drawing.Point(533, 281);
            this.btnSeleccionarArchivoRestore.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSeleccionarArchivoRestore.Name = "btnSeleccionarArchivoRestore";
            this.btnSeleccionarArchivoRestore.Size = new System.Drawing.Size(107, 34);
            this.btnSeleccionarArchivoRestore.TabIndex = 7;
            this.btnSeleccionarArchivoRestore.Text = "📁";
            this.btnSeleccionarArchivoRestore.UseVisualStyleBackColor = false;
            this.btnSeleccionarArchivoRestore.Click += new System.EventHandler(this.btnSeleccionarArchivoRestore_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(97)))), ((int)(((byte)(255)))));
            this.btnRestore.FlatAppearance.BorderSize = 0;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.Location = new System.Drawing.Point(27, 326);
            this.btnRestore.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(160, 39);
            this.btnRestore.TabIndex = 8;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(97)))), ((int)(((byte)(255)))));
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Location = new System.Drawing.Point(440, 369);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(200, 39);
            this.btnVolver.TabIndex = 9;
            this.btnVolver.Text = "Volver al menú";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lblRespaldo
            // 
            this.lblRespaldo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRespaldo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(97)))), ((int)(((byte)(255)))));
            this.lblRespaldo.Location = new System.Drawing.Point(240, 25);
            this.lblRespaldo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRespaldo.Name = "lblRespaldo";
            this.lblRespaldo.Size = new System.Drawing.Size(267, 43);
            this.lblRespaldo.TabIndex = 0;
            this.lblRespaldo.Text = "Respaldo";
            this.lblRespaldo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBackup
            // 
            this.lblBackup.Location = new System.Drawing.Point(27, 92);
            this.lblBackup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBackup.Name = "lblBackup";
            this.lblBackup.Size = new System.Drawing.Size(160, 25);
            this.lblBackup.TabIndex = 1;
            this.lblBackup.Text = "Ruta de Backup:";
            // 
            // lblRestore
            // 
            this.lblRestore.Location = new System.Drawing.Point(27, 252);
            this.lblRestore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRestore.Name = "lblRestore";
            this.lblRestore.Size = new System.Drawing.Size(267, 25);
            this.lblRestore.TabIndex = 5;
            this.lblRestore.Text = "Archivo de Restore (.bak):";
            // 
            // frmGestionRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(693, 443);
            this.Controls.Add(this.lblRespaldo);
            this.Controls.Add(this.lblBackup);
            this.Controls.Add(this.txtRutaBackup);
            this.Controls.Add(this.btnSeleccionarRutaBackup);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.lblRestore);
            this.Controls.Add(this.txtRutaRestore);
            this.Controls.Add(this.btnSeleccionarArchivoRestore);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnVolver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "frmGestionRespaldo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Respaldo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGestionRespaldo_FormClosed);
            this.Load += new System.EventHandler(this.frmGestionRespaldo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txtRutaBackup;
        private System.Windows.Forms.TextBox txtRutaRestore;
        private System.Windows.Forms.Button btnSeleccionarRutaBackup;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnSeleccionarArchivoRestore;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblRespaldo;
        private System.Windows.Forms.Label lblBackup;
        private System.Windows.Forms.Label lblRestore;
    
    }
}