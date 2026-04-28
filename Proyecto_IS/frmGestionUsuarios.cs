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
        }
        private int usuarioSeleccionadoId = -1;

        private void btnCrear_Click(object sender, EventArgs e)
        {
       

            // Limpiar campos
            txtnombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtemail.Text = "";
            txtrol.Text = "";

            // Habilitar campos
            txtnombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDNI.Enabled = true;
            txtemail.Enabled = true;
            txtrol.Enabled = true;

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

            dgvUsuarios.Columns["Contraseña"].Visible = false;
            dgvUsuarios.Columns["IntentosFallidos"].Visible = false;
            dgvUsuarios.Columns["PrimerLogin"].Visible = false;
        }

        private void frmGestionUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnaplicar_Click(object sender, EventArgs e)
        {
            UsuarioBLL_65RD gestorUsuario = new UsuarioBLL_65RD();

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
                    Perfil = (RolUsuario)Enum.Parse(typeof(RolUsuario), txtrol.Text),
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
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                usuarioSeleccionadoId = Convert.ToInt32(fila.Cells["Id"].Value);

                txtnombre.Text = fila.Cells["Nombre"].Value?.ToString();
                txtApellido.Text = fila.Cells["Apellido"].Value?.ToString();
                txtDNI.Text = fila.Cells["DNI"].Value?.ToString();
                txtemail.Text = fila.Cells["Email"].Value?.ToString();
                txtrol.Text = fila.Cells["Rol"].Value?.ToString();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Solo habilitar Email y Rol
            txtemail.Enabled = true;
            txtrol.Enabled = true;

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
    }
}
