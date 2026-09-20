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
            AccesoDatos datos = new AccesoDatos();

            //SqlConnection conexion = new SqlConnection();
            //SqlCommand comando = new SqlCommand();
            //SqlDataReader lector;

            try
            {
                datos.setearConsulta("Select Id, Descripcion From MARCAS");
                datos.ejecutarLectura();
                //conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true";
                //comando.CommandType = System.Data.CommandType.Text;
                //comando.CommandText = "Select Id, Descripcion From MARCAS";
                //comando.Connection = conexion;

                //conexion.Open();
                //lector = comando.ExecuteReader();

                while (datos.Lector.Read())
                {
                    Marca cargar = new Marca();
                    cargar.Id = (int)datos.Lector["Id"];
                    cargar.Descripcion = (string)datos.Lector["Descripcion"];

                    listaMarcas.Add(cargar);
                }

                //while (lector.Read())
                //{
                //    Marca cargar = new Marca();
                //    cargar.Id = lector.GetInt32(0);
                //    cargar.Descripcion = (string)lector["Descripcion"];

                //    listaMarcas.Add(cargar);
                //}

                return listaMarcas;
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
        public void agregar(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert Into MARCAS (Descripcion) Values (@Descripcion)");
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
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
        public void modificar(Marca modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update MARCAS Set Descripcion = @Descripcion Where Id = @Id");
                datos.setearParametro("@Descripcion", modificar.Descripcion);
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
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Delete From MARCAS Where Id = @Id");
                datos.setearParametro("@Id", id);
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