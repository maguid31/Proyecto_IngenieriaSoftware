namespace Proyecto_IS
{
    partial class frmGestionRoles
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
            this.Gestion = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblEstadoFamilia = new System.Windows.Forms.Label();
            this.btnGuardarFamilia = new System.Windows.Forms.Button();
            this.btnLimpiarFamilia = new System.Windows.Forms.Button();
            this.btnQuitarNodo = new System.Windows.Forms.Button();
            this.btnAgregarNodo = new System.Windows.Forms.Button();
            this.txtDescFamilia = new System.Windows.Forms.TextBox();
            this.txtNombreFamilia = new System.Windows.Forms.TextBox();
            this.tvFamiliaEdicion = new System.Windows.Forms.TreeView();
            this.lbFuentePermisos = new System.Windows.Forms.ListBox();
            this.cmbFamiliasExistentes = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnGuardarPerfil = new System.Windows.Forms.Button();
            this.lblEstadoPerfil = new System.Windows.Forms.Label();
            this.btnQuitarPermiso = new System.Windows.Forms.Button();
            this.btnAsignarPermiso = new System.Windows.Forms.Button();
            this.btnEliminarPerfil = new System.Windows.Forms.Button();
            this.btnCrearPerfil = new System.Windows.Forms.Button();
            this.txtNuevoPerfil = new System.Windows.Forms.TextBox();
            this.tvPermisosAsignados = new System.Windows.Forms.TreeView();
            this.lbPermisosDisponiblesTab2 = new System.Windows.Forms.ListBox();
            this.cbPerfiles = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Gestion.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Gestion
            // 
            this.Gestion.Controls.Add(this.tabPage1);
            this.Gestion.Controls.Add(this.tabPage2);
            this.Gestion.Location = new System.Drawing.Point(47, 19);
            this.Gestion.Name = "Gestion";
            this.Gestion.SelectedIndex = 0;
            this.Gestion.Size = new System.Drawing.Size(1092, 563);
            this.Gestion.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lblEstadoFamilia);
            this.tabPage1.Controls.Add(this.btnGuardarFamilia);
            this.tabPage1.Controls.Add(this.btnLimpiarFamilia);
            this.tabPage1.Controls.Add(this.btnQuitarNodo);
            this.tabPage1.Controls.Add(this.btnAgregarNodo);
            this.tabPage1.Controls.Add(this.txtDescFamilia);
            this.tabPage1.Controls.Add(this.txtNombreFamilia);
            this.tabPage1.Controls.Add(this.tvFamiliaEdicion);
            this.tabPage1.Controls.Add(this.lbFuentePermisos);
            this.tabPage1.Controls.Add(this.cmbFamiliasExistentes);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1084, 537);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Gestion Familias";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblEstadoFamilia
            // 
            this.lblEstadoFamilia.AutoSize = true;
            this.lblEstadoFamilia.Location = new System.Drawing.Point(224, 464);
            this.lblEstadoFamilia.Name = "lblEstadoFamilia";
            this.lblEstadoFamilia.Size = new System.Drawing.Size(35, 13);
            this.lblEstadoFamilia.TabIndex = 9;
            this.lblEstadoFamilia.Text = "label1";
            // 
            // btnGuardarFamilia
            // 
            this.btnGuardarFamilia.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnGuardarFamilia.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarFamilia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGuardarFamilia.Location = new System.Drawing.Point(803, 341);
            this.btnGuardarFamilia.Name = "btnGuardarFamilia";
            this.btnGuardarFamilia.Size = new System.Drawing.Size(219, 37);
            this.btnGuardarFamilia.TabIndex = 8;
            this.btnGuardarFamilia.Text = "GUARDAR FAMILIA";
            this.btnGuardarFamilia.UseVisualStyleBackColor = false;
            this.btnGuardarFamilia.Click += new System.EventHandler(this.btnGuardarFamilia_Click_1);
            // 
            // btnLimpiarFamilia
            // 
            this.btnLimpiarFamilia.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLimpiarFamilia.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarFamilia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLimpiarFamilia.Location = new System.Drawing.Point(800, 269);
            this.btnLimpiarFamilia.Name = "btnLimpiarFamilia";
            this.btnLimpiarFamilia.Size = new System.Drawing.Size(222, 37);
            this.btnLimpiarFamilia.TabIndex = 7;
            this.btnLimpiarFamilia.Text = "LIMPIAR FAMILIA";
            this.btnLimpiarFamilia.UseVisualStyleBackColor = false;
            this.btnLimpiarFamilia.Click += new System.EventHandler(this.btnLimpiarFamilia_Click_1);
            // 
            // btnQuitarNodo
            // 
            this.btnQuitarNodo.Location = new System.Drawing.Point(918, 182);
            this.btnQuitarNodo.Name = "btnQuitarNodo";
            this.btnQuitarNodo.Size = new System.Drawing.Size(75, 23);
            this.btnQuitarNodo.TabIndex = 6;
            this.btnQuitarNodo.Text = "➖";
            this.btnQuitarNodo.UseVisualStyleBackColor = true;
            this.btnQuitarNodo.Click += new System.EventHandler(this.btnQuitarNodo_Click);
            // 
            // btnAgregarNodo
            // 
            this.btnAgregarNodo.Location = new System.Drawing.Point(832, 182);
            this.btnAgregarNodo.Name = "btnAgregarNodo";
            this.btnAgregarNodo.Size = new System.Drawing.Size(75, 23);
            this.btnAgregarNodo.TabIndex = 5;
            this.btnAgregarNodo.Text = "➕";
            this.btnAgregarNodo.UseVisualStyleBackColor = true;
            this.btnAgregarNodo.Click += new System.EventHandler(this.btnAgregarNodo_Click_1);
            // 
            // txtDescFamilia
            // 
            this.txtDescFamilia.Location = new System.Drawing.Point(832, 117);
            this.txtDescFamilia.Name = "txtDescFamilia";
            this.txtDescFamilia.Size = new System.Drawing.Size(161, 20);
            this.txtDescFamilia.TabIndex = 4;
            // 
            // txtNombreFamilia
            // 
            this.txtNombreFamilia.Location = new System.Drawing.Point(832, 72);
            this.txtNombreFamilia.Name = "txtNombreFamilia";
            this.txtNombreFamilia.Size = new System.Drawing.Size(161, 20);
            this.txtNombreFamilia.TabIndex = 3;
            // 
            // tvFamiliaEdicion
            // 
            this.tvFamiliaEdicion.Location = new System.Drawing.Point(336, 76);
            this.tvFamiliaEdicion.Name = "tvFamiliaEdicion";
            this.tvFamiliaEdicion.Size = new System.Drawing.Size(277, 333);
            this.tvFamiliaEdicion.TabIndex = 2;
            // 
            // lbFuentePermisos
            // 
            this.lbFuentePermisos.FormattingEnabled = true;
            this.lbFuentePermisos.Location = new System.Drawing.Point(25, 76);
            this.lbFuentePermisos.Name = "lbFuentePermisos";
            this.lbFuentePermisos.Size = new System.Drawing.Size(275, 329);
            this.lbFuentePermisos.TabIndex = 1;
            // 
            // cmbFamiliasExistentes
            // 
            this.cmbFamiliasExistentes.FormattingEnabled = true;
            this.cmbFamiliasExistentes.Location = new System.Drawing.Point(25, 24);
            this.cmbFamiliasExistentes.Name = "cmbFamiliasExistentes";
            this.cmbFamiliasExistentes.Size = new System.Drawing.Size(121, 21);
            this.cmbFamiliasExistentes.TabIndex = 0;
            this.cmbFamiliasExistentes.SelectedIndexChanged += new System.EventHandler(this.cmbFamiliasExistentes_SelectedIndexChanged_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btnGuardarPerfil);
            this.tabPage2.Controls.Add(this.lblEstadoPerfil);
            this.tabPage2.Controls.Add(this.btnQuitarPermiso);
            this.tabPage2.Controls.Add(this.btnAsignarPermiso);
            this.tabPage2.Controls.Add(this.btnEliminarPerfil);
            this.tabPage2.Controls.Add(this.btnCrearPerfil);
            this.tabPage2.Controls.Add(this.txtNuevoPerfil);
            this.tabPage2.Controls.Add(this.tvPermisosAsignados);
            this.tabPage2.Controls.Add(this.lbPermisosDisponiblesTab2);
            this.tabPage2.Controls.Add(this.cbPerfiles);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1084, 537);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Asignación de Roles";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnGuardarPerfil
            // 
            this.btnGuardarPerfil.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnGuardarPerfil.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarPerfil.ForeColor = System.Drawing.Color.White;
            this.btnGuardarPerfil.Location = new System.Drawing.Point(781, 430);
            this.btnGuardarPerfil.Name = "btnGuardarPerfil";
            this.btnGuardarPerfil.Size = new System.Drawing.Size(215, 39);
            this.btnGuardarPerfil.TabIndex = 20;
            this.btnGuardarPerfil.Text = "GUARDAR PERFIL";
            this.btnGuardarPerfil.UseVisualStyleBackColor = false;
            this.btnGuardarPerfil.Click += new System.EventHandler(this.btnGuardarPerfil_Click);
            // 
            // lblEstadoPerfil
            // 
            this.lblEstadoPerfil.AutoSize = true;
            this.lblEstadoPerfil.Location = new System.Drawing.Point(68, 472);
            this.lblEstadoPerfil.Name = "lblEstadoPerfil";
            this.lblEstadoPerfil.Size = new System.Drawing.Size(35, 13);
            this.lblEstadoPerfil.TabIndex = 19;
            this.lblEstadoPerfil.Text = "label1";
            // 
            // btnQuitarPermiso
            // 
            this.btnQuitarPermiso.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnQuitarPermiso.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitarPermiso.ForeColor = System.Drawing.Color.White;
            this.btnQuitarPermiso.Location = new System.Drawing.Point(781, 331);
            this.btnQuitarPermiso.Name = "btnQuitarPermiso";
            this.btnQuitarPermiso.Size = new System.Drawing.Size(215, 35);
            this.btnQuitarPermiso.TabIndex = 18;
            this.btnQuitarPermiso.Text = "QUITAR PERMISO";
            this.btnQuitarPermiso.UseVisualStyleBackColor = false;
            this.btnQuitarPermiso.Click += new System.EventHandler(this.btnQuitarPermiso_Click);
            // 
            // btnAsignarPermiso
            // 
            this.btnAsignarPermiso.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnAsignarPermiso.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarPermiso.ForeColor = System.Drawing.Color.White;
            this.btnAsignarPermiso.Location = new System.Drawing.Point(781, 278);
            this.btnAsignarPermiso.Name = "btnAsignarPermiso";
            this.btnAsignarPermiso.Size = new System.Drawing.Size(215, 36);
            this.btnAsignarPermiso.TabIndex = 17;
            this.btnAsignarPermiso.Text = "ASIGNAR PERMISO";
            this.btnAsignarPermiso.UseVisualStyleBackColor = false;
            this.btnAsignarPermiso.Click += new System.EventHandler(this.btnAsignarPermiso_Click_1);
            // 
            // btnEliminarPerfil
            // 
            this.btnEliminarPerfil.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnEliminarPerfil.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarPerfil.ForeColor = System.Drawing.Color.White;
            this.btnEliminarPerfil.Location = new System.Drawing.Point(781, 223);
            this.btnEliminarPerfil.Name = "btnEliminarPerfil";
            this.btnEliminarPerfil.Size = new System.Drawing.Size(215, 35);
            this.btnEliminarPerfil.TabIndex = 16;
            this.btnEliminarPerfil.Text = "ELIMINAR PERFIL";
            this.btnEliminarPerfil.UseVisualStyleBackColor = false;
            this.btnEliminarPerfil.Click += new System.EventHandler(this.btnEliminarPerfil_Click_1);
            // 
            // btnCrearPerfil
            // 
            this.btnCrearPerfil.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnCrearPerfil.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearPerfil.ForeColor = System.Drawing.Color.White;
            this.btnCrearPerfil.Location = new System.Drawing.Point(781, 169);
            this.btnCrearPerfil.Name = "btnCrearPerfil";
            this.btnCrearPerfil.Size = new System.Drawing.Size(215, 36);
            this.btnCrearPerfil.TabIndex = 15;
            this.btnCrearPerfil.Text = "CREAR PERFIL";
            this.btnCrearPerfil.UseVisualStyleBackColor = false;
            this.btnCrearPerfil.Click += new System.EventHandler(this.btnCrearPerfil_Click_1);
            // 
            // txtNuevoPerfil
            // 
            this.txtNuevoPerfil.Location = new System.Drawing.Point(882, 91);
            this.txtNuevoPerfil.Name = "txtNuevoPerfil";
            this.txtNuevoPerfil.Size = new System.Drawing.Size(161, 20);
            this.txtNuevoPerfil.TabIndex = 13;
            // 
            // tvPermisosAsignados
            // 
            this.tvPermisosAsignados.Location = new System.Drawing.Point(371, 91);
            this.tvPermisosAsignados.Name = "tvPermisosAsignados";
            this.tvPermisosAsignados.Size = new System.Drawing.Size(271, 326);
            this.tvPermisosAsignados.TabIndex = 12;
            // 
            // lbPermisosDisponiblesTab2
            // 
            this.lbPermisosDisponiblesTab2.FormattingEnabled = true;
            this.lbPermisosDisponiblesTab2.Location = new System.Drawing.Point(57, 91);
            this.lbPermisosDisponiblesTab2.Name = "lbPermisosDisponiblesTab2";
            this.lbPermisosDisponiblesTab2.Size = new System.Drawing.Size(266, 329);
            this.lbPermisosDisponiblesTab2.TabIndex = 11;
            // 
            // cbPerfiles
            // 
            this.cbPerfiles.FormattingEnabled = true;
            this.cbPerfiles.Location = new System.Drawing.Point(57, 43);
            this.cbPerfiles.Name = "cbPerfiles";
            this.cbPerfiles.Size = new System.Drawing.Size(121, 21);
            this.cbPerfiles.TabIndex = 10;
            this.cbPerfiles.SelectedIndexChanged += new System.EventHandler(this.cbPerfiles_SelectedIndexChanged);
            this.cbPerfiles.Click += new System.EventHandler(this.cbPerfiles_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(627, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 25);
            this.label2.TabIndex = 42;
            this.label2.Text = "Nombre de la Familia";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(710, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 25);
            this.label1.TabIndex = 43;
            this.label1.Text = "Descripción";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(719, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 25);
            this.label3.TabIndex = 43;
            this.label3.Text = "Nombre Perfil";
            // 
            // frmGestionRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 594);
            this.Controls.Add(this.Gestion);
            this.Name = "frmGestionRoles";
            this.Text = "frmGestionRoles";
            this.Load += new System.EventHandler(this.frmGestionRoles_Load);
            this.Gestion.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Gestion;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtNombreFamilia;
        private System.Windows.Forms.TreeView tvFamiliaEdicion;
        private System.Windows.Forms.ListBox lbFuentePermisos;
        private System.Windows.Forms.ComboBox cmbFamiliasExistentes;
        private System.Windows.Forms.Button btnGuardarFamilia;
        private System.Windows.Forms.Button btnLimpiarFamilia;
        private System.Windows.Forms.Button btnQuitarNodo;
        private System.Windows.Forms.Button btnAgregarNodo;
        private System.Windows.Forms.TextBox txtDescFamilia;
        private System.Windows.Forms.Label lblEstadoFamilia;
        private System.Windows.Forms.Label lblEstadoPerfil;
        private System.Windows.Forms.Button btnQuitarPermiso;
        private System.Windows.Forms.Button btnAsignarPermiso;
        private System.Windows.Forms.Button btnEliminarPerfil;
        private System.Windows.Forms.Button btnCrearPerfil;
        private System.Windows.Forms.TextBox txtNuevoPerfil;
        private System.Windows.Forms.TreeView tvPermisosAsignados;
        private System.Windows.Forms.ListBox lbPermisosDisponiblesTab2;
        private System.Windows.Forms.ComboBox cbPerfiles;
        private System.Windows.Forms.Button btnGuardarPerfil;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}