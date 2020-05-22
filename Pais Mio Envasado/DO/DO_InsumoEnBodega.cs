using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Esta clase sirve para representar un insumo y una cantidad del mismo, para hacer listas
    /// </summary>
    public class DO_InsumoEnBodega
    {
        public DO_Insumo insumo { set; get; }
        public Int32 cantidadDisponible { set; get; }
    }
}
