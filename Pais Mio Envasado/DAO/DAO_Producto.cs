using DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    /// <summary>
    /// Esta clase contiene el acceso a base de datos de los productos
    /// </summary>
    public class DAO_Producto
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);
        
        /// <summary>
        /// Este metodo permite registrar un producto en la base de datos
        /// </summary>
        /// <param name="doProducto"> Es el producto que se va a guardar </param>
        /// <returns>true si se ingresa el producto, false si no se logra ingresar</returns>
        public bool ingresarProducto(DO_Producto doProducto) {
            SqlCommand comandoInsertar = new SqlCommand("INSERT INTO PRODUCTO (EST_HAB_ESTADO, PRO_NOMBRE, PRO_DESCRIPCION) " +
            "VALUES ('HABILITADO', @nombre, @descripcion)", conexion);
            
            comandoInsertar.Parameters.AddWithValue("@nombre", doProducto.nombre);
            comandoInsertar.Parameters.AddWithValue("@descripcion", doProducto.descripcion);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                comandoInsertar.ExecuteNonQuery();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Saca la lista de los productos de la base de datos
        /// </summary>
        /// <returns>La lista de productos, si no se sacan objetos, retorna un null</returns>
        public List<DO_Producto> obtenerListaProductos() {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("SELECT * FROM PRODUCTO", conexion);
            DataTable datatable = new DataTable();
            List<DO_Producto> listaProductos = new List<DO_Producto>();

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    DO_Producto nuevoProducto = new DO_Producto();

                    nuevoProducto.codigo = Convert.ToInt32(row["PRO_CODIGO"]);
                    nuevoProducto.estado = new DO_EstadoHabilitacion((String)row["EST_HAB_ESTADO"]);
                    nuevoProducto.nombre = (String)row["PRO_NOMBRE"];
                    nuevoProducto.descripcion = (String)row["PRO_DESCRIPCION"];

                    listaProductos.Add(nuevoProducto);
                }
                return listaProductos;
            }
            catch (SqlException) {
                return null;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Busca el codigo de un producto por nombre
        /// </summary>
        /// <param name="nombreProducto">El nombre del producto para buscar</param>
        /// <returns>retorna 0 si no lo encuentra y si lo encuentra retorna el codigo</returns>
        public int obtenerCodigoProducto(String nombreProducto) {
            SqlCommand obtenerCodigo = new SqlCommand("SELECT PRO_CODIGO FROM PRODUCTO WHERE PRO_NOMBRE = @nombreProducto", conexion);

            try {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                Int32 codigoEncontrado = Convert.ToInt32(obtenerCodigo.ExecuteScalar());
                return codigoEncontrado;
            }
            catch (SqlException) {
                return 0;
            }
            finally {

            }
        }

        /// <summary>
        /// Saca los datos de un producto buscado por codigo
        /// </summary>
        /// <param name="doProducto">Codigo del producto para buscar</param>
        /// <returns>Retorna el producto si lo encuentra y si no retorna null</returns>
        public DO_Producto obtenerProducto(Int32 codigoProducto) {
            SqlCommand consultaCredito = new SqlCommand("SELECT * FROM PRODUCTO WHERE PRO_CODIGO = @codigoProducto", conexion);
            consultaCredito.Parameters.AddWithValue("@codigoProducto", codigoProducto);

            DO_Producto doProducto = new DO_Producto();
            doProducto.codigo = codigoProducto;

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                SqlDataReader lector = consultaCredito.ExecuteReader();
                if (lector.HasRows)
                {
                    doProducto.estado = new DO_EstadoHabilitacion((String)(lector["EST_HAB_ESTADO"]));
                    doProducto.nombre = (String)(lector["PRO_NOMBRE"]);
                    doProducto.descripcion = (String)(lector["PRO_DESCRIPCION"]);
                    return doProducto;
                }
                else {
                    return null;
                }
            }
            catch (SqlException)
            {
                return null;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Modifica los datos de un producto dado
        /// </summary>
        /// <param name="doProducto">El producto con los datos a modificar</param>
        /// <returns>True si lo logra modificar, false si no lo logra</returns>
        public bool modificarProducto(DO_Producto doProducto) {
            SqlCommand comandoModificar = new SqlCommand("UPDATE PRODUCTO SET PRO_NOMBRE = @nombreProducto, " +
                "PRO_DESCRIPCION = @descripcionProducto, " +
                "EST_HAB_ESTADO = @estado" +
                "where PRO_CODIGO = @codigoProducto", conexion);

            comandoModificar.Parameters.AddWithValue("@descripcionProducto", doProducto.descripcion);
            comandoModificar.Parameters.AddWithValue("@estado", doProducto.estado.estado);
            comandoModificar.Parameters.AddWithValue("@nombreProveedor", doProducto.nombre);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                if (comandoModificar.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }
    }
}
