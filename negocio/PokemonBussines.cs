using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class PokemonBussines
    {
        public List<Pokemon> Listar()
        {
            List<Pokemon> list = new List<Pokemon>();
            //1.Instanciar los objetos para conectar la base de datos
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                //2. establezco la conexion a mi base de datos 1.basedatos; 2.que base 3.forma de conectarse
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT P.Numero, P.Nombre, P.Descripcion, P.UrlImagen, E.Descripcion AS Tipo, D.Descripcion AS Debilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.id = P.IdTipo AND D.Id = P.IdDebilidad;\r\nSELECT P.Numero, P.Nombre, P.Descripcion, P.UrlImagen, E.Descripcion AS Tipo, D.Descripcion AS Debilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.id = P.IdTipo AND D.Id = P.IdDebilidad;\r\n";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();
 
                while(lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.UrlImagen = (string)lector["UrlImagen"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];

                    list.Add(aux);
                }

                return list;
            }
            catch (Exception exc)
            {
                throw exc;
            }

            finally
            {
                conexion.Close(); 
            }
        }

        public void agregar(Pokemon poke)
        {
            AccesoDato acceso = new AccesoDato();
            try
            { 
                acceso.setearConsulta("INSERT INTO POKEMONS(Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad) VALUES ('" + poke.Numero + "', '" + poke.Nombre + "', '" + poke.Descripcion + "', 1, @idTipo, @idDebilidad)");
                acceso.setearParaemtro("@idTipo", poke.Tipo.Id);
                acceso.setearParaemtro("@idDebilidad", poke.Debilidad.Id);
                acceso.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                acceso.cerrarConexion();
            }
        }
    }
}
