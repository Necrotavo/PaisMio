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
            SqlCommand insumoExiste = new SqlCommand("SELECT IEB_CANTIDAD_DISPONIBLE FROM INS_ESTA_BOD WHERE INS_CODIGO = @codigoInsumo AND BOD_CODIGO = @codigoBodega", conexion);
            insumoExiste.Parameters.AddWithValue("@codigoInsumo", insumoEnBodega.insumo.codigo);
            insumoExiste.Parameters.AddWithValue("@codigoBodega", codigoBodega);
            
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                Object obj = insumoExiste.ExecuteScalar();
                if (obj is null)
                {
                    return false;
                }
                else {
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

        /// <summary>
        /// En este método se ingresan los insumos cuando hay entrada de insumos a una bodega
        /// </summary>
        /// <param name="bodega">Bodega con la lista de insumos entrantes</param>
        /// <returns>True si logra ingresar los insumos, false si sucede un error</returns>
        public bool entradaInsumos(DO_Bodega bodega, String correoOperario)
        {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                Int32 eniCodigo = registrarEntradaInsumo(correoOperario);

                String comando = "BEGIN TRANSACTION ";

                foreach (DO_InsumoEnBodega insumoEnBodega in bodega.listaInsumosEnBodega)
                {
                    if (existeInsumoEnbodega(insumoEnBodega, bodega.codigo)) // Ya hay registro de ese insumo en la bodega
                    {
                        comando += "UPDATE INS_ESTA_BOD SET IEB_CANTIDAD_DISPONIBLE = IEB_CANTIDAD_DISPONIBLE + " + insumoEnBodega.cantidadDisponible 
                            + " WHERE BOD_CODIGO = "+ bodega.codigo +" AND INS_CODIGO = "+ insumoEnBodega.insumo.codigo + " ";
                    }
                    else
                    { //No hay registro del insumo en la bodega por lo que se crea e ingresa la cantidad
                        comando += "INSERT INTO INS_ESTA_BOD (BOD_CODIGO, INS_CODIGO, IEB_CANTIDAD_DISPONIBLE)"
                                + "VALUES (" + bodega.codigo + ", " + insumoEnBodega.insumo.codigo + ", " + insumoEnBodega.cantidadDisponible + ") ";
                    }
                    comando += "INSERT INTO INSUMO_ENTRANTE (ENI_CODIGO, BOD_CODIGO, INS_CODIGO, IENT_CANTIDAD)"
                                + "VALUES (" + eniCodigo + "," + bodega.codigo + ", " + insumoEnBodega.insumo.codigo + ", " + insumoEnBodega.cantidadDisponible + ") ";
                }

                comando += "COMMIT";
                SqlCommand ingresarLista = new SqlCommand(comando, conexion);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (ingresarLista.ExecuteNonQuery() <= 0)
                {
                    eliminarEntradaInsumo(eniCodigo);
                    return false;
                }
                else {
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

        /// <summary>
        /// Registra una entrada de insumo de la base de datos
        /// </summary>
        /// <param name="correoOperario">Correo del operario que realiza la entrada</param>
        /// <param name="codigoBodega">Codigo de la bodega a la que entran los insumos</param>
        /// <returns>El código de la entrada de insumo registrada, 0 si sucede un error</returns>
        public Int32 registrarEntradaInsumo(String correoOperario) {
            return 6;
            //SqlCommand registrarEntrada = new SqlCommand("INSERT INTO ENTRADA_INSUMO (OPE_CORREO, ENI_FECHA) VALUES (@correoOperario,"+ DateTime.Now.ToString("dd/MM/yyyy")+")", conexion);
            //registrarEntrada.Parameters.AddWithValue("@correoOperario", correoOperario);

            //try
            //{
            //    if (conexion.State != ConnectionState.Open)
            //    {
            //        conexion.Open();
            //    }

            //    if (registrarEntrada.ExecuteNonQuery() > 0)
            //    {
            //        return obtenerCodigoUltimaEntrada();
            //    }
            //    else {
            //        return 0;
            //    }
            //}
            //catch (SqlException e)
            //{
            //    return 0;
            //}
            //finally
            //{
            //    if (conexion.State != ConnectionState.Closed)
            //    {
            //        conexion.Close();
            //    }
            //}
        }

        /// <summary>
        /// Con este método se obtiene el código de la última entrada registrada
        /// </summary>
        /// <returns>El código de la entrada, 0 si sucede un error</returns>
        public Int32 obtenerCodigoUltimaEntrada() {
            SqlCommand obtenerCodigo = new SqlCommand("SELECT ENI_CODIGO FROM ENTRADA_INSUMO ORDER BY ENI_CODIGO DESC", conexion);

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
        /// Con este método se elimina una entrada de la base de datos
        /// </summary>
        /// <param name="codigoEntrada">Codigo de la entrada que se va a eliminar</param>
        /// <returns>True si se elimina, False si hay un error</returns>
        public bool eliminarEntradaInsumo(Int32 codigoEntrada) {
            SqlCommand eliminarEntrada = new SqlCommand("DELETE FROM ENTRADA_INSUMO WHERE ENI_CODIGO = @codigoEntrada", conexion);
            eliminarEntrada.Parameters.AddWithValue("@codigoEntrada", codigoEntrada);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (eliminarEntrada.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else {
                    return false;
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


        /// <summary>
        /// Este metodo permite sacar los datos de una bodega y la lista de insumos que contiene
        /// </summary>
        /// <param name="codigoBodega">Codigo de la bodega</param>
        /// <returns>La bodega con todos sus datos</returns>
        public DO_Bodega obtenerBodega(Int32 codigoBodega) {
            SqlCommand consultaBodega = new SqlCommand("SELECT * FROM BODEGA WHERE BOD_CODIGO = @codigoBodega", conexion);
            consultaBodega.Parameters.AddWithValue("@codigoBodega", codigoBodega);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                DO_Bodega doBodega = new DO_Bodega();

                SqlDataReader lectorConsultaBodega = consultaBodega.ExecuteReader();
                if (lectorConsultaBodega.HasRows)
                {
                    while (lectorConsultaBodega.Read())
                    {
                        doBodega.codigo = Convert.ToInt32(lectorConsultaBodega["BOD_CODIGO"]);
                        doBodega.estado = (String)lectorConsultaBodega["EST_HAB_ESTADO"];
                        doBodega.nombre = (String)lectorConsultaBodega["BOD_NOMBRE"];
                        doBodega.direccion = (String)lectorConsultaBodega["BOD_DIRECCION"];
                        doBodega.telefono = (String)lectorConsultaBodega["BOD_TELEFONO"];
                    }//Ya se sacaron los datos de la bodega, faltan los insumos que tiene
                    lectorConsultaBodega.Close();
                    doBodega.listaInsumosEnBodega = obtenerInsumosBodega(new List<DO_InsumoEnBodega>(), codigoBodega);
                }
                return doBodega;
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

        public List<DO_InsumoEnBodega> obtenerInsumosBodega(List<DO_InsumoEnBodega> listaInsumosEnBodega, Int32 codigoBodega) {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                
                SqlDataAdapter adaptador = new SqlDataAdapter();
                DataTable datatable = new DataTable();
                adaptador.SelectCommand = new SqlCommand("SELECT * FROM INS_ESTA_BOD WHERE BOD_CODIGO = @codigoBodega", conexion);
                adaptador.SelectCommand.Parameters.AddWithValue("@codigoBodega", codigoBodega);

                adaptador.Fill(datatable);

                listaInsumosEnBodega = new List<DO_InsumoEnBodega>();
                foreach (DataRow row in datatable.Rows)
                {
                    DO_InsumoEnBodega insumoBodega = new DO_InsumoEnBodega();
                    insumoBodega.insumo = new DO_Insumo();

                    insumoBodega.insumo.codigo = Convert.ToInt32(row["INS_CODIGO"]);
                    insumoBodega.cantidadDisponible = Convert.ToInt32(row["IEB_CANTIDAD_DISPONIBLE"]);

                    listaInsumosEnBodega.Add(insumoBodega);
                }
                return listaInsumosEnBodega;
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
    }
}
