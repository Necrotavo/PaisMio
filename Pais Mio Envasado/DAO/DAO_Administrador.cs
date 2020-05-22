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
    /// Esta clase comprende el acceso y manejo de los datos correspondientes a los administradores
    /// </summary>
    public class DAO_Administrador
    {

        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        private string queryInsertar = "";

        public DAO_Administrador() {
            this.queryInsertar= "INSERT INTO ADMINISTRADOR (OPE_CORREO) VALUES (@correo)";
        }

        /// <summary>
        /// Método para insertar administradores en la tabla Administradores
        /// </summary>
        /// <param name="supervisor"> true si se insertó correctamente en la tabla OPERARIO y SUPERVISOR, false 
        /// si no se insertó correctamente</param>
        /// <param name="correo"></param>
        /// <returns></returns>
        public bool agregarAdministrador(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena, string querySupervisor) {


            SqlCommand comandoInsertar = new SqlCommand("BEGIN TRANSACTION "+querySupervisor+queryInsertar+ " COMMIT", conexion);

            comandoInsertar.Parameters.AddWithValue("@correo", correo);
            comandoInsertar.Parameters.AddWithValue("@estado", estado.estado);
            comandoInsertar.Parameters.AddWithValue("@nombre", nombre);
            comandoInsertar.Parameters.AddWithValue("@apellidos", apellidos);
            comandoInsertar.Parameters.AddWithValue("@contrasena", contrasena);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                
                comandoInsertar.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
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
