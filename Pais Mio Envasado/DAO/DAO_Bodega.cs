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
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ProductionConnection);

        /// <summary>
        /// Verifica si existe registro de un insumo dado en una bodega dada
        /// </summary>
        /// <param name="insumoEnBodega">Insumo que se va a buscar</param>
        /// <param name="codigoBodega">Bodega en la que se va a buscar</param>
        /// <returns>True si existe registro de ese insumo en bodega, false si no hay registro de este en la bodega</returns>
        public bool existeInsumoEnbodega(Int32 insumoEnBodega, Int32 codigoBodega) {
            SqlCommand insumoExiste = new SqlCommand("SELECT IEB_CANTIDAD_DISPONIBLE FROM INS_ESTA_BOD WHERE INS_CODIGO = @codigoInsumo AND BOD_CODIGO = @codigoBodega", conexion);
            insumoExiste.Parameters.AddWithValue("@codigoInsumo", insumoEnBodega);
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
                    if (existeInsumoEnbodega(insumoEnBodega.insumo.codigo, bodega.codigo)) // Ya hay registro de ese insumo en la bodega
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
        }/////////////////////////////////

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
            catch (SqlException)
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
            catch (SqlException)
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
            //return 6;
            SqlCommand registrarEntrada = new SqlCommand("INSERT INTO ENTRADA_INSUMO (OPE_CORREO, ENI_FECHA) VALUES (@correoOperario, @fecha)", conexion);
            registrarEntrada.Parameters.AddWithValue("@correoOperario", correoOperario);
            registrarEntrada.Parameters.AddWithValue("@fecha", DateTime.Now);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (registrarEntrada.ExecuteNonQuery() > 0)
                {
                    return obtenerCodigoUltimaEntrada();
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlException)
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
            catch (SqlException)
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
        private bool eliminarEntradaInsumo(Int32 codigoEntrada) {
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
                    doBodega.listaInsumosEnBodega = obtenerInsumosBodega(codigoBodega);
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

        /// <summary>
        /// Permite obtener los insumos que están en una bodega
        /// </summary>
        /// <param name="codigoBodega">Codigo de la bodega de la que se quieren los insumos</param>
        /// <returns>La lista de los insumos de la bodega, null si no encuentra la bodega, o la bodega no tiene insumos</returns>
        public List<DO_InsumoEnBodega> obtenerInsumosBodega(Int32 codigoBodega) {
            if (!existeBodega(codigoBodega)) {
                return null;
            } 

            List<DO_InsumoEnBodega> listaInsumosEnBodega = new List<DO_InsumoEnBodega>();

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
                    Int32 cantDisponible = Convert.ToInt32(row["IEB_CANTIDAD_DISPONIBLE"]);

                    if (cantDisponible > 0) {

                        DO_InsumoEnBodega insumoBodega = new DO_InsumoEnBodega();
                        DAO_Insumo daoInsumo = new DAO_Insumo();

                        insumoBodega.insumo = daoInsumo.buscarInsumoPorCódigo(Convert.ToInt32(row["INS_CODIGO"]));
                        insumoBodega.cantidadDisponible = cantDisponible;

                        listaInsumosEnBodega.Add(insumoBodega);
                    }
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

        private bool existeBodega(Int32 codigoBodega) {
            try
            {
                SqlCommand existe = new SqlCommand("SELECT BOD_CODIGO FROM BODEGA WHERE BOD_CODIGO = @codigoBodega", conexion);
                existe.Parameters.AddWithValue("@codigoBodega", codigoBodega);

                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }
                Object resultado = existe.ExecuteScalar();
                if (resultado != null) {
                    Int32 codigo = Convert.ToInt32(resultado);
                    if (codigo > 0) {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
            finally {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Permite registrar una bodega en la base de datos
        /// </summary>
        /// <param name="doBodega">Bodega que se va a ingresar</param>
        /// <returns>True si se ingresa, False si ocurre un error</returns>
        public bool registrarBodega(DO_Bodega doBodega) {
            
            SqlCommand insertBodega = new SqlCommand("INSERT BODEGA (EST_HAB_ESTADO, BOD_NOMBRE, BOD_DIRECCION, BOD_TELEFONO)" +
                "VALUES('HABILITADO',@nombre,@direccion,@telefono)", conexion);
            insertBodega.Parameters.AddWithValue("@nombre", doBodega.nombre);
            insertBodega.Parameters.AddWithValue("@direccion", doBodega.direccion);
            insertBodega.Parameters.AddWithValue("@telefono", doBodega.telefono);

            try
            {
                if (conexion.State != ConnectionState.Open) {
                    conexion.Open();
                }
                if (insertBodega.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally {
                if (conexion.State != ConnectionState.Closed) {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Permite modificar los datos de una bodega
        /// </summary>
        /// <param name="doBodega">Bodega con los datos a modificar</param>
        /// <returns>True si se modifica, false si no se modifica</returns>
        public bool modificarBodega(DO_Bodega doBodega) {
            SqlCommand modificarBodega = new SqlCommand("UPDATE BODEGA SET " +
                "BOD_NOMBRE = @nombre, " +
                "BOD_DIRECCION = @direccion, " +
                "BOD_TELEFONO = @telefono, " +
                "EST_HAB_ESTADO = @estado " +
                "WHERE BOD_CODIGO = @codigoBodega", conexion);
            modificarBodega.Parameters.AddWithValue("@nombre", doBodega.nombre);
            modificarBodega.Parameters.AddWithValue("@direccion", doBodega.direccion);
            modificarBodega.Parameters.AddWithValue("@telefono", doBodega.telefono);
            modificarBodega.Parameters.AddWithValue("@codigoBodega", doBodega.codigo);
            modificarBodega.Parameters.AddWithValue("@estado",doBodega.estado);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                if (modificarBodega.ExecuteNonQuery() > 0)
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

        /// <summary>
        /// Cambia el estado de las bodegas
        /// </summary>
        /// <param name="codigoBodega">Codigo de la bodega que se va a modificar</param>
        /// <param name="estado">El estado al que se va a cambiar</param>
        /// <returns>True si modifica el estado, false si no</returns>
        public bool cambiarEstadoBodega(Int32 codigoBodega, String estado) {
            SqlCommand modificarBodega = new SqlCommand("UPDATE BODEGA SET " +
                "EST_HAB_ESTADO = @estado " +
                "WHERE BOD_CODIGO = @codigoBodega", conexion);
            modificarBodega.Parameters.AddWithValue("@estado", estado);
            modificarBodega.Parameters.AddWithValue("@codigoBodega", codigoBodega);

            try
            {
                if (estado == "DESHABILITADO") {
                    if (tieneInsumosAsociados(codigoBodega)) { //No se podria deshabilitar porque tiene insumos asociados
                        return false;
                    }
                }
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                if (modificarBodega.ExecuteNonQuery() > 0)
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

        /// <summary>
        /// Confirma si la bodega aún tiene insumos asociados
        /// </summary>
        /// <param name="codigoBodega">Código de la bodega</param>
        /// <returns>True si tiene insumos asociados, false si no</returns>
        private bool tieneInsumosAsociados(Int32 codigoBodega)
        {
            SqlCommand modificarBodega = new SqlCommand("SELECT COUNT(INS_CODIGO) FROM INS_ESTA_BOD " +
                "WHERE (BOD_CODIGO = @codigoBodega AND IEB_CANTIDAD_DISPONIBLE > 0)", conexion);
            modificarBodega.Parameters.AddWithValue("@codigoBodega", codigoBodega);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                Object resultado = modificarBodega.ExecuteScalar();
                Int32 cantInsumosAsociados = 0;
                if (resultado != null)
                {
                    cantInsumosAsociados = Convert.ToInt32(resultado);
                }
                else {
                    return false;
                }
                
                if (cantInsumosAsociados <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
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

        /// <summary>
        /// Retorna la lista de bodegas, todas o solo las habilitadas
        /// </summary>
        /// <param name="todos">Define si requiere todas las bodegas o solo las habilitadas</param>
        /// <returns>La lista de bodegas solicitada</returns>
        public List<DO_Bodega> obtenerListaBodegas(bool todos)
        {
            String comando;
            if (todos)
            {
                comando = "SELECT * FROM BODEGA";
            }
            else
            {
                comando = "SELECT * FROM BODEGA WHERE EST_HAB_ESTADO = 'HABILITADO'";
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(comando, conexion);
            DataTable datatable = new DataTable();
            List<DO_Bodega> listaBodega = new List<DO_Bodega>();

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    DO_Bodega doBodega = new DO_Bodega();

                    doBodega.codigo = Convert.ToInt32(row["BOD_CODIGO"]);
                    doBodega.estado = (String)row["EST_HAB_ESTADO"];
                    doBodega.nombre = (String)row["BOD_NOMBRE"];
                    doBodega.telefono = (String)row["BOD_TELEFONO"];
                    doBodega.direccion = (String)row["BOD_DIRECCION"];

                    doBodega.listaInsumosEnBodega = obtenerInsumosBodega(doBodega.codigo);

                    listaBodega.Add(doBodega);
                }
                return listaBodega;
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
        /// Mueve una cantidad de insumos de una bodega a otra
        /// </summary>
        /// <param name="codigoDesdeBodega">Código de la bodega desde donde se van a mover los insumos</param>
        /// <param name="codigoHastaBodega">Código de la bodega hasta donde se van a mover los insumos</param>
        /// <param name="codigoInsumo">Código de insumo que se va a mover</param>
        /// <param name="cantidad">Cantidad del insumo que se va a mover</param>
        /// <returns>true si se mueve el insumo correctamente</returns>
        public bool moverInsumoDeBodega(Int32 codigoDesdeBodega, Int32 codigoHastaBodega, Int32 codigoInsumo, Int32 cantidad) {
            try
            {
                SqlCommand moverinsumo;
                String comando = "";
                Int32 cantidadInsumoDesdeBodega = cantidadInsumoBodega(codigoDesdeBodega, codigoInsumo);
                //Int32 cantidadInsumoHastaBodega = cantidadInsumoBodega(codigoHastaBodega, codigoInsumo);

                if ((cantidadInsumoDesdeBodega - cantidad) <= 0)
                {
                    return false;
                }

                if (existeInsumoEnbodega(codigoInsumo, codigoHastaBodega))
                {
                    comando = "UPDATE INS_ESTA_BOD SET IEB_CANTIDAD_DISPONIBLE = (IEB_CANTIDAD_DISPONIBLE + @cantidad) " +
                        "WHERE BOD_CODIGO = @hastaBodega AND INS_CODIGO = @codigoInsumo ";
                    comando += "UPDATE INS_ESTA_BOD SET IEB_CANTIDAD_DISPONIBLE = (IEB_CANTIDAD_DISPONIBLE - @cantidad) " +
                        "WHERE BOD_CODIGO = @desdeBodega AND INS_CODIGO = @codigoInsumo";
                }
                else {
                    comando = "INSERT INTO INS_ESTA_BOD (BOD_CODIGO, INS_CODIGO, IEB_CANTIDAD_DISPONIBLE) " +
                        "VALUES (@hastaBodega,@codigoInsumo,@cantidad) ";
                    comando += "UPDATE INS_ESTA_BOD SET IEB_CANTIDAD_DISPONIBLE = (IEB_CANTIDAD_DISPONIBLE - @cantidad) " +
                        "WHERE BOD_CODIGO = @desdeBodega AND INS_CODIGO = @codigoInsumo";
                }

                moverinsumo = new SqlCommand(comando, conexion);

                moverinsumo.Parameters.AddWithValue("@hastaBodega", codigoHastaBodega);
                moverinsumo.Parameters.AddWithValue("@desdeBodega", codigoDesdeBodega);
                moverinsumo.Parameters.AddWithValue("@codigoInsumo", codigoInsumo);
                moverinsumo.Parameters.AddWithValue("@cantidad", cantidad);


                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (moverinsumo.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
        } 

        /// <summary>
        /// Retorna la cantidad que hay de un insumo en una bodega
        /// </summary>
        /// <param name="codigoBodega">Código de la bodega en la que se busca</param>
        /// <param name="codigoInsumo">Có digo del insumo que se busca</param>
        /// <returns></returns>
        public Int32 cantidadInsumoBodega(Int32 codigoBodega, Int32 codigoInsumo)
        {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlCommand cantInsumoBodega = new SqlCommand("SELECT IEB_CANTIDAD_DISPONIBLE FROM INS_ESTA_BOD " +
                    "WHERE BOD_CODIGO = @codigoBodega AND INS_CODIGO = @codigoInsumo", conexion);
                cantInsumoBodega.Parameters.AddWithValue("@codigoBodega", codigoBodega);
                cantInsumoBodega.Parameters.AddWithValue("@codigoInsumo", codigoInsumo);

                Object result = cantInsumoBodega.ExecuteScalar();
                if (result != null)
                {
                    Int32 cantidad = Convert.ToInt32(result);
                    return cantidad;
                }
                else {
                    return 0;
                }
            }
            catch (SqlException)
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

        public String obtenerNombreBodega(Int32 codigoBodega)
        {
            SqlCommand consulta = new SqlCommand("SELECT BOD_NOMBRE FROM BODEGA WHERE BOD_CODIGO = @codigoBodega", conexion);
            consulta.Parameters.AddWithValue("@codigoBodega", codigoBodega);
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                SqlDataReader lector = consulta.ExecuteReader();
                if (lector.HasRows)
                {
                    String nombre = "";
                    while (lector.Read())
                    {
                        nombre = (String)lector["BOD_NOMBRE"];
                    }
                    return nombre;
                }
                else
                {
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
        /// Mueve todos los insumos de una bodega a otra
        /// </summary>
        /// <param name="codigoDesdeBodega">Códiga de la bodega de la que se van a mover los insumos</param>
        /// <param name="codigoHastaBodega">Código de la bodega en la que se van a mover los insumos</param>
        /// <returns>True si se muen¿ven de una bodega a la otra</returns>
        //public bool moverTodosInsumosDeBodega(Int32 codigoDesdeBodega, Int32 codigoHastaBodega) {
        //    try
        //    {
        //        if (conexion.State != ConnectionState.Open) {
        //            conexion.Open();
        //        }

        //        SqlCommand moverTodos = new SqlCommand("UPDATE INS_ESTA_BOD SET BOD_CODIGO = @codigoHastaBodega " +
        //                "WHERE BOD_CODIGO = @codigoDesdeBodega", conexion);
        //        moverTodos.Parameters.AddWithValue("@codigoHastaBodega", codigoHastaBodega);
        //        moverTodos.Parameters.AddWithValue("@codigoDesdeBodega", codigoDesdeBodega);

        //        if (moverTodos.ExecuteNonQuery() > 0)
        //        {
        //            return true;
        //        }
        //        else {
        //            return false;
        //        }
        //    }
        //    catch (SqlException)
        //    {
        //        return false;
        //    }
        //    finally {
        //        if (conexion.State != ConnectionState.Closed)
        //        {
        //            conexion.Close();
        //        }
        //    }
        //} ///////////////////////
    }
}
