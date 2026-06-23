namespace Proyecto_IS
{
    partial class frmGestionPerfiles
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
            this.btnLimpiarPerfil = new System.Windows.Forms.Button();
            this.lbPerfiles = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtBuscarPerfil = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGuardarPerfil = new System.Windows.Forms.Button();
            this.btnQuitarPermiso = new System.Windows.Forms.Button();
            this.btnAsignarPermiso = new System.Windows.Forms.Button();
            this.btnEliminarPerfil = new System.Windows.Forms.Button();
            this.txtNuevoPerfil = new System.Windows.Forms.TextBox();
            this.tvPermisosAsignados = new System.Windows.Forms.TreeView();
            this.lbPermisosDisponiblesTab2 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnLimpiarPerfil
            // 
            this.btnLimpiarPerfil.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLimpiarPerfil.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarPerfil.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarPerfil.Location = new System.Drawing.Point(1172, 387);
            this.btnLimpiarPerfil.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiarPerfil.Name = "btnLimpiarPerfil";
            this.btnLimpiarPerfil.Size = new System.Drawing.Size(229, 43);
            this.btnLimpiarPerfil.TabIndex = 49;
            this.btnLimpiarPerfil.Text = " LIMPIAR PERFIL";
            this.btnLimpiarPerfil.UseVisualStyleBackColor = false;
            this.btnLimpiarPerfil.Click += new System.EventHandler(this.btnLimpiarPerfil_Click);
            // 
            // lbPerfiles
            // 
            this.lbPerfiles.FormattingEnabled = true;
            this.lbPerfiles.ItemHeight = 16;
            this.lbPerfiles.Location = new System.Drawing.Point(372, 159);
            this.lbPerfiles.Name = "lbPerfiles";
            this.lbPerfiles.Size = new System.Drawing.Size(241, 404);
            this.lbPerfiles.TabIndex = 47;
            this.lbPerfiles.SelectedIndexChanged += new System.EventHandler(this.lbPerfiles_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(1004, 174);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 32);
            this.label1.TabIndex = 46;
            this.label1.Text = " Descripcion";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(1213, 174);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(213, 22);
            this.txtDescripcion.TabIndex = 45;
            // 
            // txtBuscarPerfil
            // 
            this.txtBuscarPerfil.Location = new System.Drawing.Point(372, 116);
            this.txtBuscarPerfil.Name = "txtBuscarPerfil";
            this.txtBuscarPerfil.Size = new System.Drawing.Size(127, 22);
            this.txtBuscarPerfil.TabIndex = 44;
            this.txtBuscarPerfil.TextChanged += new System.EventHandler(this.txtBuscarPerfil_TextChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(978, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 32);
            this.label3.TabIndex = 43;
            this.label3.Text = " Nombre Perfil";
            // 
            // btnGuardarPerfil
            // 
            this.btnGuardarPerfil.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnGuardarPerfil.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarPerfil.ForeColor = System.Drawing.Color.White;
            this.btnGuardarPerfil.Location = new System.Drawing.Point(1172, 520);
            this.btnGuardarPerfil.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardarPerfil.Name = "btnGuardarPerfil";
            this.btnGuardarPerfil.Size = new System.Drawing.Size(229, 39);
            this.btnGuardarPerfil.TabIndex = 20;
            this.btnGuardarPerfil.Text = " GUARDAR PERFIL";
            this.btnGuardarPerfil.UseVisualStyleBackColor = false;
            this.btnGuardarPerfil.Click += new System.EventHandler(this.btnGuardarPerfil_Click);
            // 
            // btnQuitarPermiso
            // 
            this.btnQuitarPermiso.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnQuitarPermiso.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitarPermiso.ForeColor = System.Drawing.Color.White;
            this.btnQuitarPermiso.Location = new System.Drawing.Point(1172, 325);
            this.btnQuitarPermiso.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuitarPermiso.Name = "btnQuitarPermiso";
            this.btnQuitarPermiso.Size = new System.Drawing.Size(229, 43);
            this.btnQuitarPermiso.TabIndex = 18;
            this.btnQuitarPermiso.Text = " QUITAR PERMISO";
            this.btnQuitarPermiso.UseVisualStyleBackColor = false;
            this.btnQuitarPermiso.Click += new System.EventHandler(this.btnQuitarPermiso_Click);
            // 
            // btnAsignarPermiso
            // 
            this.btnAsignarPermiso.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnAsignarPermiso.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarPermiso.ForeColor = System.Drawing.Color.White;
            this.btnAsignarPermiso.Location = new System.Drawing.Point(1172, 257);
            this.btnAsignarPermiso.Margin = new System.Windows.Forms.Padding(4);
            this.btnAsignarPermiso.Name = "btnAsignarPermiso";
            this.btnAsignarPermiso.Size = new System.Drawing.Size(229, 44);
            this.btnAsignarPermiso.TabIndex = 17;
            this.btnAsignarPermiso.Text = "ASIGNAR PERMISO";
            this.btnAsignarPermiso.UseVisualStyleBackColor = false;
            this.btnAsignarPermiso.Click += new System.EventHandler(this.btnAsignarPermiso_Click);
            // 
            // btnEliminarPerfil
            // 
            this.btnEliminarPerfil.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnEliminarPerfil.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarPerfil.ForeColor = System.Drawing.Color.White;
            this.btnEliminarPerfil.Location = new System.Drawing.Point(1172, 455);
            this.btnEliminarPerfil.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminarPerfil.Name = "btnEliminarPerfil";
            this.btnEliminarPerfil.Size = new System.Drawing.Size(229, 43);
            this.btnEliminarPerfil.TabIndex = 16;
            this.btnEliminarPerfil.Text = " ELIMINAR PERFIL";
            this.btnEliminarPerfil.UseVisualStyleBackColor = false;
            this.btnEliminarPerfil.Click += new System.EventHandler(this.btnEliminarPerfil_Click);
            // 
            // txtNuevoPerfil
            // 
            this.txtNuevoPerfil.Location = new System.Drawing.Point(1213, 130);
            this.txtNuevoPerfil.Margin = new System.Windows.Forms.Padding(4);
            this.txtNuevoPerfil.Name = "txtNuevoPerfil";
            this.txtNuevoPerfil.Size = new System.Drawing.Size(213, 22);
            this.txtNuevoPerfil.TabIndex = 13;
            // 
            // tvPermisosAsignados
            // 
            this.tvPermisosAsignados.Location = new System.Drawing.Point(646, 159);
            this.tvPermisosAsignados.Margin = new System.Windows.Forms.Padding(4);
            this.tvPermisosAsignados.Name = "tvPermisosAsignados";
            this.tvPermisosAsignados.Size = new System.Drawing.Size(288, 400);
            this.tvPermisosAsignados.TabIndex = 12;
            this.tvPermisosAsignados.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPermisosAsignados_AfterSelect);
            // 
            // lbPermisosDisponiblesTab2
            // 
            this.lbPermisosDisponiblesTab2.FormattingEnabled = true;
            this.lbPermisosDisponiblesTab2.ItemHeight = 16;
            this.lbPermisosDisponiblesTab2.Location = new System.Drawing.Point(37, 159);
            this.lbPermisosDisponiblesTab2.Margin = new System.Windows.Forms.Padding(4);
            this.lbPermisosDisponiblesTab2.Name = "lbPermisosDisponiblesTab2";
            this.lbPermisosDisponiblesTab2.Size = new System.Drawing.Size(282, 404);
            this.lbPermisosDisponiblesTab2.TabIndex = 11;
            // 
            // frmGestionPerfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1528, 695);
            this.Controls.Add(this.tvPermisosAsignados);
            this.Controls.Add(this.lbPerfiles);
            this.Controls.Add(this.btnLimpiarPerfil);
            this.Controls.Add(this.btnGuardarPerfil);
            this.Controls.Add(this.btnEliminarPerfil);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.btnQuitarPermiso);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAsignarPermiso);
            this.Controls.Add(this.lbPermisosDisponiblesTab2);
            this.Controls.Add(this.txtBuscarPerfil);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNuevoPerfil);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmGestionPerfiles";
            this.Text = "frmGestionRoles";
            this.Load += new System.EventHandler(this.frmGestionRoles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGuardarPerfil;
        private System.Windows.Forms.Button btnQuitarPermiso;
        private System.Windows.Forms.Button btnAsignarPermiso;
        private System.Windows.Forms.Button btnEliminarPerfil;
        private System.Windows.Forms.TextBox txtNuevoPerfil;
        private System.Windows.Forms.TreeView tvPermisosAsignados;
        private System.Windows.Forms.ListBox lbPermisosDisponiblesTab2;
        private System.Windows.Forms.TextBox txtBuscarPerfil;
        private System.Windows.Forms.ListBox lbPerfiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnLimpiarPerfil;
    }
}