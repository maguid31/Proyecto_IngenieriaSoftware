using BLL;
using BLL_65RD;
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

        private void btnCrear_Click(object sender, EventArgs e)
        {
       

            // Limpiar campos
            txtnombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtemail.Text = "";
            cmbRol.Text = "";
            txtnombreUsuario.Text = ""; 

            // Habilitar campos
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

            cmbRol.Items.Clear();
            cmbRol.Items.Add("Basico");
            cmbRol.Items.Add("Administrador");

            CargarUsuarios();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnaplicar_Click(object sender, EventArgs e)
        {
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();

            // --- VALIDACIÓN DE CAMPOS ---
            if (string.IsNullOrWhiteSpace(txtnombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDNI.Text) ||
                cmbRol.SelectedIndex == -1) 
            {
                MessageBox.Show("Por favor, complete todos los datos y seleccione un rol.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Corta la ejecución para que no guarde nada
            }

            if (usuarioSeleccionadoId == -1) // CREAR
            {
                Usuario_65RD nuevoUsuario = new Usuario_65RD
                {
                    Nombre = txtnombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    DNI = txtDNI.Text.Trim(),
                    Email = txtemail.Text.Trim(),
                    Contraseña = Seguridad_65RD.Encriptar(txtDNI.Text.Trim()),
                    Perfil = RolUsuario.Basico,
                    Activo = true,
                    IntentosFallidos = 0,
                    PrimerLogin = true
                };

                bool registrado = gestorUsuario.RegistrarUsuario(nuevoUsuario);
                if (registrado) MessageBox.Show("Usuario creado correctamente.");
            }
            else // MODIFICAR
            {
                Usuario_65RD usuarioModificado = new Usuario_65RD
                {
                    Id = usuarioSeleccionadoId,
                    Email = txtemail.Text.Trim(),
                    Perfil = (RolUsuario)Enum.Parse(typeof(RolUsuario), cmbRol.Text),
                    Activo = true
                };

                bool actualizado = gestorUsuario.ActualizarUsuario(usuarioModificado);
                if (actualizado) MessageBox.Show("Usuario modificado correctamente.");
            }

            usuarioSeleccionadoId = -1; // reset
            CargarUsuarios();
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Extraemos el usuario completo a
                Usuario_65RD usuarioFila = (Usuario_65RD)dgvUsuarios.Rows[e.RowIndex].DataBoundItem;

                // Guardamos el ID real
                usuarioSeleccionadoId = usuarioFila.Id;

                // Bajamos los datos a los casilleros
                txtnombre.Text = usuarioFila.Nombre;
                txtApellido.Text = usuarioFila.Apellido;
                txtDNI.Text = usuarioFila.DNI;
                txtemail.Text = usuarioFila.Email;
                cmbRol.Text = usuarioFila.Perfil.ToString();
                txtnombreUsuario.Text = usuarioFila.NombreUsuario;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Solo habilitar Email y Rol
            txtemail.Enabled = true;
            cmbRol.Enabled = true;

            // Bloquear los demás campos
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
                BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                bitacora.RegistrarEvento(usuarioSeleccionadoId, "Usuario deshabilitado", "El usuario fue marcado como inactivo");

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
                cbactivos.Checked = false; // desmarcar el otro
                CargarUsuarios(false); // traer todos
            }
        }

        private void cbactivos_CheckedChanged(object sender, EventArgs e)
        {

            if (cbactivos.Checked)
            {
                cbtodos.Checked = false; // desmarcar el otro
                CargarUsuarios(true); // traer solo activos
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

                BitacoraBLL_65RD bitacora = new BitacoraBLL_65RD();
                bitacora.RegistrarEvento(usuarioSeleccionadoId, "Usuario habilitado", "El usuario fue marcado como activo nuevamente");

                MessageBox.Show("Usuario habilitado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("Seleccione un usuario de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
