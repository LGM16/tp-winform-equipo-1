using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Winform_App
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;

        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar artículo";
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            try
            {
                cboMarca.DataSource = new MarcaNegocio().listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = new CategoriaNegocio().listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;

                    foreach (Imagen img in articulo.Imagenes)
                        lstImagenes.Items.Add(img.Url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtImagenUrl.Text))
                return;

            lstImagenes.Items.Add(txtImagenUrl.Text);
            txtImagenUrl.Clear();

            try
            {
                pbxImagen.Load(lstImagenes.Items[lstImagenes.Items.Count - 1].ToString());
            }
            catch (Exception)
            {
                pbxImagen.Image = null;
            }
        }

        private void lstImagenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstImagenes.SelectedItem == null) return;
            try
            {
                pbxImagen.Load(lstImagenes.SelectedItem.ToString());
            }
            catch (Exception)
            {
                pbxImagen.Image = null;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Articulo art = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            //  validaciones + guardar
            try
            {
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Dominio.Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Dominio.Categoria)cboCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado de manera exitosa", "Atención");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado de manera exitosa", "Atención");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()) ;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}