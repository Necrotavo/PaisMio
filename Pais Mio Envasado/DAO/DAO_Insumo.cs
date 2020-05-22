using DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    /// <summary>
    /// En esta clase se trabajan las acciones de acceso a base de datos relacionadas con los insumos
    /// </summary>
    public class DAO_Insumo
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        /// <summary>
        /// Permite guardar un insumo en la base de datos
        /// </summary>
        /// <param name="insumo">Insumo que se va a guardar</param>
        /// <returns>El código del insumo guardado, 0 si no se puede crear</returns>
        public int guardarInsumo(DO_Insumo insumo)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO INSUMO (EST_HAB_ESTADO, UDM_UNIDAD, INS_NOMBRE, INS_CANT_MIN_STOCK)" +
                "VALUES (@estado, @unidad, @nombre, @cantMinStock", conexion);
            insert.Parameters.AddWithValue("@estado", insumo.estado.estado);
            insert.Parameters.AddWithValue("@unidad", insumo.unidad.unidad);
            insert.Parameters.AddWithValue("@nombre", insumo.nombre);
            insert.Parameters.AddWithValue("@cantMinStock", insumo.cantMinStock);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (insert.ExecuteNonQuery() > 0)
                {

                    return obtenerCodigoUltimoInsumo();
                }
                else
                {
                    return 0;
                }

            }
            catch (SqlException e)
            {
                return 0;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Busca el código del último insumo creado
        /// </summary>
        /// <returns>El código del último insumo creado, 0 si no se hay un error</returns>
        public int obtenerCodigoUltimoInsumo() {
            SqlCommand obtenerCodigo = new SqlCommand("Select INS_CODIGO from INSUMO ORDER BY INS_CODIGO DESC", conexion);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                return Convert.ToInt32(obtenerCodigo.ExecuteScalar());

            }
            catch (SqlException e)
            {
                return 0;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Busca el código de un insumo por nombre
        /// </summary>
        /// <param name="nombre">Nombre por el que se va a buscar el insumo</param>
        /// <returns>El código del insumo buscado, o 0 si no se encuentra</returns>
        public int buscarCodigoInsumo(String nombre) {
            SqlCommand obtenerCodigo = new SqlCommand("SELECT INS_CODIGO FROM INSUMO WHERE INS_NOMBRE = @nombre;", conexion);
            obtenerCodigo.Parameters.AddWithValue("@nombre", nombre);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                return Convert.ToInt32(obtenerCodigo.ExecuteScalar());

            }
            catch (SqlException e)
            {
                return 0;
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
