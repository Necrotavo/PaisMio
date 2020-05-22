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
    /// Esta clase permite el acceso a base de datos relacionado a Bodega 
    /// </summary>
    public class DAO_Bodega
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        /// <summary>
        /// Verifica si existe registro de un insumo dado en una bodega dada
        /// </summary>
        /// <param name="insumoEnBodega">Insumo que se va a buscar</param>
        /// <param name="codigoBodega">Bodega en la que se va a buscar</param>
        /// <returns>True si existe registro de ese insumo en bodega, false si no hay registro de este en la bodega</returns>
        public bool existeInsumoEnbodega(DO_InsumoEnBodega insumoEnBodega, Int32 codigoBodega) {
            SqlCommand insumoExiste = new SqlCommand("SELECT IEB_CANTIDAD_DISPONIBLE FROM INS_ESTA_BOD WHERE INS_CODIGO = @codigoInsumo AND BOD_CODIGO = @codigoBodega)");
            insumoExiste.Parameters.AddWithValue("@codigoInsumo", insumoEnBodega.insumo.codigo);
            insumoExiste.Parameters.AddWithValue("@codigoBodega", codigoBodega);
            
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                insumoExiste.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e)
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
        /// En este método se ingresan los insumos cuando hay entrada de insumos a una bodega
        /// </summary>
        /// <param name="bodega">Bodega con la lista de insumos entrantes</param>
        /// <returns>True si logra ingresar los insumos, false si sucede un error</returns>
        public bool entradaInsumos(DO_Bodega bodega)
        {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                String comando = "";

                foreach (DO_InsumoEnBodega insumoEnBodega in bodega.listaInsumosEnBodega)
                {
                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }
                    if (existeInsumoEnbodega(insumoEnBodega, bodega.codigo)) // Ya hay registro de ese insumo en la bodega
                    {
                        comando += "UPDATE INS_ESTA_BOD COLUMN IEB_CANTIDAD_DISPONIBLE = IEB_CANTIDAD_DISPONIBLE + " + insumoEnBodega.cantidadDisponible 
                            + " WHERE BOD_CODIGO = "+ bodega.codigo +"AND INS_CODIGO = "+ insumoEnBodega.insumo.codigo + " ";
                    }
                    else
                    { //No hay registro del insumo en la bodega por lo que se crea e ingresa la cantidad
                        comando += "INSERT INTO INS_ESTA_BOD (BOD_CODIGO, INS_CODIGO, IEB_CANTIDAD_DISPONIBLE)"
                                + "VALUES (" + bodega.codigo + ", " + insumoEnBodega.insumo.codigo + ", " + insumoEnBodega.cantidadDisponible + ") ";
                    }
                }

                SqlCommand ingresarLista = new SqlCommand(comando, conexion);
                ingresarLista.ExecuteNonQuery();

                return true;
            }
            catch (SqlException e)
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
        /// Este metodo sirve para obtener el codigo de la última bodega ingresada
        /// </summary>
        /// <returns>El código de la última bodega</returns>
        public int obtenerCodigoUltimoBodega()
        {
            SqlCommand obtenerCodigo = new SqlCommand("Select BOD_CODIGO from BODEGA ORDER BY BOD_CODIGO DESC", conexion);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                return Convert.ToInt32(obtenerCodigo.ExecuteScalar());

            }
            catch (SqlException e)
            {
                return 0;
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
        /// Permite buscar el código de una bodega por nombre
        /// </summary>
        /// <param name="nombre">Nombre de la bodega que se va a buscar</param>
        /// <returns>El código de la Bodega buscada</returns>
        public int buscarCodigoBodega(String nombre)
        {
            SqlCommand obtenerCodigo = new SqlCommand("SELECT BOD_CODIGO FROM BODEGA WHERE BOD_NOMBRE = @nombre;", conexion);
            obtenerCodigo.Parameters.AddWithValue("@nombre", nombre);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                return Convert.ToInt32(obtenerCodigo.ExecuteScalar());

            }
            catch (SqlException e)
            {
                return 0;
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
