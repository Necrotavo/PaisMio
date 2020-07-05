using DAO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BL_Analisis_AA
    {
        /// <summary>
        /// Método para llamar al método agregarAnalisisAA del DAOanalisisAA
        /// </summary>
        /// <param name="analisisAA">objeto DO_Análisis_AA que contiene toda la información de un análisis de aguardiente</param>
        /// <returns> true si se agrega un análisis de aguardiente, false si ocurre algún error</returns>
        public bool agregarAnalisisAA(DO_Analisis_AA analisisAA) {
            /*GRADO ALCOHÓLICO APARENTE
                    PH
                    TURBIEDAD
                    DENSIDAD APARENTE
                    ALCOHOLES SUPERIORES
                    ALDEHÍDOS
                    ÉSTERES
                    METANOL*/
            //String[] tiposAnalisisFG = { "", "" };
            for (int i = 0; i < analisisAA.analisisFQs.Count; i++)
            {
                analisisAA.analisisFQs[i].pedCodigo = analisisAA.pedCodigo; 
            }

            DAO_Analisis_AA DAOanalisisAA = new DAO_Analisis_AA();
           
           // analisisAA.fechaEmision = System.DateTime.Now.ToString();
           // analisisAA.fechaVigencia = System.DateTime.Now.AddDays(100).ToString();
            return DAOanalisisAA.agregarAnalisisAA(analisisAA);
        }

        /// <summary>
        /// Método para llamar al método buscarAnalisisAAporPedCodigo del DAOanalisisAA
        /// </summary>
        /// <param name="pedCodigo"> código de un pedido</param>
        /// <returns>objeto DO_Analisis_AA con la información de un análisis de aguardiente, null si no se encuentra</returns>
        public DO_Analisis_AA buscarAnalisisAAporPedCodigo(int pedCodigo) {
            DAO_Analisis_AA DAOanalisisAA = new DAO_Analisis_AA();

            return DAOanalisisAA.buscarAnalisisAAporPedCodigo(pedCodigo);
        }

        public List<DO_Analisis_FQ> getTipoAnalisisFQs() {
            DAO_Analisis_AA DAOanalisisAA = new DAO_Analisis_AA();

            return DAOanalisisAA.getTipoAnalisisFQs();
        }
    }
}
