using DAO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BL_Insumo
    {
        public bool guardarFactura(DO_Insumo doInsumo)
        {
            DAO_Insumo daoInsumo = new DAO_Insumo();
            if (daoInsumo.guardarInsumo(doInsumo) <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
