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
        [DataMember(Name = "listaPedidos")]
        public List<DO_Pedido> listaPedidos { get; set; }

        [DataMember(Name = "infoPaisMio")]
        public DO_PaisMio infoPaisMio { get; set; }

        [DataMember(Name = "fechaInicio")]
        public String fechaInicio { get; set; }

        [DataMember(Name = "fechaFinal")]
        public String fechaFinal { get; set; }

        public DO_ReportePedido(List<DO_Pedido> listaPedidos, DO_PaisMio infoPaisMio, string fechaInicio, string fechaFinal)
        {
            this.listaPedidos = listaPedidos;
            this.infoPaisMio = infoPaisMio;
            this.fechaInicio = fechaInicio;
            this.fechaFinal = fechaFinal;
        }

        public DO_ReportePedido(){}
    }
}
