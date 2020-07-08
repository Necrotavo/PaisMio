using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DO;


namespace DAO
{
    /// <summary>
    /// Esta clase comprende el acceso a datos relacionado a los clientes
    /// </summary>
    public class DAO_Cliente
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ProductionConnection);

        /// <summary>
        /// Método para agregar un nuevo cliente a la base de datos 
        /// </summary>
        /// <param name="cliente">El cliente a registrar (DO_Cliente)</param>
        /// <returns>(True) si se resgistró el cliente en la base.(False) si ocurrió algún error y no se registró.</returns>
        public bool agregarCliente(DO_Cliente cliente)
        {
            SqlCommand comandoInsertar = new SqlCommand("INSERT INTO CLIENTE (CLI_CEDULA," +
           "EST_HAB_ESTADO" +
           ",CLI_NOMBRE" +
           ",CLI_TELEFONO" +
           ",CLI_CORREO" +
           ",CLI_DIRECCION) VALUES (@cedula, @estado,@nombre,@telefono,@correo, @direccion)", conexion);

            comandoInsertar.Parameters.AddWithValue("@cedula", cliente.cedula);
            comandoInsertar.Parameters.AddWithValue("@estado", cliente.estado);
            comandoInsertar.Parameters.AddWithValue("@nombre", cliente.nombre);
            comandoInsertar.Parameters.AddWithValue("@telefono", cliente.telefono);
            comandoInsertar.Parameters.AddWithValue("@correo", cliente.correo);
            comandoInsertar.Parameters.AddWithValue("@direccion", cliente.direccion);

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
        /// Método para modificar datos del cliente
        /// </summary>
        /// <param name="cliente">El cliente con sus datos a modificar</param>
        /// <returns>(True) si se resgistró el cambio en la base.(False) si ocurrió algún error y no se registró</returns>
        public bool modificarCliente(DO_Cliente cliente)
        {
            SqlCommand comandoModificar = new SqlCommand("UPDATE CLIENTE SET "+
                "EST_HAB_ESTADO = @estado " +
                ",CLI_NOMBRE = @nombre " +
                ",CLI_TELEFONO = @telefono " +
                ",CLI_CORREO = @correo " +
                ",CLI_DIRECCION = @direccion WHERE CLI_CEDULA = @cedula ", conexion);

            comandoModificar.Parameters.AddWithValue("@estado", cliente.estado);
            comandoModificar.Parameters.AddWithValue("@nombre", cliente.nombre);
            comandoModificar.Parameters.AddWithValue("@telefono", cliente.telefono);
            comandoModificar.Parameters.AddWithValue("@correo", cliente.correo);
            comandoModificar.Parameters.AddWithValue("@direccion", cliente.direccion);
            comandoModificar.Parameters.AddWithValue("@cedula", cliente.cedula);

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

                return false;
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
        /// Método para obtener la lista de clientes habilitados
        /// </summary>
        /// <returns>La lista de los clientes registrados y habilitados (List<DO_Cliente>)</returns>
        public List<DO_Cliente> listarClientesHabilitados()
        {
            List<DO_Cliente> listaClientes = new List<DO_Cliente>();
            SqlCommand comandoBuscar = new SqlCommand("SELECT * FROM CLIENTE WHERE EST_HAB_ESTADO = 'Habilitado'", conexion);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoBuscar.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        DO_Cliente cliente = new DO_Cliente();

                        cliente.cedula = (String)lector["CLI_CEDULA"];
                        cliente.estado = (String)lector["EST_HAB_ESTADO"];
                        cliente.nombre = (String)lector["CLI_NOMBRE"];
                        cliente.telefono = (String)lector["CLI_TELEFONO"];
                        cliente.correo = (String)lector["CLI_CORREO"];
                        cliente.direccion = (String)lector["CLI_DIRECCION"];
                        listaClientes.Add(cliente);
                    }
                }
                return listaClientes;
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
        /// Método para obtener todos los clientes
        /// </summary>
        /// <returns>La lista total de los clientes (List<DO_Cliente>)</returns>
        public List<DO_Cliente> listarTodosLosClientes()
        {
            SqlDataAdapter adaptador = new SqlDataAdapter();
            DataTable datatable = new DataTable();
            List<DO_Cliente> listaClientes = new List<DO_Cliente>();

            adaptador.SelectCommand = new SqlCommand("SELECT * FROM CLIENTE",conexion);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                adaptador.Fill(datatable);
                
                foreach (DataRow fila in datatable.Rows)
                {
                    DO_Cliente doCliente = new DO_Cliente();

                    doCliente.cedula = (String)fila["CLI_CEDULA"];                   
                    doCliente.estado = (String)fila["EST_HAB_ESTADO"];
                    doCliente.nombre = (String)fila["CLI_NOMBRE"];
                    doCliente.telefono = (String)fila["CLI_TELEFONO"];
                    doCliente.correo = (String)fila["CLI_CORREO"];
                    doCliente.direccion = (String)fila["CLI_DIRECCION"];

                    listaClientes.Add(doCliente);

                }
                return listaClientes;
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
        /// Método para buscar a un cliente en la base de datos según el nombre
        /// </summary>
        /// <param name="nombre">Nombre del cliente a buscar</param>
        /// <returns>El cliente encontrado (DO_Cliente). (Null) si no existe algún cliente con ese nombre</returns>
        public DO_Cliente buscarCliente(String cedula)
        {
            DO_Cliente cliente = new DO_Cliente();
            SqlCommand comandoBuscar = new SqlCommand("SELECT * FROM CLIENTE WHERE CLI_CEDULA = @cedula",conexion);
            comandoBuscar.Parameters.AddWithValue("@cedula", cedula);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoBuscar.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        
                        
                        cliente.cedula = (String)lector["CLI_CEDULA"];                       
                        cliente.estado = (String)lector["EST_HAB_ESTADO"];
                        cliente.nombre = (String)lector["CLI_NOMBRE"];
                        cliente.telefono = (String)lector["CLI_TELEFONO"];
                        cliente.correo = (String)lector["CLI_CORREO"];
                        cliente.direccion = (String)lector["CLI_DIRECCION"];

                       
                    }
                }

                return cliente;
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
        /// Método para modificar el estado de un cliente según su cédula jurídica
        /// </summary>
        /// <param name="estado">Nuevo estado a modificar</param>
        /// <param name="cedula">Cédula jurídica del cliente a modificar</param>
        /// <returns>(True) si el cambió se registró correctamente.(False)si no se pudo registrar el cambio.</returns>
        public bool modificarEstado(String estado, String cedula)
        {
            SqlCommand comandoModificar = new SqlCommand("UPDATE CLIENTE SET " +
                "EST_HAB_ESTADO = @estado " +
                "WHERE CLI_CEDULA = @cedula", conexion);

            comandoModificar.Parameters.AddWithValue("@estado", estado);
            comandoModificar.Parameters.AddWithValue("@cedula", cedula);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                comandoModificar.ExecuteNonQuery();
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

    }
}
