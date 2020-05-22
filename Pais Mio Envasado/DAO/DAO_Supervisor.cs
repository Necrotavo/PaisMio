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
    public class DAO_Supervisor
    {

        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        private String queryInsertar = "";

        public DAO_Supervisor() {
            this.queryInsertar = "INSERT INTO SUPERVISOR (OPE_CORREO) VALUES (@correo)";
        }

        public String getQueryInsertar()
        {
            return queryInsertar;
        }

        /// <summary>
        /// Método para insertar Supervisores a la tabla SUPERVISORES de la base de datos
        /// </summary>
        /// <param name="operario"> true se añnadió correctamente a la tabla OPERARIO,false si no se añadió correctamente</param>
        /// <param name="correo"> Correo del supervisor</param>
        /// <returns> true si el supervisor se añadió correctamente, falso si no se añadió</returns>
        public bool agregarSupervisor(string correo, DO_EstadoHabilitacion estado, string nombre, string apellidos, string contrasena, string queryOperario) {

         
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

                comandoInsertar.Transaction.Commit();
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
