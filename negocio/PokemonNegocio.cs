using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class PokemonNegocio
    {

        public List<Pokemon> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select P.Id, Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad From POKEMONS P, ELEMENTOS E, ELEMENTOS D Where E.Id = P.IdTipo And D.Id = P.IdDebilidad";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)lector["Id"];
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    // validacion
                    //if(!lector.IsDBNull(lector.GetOrdinal("UrlImagen")))
                    //    aux.UrlImagen = (string)lector["UrlImagen"];

                    // validacion alternativa
                    if (!(lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion, Activo, UrlImagen, IdTipo, IdDebilidad)values(" + nuevo.Numero + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', 1, @url, @idTipo, @idDebilidad)");
                datos.setearParametro("@idTipo", nuevo.Tipo.Id);
                datos.setearParametro("@url", nuevo.UrlImagen);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
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

        public void modificar(Pokemon modificar) 
        {
            AccesoDatos consultar = new AccesoDatos();
            try
            {
                consultar.setearConsulta("UPDATE POKEMONS SET Numero = @Numero, Nombre = @Nombre, Descripcion = @Desc, UrlImagen = @Url, IdTipo = @IdTipo, IdDebilidad = @IdDebilidad WHERE Id = @Id;");
                consultar.setearParametro("@Numero", modificar.Numero);
                consultar.setearParametro("@Nombre", modificar.Nombre);
                consultar.setearParametro("@Desc", modificar.Descripcion);
                consultar.setearParametro("@Url", modificar.UrlImagen);
                consultar.setearParametro("@IdTipo", modificar.Tipo);
                consultar.setearParametro("@IdDebilidad", modificar.Debilidad);
                consultar.setearParametro("@Id", modificar.Id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                consultar.cerrarConexion();
            }
        }
    }
}
