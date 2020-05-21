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
    public class DAO_Bodega
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

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

        public bool modificarCantDispInsumo(DO_InsumoEnBodega insumoEnBodega, Int32 codigoBodega) {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                if (existeInsumoEnbodega(insumoEnBodega, codigoBodega)) // Ya hay registro de ese insumo en la bodega
                {
                    SqlCommand modificarCantDisp = new SqlCommand("UPDATE INS_ESTA_BOD COLUMN IEB_CANTIDAD_DISPONIBLE = IEB_CANTIDAD_DISPONIBLE + @cantIngresa");
                    modificarCantDisp.Parameters.AddWithValue("@cantIngresa", insumoEnBodega.cantidadDisponible);

                    modificarCantDisp.ExecuteNonQuery();
                    return true;
                }
                else
                { //No hay registro del insumo en la bodega por lo que se crea e ingresa la cantidad
                    SqlCommand insert = new SqlCommand("INSERT INTO INS_ESTA_BOD (BOD_CODIGO, INS_CODIGO, IEB_CANTIDAD_DISPONIBLE)"
                            + "VALUES (@codigoBodega, @codigoInsumo, @cantidadDisponible, conexion)");
                    insert.Parameters.AddWithValue("@codigoBodega", codigoBodega);
                    insert.Parameters.AddWithValue("@codigoInsumo", insumoEnBodega.insumo);
                    insert.Parameters.AddWithValue("@cantidadDisponible", insumoEnBodega.cantidadDisponible);

                    insert.ExecuteNonQuery();
                    return true;
                }
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

        public bool entradaInsumos(DO_Bodega bodega)
        {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                foreach (DO_InsumoEnBodega insumoEnBodega in bodega.listaInsumosEnBodega)
                {
                    modificarCantDispInsumo(insumoEnBodega, bodega.codigo);
                }

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
