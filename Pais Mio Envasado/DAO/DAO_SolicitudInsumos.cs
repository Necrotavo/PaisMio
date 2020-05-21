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
    public class DAO_SolicitudInsumos
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);
        public bool guardarSolicitudConInsumos(DO_SolicitudInsumos solicitudInsumos)
        {
            if (guardarSolicitudSinInsumos(solicitudInsumos))
            {

                foreach (DO_InsumoEnBodega insumo in solicitudInsumos.listaConsumo)
                {
                    guardarInsumoConsumido(insumo, solicitudInsumos.codigoSolicitud);
                }

                foreach (DO_InsumoEnBodega insumo in solicitudInsumos.listaConsumo)
                {
                    guardarInsumoDescartado(insumo, solicitudInsumos.codigoSolicitud);
                }

                return true;
            }
            else
            {
                return false;
            }


        }

        private bool guardarSolicitudSinInsumos(DO_SolicitudInsumos solicitudInsumos)
        {

            SqlCommand insert = new SqlCommand("INSERT INTO SOLICITUD_INSUMO (OPE_CORREO, PED_CODIGO, SUP_OPE_CORREO, EST_SOL_ESTADO, SOL_FECHA)" +
                "VALUES (@operadorId, @codigoPedido, @correoAdmin, @estado, @fecha)", conexion);
            insert.Parameters.AddWithValue("@operadorId", solicitudInsumos.correoOperario);
            insert.Parameters.AddWithValue("@codigoPedido", solicitudInsumos.codigoPedido);
            insert.Parameters.AddWithValue("@correoAdmin", solicitudInsumos.correoAdministrador);
            insert.Parameters.AddWithValue("@estado", solicitudInsumos.estado);
            insert.Parameters.AddWithValue("@fecha", solicitudInsumos.fechaSolicitud);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (insert.ExecuteNonQuery() > 0)
                {
                    SqlCommand tomarCodigo = new SqlCommand("SELECT SOL_CODIGO FROM SOLICITUD_INSUMO ORDER BY SOL_CODIGO DESC");
                    solicitudInsumos.codigoSolicitud = (int)tomarCodigo.ExecuteScalar();
                }
                
                return true;
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
        }

        private bool guardarInsumoConsumido(DO_InsumoEnBodega insumo, int codigoSolicitud)
        {
                SqlCommand insert = new SqlCommand("INSERT INTO SOL_A_CONSUMIR (INS_CODIGO, SOL_CODIGO, ACS_CANTIDAD)" +
                "VALUES (@codigoInsumo, @codigoSolicitud, @cantidad)", conexion);
                insert.Parameters.AddWithValue("@codigoInsumo", insumo.insumo.codigo);
                insert.Parameters.AddWithValue("@codigoSolicitud", codigoSolicitud);
                insert.Parameters.AddWithValue("@cantidad", insumo.cantidadDisponible);

                try
                {
                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    insert.ExecuteNonQuery();
                    return true;
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
            
        }

        private bool guardarInsumoDescartado(DO_InsumoEnBodega insumo, int codigoSolicitud)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO POR_DESCARTE (INS_CODIGO, SOL_CODIGO, PDS_CANTIDAD)" +
            "VALUES (@codigoInsumo, @codigoSolicitud, @cantidad)", conexion);
            insert.Parameters.AddWithValue("@codigoInsumo", insumo.insumo.codigo);
            insert.Parameters.AddWithValue("@codigoSolicitud", codigoSolicitud);
            insert.Parameters.AddWithValue("@cantidad", insumo.cantidadDisponible);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                insert.ExecuteNonQuery();
                return true;
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

        }
        public void reducirInsumos(DO_SolicitudInsumos solicitud)
        {

            foreach (DO_InsumoEnBodega insumo in solicitud.listaConsumo)
            {
                reducirUnInsumo(insumo.insumo.codigo, solicitud.codigoBodega, insumo.cantidadDisponible);
            }

            foreach (DO_InsumoEnBodega insumo in solicitud.listaDescarte)
            {
                reducirUnInsumo(insumo.insumo.codigo, solicitud.codigoBodega, insumo.cantidadDisponible);
            }

        }
        private bool reducirUnInsumo (int codInsumo, int codigoBodega, int cantidad)
        {
            SqlCommand reducirCantidad = new SqlCommand("UPDATE INS_ESTA_BOD SET IEB_CANTIDAD_DISPONIBLE = IEB_CANTIDAD_DISPONIBLE - @cantIngresa " +
                "WHERE BOD_CODIGO = @codigoBodega AND INS_CODIGO = @codigoInsumo");
            reducirCantidad.Parameters.AddWithValue("@cantIngresa", cantidad);
            reducirCantidad.Parameters.AddWithValue("@codigoBodega", codigoBodega);
            reducirCantidad.Parameters.AddWithValue("@codigoInsumo", codInsumo);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                reducirCantidad.ExecuteNonQuery();
                return true;
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
        }

        public bool decisionSolicitud(DO_Administrador admin, string estado, DO_SolicitudInsumos solicitud)
        {
            SqlCommand actualizarSolicitud = new SqlCommand("UPDATE SOLICITUD_INSUMO " +
                "SET SUP_OPE_CORREO = @adminId, EST_SOL_ESTADO = @estado" +
               "WHERE SOL_CODIGO = @codigoSolicitud");
            actualizarSolicitud.Parameters.AddWithValue("@adminId", admin.correo);
            actualizarSolicitud.Parameters.AddWithValue("@estado", estado);
            actualizarSolicitud.Parameters.AddWithValue("@codigoSolicitud", solicitud.codigoSolicitud);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                actualizarSolicitud.ExecuteNonQuery();
                return true;
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
        }



    }
}
