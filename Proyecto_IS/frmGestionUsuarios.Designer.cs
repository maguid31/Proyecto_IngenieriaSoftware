namespace Proyecto_IS
{
    partial class frmGestionUsuarios
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
            this.btnCrear = new System.Windows.Forms.Button();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnHabilitar = new System.Windows.Forms.Button();
            this.btnDeshabilitar = new System.Windows.Forms.Button();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.NombreUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DNI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bloqueado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnaplicar = new System.Windows.Forms.Button();
            this.btncancelar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtrol = new System.Windows.Forms.TextBox();
            this.txtnombreUusario = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.deshabilitar = new System.Windows.Forms.Button();
            this.cbtodos = new System.Windows.Forms.CheckBox();
            this.cbactivos = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCrear
            // 
            this.btnCrear.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrear.ForeColor = System.Drawing.Color.White;
            this.btnCrear.Location = new System.Drawing.Point(767, 580);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(115, 35);
            this.btnCrear.TabIndex = 46;
            this.btnCrear.Text = "CREAR";
            this.btnCrear.UseVisualStyleBackColor = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // txtApellido
            // 
            this.txtApellido.BackColor = System.Drawing.Color.LightGray;
            this.txtApellido.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtApellido.Location = new System.Drawing.Point(238, 454);
            this.txtApellido.Multiline = true;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(216, 28);
            this.txtApellido.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(101, 445);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 32);
            this.label3.TabIndex = 43;
            this.label3.Text = "Apellido";
            // 
            // txtDNI
            // 
            this.txtDNI.BackColor = System.Drawing.Color.LightGray;
            this.txtDNI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDNI.Location = new System.Drawing.Point(238, 406);
            this.txtDNI.Multiline = true;
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(216, 28);
            this.txtDNI.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(152, 406);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 32);
            this.label2.TabIndex = 41;
            this.label2.Text = "DNI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label1.Location = new System.Drawing.Point(57, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 46);
            this.label1.TabIndex = 40;
            this.label1.Text = "GESTION USUARIOS";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.ForeColor = System.Drawing.Color.White;
            this.btnModificar.Location = new System.Drawing.Point(767, 457);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(115, 35);
            this.btnModificar.TabIndex = 49;
            this.btnModificar.Text = "MODIFICAR";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnHabilitar
            // 
            this.btnHabilitar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnHabilitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHabilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHabilitar.ForeColor = System.Drawing.Color.White;
            this.btnHabilitar.Location = new System.Drawing.Point(767, 498);
            this.btnHabilitar.Name = "btnHabilitar";
            this.btnHabilitar.Size = new System.Drawing.Size(115, 35);
            this.btnHabilitar.TabIndex = 50;
            this.btnHabilitar.Text = "HABILITAR";
            this.btnHabilitar.UseVisualStyleBackColor = false;
            // 
            // btnDeshabilitar
            // 
            this.btnDeshabilitar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnDeshabilitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeshabilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeshabilitar.ForeColor = System.Drawing.Color.White;
            this.btnDeshabilitar.Location = new System.Drawing.Point(767, 539);
            this.btnDeshabilitar.Name = "btnDeshabilitar";
            this.btnDeshabilitar.Size = new System.Drawing.Size(154, 35);
            this.btnDeshabilitar.TabIndex = 51;
            this.btnDeshabilitar.Text = "DESHABILITAR";
            this.btnDeshabilitar.UseVisualStyleBackColor = false;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NombreUsuario,
            this.Nombre,
            this.Apellido,
            this.DNI,
            this.Rol,
            this.Email,
            this.Bloqueado});
            this.dgvUsuarios.Location = new System.Drawing.Point(33, 83);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.RowHeadersWidth = 51;
            this.dgvUsuarios.Size = new System.Drawing.Size(928, 239);
            this.dgvUsuarios.TabIndex = 52;
            this.dgvUsuarios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellContentClick);
            // 
            // NombreUsuario
            // 
            this.NombreUsuario.HeaderText = "NombreUsuario";
            this.NombreUsuario.MinimumWidth = 6;
            this.NombreUsuario.Name = "NombreUsuario";
            this.NombreUsuario.Width = 125;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 6;
            this.Nombre.Name = "Nombre";
            this.Nombre.Width = 125;
            // 
            // Apellido
            // 
            this.Apellido.HeaderText = "Apellido";
            this.Apellido.MinimumWidth = 6;
            this.Apellido.Name = "Apellido";
            this.Apellido.Width = 125;
            // 
            // DNI
            // 
            this.DNI.HeaderText = "DNI";
            this.DNI.MinimumWidth = 6;
            this.DNI.Name = "DNI";
            this.DNI.Width = 125;
            // 
            // Rol
            // 
            this.Rol.HeaderText = "Rol";
            this.Rol.MinimumWidth = 6;
            this.Rol.Name = "Rol";
            this.Rol.Width = 125;
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 6;
            this.Email.Name = "Email";
            this.Email.Width = 125;
            // 
            // Bloqueado
            // 
            this.Bloqueado.HeaderText = "Bloqueado";
            this.Bloqueado.MinimumWidth = 6;
            this.Bloqueado.Name = "Bloqueado";
            this.Bloqueado.Width = 125;
            // 
            // btnaplicar
            // 
            this.btnaplicar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnaplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnaplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaplicar.ForeColor = System.Drawing.Color.White;
            this.btnaplicar.Location = new System.Drawing.Point(518, 515);
            this.btnaplicar.Name = "btnaplicar";
            this.btnaplicar.Size = new System.Drawing.Size(115, 35);
            this.btnaplicar.TabIndex = 53;
            this.btnaplicar.Text = "APLICAR";
            this.btnaplicar.UseVisualStyleBackColor = false;
            this.btnaplicar.Click += new System.EventHandler(this.btnaplicar_Click);
            // 
            // btncancelar
            // 
            this.btncancelar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btncancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancelar.ForeColor = System.Drawing.Color.White;
            this.btncancelar.Location = new System.Drawing.Point(518, 580);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(115, 35);
            this.btncancelar.TabIndex = 54;
            this.btncancelar.Text = "CANCELAR";
            this.btncancelar.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(103, 489);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 32);
            this.label4.TabIndex = 55;
            this.label4.Text = "Nombre";
            // 
            // txtnombre
            // 
            this.txtnombre.BackColor = System.Drawing.Color.LightGray;
            this.txtnombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtnombre.Location = new System.Drawing.Point(238, 498);
            this.txtnombre.Multiline = true;
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(216, 28);
            this.txtnombre.TabIndex = 56;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(152, 544);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 32);
            this.label5.TabIndex = 57;
            this.label5.Text = "Rol";
            // 
            // txtrol
            // 
            this.txtrol.BackColor = System.Drawing.Color.LightGray;
            this.txtrol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtrol.Location = new System.Drawing.Point(238, 548);
            this.txtrol.Multiline = true;
            this.txtrol.Name = "txtrol";
            this.txtrol.Size = new System.Drawing.Size(216, 28);
            this.txtrol.TabIndex = 58;
            // 
            // txtnombreUusario
            // 
            this.txtnombreUusario.BackColor = System.Drawing.Color.LightGray;
            this.txtnombreUusario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtnombreUusario.Location = new System.Drawing.Point(238, 596);
            this.txtnombreUusario.Multiline = true;
            this.txtnombreUusario.Name = "txtnombreUusario";
            this.txtnombreUusario.Size = new System.Drawing.Size(216, 28);
            this.txtnombreUusario.TabIndex = 59;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(31, 592);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(201, 32);
            this.label7.TabIndex = 60;
            this.label7.Text = "Nombre usuario";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(135, 635);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 32);
            this.label8.TabIndex = 61;
            this.label8.Text = "Email";
            // 
            // txtemail
            // 
            this.txtemail.BackColor = System.Drawing.Color.LightGray;
            this.txtemail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtemail.Location = new System.Drawing.Point(238, 635);
            this.txtemail.Multiline = true;
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(216, 28);
            this.txtemail.TabIndex = 62;
            // 
            // deshabilitar
            // 
            this.deshabilitar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.deshabilitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deshabilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deshabilitar.ForeColor = System.Drawing.Color.White;
            this.deshabilitar.Location = new System.Drawing.Point(767, 621);
            this.deshabilitar.Name = "deshabilitar";
            this.deshabilitar.Size = new System.Drawing.Size(154, 42);
            this.deshabilitar.TabIndex = 63;
            this.deshabilitar.Text = "DESHABILITAR";
            this.deshabilitar.UseVisualStyleBackColor = false;
            this.deshabilitar.Click += new System.EventHandler(this.deshabilitar_Click);
            // 
            // cbtodos
            // 
            this.cbtodos.AutoSize = true;
            this.cbtodos.Enabled = false;
            this.cbtodos.Location = new System.Drawing.Point(518, 384);
            this.cbtodos.Name = "cbtodos";
            this.cbtodos.Size = new System.Drawing.Size(90, 27);
            this.cbtodos.TabIndex = 64;
            this.cbtodos.Text = "TODOS";
            this.cbtodos.UseVisualStyleBackColor = true;
            this.cbtodos.CheckedChanged += new System.EventHandler(this.cbtodos_CheckedChanged);
            // 
            // cbactivos
            // 
            this.cbactivos.AutoSize = true;
            this.cbactivos.Location = new System.Drawing.Point(518, 418);
            this.cbactivos.Name = "cbactivos";
            this.cbactivos.Size = new System.Drawing.Size(104, 27);
            this.cbactivos.TabIndex = 65;
            this.cbactivos.Text = "ACTIVOS";
            this.cbactivos.UseVisualStyleBackColor = true;
            this.cbactivos.CheckedChanged += new System.EventHandler(this.cbactivos_CheckedChanged);
            // 
            // frmGestionUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1109, 726);
            this.Controls.Add(this.cbactivos);
            this.Controls.Add(this.cbtodos);
            this.Controls.Add(this.deshabilitar);
            this.Controls.Add(this.txtemail);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtnombreUusario);
            this.Controls.Add(this.txtrol);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtnombre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btncancelar);
            this.Controls.Add(this.btnaplicar);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.btnDeshabilitar);
            this.Controls.Add(this.btnHabilitar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmGestionUsuarios";
            this.Text = "mainForm";
            this.Load += new System.EventHandler(this.frmGestionUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnHabilitar;
        private System.Windows.Forms.Button btnDeshabilitar;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn DNI;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bloqueado;
        private System.Windows.Forms.Button btnaplicar;
        private System.Windows.Forms.Button btncancelar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtrol;
        private System.Windows.Forms.TextBox txtnombreUusario;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.Button deshabilitar;
        private System.Windows.Forms.CheckBox cbtodos;
        private System.Windows.Forms.CheckBox cbactivos;
    }
}