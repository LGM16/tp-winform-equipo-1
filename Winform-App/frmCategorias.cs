using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_App
{
    public partial class frmCategorias : Form
    {
        private Categoria categoriaSeleccionada = null;

        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                dgvCategorias.DataSource = negocio.listar();
                dgvCategorias.Columns["Id"].Visible = false;
                categoriaSeleccionada = null;
                txtDescripcion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null) return;
            categoriaSeleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
            txtDescripcion.Text = categoriaSeleccionada.Descripcion;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Ingresá una descripción.");
                return;
            }

            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                negocio.agregar(new Categoria { Descripcion = txtDescripcion.Text });
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (categoriaSeleccionada == null)
            {
                MessageBox.Show("Seleccioná una categoría.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Ingresá una descripción.");
                return;
            }

            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                categoriaSeleccionada.Descripcion = txtDescripcion.Text;
                negocio.modificar(categoriaSeleccionada);
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (categoriaSeleccionada == null)
            {
                MessageBox.Show("Seleccioná una categoría.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro querés eliminar la categoría '" + categoriaSeleccionada.Descripcion + "'?",
                "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes) return;

            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                negocio.eliminar(categoriaSeleccionada.Id);
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar. Puede que esté en uso por algún artículo.\n\n" + ex.ToString());
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}