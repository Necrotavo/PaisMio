using DO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// En esta clase se encuentra la logica de negocio de los productos
    /// </summary>
    public class BL_Producto
    {
        /// <summary>
        /// Este m[etodo registra un producto
        /// </summary>
        /// <param name="doProducto">Producto a ingresar</param>
        /// <returns>True si se ingresa exitosamente, si no, false</returns>
        public bool ingresarProducto(DO_Producto doProducto) {
            DAO_Producto daoProducto = new DAO_Producto();
            return daoProducto.ingresarProducto(doProducto);
        }

        /// <summary>
        /// Retorna la lista de productos existentes
        /// </summary>
        /// <returns>La lista de productos existentes, null si no hay o no se pudo sacar la lista</returns>
        public List<DO_Producto> obtenerListaProductos() {
            DAO_Producto daoProducto = new DAO_Producto();
            return daoProducto.obtenerListaProductos(true);
        }

        /// <summary>
        /// Retorna la lista de productos habilitados
        /// </summary>
        /// <returns>Lista de productos habilitados</returns>
        public List<DO_Producto> obtenerListaProductosHabilitados()
        {
            DAO_Producto daoProducto = new DAO_Producto();
            return daoProducto.obtenerListaProductos(false);
        }

        /// <summary>
        /// Retorna un producto a partir de un codigo dado
        /// </summary>
        /// <param name="codigoProducto">Codigo del producto a retornar</param>
        /// <returns>El producto buscado, null si no lo puede encontrar</returns>
        public DO_Producto obtenerProducto(Int32 codigoProducto) {
            DAO_Producto daoProducto = new DAO_Producto();
            return daoProducto.obtenerProducto(codigoProducto);
        }

        /// <summary>
        /// Modifica los datos de un producto registrado
        /// </summary>
        /// <param name="productoModificado">Producto con los datos actualizados que se van a registrar</param>
        /// <returns>True si se logra modificar el producto, false si no se logra</returns>
        public bool modificarProducto(DO_Producto productoModificado) {
            DAO_Producto daoProducto = new DAO_Producto();
            return daoProducto.modificarProducto(productoModificado);
        }
    }
}
