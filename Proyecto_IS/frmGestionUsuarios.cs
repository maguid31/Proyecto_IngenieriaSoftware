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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_IS
{
    public partial class frmGestionUsuarios : Form
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
            txtnombreUsuario.Text = ""; 

            
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
            var listaUsuarios = gestorUsuario.ObtenerUsuarios(); // trae todos

            if (soloActivos)
                listaUsuarios = listaUsuarios.Where(u => u.Activo).ToList();

            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = listaUsuarios;

            
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
                MessageBox.Show("Por favor, complete todos los datos y seleccione un rol.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
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
                    //  REGISTRO DE LA BITÁCORA 
                    int idAdminLogueado = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                    BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                    bitacora.RegistrarEvento(idAdminLogueado, "Usuarios", "Alta Usuario", 3, $"Se registró un nuevo usuario: {nuevoUsuario.Apellido}{nuevoUsuario.DNI}");

                    
                }
            }
            else // MODIFICAR
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
                txtnombreUsuario.Text = usuarioFila.NombreUsuario;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Solo habilitar email y rol
            txtemail.Enabled = true;
            cmbRol.Enabled = true;

            // Bloquear lo demás
            txtnombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDNI.Enabled = false;
        }

        private void deshabilitar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId != -1)
            {
                UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();
                gestorUsuario.DeshabilitarUsuario(usuarioSeleccionadoId);

                // Registrar en bitácora
                int idAdminLogueado = SessionManager_65RD.Instancia.UsuarioLogueado.Id;
                BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                bitacora.RegistrarEvento(idAdminLogueado, "Usuarios", "Bloquear Usuario", 3, $"Se deshabilitó al usuario: {usuarioSeleccionadoNombre}");

                MessageBox.Show("Usuario deshabilitado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("Seleccione un usuario de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                MessageBox.Show("Usuario habilitado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("Seleccione un usuario de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show(
            "¿Está seguro de que desea cerrar la sesión?",
            "Confirmar salida",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Question
            );

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
    }
}
