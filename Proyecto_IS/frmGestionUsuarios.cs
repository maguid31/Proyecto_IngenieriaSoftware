using BLL;
using BLL_65RD;
using Servicios;
using Servicios_65RD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_IS
{
    public partial class frmGestionUsuarios : Form , IidiomaObserver
    {
        public frmGestionUsuarios()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = true; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }
        private int usuarioSeleccionadoId = -1;
        private string usuarioSeleccionadoNombre = ""; 

        private void btnCrear_Click(object sender, EventArgs e)
        {
       

            
            txtnombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtemail.Text = "";
            cmbRol.Text = "";

            
            txtnombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDNI.Enabled = true;
            txtemail.Enabled = true;
            cmbRol.SelectedIndex = -1;
            cmbRol.Enabled = true;

            txtnombre.Focus();
        }

        private void CargarUsuarios(bool soloActivos = false)
        {
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
            var listaUsuarios = gestorUsuario.ObtenerUsuarios(); 

            if (soloActivos)
                listaUsuarios = listaUsuarios.Where(u => u.Activo).ToList();

            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = listaUsuarios;

            
        }
        private void RefrescarComboRoles()
        {
            UsuarioBLL_65RD gestor = new UsuarioBLL_65RD();
            cmbRol.DataSource = null; 
            cmbRol.DataSource = gestor.ObtenerPerfiles(); 
            cmbRol.DisplayMember = "Nombre";
            cmbRol.ValueMember = "Id";
            cmbRol.SelectedIndex = -1;
        }

        private void frmGestionUsuarios_Load(object sender, EventArgs e)
        {
            dgvUsuarios.AutoGenerateColumns = false;

            
            UsuarioBLL_65RD gestor = new UsuarioBLL_65RD();
            cmbRol.DataSource = gestor.ObtenerPerfiles();
            cmbRol.DisplayMember = "Nombre"; 
            cmbRol.ValueMember = "Id";       
            cmbRol.SelectedIndex = -1;

            CargarUsuarios();
            RefrescarComboRoles();
            IdiomaManager.GetInstance().RegisterObserver(this);
            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnaplicar_Click(object sender, EventArgs e)
        {
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();

            if (string.IsNullOrWhiteSpace(txtnombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDNI.Text) ||
                cmbRol.SelectedIndex == -1)
            {
                string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgDatosIncompletosCuerpo");
                string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgDatosIncompletosTitulo");
                MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (usuarioSeleccionadoId == -1)
            {
                var usuariosExistentes = gestorUsuario.ObtenerUsuarios();
                bool dniDuplicado = usuariosExistentes.Any(u => u.DNI == txtDNI.Text.Trim());

                if (dniDuplicado)
                {
                    string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgDniDuplicadoCuerpo");
                    string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgDniDuplicadoTitulo");
                    MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            if (usuarioSeleccionadoId == -1) 
            {
                Usuario_65RD nuevoUsuario = new Usuario_65RD
                {
                    Nombre = txtnombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    DNI = txtDNI.Text.Trim(),
                    Email = txtemail.Text.Trim(),
                    Contraseña = Seguridad_65RD.Encriptar(txtDNI.Text.Trim()), // Hash 
                    Perfil = new Perfil_65RD { Id = (int)cmbRol.SelectedValue }, 
                    Activo = true,
                    PrimerLogin = true
                };

                bool registrado = gestorUsuario.RegistrarUsuario(nuevoUsuario);
                if (registrado)
                {
                    int idAdminLogueado = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                    BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                    bitacora.RegistrarEvento(idAdminLogueado, "Usuarios", "Alta Usuario", 3, $"Se registró un nuevo usuario: {nuevoUsuario.Apellido}{nuevoUsuario.DNI}");

                    
                }
            }
            else 
            {
               
                Usuario_65RD usuarioModificado = new Usuario_65RD
                {
                    Id = usuarioSeleccionadoId,
                    Email = txtemail.Text.Trim(),
                    Perfil = new Perfil_65RD { Id = (int)cmbRol.SelectedValue }, 
                    Activo = true
                };

                bool actualizado = gestorUsuario.ActualizarUsuario(usuarioModificado);
                if (actualizado)
                {
                    int idAdminLogueado = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                    new BitacoraBLL_65RD().RegistrarEvento(idAdminLogueado, "Usuarios", "Modificar Usuario", 2, $"Se modificó al usuario: {usuarioSeleccionadoNombre}");
                    
                }
            }

            usuarioSeleccionadoId = -1; 
            CargarUsuarios();
            RefrescarComboRoles();
            txtnombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtemail.Text = "";
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
                Usuario_65RD usuarioFila = (Usuario_65RD)dgvUsuarios.Rows[e.RowIndex].DataBoundItem;

                
                usuarioSeleccionadoId = usuarioFila.Id;
                usuarioSeleccionadoNombre = usuarioFila.NombreUsuario;

                
                txtnombre.Text = usuarioFila.Nombre;
                txtApellido.Text = usuarioFila.Apellido;
                txtDNI.Text = usuarioFila.DNI;
                txtemail.Text = usuarioFila.Email;
                cmbRol.SelectedValue = usuarioFila.Perfil.Id;
               
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == SessionManager_65RD.Instancia.UsuarioLogueado.Id)
            {
                string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgOperacionDenegadaCuerpo");
                string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgOperacionDenegadaTitulo");
                MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtemail.Enabled = true;
            cmbRol.Enabled = true;
            txtnombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDNI.Enabled = false;
        }

        private void deshabilitar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == SessionManager_65RD.Instancia.UsuarioLogueado.Id)
            {
                string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgOperacionDenegadaCuerpo");
                string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgOperacionDenegadaTitulo");
                MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (usuarioSeleccionadoId != -1)
            {
                UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
                gestorUsuario.ActualizarEstado(usuarioSeleccionadoId, false);

                int idAdminLogueado = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                bitacora.RegistrarEvento(idAdminLogueado, "Usuarios", "Bloquear Usuario", 3, $"Se deshabilitó al usuario: {usuarioSeleccionadoNombre}");

                string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgUsuarioDeshabilitadoCuerpo");
                string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgInformacionTitulo");
                MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
            else
            {
                string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgSeleccioneUsuarioCuerpo");
                string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgAtencionTitulo");
                MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbtodos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbtodos.Checked)
            {
                cbactivos.Checked = false; 
                CargarUsuarios(false); 
            }
        }

        private void cbactivos_CheckedChanged(object sender, EventArgs e)
        {

            if (cbactivos.Checked)
            {
                cbtodos.Checked = false; 
                CargarUsuarios(true); 
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {

        }

        private void txtnombreUsario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtrol_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId != -1)
            {
                UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
                gestorUsuario.ActualizarEstado(usuarioSeleccionadoId, true);

                int idAdminLogueado = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                bitacora.RegistrarEvento(idAdminLogueado, "Usuarios", "Modificar Usuario", 2, $"Se habilitó al usuario: {usuarioSeleccionadoNombre}");

                string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgUsuarioHabilitadoCuerpo");
                string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgInformacionTitulo");
                MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
            else
            {
                string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgSeleccioneUsuarioCuerpo");
                string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgAtencionTitulo");
                MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgConfirmarSalidaCuerpo");
            string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgConfirmarSalidaTitulo");

            DialogResult confirmacion = MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.OK)
            {
                
                int idUsuarioActual = 0;
                if (SessionManager_65RD.Instancia.UsuarioLogueado != null)
                {
                    idUsuarioActual = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                }

                
                BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                bitacora.RegistrarEvento(idUsuarioActual, "Usuarios", "Logout", 1, "El usuario cerró sesión y salió del sistema");

                
                SessionManager_65RD.Instancia.CerrarSesion();

                
                Application.Exit();
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId != -1)
            {
                UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
                gestorUsuario.ActualizarBloqueo(usuarioSeleccionadoId, false);

                int idAdminLogueado = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                new BitacoraBLL_65RD().RegistrarEvento(idAdminLogueado, "Usuarios", "Desbloquear Usuario", 2, $"Se desbloqueó al usuario: {usuarioSeleccionadoNombre}");

                string msgCuerpo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgUsuarioDesbloqueadoCuerpo");
                string msgTitulo = IdiomaManager.GetInstance().GetTexto(this.Name, "msgExitoTitulo");
                MessageBox.Show(msgCuerpo, msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
        }

        private void frmGestionUsuarios_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().RemoveObserver(this);
        }

        public void UpdateIdioma(string idioma)
        {
            this.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloVentana");

            if (btnModificar != null) btnModificar.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnModificar");
            if (btnHabilitar != null) btnHabilitar.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnHabilitar");
            if (btnNuevo != null) btnNuevo.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnCrear"); 
            if (btnDeshabilitar != null) btnDeshabilitar.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "deshabilitar"); 
            if (btnDesbloquear != null) btnDesbloquear.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnDesbloquear");
            if (btnAplicar != null) btnAplicar.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnaplicar"); 
            if (btnCancelar != null) btnCancelar.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnCancelar"); 
            if (btnCerrarSesion != null) btnCerrarSesion.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnCerrarSesion"); 

            if (cbtodos != null) cbtodos.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "cbtodos");
            if (cbactivos != null) cbactivos.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "cbactivos");

            if (lblGestionUsuarios != null) lblGestionUsuarios.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloPantalla");

            if (label2 != null) label2.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblDni");       
            if (label3 != null) label3.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblApellido");  
            if (label4 != null) label4.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblNombre");   
            if (label5 != null) label5.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblRol");       
            if (label8 != null) label8.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblEmail"); 

            if (dgvUsuarios.Columns.Count >= 8) 
            {
                dgvUsuarios.Columns[0].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "dgvColNombreUsuario");
                dgvUsuarios.Columns[1].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "dgvColNombre");
                dgvUsuarios.Columns[2].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "dgvColApellido");
                dgvUsuarios.Columns[3].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "dgvColDNI");
                dgvUsuarios.Columns[4].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "dgvColRol");
                dgvUsuarios.Columns[5].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "dgvColEmail");
                dgvUsuarios.Columns[6].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "dgvColHabilitado");
                dgvUsuarios.Columns[7].HeaderText = IdiomaManager.GetInstance().GetTexto(this.Name, "dgvColBloqueado");
            }
        }
    }
}
