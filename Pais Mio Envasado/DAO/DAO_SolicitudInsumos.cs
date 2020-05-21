using DO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_SolicitudInsumos
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);
        public int guardarSolicitud(DO_SolicitudInsumos solicitudInsumos)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO SOLICITUD_INSUMO (OPE_CORREO, PED_CODIGO, INS_NOMBRE, INS_CANT_MIN_STOCK)" +
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
    }
}
