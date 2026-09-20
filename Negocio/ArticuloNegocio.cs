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
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio From ARTICULOS");
                datos.ejecutarLectura();

                ImagenNegocio imagenNegocio = new ImagenNegocio();
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                List<Marca> marcas = marcaNegocio.listar();
                List<Categoria> categorias = categoriaNegocio.listar();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = datos.Lector.GetInt32(0);
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    //Todo esto por no hacer JOIN en ArticuloNegocio
                    int idMarca = datos.Lector.GetInt32(4);
                    int idCategoria = datos.Lector.GetInt32(5);

                    aux.Marca = marcas.Find(marca => marca.Id == idMarca);
                    aux.Categoria = categorias.Find(categoria => categoria.Id == idCategoria);

                    aux.Precio = datos.Lector.GetDecimal(6);

                    aux.Imagenes = imagenNegocio.listarPorArticulo(aux.Id);

                    lista.Add(aux);
                }

                return lista;
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

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) " +
                                      "VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio); " +
                                      "SELECT SCOPE_IDENTITY()");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@Precio", nuevo.Precio);

                object idGenerado = datos.ejecutarAccionEscalar();
                nuevo.Id = Convert.ToInt32(idGenerado);

                if (nuevo.Imagenes != null && nuevo.Imagenes.Count > 0)
                {
                    ImagenNegocio imagenNegocio = new ImagenNegocio();
                    foreach (Imagen img in nuevo.Imagenes)
                    {
                        img.IdArticulo = nuevo.Id;
                        imagenNegocio.agregar(img);
                    }
                }
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
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio WHERE Id = @Id");

                datos.setearParametro("@Codigo", modificar.Codigo);
                datos.setearParametro("@Nombre", modificar.Nombre);
                datos.setearParametro("@Descripcion", modificar.Descripcion);
                datos.setearParametro("@IdMarca", modificar.Marca.Id);
                datos.setearParametro("@IdCategoria", modificar.Categoria.Id);
                datos.setearParametro("@Precio", modificar.Precio);
                datos.setearParametro("@Id", modificar.Id);

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

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("Delete From ARTICULOS Where Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
