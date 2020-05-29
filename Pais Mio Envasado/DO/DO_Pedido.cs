using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    [DataContract]
    public class DO_Pedido
    {
        [DataMember (Name = "codigo")]
        public Int32 codigo { set; get; }
        [DataMember(Name = "cedulaCliente")]
        public String cedulaCliente { set; get; }
        [DataMember(Name = "correoAdminIngreso")]
        public String correoAdminIngreso { set; get; }
        [DataMember(Name = "correoAdminDespacho")]
        public String correoAdminDespacho { set; get; } //Correo del administrador que despacha, null al inicio
        [DataMember(Name = "estado")]
        public String estado { set; get; }
        [DataMember(Name = "fechaIngreso")]
        public DateTime fechaIngreso { set; get; }
        [DataMember(Name = "fechaDespacho")]
        public DateTime fechaDespacho { set; get; } //Fecha de despacho, null al inicio
        [DataMember(Name = "listaProductos")]
        public List<DO_ProductoEnPedido>listaProductos { set; get; }

        public DO_Pedido(int codigo, string cedulaCliente, string correoAdminIngreso, string estado, DateTime fechaIngreso, List<DO_ProductoEnPedido> listaProductos)
        {
            this.codigo = codigo;
            this.cedulaCliente = cedulaCliente;
            this.correoAdminIngreso = correoAdminIngreso;
            this.estado = estado;
            this.fechaIngreso = fechaIngreso;
            this.listaProductos = listaProductos;
        }

        public DO_Pedido()
        {
        }
    }
}
