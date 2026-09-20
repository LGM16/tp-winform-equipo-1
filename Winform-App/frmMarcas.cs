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
    public partial class frmMarcas : Form
    {
        private Marca marcaSeleccionada = null;
        public frmMarcas()
        {
            InitializeComponent();
        }

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                dgvMarcas.DataSource = negocio.listar();
                dgvMarcas.Columns["Id"].Visible = false;
                marcaSeleccionada = null;
                txtDescripcion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvMarcas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null) return;
            marcaSeleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
            txtDescripcion.Text = marcaSeleccionada.Descripcion;

        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("La descripción no puede estar vacía.");
                    return;
                }
                Marca nuevaMarca = new Marca();
                nuevaMarca.Descripcion = txtDescripcion.Text;
                negocio.agregar(nuevaMarca);
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (marcaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una marca para modificar.");
                return;
            }
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("La descripción no puede estar vacía.");
                    return;
                }
                marcaSeleccionada.Descripcion = txtDescripcion.Text;
                negocio.modificar(marcaSeleccionada);
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (marcaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una marca para eliminar.");
                return;
            }
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro de eliminar?", "Eliminar Marca", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(marcaSeleccionada.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
