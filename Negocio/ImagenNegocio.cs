using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listarPorArticulo(int idArticulo)
        {
            List<Imagen> listaImagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select Id, IdArticulo, ImagenUrl From IMAGENES Where IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen cargar = new Imagen();
                    cargar.Id = datos.Lector.GetInt32(0);
                    cargar.IdArticulo = datos.Lector.GetInt32(1);

                    //if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl"))))
                    //    cargar.Url = (string)datos.Lector["ImagenUrl"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        cargar.Url = (string)datos.Lector["ImagenUrl"];

                    listaImagenes.Add(cargar);
                }

                return listaImagenes;
            }
            catch (Exception excepcion)
            {

                throw excepcion;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void agregar(Imagen imagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", imagen.IdArticulo);
                datos.setearParametro("@ImagenUrl", imagen.Url);
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

    }
}
