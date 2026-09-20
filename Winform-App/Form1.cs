using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Winform_App
{
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulos;
        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            cargar();
            cargarCombosFiltro();
        }

        private void cargarCombosFiltro()
        {
            List<Marca> marcas = new MarcaNegocio().listar();
            marcas.Insert(0, new Marca { Id = 0, Descripcion = "(Todas)" });
            cboMarca.DataSource = marcas;
            cboMarca.ValueMember = "Id";
            cboMarca.DisplayMember = "Descripcion";

            List<Categoria> categorias = new CategoriaNegocio().listar();
            categorias.Insert(0, new Categoria { Id = 0, Descripcion = "(Todas)" });
            cboCategoria.DataSource = categorias;
            cboCategoria.ValueMember = "Id";
            cboCategoria.DisplayMember = "Descripcion";
        }
        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            if (seleccionado.Imagenes.Count > 0)
                cargarImagen(seleccionado.Imagenes[0].Url);
            else
                pbxArticulo.Image = null;
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulo.DataSource = listaArticulos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvArticulo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

            frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado = new Articulo();
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro de eliminar?", "Eliminar Articulo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString()); ;
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un artículo para ver el detalle.");
                return;
            }
            Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            frmDetalleArticulo detalle = new frmDetalleArticulo(seleccionado);
            detalle.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var codigo = txtCodigo.Text.ToLower();
            var nombre = txtNombre.Text.ToLower();

            var marcaId = (int)cboMarca.SelectedValue;
            var categoriaId = (int)cboCategoria.SelectedValue;

            var filtrado = listaArticulos.Where(a =>
                (string.IsNullOrEmpty(codigo) || a.Codigo.ToLower().Contains(codigo)) &&
                (string.IsNullOrEmpty(nombre) || a.Nombre.ToLower().Contains(nombre)) &&
                (marcaId == 0 || a.Marca.Id == marcaId) &&
                (categoriaId == 0 || a.Categoria.Id == categoriaId)
            ).ToList();

            dgvArticulo.DataSource = filtrado;
     
            if (filtrado.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados para la búsqueda.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgvArticulo.ClearSelection();
                dgvArticulo.Rows[0].Selected = true;
                Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                if (seleccionado.Imagenes.Count > 0)
                    cargarImagen(seleccionado.Imagenes[0].Url);
                else
                    pbxArticulo.Image = null;
            }
            
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            cboMarca.SelectedIndex = 0;
            cboCategoria.SelectedIndex = 0;
            dgvArticulo.DataSource = listaArticulos;
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            frmMarcas marcas = new frmMarcas();
            marcas.ShowDialog();
            cargarCombosFiltro();
            cargar();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            frmCategorias categorias = new frmCategorias();
            categorias.ShowDialog();
            cargarCombosFiltro();
            cargar();
        }

    }
}
