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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbFamiliasExistentes = new System.Windows.Forms.ComboBox();
            this.lbFuentePermisos = new System.Windows.Forms.ListBox();
            this.tvFamiliaEdicion = new System.Windows.Forms.TreeView();
            this.txtNombreFamilia = new System.Windows.Forms.TextBox();
            this.txtDescFamilia = new System.Windows.Forms.TextBox();
            this.btnAgregarNodo = new System.Windows.Forms.Button();
            this.btnQuitarNodo = new System.Windows.Forms.Button();
            this.btnLimpiarFamilia = new System.Windows.Forms.Button();
            this.btnGuardarFamilia = new System.Windows.Forms.Button();
            this.lblEstadoFamilia = new System.Windows.Forms.Label();
            this.lblEstadoPerfil = new System.Windows.Forms.Label();
            this.btnQuitarPermiso = new System.Windows.Forms.Button();
            this.btnAsignarPermiso = new System.Windows.Forms.Button();
            this.btnEliminarPerfil = new System.Windows.Forms.Button();
            this.btnCrearPerfil = new System.Windows.Forms.Button();
            this.txtNuevoPerfil = new System.Windows.Forms.TextBox();
            this.tvPermisosAsignados = new System.Windows.Forms.TreeView();
            this.lbPermisosDisponiblesTab2 = new System.Windows.Forms.ListBox();
            this.cbPerfiles = new System.Windows.Forms.ComboBox();
            this.btnGuardarPerfil = new System.Windows.Forms.Button();
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
            // tabPage2
            // 
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
            // cmbFamiliasExistentes
            // 
            this.cmbFamiliasExistentes.FormattingEnabled = true;
            this.cmbFamiliasExistentes.Location = new System.Drawing.Point(25, 24);
            this.cmbFamiliasExistentes.Name = "cmbFamiliasExistentes";
            this.cmbFamiliasExistentes.Size = new System.Drawing.Size(121, 21);
            this.cmbFamiliasExistentes.TabIndex = 0;
            // 
            // lbFuentePermisos
            // 
            this.lbFuentePermisos.FormattingEnabled = true;
            this.lbFuentePermisos.Location = new System.Drawing.Point(25, 72);
            this.lbFuentePermisos.Name = "lbFuentePermisos";
            this.lbFuentePermisos.Size = new System.Drawing.Size(218, 238);
            this.lbFuentePermisos.TabIndex = 1;
            // 
            // tvFamiliaEdicion
            // 
            this.tvFamiliaEdicion.Location = new System.Drawing.Point(328, 72);
            this.tvFamiliaEdicion.Name = "tvFamiliaEdicion";
            this.tvFamiliaEdicion.Size = new System.Drawing.Size(191, 238);
            this.tvFamiliaEdicion.TabIndex = 2;
            // 
            // txtNombreFamilia
            // 
            this.txtNombreFamilia.Location = new System.Drawing.Point(579, 72);
            this.txtNombreFamilia.Name = "txtNombreFamilia";
            this.txtNombreFamilia.Size = new System.Drawing.Size(161, 20);
            this.txtNombreFamilia.TabIndex = 3;
            // 
            // txtDescFamilia
            // 
            this.txtDescFamilia.Location = new System.Drawing.Point(579, 122);
            this.txtDescFamilia.Name = "txtDescFamilia";
            this.txtDescFamilia.Size = new System.Drawing.Size(161, 20);
            this.txtDescFamilia.TabIndex = 4;
            // 
            // btnAgregarNodo
            // 
            this.btnAgregarNodo.Location = new System.Drawing.Point(566, 182);
            this.btnAgregarNodo.Name = "btnAgregarNodo";
            this.btnAgregarNodo.Size = new System.Drawing.Size(75, 23);
            this.btnAgregarNodo.TabIndex = 5;
            this.btnAgregarNodo.Text = "➕";
            this.btnAgregarNodo.UseVisualStyleBackColor = true;
            // 
            // btnQuitarNodo
            // 
            this.btnQuitarNodo.Location = new System.Drawing.Point(665, 182);
            this.btnQuitarNodo.Name = "btnQuitarNodo";
            this.btnQuitarNodo.Size = new System.Drawing.Size(75, 23);
            this.btnQuitarNodo.TabIndex = 6;
            this.btnQuitarNodo.Text = "➖";
            this.btnQuitarNodo.UseVisualStyleBackColor = true;
            // 
            // btnLimpiarFamilia
            // 
            this.btnLimpiarFamilia.Location = new System.Drawing.Point(579, 287);
            this.btnLimpiarFamilia.Name = "btnLimpiarFamilia";
            this.btnLimpiarFamilia.Size = new System.Drawing.Size(92, 23);
            this.btnLimpiarFamilia.TabIndex = 7;
            this.btnLimpiarFamilia.Text = "Limpiar Familia";
            this.btnLimpiarFamilia.UseVisualStyleBackColor = true;
            // 
            // btnGuardarFamilia
            // 
            this.btnGuardarFamilia.Location = new System.Drawing.Point(713, 287);
            this.btnGuardarFamilia.Name = "btnGuardarFamilia";
            this.btnGuardarFamilia.Size = new System.Drawing.Size(104, 23);
            this.btnGuardarFamilia.TabIndex = 8;
            this.btnGuardarFamilia.Text = "Guardar Familia";
            this.btnGuardarFamilia.UseVisualStyleBackColor = true;
            this.btnGuardarFamilia.Click += new System.EventHandler(this.btnGuardarFamilia_Click_1);
            // 
            // lblEstadoFamilia
            // 
            this.lblEstadoFamilia.AutoSize = true;
            this.lblEstadoFamilia.Location = new System.Drawing.Point(211, 428);
            this.lblEstadoFamilia.Name = "lblEstadoFamilia";
            this.lblEstadoFamilia.Size = new System.Drawing.Size(35, 13);
            this.lblEstadoFamilia.TabIndex = 9;
            this.lblEstadoFamilia.Text = "label1";
            // 
            // lblEstadoPerfil
            // 
            this.lblEstadoPerfil.AutoSize = true;
            this.lblEstadoPerfil.Location = new System.Drawing.Point(243, 447);
            this.lblEstadoPerfil.Name = "lblEstadoPerfil";
            this.lblEstadoPerfil.Size = new System.Drawing.Size(35, 13);
            this.lblEstadoPerfil.TabIndex = 19;
            this.lblEstadoPerfil.Text = "label1";
            // 
            // btnQuitarPermiso
            // 
            this.btnQuitarPermiso.Location = new System.Drawing.Point(751, 223);
            this.btnQuitarPermiso.Name = "btnQuitarPermiso";
            this.btnQuitarPermiso.Size = new System.Drawing.Size(104, 23);
            this.btnQuitarPermiso.TabIndex = 18;
            this.btnQuitarPermiso.Text = "Quitar Permiso";
            this.btnQuitarPermiso.UseVisualStyleBackColor = true;
            // 
            // btnAsignarPermiso
            // 
            this.btnAsignarPermiso.Location = new System.Drawing.Point(628, 223);
            this.btnAsignarPermiso.Name = "btnAsignarPermiso";
            this.btnAsignarPermiso.Size = new System.Drawing.Size(92, 23);
            this.btnAsignarPermiso.TabIndex = 17;
            this.btnAsignarPermiso.Text = "Asignar Permiso";
            this.btnAsignarPermiso.UseVisualStyleBackColor = true;
            // 
            // btnEliminarPerfil
            // 
            this.btnEliminarPerfil.Location = new System.Drawing.Point(722, 160);
            this.btnEliminarPerfil.Name = "btnEliminarPerfil";
            this.btnEliminarPerfil.Size = new System.Drawing.Size(99, 23);
            this.btnEliminarPerfil.TabIndex = 16;
            this.btnEliminarPerfil.Text = "Eliminar perfil";
            this.btnEliminarPerfil.UseVisualStyleBackColor = true;
            // 
            // btnCrearPerfil
            // 
            this.btnCrearPerfil.Location = new System.Drawing.Point(628, 160);
            this.btnCrearPerfil.Name = "btnCrearPerfil";
            this.btnCrearPerfil.Size = new System.Drawing.Size(75, 23);
            this.btnCrearPerfil.TabIndex = 15;
            this.btnCrearPerfil.Text = "Crear Perfil";
            this.btnCrearPerfil.UseVisualStyleBackColor = true;
            // 
            // txtNuevoPerfil
            // 
            this.txtNuevoPerfil.Location = new System.Drawing.Point(611, 91);
            this.txtNuevoPerfil.Name = "txtNuevoPerfil";
            this.txtNuevoPerfil.Size = new System.Drawing.Size(161, 20);
            this.txtNuevoPerfil.TabIndex = 13;
            // 
            // tvPermisosAsignados
            // 
            this.tvPermisosAsignados.Location = new System.Drawing.Point(360, 91);
            this.tvPermisosAsignados.Name = "tvPermisosAsignados";
            this.tvPermisosAsignados.Size = new System.Drawing.Size(191, 238);
            this.tvPermisosAsignados.TabIndex = 12;
            // 
            // lbPermisosDisponiblesTab2
            // 
            this.lbPermisosDisponiblesTab2.FormattingEnabled = true;
            this.lbPermisosDisponiblesTab2.Location = new System.Drawing.Point(57, 91);
            this.lbPermisosDisponiblesTab2.Name = "lbPermisosDisponiblesTab2";
            this.lbPermisosDisponiblesTab2.Size = new System.Drawing.Size(218, 238);
            this.lbPermisosDisponiblesTab2.TabIndex = 11;
            // 
            // cbPerfiles
            // 
            this.cbPerfiles.FormattingEnabled = true;
            this.cbPerfiles.Location = new System.Drawing.Point(57, 43);
            this.cbPerfiles.Name = "cbPerfiles";
            this.cbPerfiles.Size = new System.Drawing.Size(121, 21);
            this.cbPerfiles.TabIndex = 10;
            // 
            // btnGuardarPerfil
            // 
            this.btnGuardarPerfil.Location = new System.Drawing.Point(683, 294);
            this.btnGuardarPerfil.Name = "btnGuardarPerfil";
            this.btnGuardarPerfil.Size = new System.Drawing.Size(125, 23);
            this.btnGuardarPerfil.TabIndex = 20;
            this.btnGuardarPerfil.Text = "Guardar Perfil";
            this.btnGuardarPerfil.UseVisualStyleBackColor = true;
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
    }
}