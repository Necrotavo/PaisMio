using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    /// <summary>
    /// Clase de acceso a datos de los pedidos.
    /// </summary>
    public class DAO_Pedido
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        /// <summary>
        /// Método para agregar un nuevo pedido a la base de datos, se encarga de llamar al método para agregar los productos relacionados con el pedido
        /// </summary>
        /// <param name="pedido">El nuevo pedido a registrar</param>
        /// <returns>(True) si la operación se realizó correctamente. (False) si no se registró el pedido</returns>
        public bool guardarPedido(DO_Pedido pedido)
        {
            SqlCommand comandoInsertar = new SqlCommand("INSERT INTO PEDIDO (CLI_CEDULA , OPE_CORREO, ESTADO" +
           ", PED_FECHA_INGRESO) VALUES (@cedula,@correoAdmin,@estado, @fechaIngreso)", conexion);

            comandoInsertar.Parameters.AddWithValue("@cedula", pedido.cliente.cedula);
            comandoInsertar.Parameters.AddWithValue("@correoAdmin", pedido.correoAdminIngreso);
            comandoInsertar.Parameters.AddWithValue("@estado", pedido.estado);
            comandoInsertar.Parameters.AddWithValue("@fechaIngreso", pedido.fechaIngreso);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (comandoInsertar.ExecuteNonQuery() > 0)
                {
                    SqlCommand obtenerCodigo = new SqlCommand("SELECT PED_CODIGO FROM PEDIDO ORDER BY PED_CODIGO DESC",conexion);
                    pedido.codigo = Convert.ToInt32(obtenerCodigo.ExecuteScalar());
                }

                if (!agregarProductos(pedido))
                {
                    eliminarPedido(pedido.codigo); //Si no se agregaron todos los productos entonces se elimina el pedido y se devuelve false.
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

        /// <summary>
        /// Método para agregar los productos contenidos en el pedido
        /// </summary>
        /// <param name="pedido">El pedido con sus respectivos productos</param>
        /// <returns>(True) si la operación se realizó correctamente. (False) si no se registraron los cambios</returns>
        public bool agregarProductos(DO_Pedido pedido)
        {
            String comandoInsertarProductos = "INSERT INTO PED_POSEE_PRO(PRO_CODIGO, PED_CODIGO, PPP_CANTIDAD) VALUES ";

            if (pedido.listaProductos.Count != 0)
            {

                foreach (DO_ProductoEnPedido producto in pedido.listaProductos)
                {
                    comandoInsertarProductos += "(" + producto.producto.codigo + ", " + pedido.codigo + ", " + producto.cantidad + "),";
                }
            }
            else
            {
                return false;
            }

            comandoInsertarProductos = comandoInsertarProductos.Substring(0, comandoInsertarProductos.Length - 1);

            SqlCommand comandoInsertar = new SqlCommand(comandoInsertarProductos, conexion);

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
        /// Método para consultar los productos asociados a un determinado producto
        /// </summary>
        /// <param name="codigoPedido">(Int32) Código del pedido a consultar sus productos</param>
        /// <returns>(List<DO_ProductoEnPedido>) lista de los productos asociados al pedido</returns>
        public List<DO_ProductoEnPedido> listaProductos(Int32 codigoPedido)
        {

            List<DO_ProductoEnPedido> listaProductos = new List<DO_ProductoEnPedido>();

            SqlCommand comandoConsultar = new SqlCommand("SELECT PED_POSEE_PRO.PPP_CANTIDAD, PRODUCTO.* "+
                "FROM PRODUCTO, PED_POSEE_PRO WHERE(PRODUCTO.PRO_CODIGO = PED_POSEE_PRO.PRO_CODIGO AND PED_POSEE_PRO.PED_CODIGO = @codigo)", conexion);

            comandoConsultar.Parameters.AddWithValue("@codigo", codigoPedido);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoConsultar.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        DO_ProductoEnPedido producto = new DO_ProductoEnPedido();
                        DO_Producto detallesProducto = new DO_Producto();
                        producto.cantidad = Convert.ToInt32(lector["PPP_CANTIDAD"]);
                        detallesProducto.codigo = Convert.ToInt32(lector["PRO_CODIGO"]);
                        detallesProducto.estado = (String)(lector["EST_HAB_ESTADO"]);
                        detallesProducto.nombre = (String)(lector["PRO_NOMBRE"]);
                        detallesProducto.descripcion = (String)(lector["PRO_DESCRIPCION"]);
                        producto.producto = detallesProducto;


                        listaProductos.Add(producto);
                    }
                }
                conexion.Close();
                
                return listaProductos;
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
        /// Método para elimnar un respectivo pedido de la base de datos
        /// </summary>
        /// <param name="codigoPedido">El código del pedido a eliminar</param>
        /// <returns>(True) si se eliminó el pedido. (False) si no se pudo eliminar</returns>
        public bool eliminarPedido(Int32 codigoPedido)
        {
            SqlCommand comandoBorrar = new SqlCommand("DELETE FROM PEDIDO WHERE PED_CODIGO = @codigo", conexion);
            comandoBorrar.Parameters.AddWithValue("@codigo", codigoPedido);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                comandoBorrar.ExecuteNonQuery();
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
        /// Método para actualizar el estado de un producto en la base de datos
        /// </summary>
        /// <param name="codigoPedido">Código del pedido a actualizar (Int32).</param>
        /// <param name="nuevoEstado">Nuevo estado del pedido (String)</param>
        /// <returns>(True) si se actualizó el estado. (False) si no se actualizó</returns>
        public bool modificarEstado(Int32 codigoPedido, String nuevoEstado) 
        {
            SqlCommand comandoActualizar = new SqlCommand("UPDATE PEDIDO SET ESTADO = @estado WHERE PED_CODIGO = @codigo", conexion);

            comandoActualizar.Parameters.AddWithValue("@estado", nuevoEstado);
            comandoActualizar.Parameters.AddWithValue("@codigo", codigoPedido);


            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                
                if (comandoActualizar.ExecuteNonQuery() > 0)
                {
                    return true;
                }

                return false;
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
        /// Método para consultar los datos de un determinado pedido
        /// </summary>
        /// <param name="codigoPedido">Código del pedido a buscar (Int32)</param>
        /// <returns>El pedido con sus respetivos detalles (DO_Pedido)</returns>
        public DO_Pedido consultarDetalles(Int32 codigoPedido)
        {
            SqlCommand comandoConsultar = new SqlCommand("SELECT * FROM PEDIDO WHERE PED_CODIGO = @codigo", conexion);
            DAO_Cliente daoCliente = new DAO_Cliente();
            DO_Pedido pedido = new DO_Pedido();

            comandoConsultar.Parameters.AddWithValue("codigo", codigoPedido);
            

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoConsultar.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        pedido.codigo = Convert.ToInt32(lector["PED_CODIGO"]);
                        pedido.cliente = daoCliente.buscarCliente((String)(lector["CLI_CEDULA"]));
                        pedido.correoAdminIngreso = (String)(lector["OPE_CORREO"]);
                        pedido.fechaIngreso = (DateTime)(lector["PED_FECHA_INGRESO"]);
                        pedido.estado = (String)(lector["ESTADO"]);

                        if (lector["ADM_OPE_CORREO"] is System.DBNull)
                        {
                            pedido.correoAdminDespacho = "";
                        }
                        else
                        {
                            pedido.correoAdminDespacho = (String)(lector["ADM_OPE_CORREO"]);
                        }

                        if (lector["PED_FECHA_DESPACHO"] is System.DBNull)
                        {
                            pedido.fechaDespacho = null;
                        }
                        else
                        {
                            pedido.fechaDespacho = (DateTime)(lector["PED_FECHA_DESPACHO"]);
                        }


                    }
                }
                conexion.Close();

                pedido.listaProductos = listaProductos(codigoPedido);
               
                return pedido;
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
        /// Método para actualizar los datos de un pedido despachado.
        /// </summary>
        /// <param name="codigoPedido">(Int32) Código del pedido a despachar</param>
        /// <param name="correoAdmin">(String) Correo del administrador que despacha el pedido</param>
        /// <param name="fechaDespacho">(DateTime) fecha de desapacho</param>
        /// <param name="estado">(String) estado del pedido</param>
        /// <returns>(True) si se actualizaron los datos. (False) si no se realizaron los cambios.</returns>
        public bool despacharPedido(Int32 codigoPedido, String correoAdmin, DateTime fechaDespacho, String estado)
        {
            SqlCommand comandoActualizar = new SqlCommand("UPDATE PEDIDO SET ADM_OPE_CORREO = @correo, ESTADO = @estado, PED_FECHA_DESPACHO = @fecha WHERE PED_CODIGO = @codigo", conexion);

            comandoActualizar.Parameters.AddWithValue("@estado", estado);
            comandoActualizar.Parameters.AddWithValue("@correo", correoAdmin);
            comandoActualizar.Parameters.AddWithValue("@fecha", fechaDespacho.ToString("dd/MM/yyyy"));
            comandoActualizar.Parameters.AddWithValue("@codigo", codigoPedido);


            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (comandoActualizar.ExecuteNonQuery() > 0)
                {
                    return true;
                }

                return false;
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
        /// Método para buscar todos los pedidos activos del momento.
        /// </summary>
        /// <returns></returns>
        public List<DO_Pedido> listarPedidos()
        {
            SqlCommand comandoConsultar = new SqlCommand("SELECT * FROM PEDIDO WHERE ESTADO = 'EN PROCESO' ", conexion);
            DAO_Cliente daoCliente = new DAO_Cliente();
            
            List<DO_Pedido> listaPedidos = new List<DO_Pedido>();

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoConsultar.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        DO_Pedido pedido = new DO_Pedido();

                        pedido.codigo = Convert.ToInt32(lector["PED_CODIGO"]);
                        pedido.cliente = daoCliente.buscarCliente((String)(lector["CLI_CEDULA"]));
                        pedido.correoAdminIngreso = (String)(lector["OPE_CORREO"]);
                        pedido.fechaIngreso = (DateTime)(lector["PED_FECHA_INGRESO"]);
                        pedido.estado = (String)(lector["ESTADO"]);

                        if (lector["ADM_OPE_CORREO"] is System.DBNull)
                        {
                            pedido.correoAdminDespacho = "";
                        }
                        else
                        {
                            pedido.correoAdminDespacho = (String)(lector["ADM_OPE_CORREO"]);
                        }

                        if (lector["PED_FECHA_DESPACHO"] is System.DBNull)
                        {
                            pedido.fechaDespacho = null;
                        }
                        else
                        {
                            pedido.fechaDespacho = (DateTime)(lector["PED_FECHA_DESPACHO"]);
                        }

                        listaPedidos.Add(pedido);
                    }
                }
                conexion.Close();

                foreach (DO_Pedido pedidoEnLista in listaPedidos)
                {
                    pedidoEnLista.listaProductos = listaProductos(pedidoEnLista.codigo);
                }

                

                return listaPedidos;
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
