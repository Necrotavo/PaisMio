using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAO;

namespace BL
{
    public class BL_Pedido
    {
        /// <summary>
        /// Método para guardar un nuevo pedido
        /// </summary>
        /// <param name="pedido">El pedido a guardar</param>
        /// <returns>(True) si se guardó el pedido. (False) si no se guardó.</returns>
        public bool registrarPedido(DO_Pedido pedido)
        {
            DAO_Pedido daoPedido = new DAO_Pedido();
            return daoPedido.guardarPedido(pedido);
        }

        /// <summary>
        /// Método para eliminar un pedido.
        /// </summary>
        /// <param name="codigoPedido">Código del pedido a eliminar</param>
        /// <returns>(True) si se eliminó el pedido. (False) si no se eliminó</returns>
        public bool eliminarPedido(Int32 codigoPedido)
        {
            DAO_Pedido daoPedido = new DAO_Pedido();
            return daoPedido.eliminarPedido(codigoPedido);
        }

        /// <summary>
        /// Método para modificar el estado de un pedido
        /// </summary>
        /// <param name="codigoPedido">(Int32) Código del pedido.</param>
        /// <param name="estado">(String) Nuevo estado</param>
        /// <returns></returns>
        public bool modificarEstado(Int32 codigoPedido, String estado)
        {
            DAO_Pedido daoPedido = new DAO_Pedido();
            return daoPedido.modificarEstado(codigoPedido, estado);
        }

        /// <summary>
        /// Método para consultar los datos de un determinado pedido.
        /// </summary>
        /// <param name="codigoPedido">(Int32) Código del pedido a consultar</param>
        /// <returns>(DO_Pedido) Pedido con sus respectivos datos.</returns>
        public DO_Pedido consultarDatosPedido(Int32 codigoPedido)
        {
            DAO_Pedido daoPedido = new DAO_Pedido();
            return daoPedido.consultarDetalles(codigoPedido);
        }

        /// <summary>
        /// Método para realizar el despacho del producto.
        /// </summary>
        /// <param name="codigoPedido">(Int32) Código del pedido.</param>
        /// <param name="correoAdmin">(String) Correo del administrador que despacha el producto.</param>
        /// <param name="fechaDespacho">(DateTime)Fecha de despacho.</param>
        /// <param name="estado">(String) Estado del pedido.</param>
        /// <returns></returns>
        public bool despacharPedido(Int32 codigoPedido, String correoAdmin, DateTime fechaDespacho, String estado)
        {
            DAO_Pedido daoPedido = new DAO_Pedido();
            return daoPedido.despacharPedido (codigoPedido, correoAdmin, fechaDespacho,estado);
        }

        public List<DO_Pedido> listarPedidosHabilitados()
        {
            DAO_Pedido daoPedido = new DAO_Pedido();
            return daoPedido.listarPedidos();
        }
    }
}
