using System;
using System.Windows.Forms;
using Dominio;

namespace Winform_App
{
    public partial class frmDetalleArticulo : Form
    {
        private Articulo articulo;
        private int indiceImagen = 0;

        public frmDetalleArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmDetalleArticulo_Load(object sender, EventArgs e)
        {
            lblCodigoValor.Text = articulo.Codigo;
            lblNombreValor.Text = articulo.Nombre;
            lblDescripcionValor.Text = articulo.Descripcion;
            lblMarcaValor.Text = articulo.Marca != null ? articulo.Marca.Descripcion : "-";
            lblCategoriaValor.Text = articulo.Categoria != null ? articulo.Categoria.Descripcion : "-";
            lblPrecioValor.Text = articulo.Precio.ToString("C");

            if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
                mostrarImagen();
        }

        private void mostrarImagen()
        {
            try
            {
                pbxImagen.Load(articulo.Imagenes[indiceImagen].Url);
                lblContador.Text = (indiceImagen + 1) + " / " + articulo.Imagenes.Count;
            }
            catch (Exception)
            {
                pbxImagen.Image = null;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (articulo.Imagenes == null || articulo.Imagenes.Count == 0) return;
            if (indiceImagen > 0) indiceImagen--;
            else indiceImagen = articulo.Imagenes.Count - 1;
            mostrarImagen();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (articulo.Imagenes == null || articulo.Imagenes.Count == 0) return;
            if (indiceImagen < articulo.Imagenes.Count - 1) indiceImagen++;
            else indiceImagen = 0;
            mostrarImagen();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}