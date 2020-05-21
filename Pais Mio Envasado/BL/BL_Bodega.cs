using DAO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BL_Bodega
    {
        public bool entradaInsumos(DO_Bodega doBodega) {
            DAO_Bodega daoBodega = new DAO_Bodega();
            return daoBodega.entradaInsumos(doBodega);
        }
    }
}
