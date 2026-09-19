using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio From ARTICULOS";
                comando.Connection = conexion;
                
                conexion.Open();
                lector = comando.ExecuteReader();

                ImagenNegocio imagenNegocio = new ImagenNegocio();
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                List<Marca> marcas = marcaNegocio.listar();
                List<Categoria> categorias = categoriaNegocio.listar();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = lector.GetInt32(0);
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];

                    //Todo esto por no hacer JOIN en ArticuloNegocio
                    int idMarca = lector.GetInt32(4);
                    int idCategoria = lector.GetInt32(5);

                    aux.Marca = marcas.Find(marca => marca.Id == idMarca);
                    aux.Categoria = categorias.Find(categoria => categoria.Id == idCategoria);
                    
                    aux.Precio = lector.GetDecimal(6);

                    aux.Imagenes = imagenNegocio.listarPorArticulo(aux.Id);

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void agregar (Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("");
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar (Articulo modificar)
        {

        }
    }
}
