using System.Windows.Forms;

namespace Proyecto_IS
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblAppNombre;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Label lblContenidoTitulo;
        private System.Windows.Forms.Label lblContenidoDetalle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblAppNombre = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.lblRol = new System.Windows.Forms.Label();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.lblContenidoDetalle = new System.Windows.Forms.Label();
            this.lblContenidoTitulo = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.lblAppNombre);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 60);
            this.panelHeader.TabIndex = 2;
            // 
            // lblAppNombre
            // 
            this.lblAppNombre.AutoSize = true;
            this.lblAppNombre.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.lblAppNombre.Location = new System.Drawing.Point(20, 15);
            this.lblAppNombre.Name = "lblAppNombre";
            this.lblAppNombre.Size = new System.Drawing.Size(116, 37);
            this.lblAppNombre.TabIndex = 0;
            this.lblAppNombre.Text = "TITULO";
            // 
            // panelMenu
            // 
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 60);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 490);
            this.panelMenu.TabIndex = 1;
            // 
            // lblRol
            // 
            this.lblRol.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblRol.ForeColor = System.Drawing.Color.DimGray;
            this.lblRol.Location = new System.Drawing.Point(0, 440);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(220, 20);
            this.lblRol.TabIndex = 0;
            this.lblRol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.Location = new System.Drawing.Point(0, 460);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(220, 30);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelContenido
            // 
            this.panelContenido.Controls.Add(this.lblContenidoDetalle);
            this.panelContenido.Controls.Add(this.lblContenidoTitulo);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(220, 60);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(40);
            this.panelContenido.Size = new System.Drawing.Size(680, 490);
            this.panelContenido.TabIndex = 0;
            this.panelContenido.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContenido_Paint);
            // 
            // lblContenidoDetalle
            // 
            this.lblContenidoDetalle.AutoSize = true;
            this.lblContenidoDetalle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblContenidoDetalle.Location = new System.Drawing.Point(45, 90);
            this.lblContenidoDetalle.Name = "lblContenidoDetalle";
            this.lblContenidoDetalle.Size = new System.Drawing.Size(479, 25);
            this.lblContenidoDetalle.TabIndex = 0;
            this.lblContenidoDetalle.Text = "Seleccione una opción del menú lateral para comenzar.";
            // 
            // lblContenidoTitulo
            // 
            this.lblContenidoTitulo.AutoSize = true;
            this.lblContenidoTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblContenidoTitulo.Location = new System.Drawing.Point(40, 40);
            this.lblContenidoTitulo.Name = "lblContenidoTitulo";
            this.lblContenidoTitulo.Size = new System.Drawing.Size(253, 46);
            this.lblContenidoTitulo.TabIndex = 1;
            this.lblContenidoTitulo.Text = "Panel de Inicio";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelHeader);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.panelContenido.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}