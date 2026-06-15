using BLL;
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
    public partial class frmGestionRoles : Form, IidiomaObserver
    {
        // ── CAPAS BLL
        private readonly PermisoBLL_65RD _permisoBLL = new PermisoBLL_65RD();
        private readonly PerfilBLL_65RD _perfilBLL = new PerfilBLL_65RD();
        private readonly BitacoraBLL_65RD _bitacoraBLL = new BitacoraBLL_65RD();

        // ── ESTADOS EN MEMORIA
        
        private Perfil_65RD _perfilSeleccionado;
        private List<Perfil_65RD> _perfilesCompletos = new List<Perfil_65RD>();


        // ── COLORES PARA ESTADOS VISUALES
        private static readonly Color ColorDanger = Color.FromArgb(220, 53, 69);
        private static readonly Color ColorSuccess = Color.SeaGreen;
        private static readonly Color ColorWarning = Color.Orange;

        public frmGestionRoles()
        {
            InitializeComponent();
            txtBuscarPerfil.TextChanged += txtBuscarPerfil_TextChanged; // 👈 ¡Meté esta línea acá!
        }

        private void frmGestionRoles_Load(object sender, EventArgs e)
        {
            IdiomaManager.GetInstance().RegisterObserver(this);

            CargarPerfiles();
            CargarPermisosDisponiblesTab2();

            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);

        }

        private void CargarPerfiles()
        {
            _perfilesCompletos = _perfilBLL.ObtenerTodosLosPerfiles() ?? new List<Perfil_65RD>();
            FiltrarYMostrarPerfiles();
        }
        private void FiltrarYMostrarPerfiles()
        {
            lbPerfiles.Items.Clear();
            string filtro = txtBuscarPerfil.Text.Trim().ToLower();

            // Filtramos la lista en memoria usando LINQ
            var perfilesFiltrados = _perfilesCompletos
                .Where(p => string.IsNullOrEmpty(filtro) || p.Nombre.ToLower().Contains(filtro));

            foreach (var p in perfilesFiltrados)
            {
                lbPerfiles.Items.Add(new ListBoxItemPerfil(p));
            }
        }
        private void txtBuscarPerfil_TextChanged(object sender, EventArgs e)
        {
            FiltrarYMostrarPerfiles();
        }

        private void lbPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvPermisosAsignados.Nodes.Clear();
            lblEstadoPerfil.Text = string.Empty;

            if (lbPerfiles.SelectedItem == null)
            {
                _perfilSeleccionado = null;
                return;
            }

            _perfilSeleccionado = ((ListBoxItemPerfil)lbPerfiles.SelectedItem).Perfil;

            if (!string.IsNullOrWhiteSpace(_perfilSeleccionado.Descripcion))
            {
                lblDescripcionPerfil.Text = $"📝 Descripciones: {_perfilSeleccionado.Descripcion}";
            }
            else
            {
                lblDescripcionPerfil.Text = "📝 Sin descripción disponible.";
            }

            foreach (var perm in _perfilSeleccionado.PermisosAsignados)
            {
                tvPermisosAsignados.Nodes.Add(CrearNodoPermiso(perm));
            }
            tvPermisosAsignados.ExpandAll();
        }

        private void CargarPermisosDisponiblesTab2()
        {
            lbPermisosDisponiblesTab2.Items.Clear();
            var permisos = _permisoBLL.ObtenerTodosLosPermisos();
            foreach (var p in permisos)
                lbPermisosDisponiblesTab2.Items.Add(new ListBoxItemPermiso(p));
        }

      

        private void btnCrearPerfil_Click(object sender, EventArgs e)
        {
            string nombre = txtNuevoPerfil.Text;
            string descripcion = txtDescripcion.Text;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el perfil.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tvPermisosAsignados.Nodes.Count == 0)
            {
                MessageBox.Show("No podés crear el perfil vacío. Primero seleccioná un permiso de la izquierda, hacé clic en 'Asignar Permiso' para pasarlo al árbol, y luego creá el perfil.", "Operación Denegada", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            try
            {
                var nuevoPerfil = new Perfil_65RD
                {
                    Nombre = nombre.Trim(),
                    Descripcion = descripcion.Trim()
                };

                foreach (TreeNode nodo in tvPermisosAsignados.Nodes)
                {
                    if (nodo.Tag is ComponentePermiso_65RD permiso)
                    {
                        nuevoPerfil.PermisosAsignados.Add(permiso);
                    }
                }

                _perfilBLL.CrearPerfil(nuevoPerfil);
                txtNuevoPerfil.Clear();
                txtDescripcion.Clear();
                txtBuscarPerfil.Clear();
                tvPermisosAsignados.Nodes.Clear(); 

                CargarPerfiles(); 

                MessageBox.Show($"✔ El perfil '{nombre}' fue creado y guardado con sus permisos exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el perfil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null && lbPerfiles.SelectedItem != null)
            {
                _perfilSeleccionado = ((ListBoxItemPerfil)lbPerfiles.SelectedItem).Perfil;
            }
             
            if (_perfilSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccioná primero un perfil de la lista para poder eliminarlo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var res = MessageBox.Show($"¿Eliminar perfil '{_perfilSeleccionado.Nombre}'?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res != DialogResult.Yes) return;

            bool ok = _perfilBLL.IntentarEliminarPerfil(_perfilSeleccionado.Id, out string error);
            if (ok)
            {
                int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;
                _bitacoraBLL.RegistrarEvento(idUsuario, "Perfiles", "Baja", 2, $"Eliminación del rol '{_perfilSeleccionado.Nombre}'.");

                lblEstadoPerfil.Text = string.Empty;
                tvPermisosAsignados.Nodes.Clear();
                _perfilSeleccionado = null;
                CargarPerfiles();
            }
            else
            {
                MessageBox.Show(error, "Error Operación Denegada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsignarPermiso_Click(object sender, EventArgs e)
        {
            if (lbPermisosDisponiblesTab2.SelectedItem == null) return;

            var permiso = ((ListBoxItemPermiso)lbPermisosDisponiblesTab2.SelectedItem).Permiso;

            if (_perfilSeleccionado != null)
            {
                bool ok = _perfilBLL.ValidarAsignacionARol(_perfilSeleccionado, permiso);
                if (!ok)
                {
                    int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;
                    _bitacoraBLL.RegistrarEvento(idUsuario, "Perfiles", "Bloqueo Asignación", 2, $"Colisión de patentes al intentar asignar a rol '{_perfilSeleccionado.Nombre}'.");
                    MostrarMensajePerfil($"❌ Colisión de patentes: no se puede asignar.", ColorDanger);
                    return;
                }

                _perfilSeleccionado.PermisosAsignados.Add(permiso);
                MostrarMensajePerfil($"✔ '{permiso.Nombre}' asignado (pendiente de guardar).", ColorSuccess);
            }
            tvPermisosAsignados.Nodes.Add(CrearNodoPermiso(permiso));
            tvPermisosAsignados.ExpandAll();
        }

        private void btnQuitarPermiso_Click(object sender, EventArgs e)
        {
            if (tvPermisosAsignados.SelectedNode == null) return;

            var nodo = tvPermisosAsignados.SelectedNode;
            if (nodo.Parent != null)
            {
                MostrarMensajePerfil("⚠ Solo podés quitar permisos raíz del perfil.", ColorWarning);
                return;
            }

            var permiso = (ComponentePermiso_65RD)nodo.Tag;
            _perfilSeleccionado.PermisosAsignados.Remove(permiso);
            nodo.Remove();
            MostrarMensajePerfil($"✔ '{permiso.Nombre}' quitado (pendiente de guardar).", ColorSuccess);
        }

        private void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;

            try
            {
                bool ok = _perfilBLL.GuardarAsignacionPermisos(_perfilSeleccionado);
                if (ok)
                {
                    int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;
                    _bitacoraBLL.RegistrarEvento(idUsuario, "Perfiles", "Modificación", 2, $"Actualización de permisos del rol '{_perfilSeleccionado.Nombre}'.");
                    MostrarMensajePerfil($"✔ Permisos guardados exitosamente.", ColorSuccess);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajePerfil($"❌ Error: {ex.Message}", ColorDanger);
            }
        }

        private TreeNode CrearNodoPermiso(ComponentePermiso_65RD perm)
        {
            var nodo = new TreeNode($"{(perm is Familia_65RD ? "📁" : "🔑")} {perm.Nombre}") { Tag = perm };
            if (perm is Familia_65RD fam)
                AgregarHijosAlNodo(nodo, fam);
            return nodo;
        }

        private void AgregarHijosAlNodo(TreeNode nodoPadre, Familia_65RD familia)
        {
            foreach (var hijo in familia.ObtenerHijos())
            {
                var nodoHijo = new TreeNode($"{(hijo is Familia_65RD ? "📁" : "🔑")} {hijo.Nombre}") { Tag = hijo };
                if (hijo is Familia_65RD subFam)
                    AgregarHijosAlNodo(nodoHijo, subFam);
                nodoPadre.Nodes.Add(nodoHijo);
            }
        }

        private void MostrarMensajePerfil(string msj, Color color)
        {
            lblEstadoPerfil.ForeColor = color;
            lblEstadoPerfil.Text = msj;
        }

        private void frmGestionRoles_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().RemoveObserver(this);
        }

        public void UpdateIdioma(string idioma)
        {
            this.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloVentana");
        }

        private class ListBoxItemPermiso
        {
            public ComponentePermiso_65RD Permiso { get; }
            public ListBoxItemPermiso(ComponentePermiso_65RD p) { Permiso = p; }
            public override string ToString() => $"{(Permiso is Familia_65RD ? "📁" : "🔑")} {Permiso.Nombre}";
        }


        private class ListBoxItemPerfil
        {
            public Perfil_65RD Perfil { get; }
            public ListBoxItemPerfil(Perfil_65RD p) { Perfil = p; }
            public override string ToString() => Perfil.Nombre;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscarPerfil_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void lbPerfiles_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            tvPermisosAsignados.Nodes.Clear();
            lblEstadoPerfil.Text = string.Empty;
            lblDescripcionPerfil.Text = string.Empty;

            if (lbPerfiles.SelectedItem == null)
            {
                _perfilSeleccionado = null;
                return;
            }

            _perfilSeleccionado = ((ListBoxItemPerfil)lbPerfiles.SelectedItem).Perfil;

            if (!string.IsNullOrWhiteSpace(_perfilSeleccionado.Descripcion))
            {
                lblDescripcionPerfil.Text = $"📝 Descripción: {_perfilSeleccionado.Descripcion}";
            }
            else
            {
                lblDescripcionPerfil.Text = "📝 Sin descripción disponible.";
            }

            if (_perfilSeleccionado.PermisosAsignados != null)
            {
                foreach (var perm in _perfilSeleccionado.PermisosAsignados)
                {
                    tvPermisosAsignados.Nodes.Add(CrearNodoPermiso(perm));
                }
            }

            tvPermisosAsignados.ExpandAll();
        }
    }
}



