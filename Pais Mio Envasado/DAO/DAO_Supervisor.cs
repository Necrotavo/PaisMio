using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DO;
using System.Runtime.InteropServices;

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
        public string agregarSupervisor(DO_Operario doOperario, string queryOperario) {

            Console.WriteLine("BEGIN TRANSACTION BEGIN TRY " + queryOperario + queryInsertar + " COMMIT END TRY BEGIN CATCH ROLLBACK END CATCH");
            SqlCommand comandoInsertar = new SqlCommand("BEGIN TRANSACTION BEGIN TRY " + queryOperario+queryInsertar+ " COMMIT END TRY BEGIN CATCH ROLLBACK END CATCH", conexion);
            comandoInsertar.Parameters.AddWithValue("@correo", doOperario.correo);
            comandoInsertar.Parameters.AddWithValue("@estado", "HABILITADO");
            comandoInsertar.Parameters.AddWithValue("@nombre", doOperario.nombre);
            comandoInsertar.Parameters.AddWithValue("@apellidos", doOperario.apellidos);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (comandoInsertar.ExecuteNonQuery() > 0)
                {
                    DAO_Operario DAOoperario = new DAO_Operario();

                    return DAOoperario.nuevaContrasena(doOperario.correo);
                }
                else {
                    return null;
                }

                
            }
            catch (Exception)
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
