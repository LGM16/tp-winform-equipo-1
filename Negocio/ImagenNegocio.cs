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
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Id, IdArticulo, ImagenUrl From IMAGENES Where IdArticulo = " + idArticulo;
                comando.Connection = conexion;
                
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Imagen cargar = new Imagen();
                    cargar.Id = lector.GetInt32(0);
                    cargar.IdArticulo = lector.GetInt32(1);
                    cargar.Url = (string)lector["ImagenUrl"];

                    listaImagenes.Add(cargar);
                }
            }
            catch (Exception excepcion)
            {

                throw excepcion;
            }

                return listaImagenes;
        }
    }
}
