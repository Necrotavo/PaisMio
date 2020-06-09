using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DO;

namespace DAO
{
    /// <summary>
    /// Clase de acceso a datos de las unidades de medida.
    /// </summary>
    public class DAO_UnidadDeMedida
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        /// <summary>
        /// Método para insertar unidades de medida.
        /// </summary>
        /// <param name="unidad">(String) Unidad de medida</param>
        /// <returns>(True) si se ´registró el cambio.(False) si no se realizó.</returns>
        public bool agregarUnidad(String unidad)
        {
            SqlCommand comandoInsertar = new SqlCommand("INSERT INTO UNIDAD_DE_MEDIDA (UDM_UNIDAD) VALUES (@unidad)", conexion);
            comandoInsertar.Parameters.AddWithValue("@unidad", unidad);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (comandoInsertar.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }

            return false;
        }

        /// <summary>
        /// Método para listar las unidades de medida disponibles en la base de datos.
        /// </summary>
        /// <returns>(List<String>) lista de unidades de medida.</returns>
        public List<String> listaUnidades()
        {
            SqlCommand comandoBuscar = new SqlCommand("SELECT * FROM UNIDAD_DE_MEDIDA", conexion);
            List<String> listaUnidades = new List<string>();
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoBuscar.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        listaUnidades.Add((String)lector["UDM_UNIDAD"]);
                    }
                }
                return listaUnidades;
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }



    }
}
