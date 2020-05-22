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
    public class DAO_Operario
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        private String queryInsertar = "";

        public DAO_Operario() {
            this.queryInsertar = "INSERT INTO OPERARIO (OPE_CORREO," + " EST_HAB_ESTADO,"
                + " OPE_NOMBRE,"
                + " OPE_APELLIDOS, "
                + "OPE_CONTRASENA)"
                + "VALUES(@correo, @estado, @nombre, @apellidos, @contrasena) ";
        }

        public String getQueryInsertar() {
            return queryInsertar;
        }

        /// <summary>
        /// Método encargado de insertar Operarios en la tabla OPERARIO de la base de datos
        /// </summary>
        /// <param name="correo"> correo del operario</param>
        /// <param name="estado"> estado del operario, HABILITADO o DESHABILITADO</param>
        /// <param name="nombre"> nombre del operario</param>
        /// <param name="apellidos"> apellidos del operario</param>
        /// <param name="contrasena"> contrasena del operario</param>
        /// <returns></returns>
        public bool agregarOperario(string correo, DO_EstadoHabilitacion estado, string nombre,string apellidos, string contrasena) {

            SqlCommand comandoInsertar = new SqlCommand("BEGIN TRANSACTION " + queryInsertar+" COMMIT", conexion);
      
            comandoInsertar.Parameters.AddWithValue("@correo",correo);
            comandoInsertar.Parameters.AddWithValue("@estado", estado.estado);
            comandoInsertar.Parameters.AddWithValue("@nombre", nombre);
            comandoInsertar.Parameters.AddWithValue("@apellidos", apellidos);
            comandoInsertar.Parameters.AddWithValue("@contrasena", contrasena);

            try {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                comandoInsertar.ExecuteNonQuery();

                return true;
            } catch (Exception e) {

                return false;
            } finally {

                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
            
        } 
    }
}
