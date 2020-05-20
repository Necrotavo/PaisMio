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
    public class DAO_Insumo
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

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

    }
}
