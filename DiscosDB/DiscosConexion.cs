using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DiscosDB
{
    internal class DiscosConexion
    {
        public List<Disco> charts()
        {
            List<Disco> lista = new List<Disco>(); // En esta lista de tipo "Disco" (Object) almaceno los objetos del registro
            
            SqlConnection conexion = new SqlConnection(); // Permite establecer la conexion con la base de datos
            SqlCommand comando = new SqlCommand(); // Permite realizar las consulta SQL
            SqlDataReader lector; // permite leer los registros de la base de datos
            try
            {
                //1.Establezco con la conexion con la base de datos
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";
                //2.Realizo la consulta SQL
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT D.Titulo, D.FechaLanzamiento, D.CantidadCanciones, D.UrlImagenTapa, E.Descripcion AS Estilo, TE.Descripcion AS Formato FROM DISCOS D, ESTILOS E, TIPOSEDICION TE WHERE E.Id = D.IdEstilo AND TE.Id = D.IdTipoEdicion;";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while(lector.Read()) // devuelve un valor true si encuentro un reg | false si no
                {
                    Disco aux = new Disco();
                    aux.Titulo = (string)lector["Titulo"];
                    //fechas
                    string fechaString = lector["FechaLanzamiento"].ToString();
                    aux.Fecha = Convert.ToDateTime(fechaString);
                    //demas propiedades
                    aux.CantidadCanciones = (int)lector["CantidadCanciones"];
                    aux.UrlImagenTapa = (string)lector["UrlImagenTapa"];
                    aux.Estilo = (string)lector["Estilo"];
                    aux.Formato = (string)lector["Formato"];

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
                conexion.Close();
            }
        }
    }
}
