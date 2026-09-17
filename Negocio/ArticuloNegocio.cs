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
                comando.CommandText = "Select Id, Codigo, Nombre, Descripcion, Precio From ARTICULOS";
                comando.Connection = conexion;
                
                conexion.Open();
                lector = comando.ExecuteReader();

                ImagenNegocio imagenNegocio = new ImagenNegocio();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = lector.GetInt32(0);
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Precio = lector.GetDecimal(4);

                    aux.Imagenes = imagenNegocio.listarPorArticulo(aux.Id);

                    lista.Add(aux);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
    }
}
