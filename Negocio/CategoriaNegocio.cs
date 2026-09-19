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
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Id, Descripcion From CATEGORIAS";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Categoria cargar = new Categoria();

                    cargar.Id = lector.GetInt32(0);
                    cargar.Descripcion = (string)lector["Descripcion"];

                    listaCategorias.Add(cargar);
                }

                conexion.Close();
                return listaCategorias;
            }
            catch (Exception excepcion)
            {

                throw excepcion;
            }

        }
    }
}
