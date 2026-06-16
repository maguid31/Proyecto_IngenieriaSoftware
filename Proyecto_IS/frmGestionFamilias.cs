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
    public partial class frmGestionFamilias : Form, IidiomaObserver
    {

        private readonly PermisoBLL_65RD _permisoBLL = new PermisoBLL_65RD();
        private readonly BitacoraBLL_65RD _bitacoraBLL = new BitacoraBLL_65RD();

        // ── VARIABLES DE CONTROL DE ESTADO
        private Familia_65RD _familiaSeleccionada;
        private List<Familia_65RD> _familiasCompletas = new List<Familia_65RD>();

        private Familia_65RD _familiaEnEdicion = new Familia_65RD();
        private bool _editandoFamiliaExistente = false;

        // ── PALETA DE COLORES PARA MENSAJES
        private static readonly Color ColorDanger = Color.FromArgb(220, 53, 69);
        private static readonly Color ColorSuccess = Color.SeaGreen;
        private static readonly Color ColorWarning = Color.Orange;

        public frmGestionFamilias()
        {
            InitializeComponent();
            txtBuscarFamilia.TextChanged += txtBuscarFamilia_TextChanged;
        }

        private void frmGestionFamilias_Load(object sender, EventArgs e)
        {
            IdiomaManager.GetInstance().RegisterObserver(this);
            CargarPermisosDisponibles();
            CargarFamilias();
            UpdateIdioma(IdiomaManager.GetInstance().IdiomaActual);
        }

        // ── 1. MÉTODOS DE CARGA Y FILTRADO
        private void CargarPermisosDisponibles()
        {
            lbFuentePermisos.Items.Clear();
            var permisos = _permisoBLL.ObtenerTodosLosPermisos();
            foreach (var p in permisos)
            {
                lbFuentePermisos.Items.Add(new ListBoxItemPermiso(p));
            }
        }

        private void CargarFamilias()
        {
            var todos = _permisoBLL.ObtenerTodosLosPermisos();
            _familiasCompletas = todos.OfType<Familia_65RD>().ToList() ?? new List<Familia_65RD>();
            FiltrarYMostrarFamilias();
        }

        private void FiltrarYMostrarFamilias()
        {
            lbFamilias.Items.Clear();

            // Si por alguna razón la lista está nula, salimos para evitar errores
            if (_familiasCompletas == null) return;

            // Tomamos el filtro eliminando espacios extras
            string filtro = txtBuscarFamilia != null ? txtBuscarFamilia.Text.Trim().ToLower() : string.Empty;

            // Filtramos de forma segura asegurando que el nombre no sea nulo
            var filtradas = _familiasCompletas
                .Where(f => f != null && (string.IsNullOrEmpty(filtro) || (f.Nombre != null && f.Nombre.ToLower().Contains(filtro))));

            foreach (var fam in filtradas)
            {
                lbFamilias.Items.Add(new ListBoxItemFamilia(fam));
            }

            // 🌟 PRUEBA DE CONTROL: Si sigue vacía, tiramos un aviso interno rápido
            if (lbFamilias.Items.Count == 0 && string.IsNullOrEmpty(filtro))
            {
                // Esto te va a avisar si la BLL realmente devolvió familias o vino vacío desde la base de datos
                System.Diagnostics.Debug.WriteLine("⚠️ Control: _familiasCompletas no tiene elementos tipo Familia_65RD.");
            }
        }

        private void txtBuscarFamilia_TextChanged(object sender, EventArgs e)
        {
            FiltrarYMostrarFamilias();
        }

        // ── 2. SELECCIÓN DE FAMILIA (AL HACER CLIC SE MUESTRAN LOS PERMISOS EN EL TREEVIEW)
        private void lbFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvFamiliaEdicion.Nodes.Clear();
            lblDescripcionFamilia.Text = string.Empty;

            if (lbFamilias.SelectedItem == null)
            {
                _familiaSeleccionada = null;
                return;
            }

            // Rescatamos la familia de la ListBox de forma segura
            _familiaSeleccionada = ((ListBoxItemFamilia)lbFamilias.SelectedItem).Familia;

            // La cargamos completa desde la BLL con sus hijos reales
            var familiaCompletada = (Familia_65RD)_permisoBLL.ObtenerFamiliaOPatente(_familiaSeleccionada.Id);

            _familiaEnEdicion = familiaCompletada;
            _editandoFamiliaExistente = true;

            // Llenamos los cuadros de texto
            txtNombreFamilia.Text = _familiaEnEdicion.Nombre;
            txtDescFamilia.Text = _familiaEnEdicion.Descripcion;

            if (!string.IsNullOrWhiteSpace(_familiaEnEdicion.Descripcion))
                lblDescripcionFamilia.Text = $"📝 Descripción: {_familiaEnEdicion.Descripcion}";
            else
                lblDescripcionFamilia.Text = "📝 Sin descripción disponible.";

            // Dibujamos los hijos en el TreeView
            foreach (var hijo in _familiaEnEdicion.ObtenerHijos())
            {
                tvFamiliaEdicion.Nodes.Add(CrearNodoPermiso(hijo));
            }
            tvFamiliaEdicion.ExpandAll();

            MostrarMensajeFamilia($"✔ Familia '{_familiaEnEdicion.Nombre}' cargada.", ColorSuccess);
        }

        // ── 3. BOTÓN AGREGAR FAMILIA (METE EL PERMISO SELECCIONADO EN EL ARBOL)
        private void btnAgregarFamilia_Click(object sender, EventArgs e)
        {
            if (lbFuentePermisos.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccioná un permiso de la lista de la izquierda para agregar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 🌟 CORRECCIÓN: Desempaquetamos correctamente el permiso usando el Wrapper ListBoxItemPermiso
            ComponentePermiso_65RD permisoSeleccionado = ((ListBoxItemPermiso)lbFuentePermisos.SelectedItem).Permiso;

            if (_editandoFamiliaExistente && _familiaEnEdicion.Id == permisoSeleccionado.Id)
            {
                MessageBox.Show("❌ No podés agregar una familia a sí misma.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_familiaEnEdicion == null)
            {
                _familiaEnEdicion = new Familia_65RD();
            }

            // Validamos que no esté duplicado mediante la BLL o el objeto en memoria
            if (_permisoBLL.ExistePermisoEnFamilia(_familiaEnEdicion, permisoSeleccionado.Id))
            {
                MessageBox.Show($"El permiso '{permisoSeleccionado.Nombre}' ya forma parte de esta familia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregamos al objeto y al árbol visual
            _familiaEnEdicion.AgregarHijo(permisoSeleccionado);

            TreeNode nuevoNodo = CrearNodoPermiso(permisoSeleccionado);
            tvFamiliaEdicion.Nodes.Add(nuevoNodo);
            tvFamiliaEdicion.ExpandAll();
        }

        // ── 4. BOTÓN GUARDAR CAMBIOS / NUEVA FAMILIA
        private void btnGuardarFamilia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreFamilia.Text))
            {
                MessageBox.Show("❌ El nombre de la familia es obligatorio.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tvFamiliaEdicion.Nodes.Count == 0)
            {
                MessageBox.Show("❌ La familia debe tener al menos un permiso.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _familiaEnEdicion.Nombre = txtNombreFamilia.Text.Trim();
            _familiaEnEdicion.Descripcion = txtDescFamilia.Text.Trim();

            try
            {
                int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;

                if (_editandoFamiliaExistente)
                {
                    _permisoBLL.ModificarFamilia(_familiaEnEdicion);
                    _bitacoraBLL.RegistrarEvento(idUsuario, "Permisos", "Modificación", 2, $"Se modificó la familia '{_familiaEnEdicion.Nombre}'.");
                }
                else
                {
                    _permisoBLL.GuardarFamilia(_familiaEnEdicion);
                    _bitacoraBLL.RegistrarEvento(idUsuario, "Permisos", "Alta", 2, $"Creación de familia '{_familiaEnEdicion.Nombre}'.");
                }

                MessageBox.Show("✔ Familia guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLimpiarFamilia_Click(null, null); // Forzamos la limpieza

                CargarPermisosDisponibles();
                CargarFamilias();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── 5. BOTÓN ELIMINAR FAMILIA
        private void btnEliminarFamilia_Click(object sender, EventArgs e)
        {
            if (_familiaEnEdicion == null || !_editandoFamiliaExistente)
            {
                MessageBox.Show("Por favor, selecciona primero una familia existente de la lista para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var res = MessageBox.Show($"¿Confirmas que querés eliminar permanentemente la familia '{_familiaEnEdicion.Nombre}'?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res != DialogResult.Yes) return;

            try
            {
                _permisoBLL.EliminarFamiliaBLL(_familiaEnEdicion.Id);

                int idUsuario = SessionManager_65RD.Instancia.UsuarioLogueado?.Id ?? 0;
                _bitacoraBLL.RegistrarEvento(idUsuario, "Permisos", "Baja", 2, $"Se eliminó la familia '{_familiaEnEdicion.Nombre}'.");

                btnLimpiarFamilia_Click(null, null);
                CargarFamilias();

                MessageBox.Show("Familia eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── 6. BOTÓN LIMPIAR / RESET TOTAL
        private void btnLimpiarFamilia_Click(object sender, EventArgs e)
        {
            _familiaEnEdicion = new Familia_65RD();
            _editandoFamiliaExistente = false;
            _familiaSeleccionada = null;

            tvFamiliaEdicion.Nodes.Clear();
            txtNombreFamilia.Clear();
            txtDescFamilia.Clear();
            lbFamilias.ClearSelected();

            lblDescripcionFamilia.Text = "📝 Modo Creación: Nueva Familia";
        }

        // ── MÉTODOS RECURSIVOS AUXILIARES PARA EL TREEVIEW
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

        private void MostrarMensajeFamilia(string msj, Color color)
        {
            // Opcional para pintar estados en la UI
        }

        private void frmGestionFamilias_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.GetInstance().RemoveObserver(this);
        }

       

        // WRAPPERS INTERNOS DE SOPORTE PARA LAS LISTBOX
        private class ListBoxItemPermiso
        {
            public ComponentePermiso_65RD Permiso { get; }
            public ListBoxItemPermiso(ComponentePermiso_65RD p) { Permiso = p; }
            public override string ToString() => $"{(Permiso is Familia_65RD ? "📁" : "🔑")} {Permiso.Nombre}";
        }

        private class ListBoxItemFamilia
        {
            public Familia_65RD Familia { get; }
            public ListBoxItemFamilia(Familia_65RD f) { Familia = f; }
            public override string ToString() => $"📁 {Familia.Nombre}";
        }

        public void UpdateIdioma(string idioma)
        {
           
            this.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblTituloVentana");

            
            btnAgregarFamilia.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnAgregarFamilia");
            btnGuardarFamilia.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnGuardarFamilia");
            btnEliminarFamilia.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnEliminarFamilia");
            btnLimpiarFamilia.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "btnLimpiarFamilia");

            
            label2.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblNombreFamilia");
            label1.Text = IdiomaManager.GetInstance().GetTexto(this.Name, "lblDescFamilia");
        }
    }
}



