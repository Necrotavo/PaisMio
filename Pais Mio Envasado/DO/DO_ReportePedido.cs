using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    /// <summary>
    /// Clase con los datos del reporte de pedidos
    /// </summary>
    [DataContract]
    public class DO_ReportePedido
    {
        [DataMember(Name = "codigoPedido")]
        public Int32 codigo { set; get; }

        [DataMember(Name = "nombreCliente")]
        public String nombreCliente { set; get; }

        [DataMember(Name = "adminIngreso")]
        public String nombreAdminIngreso { set; get; }

        [DataMember(Name = "adminDespacho")]
        public String nombreAdminDespacho { set; get; }

        [DataMember(Name = "fechaIngreso")]
        public DateTime fechaIngreso { set; get; }

        [DataMember(Name = "fechaDespacho")]
        public DateTime fechaDespacho { set; get; }

        [DataMember(Name = "productos")]
        public List<DO_ProductoEnPedido> productos { set; get; }

        public DO_ReportePedido(int codigo, string nombreCliente, string nombreAdminIngreso, string nombreAdminDespacho, DateTime fechaIngreso, DateTime fechaDespacho, List<DO_ProductoEnPedido> productos)
        {
            this.codigo = codigo;
            this.nombreCliente = nombreCliente;
            this.nombreAdminIngreso = nombreAdminIngreso;
            this.nombreAdminDespacho = nombreAdminDespacho;
            this.fechaIngreso = fechaIngreso;
            this.fechaDespacho = fechaDespacho;
            this.productos = productos;
        }

        public DO_ReportePedido()
        {
        }
    }
}
