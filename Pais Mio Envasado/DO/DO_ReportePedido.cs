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

        [DataMember(Name = "mes")]
        public Int32 mes { get; set; }

        [DataMember(Name = "anho")]
        public Int32 anho { get; set; }

        public DO_ReportePedido(List<DO_Pedido> listaPedidos, DO_PaisMio infoPaisMio, int mes, int anho)
        {
            this.listaPedidos = listaPedidos;
            this.infoPaisMio = infoPaisMio;
            this.mes = mes;
            this.anho = anho;
        }

        public DO_ReportePedido(){}
    }
}
