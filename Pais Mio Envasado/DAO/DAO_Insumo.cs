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
    /// En esta clase se trabajan las acciones de acceso a base de datos relacionadas con los insumos
    /// </summary>
    public class DAO_Insumo
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        /// <summary>
        /// Permite guardar un insumo en la base de datos
        /// </summary>
        /// <param name="insumo">Insumo que se va a guardar</param>
        /// <returns>El código del insumo guardado, 0 si no se puede crear</returns>
        public int guardarInsumo(DO_Insumo insumo)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO INSUMO (EST_HAB_ESTADO, UDM_UNIDAD, INS_NOMBRE, INS_CANT_MIN_STOCK, ID)" +
                "VALUES ('HABILITADO', @unidad, @nombre, @cantMinStock, @id)", conexion);
            insert.Parameters.AddWithValue("@unidad", insumo.unidad);
            insert.Parameters.AddWithValue("@nombre", insumo.nombre);
            insert.Parameters.AddWithValue("@cantMinStock", insumo.cantMinStock);
            insert.Parameters.AddWithValue("@id", insumo.id);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (insert.ExecuteNonQuery() > 0)
                {

                    return obtenerCodigoUltimoInsumo();
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
        /// Busca el código del último insumo creado
        /// </summary>
        /// <returns>El código del último insumo creado, 0 si no se hay un error</returns>
        public int obtenerCodigoUltimoInsumo() {
            SqlCommand obtenerCodigo = new SqlCommand("Select INS_CODIGO from INSUMO ORDER BY INS_CODIGO DESC", conexion);

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
        /// Busca el código de un insumo por nombre
        /// </summary>
        /// <param name="nombre">Nombre por el que se va a buscar el insumo</param>
        /// <returns>El código del insumo buscado, o 0 si no se encuentra</returns>
        public int buscarCodigoInsumo(String nombre) {
            SqlCommand obtenerCodigo = new SqlCommand("SELECT INS_CODIGO FROM INSUMO WHERE INS_NOMBRE = @nombre;", conexion);
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
        /// Este método permite recuperar un insumo buscado por código
        /// </summary>
        /// <param name="codigo">Código por el que se va a buscar el insumo</param>
        /// <returns>El insumo si lo encuentra, null si no existe el insumo</returns>
        public DO_Insumo buscarInsumoPorCódigo(Int32 codigo) {
            SqlCommand consulta = new SqlCommand("SELECT * FROM INSUMO WHERE INS_CODIGO = @codigo ", conexion);
            consulta.Parameters.AddWithValue("@codigo", codigo);
            DO_Insumo insumo = new DO_Insumo();
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                SqlDataReader lector = consulta.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        insumo.codigo = codigo;
                        insumo.estado = (String)lector["EST_HAB_ESTADO"];
                        insumo.unidad = (String)(lector["UDM_UNIDAD"]);
                        insumo.nombre = (String)lector["INS_NOMBRE"];
                        insumo.cantMinStock = Convert.ToInt32(lector["INS_CANT_MIN_STOCK"]);
                        insumo.id = (String)(lector["ID"]);
                    }
                    return insumo;
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
        /// Este método retorna la lista de insummos completa
        /// </summary>
        /// <returns>La lista de insumos que hay</returns>
        public List<DO_Insumo> obtenerListaIsumos() {
            SqlDataAdapter adaptador = new SqlDataAdapter();
            DataTable datatable = new DataTable();
            List<DO_Insumo> listaInsumos = new List<DO_Insumo>();

            adaptador.SelectCommand = new SqlCommand("SELECT * FROM INSUMO", conexion);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                adaptador.Fill(datatable);

                foreach (DataRow fila in datatable.Rows)
                {
                    DO_Insumo doInsumo = new DO_Insumo();

                    doInsumo.codigo = Convert.ToInt32(fila["INS_CODIGO"]);
                    doInsumo.estado = (String)fila["EST_HAB_ESTADO"];
                    doInsumo.unidad = (String)fila["UDM_UNIDAD"];
                    doInsumo.nombre = (String)fila["INS_NOMBRE"];
                    doInsumo.cantMinStock = Convert.ToInt32(fila["INS_CANT_MIN_STOCK"]);
                    doInsumo.id = (String)(fila["ID"]);

                    listaInsumos.Add(doInsumo);

                }
                return listaInsumos;
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
        /// Este método recupera la lista de insumos habilitados
        /// </summary>
        /// <returns>La lista de insumos habilitados, null si ha ocurrido un problema</returns>
        public List<DO_Insumo> obtenerListaIsumosHabilitados()
        {
            SqlDataAdapter adaptador = new SqlDataAdapter();
            DataTable datatable = new DataTable();
            List<DO_Insumo> listaInsumos = new List<DO_Insumo>();

            adaptador.SelectCommand = new SqlCommand("SELECT * FROM INSUMO WHERE EST_HAB_ESTADO = 'HABILITADO'", conexion);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                adaptador.Fill(datatable);

                foreach (DataRow fila in datatable.Rows)
                {
                    DO_Insumo doInsumo = new DO_Insumo();

                    doInsumo.codigo = Convert.ToInt32(fila["INS_CODIGO"]);
                    doInsumo.estado = (String)fila["EST_HAB_ESTADO"];
                    doInsumo.unidad = (String)fila["UDM_UNIDAD"];
                    doInsumo.nombre = (String)fila["INS_NOMBRE"];
                    doInsumo.cantMinStock = Convert.ToInt32(fila["INS_CANT_MIN_STOCK"]);
                    doInsumo.id = (String)(fila["ID"]);

                    listaInsumos.Add(doInsumo);

                }
                return listaInsumos;
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
        /// Permite modificar el nombre, la cantidad minima, el estado y la unidad de un insumo
        /// </summary>
        /// <param name="doInsumo">Insumo con los datos que se van a modificar</param>
        /// <returns>True si es ingresado, false si sucede un error</returns>
        public bool modificarInsumo(DO_Insumo doInsumo) {
            SqlCommand comandoModificar = new SqlCommand("UPDATE INSUMO SET INS_NOMBRE = @nombreInsumo, " +
                "INS_CANT_MIN_STOCK = @cantMin, " +
                "EST_HAB_ESTADO = @estado ," +
                "UDM_UNIDAD = @unidad ," +
                "ID = @id " +
                "where INS_CODIGO = @codigoInsumo", conexion);

            comandoModificar.Parameters.AddWithValue("@nombreInsumo", doInsumo.nombre);
            comandoModificar.Parameters.AddWithValue("@cantMin", doInsumo.cantMinStock);
            comandoModificar.Parameters.AddWithValue("@estado", doInsumo.estado);
            comandoModificar.Parameters.AddWithValue("@unidad", doInsumo.unidad);
            comandoModificar.Parameters.AddWithValue("@id", doInsumo.id);
            comandoModificar.Parameters.AddWithValue("@codigoInsumo", doInsumo.codigo);

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
        /*
        public String obtenerNombreInsumo(Int32 codigoInsumo) {
            SqlCommand consulta = new SqlCommand("SELECT INS_NOMBRE FROM INSUMO WHERE INS_CODIGO = @codigo ", conexion);
            consulta.Parameters.AddWithValue("@codigo", codigoInsumo);
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
                        nombre = (String)lector["INS_NOMBRE"];
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
    */
    }
}
