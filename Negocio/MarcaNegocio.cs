using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> listaMarcas = new List<Marca>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Id, Descripcion From MARCAS";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Marca cargar = new Marca();
                    cargar.Id = lector.GetInt32(0);
                    cargar.Descripcion = (string)lector["Descripcion"];

                    listaMarcas.Add(cargar);
                }
            }
            catch (Exception excepcion)
            {

                throw excepcion;
            }

            return listaMarcas;
        }
    }
}
