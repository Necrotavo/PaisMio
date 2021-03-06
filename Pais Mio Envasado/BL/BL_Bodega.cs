﻿using DAO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// En esta clase se encuentra la logica de negocio de las Bodegas
    /// </summary>
    public class BL_Bodega
    {
        public BL_Bodega() { }

        /// <summary>
        /// Este método permite la entrada de insumos al inventario
        /// </summary>
        /// <param name="doBodega">Bodega con la lista de insumos entrantes</param> 
        /// <param name="correoOperario">Correo del operario que realiza la entrada</param>
        /// <returns>True si se ingresan los insumos en la bodega, false si sucede un error</returns>
        public bool entradaInsumos(DO_Bodega doBodega, String correoOperario) {
            if (correoOperario.Equals("") || entradaInvalida(doBodega))
            {
                return false;
            }
            else {
                DAO_Bodega daoBodega = new DAO_Bodega();
                return daoBodega.entradaInsumos(doBodega, correoOperario);
            }
        }

        /// <summary>
        /// Determina si los datos para la entrada de insumos son inválidos o no
        /// </summary>
        /// <param name="doBodega">Bodega con los datos a verificar</param>
        /// <returns>True si tiene los datos inválidos, false si son válidos</returns>
        private bool entradaInvalida(DO_Bodega doBodega) {
            if (doBodega.codigo <= 0 || doBodega.listaInsumosEnBodega is null
                || doBodega.listaInsumosEnBodega.Count <= 0) {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Recupera una bodega de la base de datos con su lista de insumos
        /// </summary>
        /// <param name="codigoBodega">Código de bodega que se busca</param>
        /// <returns>La bodega buscada o null si sucede un error</returns>
        public DO_Bodega obtenerBodega(int codigoBodega) {
            if (codigoBodega <= 0)
            {
                return null;
            }
            else {
                DAO_Bodega daoBodega = new DAO_Bodega();
                return daoBodega.obtenerBodega(codigoBodega);
            }
        }

        /// <summary>
        /// Saca la lista de los insumos de una bodega
        /// </summary>
        /// <param name="codigoBodega">Código de la bodega buscada</param>
        /// <returns>Lista de los insumos de la bodega</returns>
        public List<DO_InsumoEnBodega> obtenerInsumosBodega(Int32 codigoBodega) {
            if (codigoBodega <= 0)
            {
                return null;
            }
            else
            {
                DAO_Bodega daoBodega = new DAO_Bodega();
                return daoBodega.obtenerInsumosBodega(codigoBodega);
            }
        }

        /// <summary>
        /// Registra una bodega en el sistema
        /// </summary>
        /// <param name="doBodega">La bodega que se va a ingresar</param>
        /// <returns>True si se ingresa la bodega</returns>
        public bool registrarBodega(DO_Bodega doBodega)
        {
            if (doBodega is null || bodegaVacia(doBodega))
            {
                return false;
            }
            else
            {
                DAO_Bodega daoBodega = new DAO_Bodega();
                return daoBodega.registrarBodega(doBodega);
            }
        }

        private bool bodegaVacia(DO_Bodega doBodega) {
            if (doBodega.nombre is null || doBodega.direccion is null || doBodega.telefono is null 
                || doBodega.nombre == "" || doBodega.direccion == "" || doBodega.telefono == "")
            {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Modifica los datos de una bodega
        /// </summary>
        /// <param name="doBodega">bodega con los datos que se van a modificar</param>
        /// <returns>true si se mondifican, false si no</returns>
        public bool modificarBodega(DO_Bodega doBodega)
        {
            if (doBodega is null)
            {
                return false;
            }
            else
            {
                DAO_Bodega daoBodega = new DAO_Bodega();
                return daoBodega.modificarBodega(doBodega);
            }
        }

        /// <summary>
        /// Cambia el estado de una bodega
        /// </summary>
        /// <param name="codigoBodega">Codigo de la bodega que se va a modificar</param>
        /// <param name="estado">Estado al que se va a cambiar la bodega</param>
        /// <returns>True si se altera el estado</returns>
        public bool cambiarEstadoBodega(Int32 codigoBodega, String estado)
        {
            if (codigoBodega <= 0 || estado is null)
            {
                return false;
            }
            else
            {
                DAO_Bodega daoBodega = new DAO_Bodega();
                return daoBodega.cambiarEstadoBodega(codigoBodega, estado);
            }
        }

        /// <summary>
        /// Saca la lista de todas las bodegas
        /// </summary>
        /// <returns></returns>
        public List<DO_Bodega> obtenerListaBodegas()
        {
            DAO_Bodega daoBodega = new DAO_Bodega();
            return daoBodega.obtenerListaBodegas(true);
        }

        /// <summary>
        /// Saca la lista de las bodegas habilitadas
        /// </summary>
        /// <returns></returns>
        public List<DO_Bodega> obtenerListaBodegasHabilitados()
        {
            DAO_Bodega daoBodega = new DAO_Bodega();
            return daoBodega.obtenerListaBodegas(false);
        }

        /// <summary>
        /// Permite mover una cantidad de un insumo de una bodega a otra
        /// </summary>
        /// <param name="codigoDesdeBodega">Código de la bodega de donde se sacan los insumos</param>
        /// <param name="codigoHastaBodega">Código de la bodega donde se van a depositar los insumos</param>
        /// <param name="codigoInsumo">Código del insumo que se moverá de bodega</param>
        /// <param name="cantidad">Cantidad de insumos que se van a mover</param>
        /// <returns>True si se mueven los insumos correctamente</returns>
        public bool moverInsumoDeBodega(Int32 codigoDesdeBodega, Int32 codigoHastaBodega, Int32 codigoInsumo, Int32 cantidad) {
            if (codigoDesdeBodega <= 0 || codigoHastaBodega <= 0  || codigoInsumo <= 0  || cantidad <= 0)
            {
                return false;
            }
            else
            {
                DAO_Bodega daoBodega = new DAO_Bodega();
                return daoBodega.moverInsumoDeBodega(codigoDesdeBodega, codigoHastaBodega, codigoInsumo, cantidad);
            }
        }

        /// <summary>
        /// Permite mover todos los insumos de una bodega a otra
        /// </summary>
        /// <param name="codigoDesdeBodega">Código de la bodega de donde se sacan los insumos</param>
        /// <param name="codigoHastaBodega">Código de la bodega donde se van a depositar los insumos</param>
        /// <returns>True si se mueven los insumos correctamente</returns>
        //public bool moverTodosInsumosDeBodega(Int32 codigoDesdeBodega, Int32 codigoHastaBodega)
        //{
        //    if (codigoDesdeBodega <= 0 || codigoHastaBodega <= 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        DAO_Bodega daoBodega = new DAO_Bodega();
        //        return daoBodega.moverTodosInsumosDeBodega(codigoDesdeBodega, codigoHastaBodega);
        //    }
        //}
    }
}
