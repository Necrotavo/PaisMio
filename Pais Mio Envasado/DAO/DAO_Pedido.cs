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
            SqlCommand comandoInsertar = new SqlCommand("INSERT INTO PEDIDO (CLI_CEDU , OPE_CORREO, ESTADO" +
           ", PED_FECHA_INGRESO) VALUES (@cedula,@correoAdmin,@estado, @fechaIngreso)", conexion);

            comandoInsertar.Parameters.AddWithValue("@cedula", pedido.cedulaCliente);
            comandoInsertar.Parameters.AddWithValue("@correoAdmin", pedido.correoAdminIngreso);
            comandoInsertar.Parameters.AddWithValue("@estado", pedido.estado);
            comandoInsertar.Parameters.AddWithValue("@fechaIngreso", pedido.fechaIngreso.ToString("dd/MM/yyyy"));

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (comandoInsertar.ExecuteNonQuery() > 0)
                {
                    SqlCommand obtenerCodigo = new SqlCommand("SELECT PED_CODIGO FROM PEDIDO ORDER BY PED_CODIGO DESC");
                    pedido.codigo = (int)obtenerCodigo.ExecuteScalar();
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
                    comandoInsertarProductos += "(" + producto.producto.codigo + ", " + pedido.codigo + ", " + producto.cantidad + "), ";
                }
            }
            else
            {
                return false;
            }

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
        /// Método para elimnar un respectivo pedido de la base de datos
        /// </summary>
        /// <param name="codigoPedido">El código del pedido a eliminar</param>
        /// <returns>(True) si se eliminó el pedido. (False) si no se pudo eliminar</returns>
        public bool eliminarPedido(Int32 codigoPedido)
        {
            SqlCommand comandoBorrar = new SqlCommand("DELETE FROM PEDIDO WHERE PED_CODIGO = @codigo)", conexion);
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
    }
}
