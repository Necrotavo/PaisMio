using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    /// <summary>
    /// Clase que los datos correspondientes a los pedidos.
    /// </summary>
    [DataContract]
    public class DO_Pedido
    {
        [DataMember (Name = "codigo")]
        public Int32 codigo { set; get; }
        [DataMember(Name = "cliente")]
        public DO_Cliente cliente { set; get; }
        [DataMember(Name = "correoAdminIngreso")]
        public String correoAdminIngreso { set; get; }
        [DataMember(Name = "correoAdminDespacho")]
        public String correoAdminDespacho { set; get; } //Correo del administrador que despacha, null al inicio
        [DataMember(Name = "estado")]
        public String estado { set; get; }
        [DataMember(Name = "fechaIngreso")]
        public DateTime fechaIngreso { set; get; }
        [DataMember(Name = "fechaDespacho")]
        public DateTime? fechaDespacho { set; get; } //Fecha de despacho, null al inicio
        [DataMember(Name = "listaProductos")]
        public List<DO_ProductoEnPedido>listaProductos { set; get; }

        public DO_Pedido(int codigo, DO_Cliente cliente, string correoAdminIngreso,List<DO_ProductoEnPedido> listaProductos)
        {
            this.codigo = codigo;
            this.cliente = cliente;
            this.correoAdminIngreso = correoAdminIngreso;
            this.listaProductos = listaProductos;
        }

        public DO_Pedido()
        {
        }
    }
}
