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
        private Familia_65RD _familiaEnEdicion = new Familia_65RD();
        private Perfil_65RD _perfilSeleccionado;
        private bool _editandoFamiliaExistente = false; // Flag para saber si hacemos INSERT o UPDATE

        // ── COLORES PARA ESTADOS VISUALES
        private static readonly Color ColorDanger = Color.FromArgb(220, 53, 69);
        private static readonly Color ColorSuccess = Color.SeaGreen;
        private static readonly Color ColorWarning = Color.Orange;

        public frmGestionRoles()
        {
            InitializeComponent();
        }

        private void frmGestionRoles_Load(object sender, EventArgs e)
        {
            IdiomaManager.GetInstance().RegisterObserver(this);

            // Inicializar Tab 1 (Familias)
            CargarPermisosDisponibles();
            CargarFamiliasExistentesCombo();

            // Inicializar Tab 2 (Roles)
            CargarPerfiles();
            CargarPermisosDisponiblesTab2();

            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);
        }

        // ==============================================================================
        //  TAB 1: GESTIÓN DE FAMILIAS (CREAR Y MODIFICAR)
        // ==============================================================================

        private void CargarPermisosDisponibles()
        {
            lbFuentePermisos.Items.Clear();
            var permisos = _permisoBLL.ObtenerTodosLosPermisos();
            foreach (var p in permisos)
            {
                lbFuentePermisos.Items.Add(new ListBoxItemPermiso(p));
            }
        }

        private void CargarFamiliasExistentesCombo()
        {
            cmbFamiliasExistentes.Items.Clear();
            var permisos = _permisoBLL.ObtenerTodosLosPermisos();
            foreach (var p in permisos)
            {
                if (p is Familia_65RD) // Solo mostramos familias para editar
                {
                    cmbFamiliasExistentes.Items.Add(new ListBoxItemPermiso(p));
                }
            }
            cmbFamiliasExistentes.SelectedIndex = -1;
        }




        private void MostrarMensajeFamilia(string msj, Color color)
        {
            lblEstadoFamilia.ForeColor = color;
            lblEstadoFamilia.Text = msj;
        }

        // ==============================================================================
        //  TAB 2: ASIGNACIÓN DE PERFILES A ROLES
        // ==============================================================================

        private void CargarPerfiles()
        {
            cbPerfiles.Items.Clear();
            var perfiles = _perfilBLL.ObtenerTodosLosPerfiles();
            foreach (var p in perfiles)
                cbPerfiles.Items.Add(new ComboBoxItemPerfil(p));
        }

        private void CargarPermisosDisponiblesTab2()
        {
            lbPermisosDisponiblesTab2.Items.Clear();
            var permisos = _permisoBLL.ObtenerTodosLosPermisos();
            foreach (var p in permisos)
                lbPermisosDisponiblesTab2.Items.Add(new ListBoxItemPermiso(p));
        }


        private void MostrarMensajePerfil(string msj, Color color)
        {
            lblEstadoPerfil.ForeColor = color;
            lblEstadoPerfil.Text = msj;
        }

        // ==============================================================================
        //  MÉTODOS AUXILIARES RECURSIVOS PARA EL TREEVIEW Y CLASES EXTRA
        // ==============================================================================

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

        private class ListBoxItemPermiso
        {
            public ComponentePermiso_65RD Permiso { get; }
            public ListBoxItemPermiso(ComponentePermiso_65RD p) { Permiso = p; }
            public override string ToString() => $"{(Permiso is Familia_65RD ? "📁" : "🔑")} {Permiso.Nombre}";
        }

        private class ComboBoxItemPerfil
        {
            public Perfil_65RD Perfil { get; }
            public ComboBoxItemPerfil(Perfil_65RD p) { Perfil = p; }
            public override string ToString() => Perfil.Nombre;
        }

        // ==============================================================================
        //  TRADUCCIÓN (PATRÓN OBSERVER)
        // ==============================================================================

        private void frmGestionRoles_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().RemoveObserver(this);
        }

        public void UpdateIdioma(string idioma)
        {
            // Título general
            this.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloVentana");

            // Podes ir sumando acá el mapeo de textos como hiciste en frmBitacoraEventos
            // ej: btnGuardarFamilia.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnGuardarFamilia");
        }

        private void btnGuardarFamilia_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreFamilia.Text))
            {
                MostrarMensajeFamilia("❌ El nombre de la familia es obligatorio.", ColorDanger);
                return;
            }
            if (tvFamiliaEdicion.Nodes.Count == 0)
            {
                MostrarMensajeFamilia("❌ La familia debe tener al menos un permiso.", ColorDanger);
                return;
            }

            _familiaEnEdicion.Nombre = txtNombreFamilia.Text.Trim();
            _familiaEnEdicion.Descripcion = txtDescFamilia.Text.Trim();

            try
            {
                int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;

                if (_editandoFamiliaExistente)
                {
                    // Llama al método de UPDATE que creamos en el turno anterior
                    _permisoBLL.ModificarFamilia(_familiaEnEdicion);
                    _bitacoraBLL.RegistrarEvento(idUsuario, "Permisos", "Modificación", 2, $"Se modificó la familia '{_familiaEnEdicion.Nombre}'.");
                }
                else
                {
                    // Llama al método de INSERT
                    _permisoBLL.GuardarFamilia(_familiaEnEdicion);
                    _bitacoraBLL.RegistrarEvento(idUsuario, "Permisos", "Alta", 2, $"Creación de familia '{_familiaEnEdicion.Nombre}'.");
                }

                MostrarMensajeFamilia($"✔ Familia guardada exitosamente.", ColorSuccess);
                btnLimpiarFamilia_Click_1(null, null);

                // Refrescar todas las listas del sistema
                CargarPermisosDisponibles();
                CargarFamiliasExistentesCombo();
                CargarPermisosDisponiblesTab2();
            }
            catch (Exception ex)
            {
                MostrarMensajeFamilia($"❌ Error: {ex.Message}", ColorDanger);
            }

        }

        private void cmbFamiliasExistentes_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbFamiliasExistentes.SelectedItem == null) return;

            var itemSeleccionado = (ListBoxItemPermiso)cmbFamiliasExistentes.SelectedItem;

            // Pedimos la familia completa a la base de datos con toda su estructura
            var familiaCompletada = (Familia_65RD)_permisoBLL.ObtenerFamiliaOPatente(itemSeleccionado.Permiso.Id);

            _familiaEnEdicion = familiaCompletada;
            _editandoFamiliaExistente = true;

            txtNombreFamilia.Text = _familiaEnEdicion.Nombre;
            txtDescFamilia.Text = _familiaEnEdicion.Descripcion;

            tvFamiliaEdicion.Nodes.Clear();
            foreach (var hijo in _familiaEnEdicion.ObtenerHijos())
            {
                tvFamiliaEdicion.Nodes.Add(CrearNodoPermiso(hijo));
            }
            tvFamiliaEdicion.ExpandAll();

            lblEstadoFamilia.ForeColor = ColorSuccess;
            lblEstadoFamilia.Text = $"✔ Familia '{_familiaEnEdicion.Nombre}' cargada para su modificación.";
        }

        private void btnLimpiarFamilia_Click_1(object sender, EventArgs e)
        {
            _familiaEnEdicion = new Familia_65RD();
            _editandoFamiliaExistente = false;
            tvFamiliaEdicion.Nodes.Clear();
            txtNombreFamilia.Clear();
            txtDescFamilia.Clear();
            cmbFamiliasExistentes.SelectedIndex = -1;
            lblEstadoFamilia.Text = string.Empty;

        }

        private void btnCrearPerfil_Click_1(object sender, EventArgs e)
        {
            string nombre = txtNuevoPerfil.Text; // Asumiendo que tenés un TextBox txtNuevoPerfil
            if (string.IsNullOrWhiteSpace(nombre)) return;
            try
            {
                _perfilBLL.CrearPerfil(new Perfil_65RD { Nombre = nombre.Trim() });
                txtNuevoPerfil.Clear();
                CargarPerfiles();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cbPerfiles_Click(object sender, EventArgs e)
        {

        }

        private void cbPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvPermisosAsignados.Nodes.Clear();
            lblEstadoPerfil.Text = string.Empty;

            if (cbPerfiles.SelectedItem == null) return;

            _perfilSeleccionado = ((ComboBoxItemPerfil)cbPerfiles.SelectedItem).Perfil;
            foreach (var perm in _perfilSeleccionado.PermisosAsignados)
            {
                tvPermisosAsignados.Nodes.Add(CrearNodoPermiso(perm));
            }
            tvPermisosAsignados.ExpandAll();
        }

        private void btnEliminarPerfil_Click_1(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null) return;

            // Aplicar IdiomaManager para el MessageBox en el futuro
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

        private void btnAsignarPermiso_Click_1(object sender, EventArgs e)
        {
            if (_perfilSeleccionado == null || lbPermisosDisponiblesTab2.SelectedItem == null) return;

            var permiso = ((ListBoxItemPermiso)lbPermisosDisponiblesTab2.SelectedItem).Permiso;

            // Validación anti-repetición de patentes
            bool ok = _perfilBLL.ValidarAsignacionARol(_perfilSeleccionado, permiso);
            if (!ok)
            {
                int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;
                _bitacoraBLL.RegistrarEvento(idUsuario, "Perfiles", "Bloqueo Asignación", 2, $"Colisión de patentes al intentar asignar a rol '{_perfilSeleccionado.Nombre}'.");
                MostrarMensajePerfil($"❌ Colisión de patentes: no se puede asignar.", ColorDanger);
                return;
            }

            _perfilSeleccionado.PermisosAsignados.Add(permiso);
            tvPermisosAsignados.Nodes.Add(CrearNodoPermiso(permiso));
            tvPermisosAsignados.ExpandAll();
            MostrarMensajePerfil($"✔ '{permiso.Nombre}' asignado (pendiente de guardar).", ColorSuccess);

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

        private void btnAgregarNodo_Click_1(object sender, EventArgs e)
        {
            if (lbFuentePermisos.SelectedItem == null)
            {
                MostrarMensajeFamilia("⚠ Seleccioná un permiso de la lista izquierda.", ColorWarning);
                return;
            }

            var permiso = ((ListBoxItemPermiso)lbFuentePermisos.SelectedItem).Permiso;

            // Validación de la BLL
            bool ok = _permisoBLL.AsignarPermisoAFamilia(_familiaEnEdicion, permiso);
            if (!ok)
            {
                MostrarMensajeFamilia($"❌ '{permiso.Nombre}' ya existe en la familia.", ColorDanger);
                return;
            }

            tvFamiliaEdicion.Nodes.Add(CrearNodoPermiso(permiso));
            tvFamiliaEdicion.ExpandAll();
            MostrarMensajeFamilia($"✔ '{permiso.Nombre}' agregado a la estructura.", ColorSuccess);
        }

        private void btnQuitarNodo_Click(object sender, EventArgs e)
        {
            if (tvFamiliaEdicion.SelectedNode == null)
            {
                MostrarMensajeFamilia("⚠ Seleccioná un nodo raíz del árbol para quitar.", ColorWarning);
                return;
            }

            var nodo = tvFamiliaEdicion.SelectedNode;
            if (nodo.Parent != null)
            {
                MostrarMensajeFamilia("⚠ No podés quitar sub-nodos. Quitá la familia raíz completa.", ColorWarning);
                return;
            }

            var permiso = (ComponentePermiso_65RD)nodo.Tag;
            nodo.Remove();

            // Re-sincronizar memoria con lo que quedó en el árbol
            _familiaEnEdicion.VaciarHijos();
            foreach (TreeNode n in tvFamiliaEdicion.Nodes)
            {
                if (n.Tag is ComponentePermiso_65RD comp)
                    _familiaEnEdicion.AgregarHijo(comp);
            }

            MostrarMensajeFamilia($"✔ '{permiso.Nombre}' eliminado de la estructura.", ColorSuccess);
        }
    }
}



