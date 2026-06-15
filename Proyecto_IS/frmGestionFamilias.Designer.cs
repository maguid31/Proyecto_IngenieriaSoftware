namespace Proyecto_IS
{
    partial class frmGestionFamilias
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGuardarFamilia = new System.Windows.Forms.Button();
            this.btnLimpiarFamilia = new System.Windows.Forms.Button();
            this.txtDescFamilia = new System.Windows.Forms.TextBox();
            this.txtNombreFamilia = new System.Windows.Forms.TextBox();
            this.tvFamiliaEdicion = new System.Windows.Forms.TreeView();
            this.lbFuentePermisos = new System.Windows.Forms.ListBox();
            this.txtBuscarFamilia = new System.Windows.Forms.TextBox();
            this.lbFamilias = new System.Windows.Forms.ListBox();
            this.lblDescripcionFamilia = new System.Windows.Forms.Label();
            this.btnEliminarFamilia = new System.Windows.Forms.Button();
            this.btnAgregarFamilia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(968, 198);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 32);
            this.label1.TabIndex = 55;
            this.label1.Text = "Descripción";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(870, 148);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 32);
            this.label2.TabIndex = 54;
            this.label2.Text = "Nombre de la Familia";
            // 
            // btnGuardarFamilia
            // 
            this.btnGuardarFamilia.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnGuardarFamilia.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarFamilia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGuardarFamilia.Location = new System.Drawing.Point(941, 447);
            this.btnGuardarFamilia.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardarFamilia.Name = "btnGuardarFamilia";
            this.btnGuardarFamilia.Size = new System.Drawing.Size(296, 46);
            this.btnGuardarFamilia.TabIndex = 52;
            this.btnGuardarFamilia.Text = "GUARDAR FAMILIA";
            this.btnGuardarFamilia.UseVisualStyleBackColor = false;
            this.btnGuardarFamilia.Click += new System.EventHandler(this.btnGuardarFamilia_Click);
            // 
            // btnLimpiarFamilia
            // 
            this.btnLimpiarFamilia.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLimpiarFamilia.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarFamilia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLimpiarFamilia.Location = new System.Drawing.Point(941, 393);
            this.btnLimpiarFamilia.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiarFamilia.Name = "btnLimpiarFamilia";
            this.btnLimpiarFamilia.Size = new System.Drawing.Size(296, 46);
            this.btnLimpiarFamilia.TabIndex = 51;
            this.btnLimpiarFamilia.Text = "LIMPIAR FAMILIA";
            this.btnLimpiarFamilia.UseVisualStyleBackColor = false;
            this.btnLimpiarFamilia.Click += new System.EventHandler(this.btnLimpiarFamilia_Click);
            // 
            // txtDescFamilia
            // 
            this.txtDescFamilia.Location = new System.Drawing.Point(1150, 198);
            this.txtDescFamilia.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescFamilia.Name = "txtDescFamilia";
            this.txtDescFamilia.Size = new System.Drawing.Size(213, 22);
            this.txtDescFamilia.TabIndex = 48;
            // 
            // txtNombreFamilia
            // 
            this.txtNombreFamilia.Location = new System.Drawing.Point(1150, 148);
            this.txtNombreFamilia.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombreFamilia.Name = "txtNombreFamilia";
            this.txtNombreFamilia.Size = new System.Drawing.Size(213, 22);
            this.txtNombreFamilia.TabIndex = 47;
            // 
            // tvFamiliaEdicion
            // 
            this.tvFamiliaEdicion.Location = new System.Drawing.Point(589, 129);
            this.tvFamiliaEdicion.Margin = new System.Windows.Forms.Padding(4);
            this.tvFamiliaEdicion.Name = "tvFamiliaEdicion";
            this.tvFamiliaEdicion.Size = new System.Drawing.Size(221, 403);
            this.tvFamiliaEdicion.TabIndex = 46;
            // 
            // lbFuentePermisos
            // 
            this.lbFuentePermisos.FormattingEnabled = true;
            this.lbFuentePermisos.ItemHeight = 16;
            this.lbFuentePermisos.Location = new System.Drawing.Point(4, 128);
            this.lbFuentePermisos.Margin = new System.Windows.Forms.Padding(4);
            this.lbFuentePermisos.Name = "lbFuentePermisos";
            this.lbFuentePermisos.Size = new System.Drawing.Size(260, 404);
            this.lbFuentePermisos.TabIndex = 45;
            // 
            // txtBuscarFamilia
            // 
            this.txtBuscarFamilia.Location = new System.Drawing.Point(337, 101);
            this.txtBuscarFamilia.Name = "txtBuscarFamilia";
            this.txtBuscarFamilia.Size = new System.Drawing.Size(100, 22);
            this.txtBuscarFamilia.TabIndex = 56;
            this.txtBuscarFamilia.TextChanged += new System.EventHandler(this.txtBuscarFamilia_TextChanged);
            // 
            // lbFamilias
            // 
            this.lbFamilias.FormattingEnabled = true;
            this.lbFamilias.ItemHeight = 16;
            this.lbFamilias.Location = new System.Drawing.Point(337, 129);
            this.lbFamilias.Name = "lbFamilias";
            this.lbFamilias.Size = new System.Drawing.Size(214, 404);
            this.lbFamilias.TabIndex = 57;
            this.lbFamilias.SelectedIndexChanged += new System.EventHandler(this.lbFamilias_SelectedIndexChanged);
            // 
            // lblDescripcionFamilia
            // 
            this.lblDescripcionFamilia.AutoSize = true;
            this.lblDescripcionFamilia.Location = new System.Drawing.Point(472, 625);
            this.lblDescripcionFamilia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcionFamilia.Name = "lblDescripcionFamilia";
            this.lblDescripcionFamilia.Size = new System.Drawing.Size(44, 16);
            this.lblDescripcionFamilia.TabIndex = 58;
            this.lblDescripcionFamilia.Text = "label1";
            // 
            // btnEliminarFamilia
            // 
            this.btnEliminarFamilia.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnEliminarFamilia.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarFamilia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminarFamilia.Location = new System.Drawing.Point(941, 339);
            this.btnEliminarFamilia.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminarFamilia.Name = "btnEliminarFamilia";
            this.btnEliminarFamilia.Size = new System.Drawing.Size(296, 46);
            this.btnEliminarFamilia.TabIndex = 59;
            this.btnEliminarFamilia.Text = "ELIMINAR FAMILIA";
            this.btnEliminarFamilia.UseVisualStyleBackColor = false;
            this.btnEliminarFamilia.Click += new System.EventHandler(this.btnEliminarFamilia_Click);
            // 
            // btnAgregarFamilia
            // 
            this.btnAgregarFamilia.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnAgregarFamilia.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarFamilia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregarFamilia.Location = new System.Drawing.Point(941, 285);
            this.btnAgregarFamilia.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregarFamilia.Name = "btnAgregarFamilia";
            this.btnAgregarFamilia.Size = new System.Drawing.Size(296, 46);
            this.btnAgregarFamilia.TabIndex = 60;
            this.btnAgregarFamilia.Text = "AGREGAR FAMILIA";
            this.btnAgregarFamilia.UseVisualStyleBackColor = false;
            this.btnAgregarFamilia.Click += new System.EventHandler(this.btnAgregarFamilia_Click);
            // 
            // frmGestionFamilias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 665);
            this.Controls.Add(this.btnAgregarFamilia);
            this.Controls.Add(this.btnEliminarFamilia);
            this.Controls.Add(this.lblDescripcionFamilia);
            this.Controls.Add(this.lbFamilias);
            this.Controls.Add(this.txtBuscarFamilia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGuardarFamilia);
            this.Controls.Add(this.btnLimpiarFamilia);
            this.Controls.Add(this.txtDescFamilia);
            this.Controls.Add(this.txtNombreFamilia);
            this.Controls.Add(this.tvFamiliaEdicion);
            this.Controls.Add(this.lbFuentePermisos);
            this.Name = "frmGestionFamilias";
            this.Text = "frmGestionFamilias";
            this.Load += new System.EventHandler(this.frmGestionFamilias_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGuardarFamilia;
        private System.Windows.Forms.Button btnLimpiarFamilia;
        private System.Windows.Forms.TextBox txtDescFamilia;
        private System.Windows.Forms.TextBox txtNombreFamilia;
        private System.Windows.Forms.TreeView tvFamiliaEdicion;
        private System.Windows.Forms.ListBox lbFuentePermisos;
        private System.Windows.Forms.TextBox txtBuscarFamilia;
        private System.Windows.Forms.ListBox lbFamilias;
        private System.Windows.Forms.Label lblDescripcionFamilia;
        private System.Windows.Forms.Button btnEliminarFamilia;
        private System.Windows.Forms.Button btnAgregarFamilia;
    }
}