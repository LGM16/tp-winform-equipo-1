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
    }
}
