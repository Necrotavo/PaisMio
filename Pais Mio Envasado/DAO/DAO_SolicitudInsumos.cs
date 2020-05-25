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
    /// Esta clase comprende los accesos a datos relacionados a las solicitudes de insumos creadas por los insumos
    /// </summary>
    public class DAO_SolicitudInsumos
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        /// <summary>
        /// Permite guardar la solicitud de insumos junto a la lista de descarte y consumo en la base de datos.
        /// </summary>
        /// <param name="solicitudInsumos">La solicitud de insumos</param>
        /// <returns></returns>
        public bool guardarSolicitudInsumos(DO_SolicitudInsumos solicitudInsumos)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO SOLICITUD_INSUMO (OPE_CORREO, PED_CODIGO, SUP_OPE_CORREO, EST_SOL_ESTADO, SOL_FECHA, BODEGA)" +
                "VALUES (@operadorId, @codigoPedido, @estado, @fecha, @bodega)", conexion);
            insert.Parameters.AddWithValue("@operadorId", solicitudInsumos.correoOperario);
            insert.Parameters.AddWithValue("@codigoPedido", solicitudInsumos.codigoPedido);
            insert.Parameters.AddWithValue("@estado", solicitudInsumos.estado);
            insert.Parameters.AddWithValue("@fecha", solicitudInsumos.fechaSolicitud);
            insert.Parameters.AddWithValue("@bodega", solicitudInsumos.codigoBodega);
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
                if (!agregarInsumosSolicitud(solicitudInsumos))
                {
                    borrarSolicitud(solicitudInsumos.codigoSolicitud);
                    return false;
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

        private bool borrarSolicitud(int codigo)
        {
            SqlCommand borrar = new SqlCommand("DELETE FROM SOLICITUD_INSUMO WHERE SOL_CODIGO = @codigo)", conexion);
            borrar.Parameters.AddWithValue("@codigo", codigo);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                borrar.ExecuteNonQuery();
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

        /// <summary>
        /// Este método agrega los insumos de la solicitud de insumos a la base de datos
        /// </summary>
        /// <param name="solicitud">Solicitud con las listas de insumos que se van a agregar</param>
        /// <returns>True si se ingresan los insumos, False si sucede un error</returns>
        private bool agregarInsumosSolicitud(DO_SolicitudInsumos solicitud)
        {
            string query = consumidosConstructor(solicitud) + " ";
            query += descartadosConstructor(solicitud);
            SqlCommand comando = new SqlCommand(query, conexion);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                comando.ExecuteNonQuery();
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

        /// <summary>
        /// En este método se formula el comando para insertar los insumos por consumir de la solicitud de insumos
        /// </summary>
        /// <param name="solicitud">Solicitud con la lista de insumos por consumir que se van a agregar en la base de datos</param>
        /// <returns>El comando creado o una cadena de texto vacía si sucede un error</returns>
        private string consumidosConstructor(DO_SolicitudInsumos solicitud)
        {
            string query;
            if (solicitud.listaConsumo.Count == 0)
            {
                return "";
            }
            else
            {
                query = "INSERT INTO SOL_A_CONSUMIR (INS_CODIGO, SOL_CODIGO, ACS_CANTIDAD) VALUES ";
            }
            
            foreach (DO_InsumoEnBodega insumo in solicitud.listaConsumo)
            {
                query += "("+insumo.insumo.codigo+solicitud.codigoSolicitud+insumo.cantidadDisponible+"),";
            }

            return query.Substring(0,query.Length-1);
        }

        /// <summary>
        /// En este método se formula el comando para insertar los insumos por descarte de la solicitud de insumos
        /// </summary>
        /// <param name="solicitud">Solicitud con la lista de insumos por descarte que se van a agregar en la base de datos</param>
        /// <returns>El comando creado o una cadena de texto vacía si sucede un error</returns>
        private string descartadosConstructor(DO_SolicitudInsumos solicitud)
        {
            string query;
            if (solicitud.listaConsumo.Count == 0)
            {
                return "";
            }
            else
            {
                query = "INSERT INTO POR_DESCARTE (INS_CODIGO, SOL_CODIGO, PDS_CANTIDAD) VALUES ";
            }

            foreach (DO_InsumoEnBodega insumo in solicitud.listaDescarte)
            {
                query += "(" + insumo.insumo.codigo + solicitud.codigoSolicitud + insumo.cantidadDisponible + "),";
            }

            return query.Substring(0, query.Length - 1);
        }

        /// <summary>
        /// Reduce los insumos de la base de datos según la cantidad que tenga la solicitud de insumos
        /// </summary>
        /// <param name="solicitud">La solicitud de insumos</param>
        /// <returns></returns>
        public bool reducirInsumos (DO_SolicitudInsumos solicitud)
        {
            string query = "";
            foreach (DO_InsumoEnBodega insumo in solicitud.listaConsumo)
            {
                query += "UPDATE INS_ESTA_BOD SET IEB_CANTIDAD_DISPONIBLE = IEB_CANTIDAD_DISPONIBLE - " + insumo.cantidadDisponible +
                "WHERE BOD_CODIGO = " + solicitud.codigoBodega + " AND INS_CODIGO = "+ insumo.insumo.codigo + " ";
            }
            foreach (DO_InsumoEnBodega insumo in solicitud.listaDescarte)
            {
                query += "UPDATE INS_ESTA_BOD SET IEB_CANTIDAD_DISPONIBLE = IEB_CANTIDAD_DISPONIBLE - " + insumo.cantidadDisponible +
                "WHERE BOD_CODIGO = " + solicitud.codigoBodega + " AND INS_CODIGO = " + insumo.insumo.codigo + " ";
            }

            SqlCommand reducirCantidad = new SqlCommand(query, conexion);
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

        /// <summary>
        /// Refleja en la base de datos la aceptación o rechazo de la solicitud de insumos
        /// </summary>
        /// <param name="admin"> Objeto DO de administrador </param>
        /// <param name="estado"> Nombre del estado a asignar(Debe ser un estado valido) </param>
        /// <param name="solicitud">La solicitud de insumos</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna las solicitudes de insumos de la base de datos en una lista
        /// </summary>
        /// <returns></returns>
        public List<DO_SolicitudInsumos> listarSolicitudes()
        {
            List<DO_SolicitudInsumos> listaSolicitud = new List<DO_SolicitudInsumos>();
            SqlCommand comandoBuscar = new SqlCommand("SELECT * FROM SOLICITUD_INSUMO", conexion);

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
                        DO_SolicitudInsumos insumo = new DO_SolicitudInsumos();
                        
                        insumo.codigoSolicitud = Convert.ToInt32(lector["SOL_CODIGO"]);
                        insumo.correoOperario = (string)lector["OPE_CORREO"];
                        insumo.codigoPedido = Convert.ToInt32(lector["PED_CODIGO"]);
                        if (lector["SUP_OPE_CORREO"] is System.DBNull)
                        {
                            insumo.correoAdministrador = "";
                        }
                        else
                        {
                            insumo.correoAdministrador = (string)lector["SUP_OPE_CORREO"];
                        }
                        insumo.estado = (string)lector["EST_SOL_ESTADO"];
                        insumo.fechaSolicitud = Convert.ToDateTime(lector["SOL_FECHA"]);
                        insumo.codigoBodega = Convert.ToInt32(lector["BODEGA"]);
                        listaSolicitud.Add(insumo);
                    }
                }
                conexion.Close();
                foreach (DO_SolicitudInsumos item in listaSolicitud)
                {
                    item.listaConsumo = listaConsumo(item.codigoSolicitud);
                }
                foreach (DO_SolicitudInsumos item in listaSolicitud)
                {
                    item.listaDescarte = listaDescarte(item.codigoSolicitud);
                }
                return listaSolicitud;
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

        /// <summary>
        /// Retorna los insumos consumidos de una solicitud de insumos en específico
        /// </summary>
        /// <param name="codigoSolicitud">El código de la solicitud de insumos</param>
        /// <returns></returns>
        private List<DO_InsumoEnBodega> listaConsumo(int codigoSolicitud)
        {
            List<DO_InsumoEnBodega> listaConsumidos = new List<DO_InsumoEnBodega>();
            SqlCommand comandoBuscar = new SqlCommand("SELECT * FROM SOL_A_CONSUMIR_INS WHERE SOL_CODIGO = @codigo", conexion);
            comandoBuscar.Parameters.AddWithValue("@codigo", codigoSolicitud);

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
                        DO_InsumoEnBodega insumo = new DO_InsumoEnBodega();

                        insumo.cantidadDisponible = Convert.ToInt32(lector["ACS_CANTIDAD"]);
                        //NECESITO UN METODO QUE ME DEVUELVA UN DO_INSUMOS POR CODIGO (int)lector["INS_CODIGO"];
                        listaConsumidos.Add(insumo);
                    }
                }
                return listaConsumidos;
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

        /// <summary>
        /// Retorna los insumos descartados de una solicitud de insumos en específico
        /// </summary>
        /// <param name="codigoSolicitud">El código de la solicitud de insumos</param>
        /// <returns></returns>
        private List<DO_InsumoEnBodega> listaDescarte(int codigoSolicitud)
        {
            List<DO_InsumoEnBodega> listaConsumidos = new List<DO_InsumoEnBodega>();
            SqlCommand comandoBuscar = new SqlCommand("SELECT * FROM POR_DESCARTE WHERE SOL_CODIGO = @codigo", conexion);
            comandoBuscar.Parameters.AddWithValue("@codigo", codigoSolicitud);

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
                        DO_InsumoEnBodega insumo = new DO_InsumoEnBodega();

                        insumo.cantidadDisponible = Convert.ToInt32(lector["PDS_CANTIDAD"]);
                        //NECESITO UN METODO QUE ME DEVUELVA UN DO_INSUMOS POR CODIGO (int)lector["INS_CODIGO"];
                        listaConsumidos.Add(insumo);
                    }
                }
                return listaConsumidos;
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

        /// <summary>
        /// Metodo que retorna una sola solicitud de insumos
        /// </summary>
        /// <param name="codigoSolicitud">El codigo de la solicitud de insumos a retornar</param>
        /// <returns></returns>
        public DO_SolicitudInsumos consultarSolicitud(int codigoSolicitud)
        {
            SqlCommand comandoBuscar = new SqlCommand("SELECT * FROM SOLICITUD_INSUMO WHERE SOL_CODIGO = @codigo", conexion);
            comandoBuscar.Parameters.AddWithValue("@codigo", codigoSolicitud);
            DO_SolicitudInsumos insumo = new DO_SolicitudInsumos();
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
                        

                        insumo.codigoSolicitud = (int)lector["SOL_CODIGO"];
                        insumo.correoOperario = (string)lector["OPE_CORREO"];
                        insumo.codigoPedido = (int)lector["PED_CODIGO"];
                        insumo.correoAdministrador = (string)lector["SUP_OPE_CORREO"];
                        insumo.estado = (string)lector["EST_SOL_ESTADO"];
                        insumo.fechaSolicitud = (DateTime)lector["SOL_FECHA"];
                        insumo.codigoBodega = (int)lector["BODEGA"];
                        insumo.listaConsumo = listaConsumo(insumo.codigoSolicitud);
                        insumo.listaDescarte = listaDescarte(insumo.codigoSolicitud);
                    }
                }
                return insumo;
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
