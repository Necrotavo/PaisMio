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
    public class DAO_Cliente
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        /// <summary>
        /// Método para agregar un nuevo cliente a la base de datos 
        /// </summary>
        /// <param name="cliente">El cliente a registrar (DO_Cliente)</param>
        /// <returns>(True) si se resgist´ró el cambio en la base. (False si ocurrió algún error y no se registró).</returns>
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


    }
}
