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
    /// Esta clase comprende el acceso y manejo a los datos correspondientes a los Operarios
    /// </summary>
    public class DAO_Supervisor
    {

        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        private String queryInsertar = "";

        public DAO_Supervisor() {
            this.queryInsertar = " INSERT INTO SUPERVISOR (OPE_CORREO) VALUES (@correo)";
        }

        public String getQueryInsertar()
        {
            return queryInsertar;
        }

        /// <summary>
        /// Método encargado de insertar Operarios en la tabla SUPERVISOR de la base de datos
        /// </summary>
        /// <param name="correo"> correo del supervisor</param>
        /// <param name="estado"> estado del supervisor, HABILITADO o DESHABILITADO</param>
        /// <param name="nombre"> nombre del supervisor</param>
        /// <param name="apellidos"> apellidos del supervisor</param>
        /// <param name="contrasena"> contrasena del supervisor</param>
        /// <param name="queryOperario"> query del operario para concatenarlo al comando</param>
        /// <returns>true si se agregó correctamente, false si ocurrió algún error</returns>
        public bool agregarSupervisor(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena, string queryOperario) {

            Console.WriteLine("BEGIN TRANSACTION " + queryOperario + queryInsertar + " COMMIT");
            SqlCommand comandoInsertar = new SqlCommand("BEGIN TRANSACTION "+queryOperario+queryInsertar+" COMMIT", conexion);
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
            catch (Exception)
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
