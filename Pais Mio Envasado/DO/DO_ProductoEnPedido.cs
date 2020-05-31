using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    [DataContract]
    public class DO_ProductoEnPedido
    {
        [DataMember (Name ="producto")]
        public DO_Producto producto { set; get; }
        [DataMember (Name ="cantidad")]
        public Int32 cantidad { set; get; }

        public DO_ProductoEnPedido(DO_Producto producto, Int32 cantidad)
        {
            this.producto = producto;
            this.cantidad = cantidad;
        }

        public DO_ProductoEnPedido()
        {
        }
    }
}
