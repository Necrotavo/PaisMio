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
    }
}
