using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Windows.Forms;

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
                comando.CommandText = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id From POKEMONS P, ELEMENTOS E, ELEMENTOS D Where E.Id = P.IdTipo And D.Id = P.IdDebilidad AND Activo = 1";
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

        public void modificar(Pokemon poke) 
        {
            AccesoDatos consultar = new AccesoDatos();
            try
            {
                consultar.setearConsulta("UPDATE POKEMONS SET Numero = @numero, Nombre = @nombre, Descripcion = @info, UrlImagen = @url, IdTipo = @idTipo, IdDebilidad = @idDebilidad WHERE Id = @id;");
                //SETEAR PARAMETROS
                consultar.setearParametro("@numero", poke.Numero);
                consultar.setearParametro("@nombre", poke.Nombre);
                consultar.setearParametro("@info", poke.Descripcion);
                consultar.setearParametro("@url", poke.UrlImagen);
                consultar.setearParametro("@idTipo", poke.Tipo.Id);
                consultar.setearParametro("@idDebilidad", poke.Debilidad.Id);
                consultar.setearParametro("@id", poke.Id);

                consultar.ejecutarAccion();
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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM POKEMONS WHERE Id = @Id;");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE POKEMONS SET Activo = 0 WHERE ID = @Id;");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Pokemon> filtrar(string campo, object criterio, string filtro)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos consulta = new AccesoDatos();
            try
            {
                string queryToSend = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id From POKEMONS P, ELEMENTOS E, ELEMENTOS D Where E.Id = P.IdTipo And D.Id = P.IdDebilidad AND Activo = 1 AND ";
                if(campo == "Número")
                {
                    switch(criterio)
                    {
                        case "Menor a:":
                            queryToSend += "Numero < " + filtro;break;
                        case "Mayor a:":
                            queryToSend += "Numero > " + filtro;break;
                        default:
                            queryToSend += "Numero = " + filtro;break;
                    }
                }
                else if(campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con:":
                            queryToSend += "Nombre LIKE '" + filtro + "%'";break;
                        case "Termina con:":
                            queryToSend += "Nombre LIKE '%" + filtro + "'";break;
                        default:
                            queryToSend += "Nombre LIKE '%" + filtro + "%'"; break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con:":
                            queryToSend += "P.Descripcion LIKE '" + filtro + "%'"; break;
                        case "Termina con:":
                            queryToSend += "P.Descripcion '%" + filtro + "'"; break;
                        default:
                            queryToSend += "P.Descripcion '%" + filtro + "%'"; break;
                    }
                }
                consulta.setearConsulta(queryToSend);
                consulta.ejecutarLectura();

                while (consulta.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)consulta.Lector["Id"];
                    aux.Numero = consulta.Lector.GetInt32(0);
                    aux.Nombre = (string)consulta.Lector["Nombre"];
                    aux.Descripcion = (string)consulta.Lector["Descripcion"];

                    if (!(consulta.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)consulta.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)consulta.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)consulta.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)consulta.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)consulta.Lector["Debilidad"];

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
                consulta.cerrarConexion();
            }
        }
    }
}
