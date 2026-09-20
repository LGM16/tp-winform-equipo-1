using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
       public List<Categoria> listar()
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion From CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria cargar = new Categoria();

                    cargar.Id = datos.Lector.GetInt32(0);
                    cargar.Descripcion = (string)datos.Lector["Descripcion"];

                    listaCategorias.Add(cargar);
                }

                return listaCategorias;
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
