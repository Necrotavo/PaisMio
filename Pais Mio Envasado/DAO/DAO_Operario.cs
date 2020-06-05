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
                + "VALUES(@correo, @estado, @nombre, @apellidos, @contrasena)";
        }

        public String getQueryInsertar() {
            return this.queryInsertar;
        }

        /// <summary>
        /// Método encargado de insertar Operarios en la tabla OPERARIO de la base de datos
        /// </summary>
        /// <param name="correo"> correo del operario</param>
        /// <param name="estado"> estado del operario, HABILITADO o DESHABILITADO</param>
        /// <param name="nombre"> nombre del operario</param>
        /// <param name="apellidos"> apellidos del operario</param>
        /// <param name="contrasena"> contrasena del operario</param>
        /// <returns>true si se agregó correctamente, false si ocurrió algún error</returns>
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

        public DO_Operario buscarOperario(String correo) {
            DO_Operario operario = new DO_Operario();

            try
            {
                SqlCommand comandoSelect = new SqlCommand("Select * from OPERARIO where OPE_CORREO = @correo", conexion);
                comandoSelect.Parameters.AddWithValue("@correo",correo);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                SqlDataReader lector = comandoSelect.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {


                        operario.correo = (String)lector["OPE_CORREO"];
                        operario.estado = new DO_EstadoHabilitacion((String)lector["EST_HAB_ESTADO"]);
                        operario.nombre = (String)lector["OPE_NOMBRE"];
                        operario.apellidos = (String)lector["OPE_APELLIDOS"];
                        operario.contrasena = (String)lector["OPE_CONTRASENA"];

                    }

                    return operario;
                }
            }
            catch
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

            return operario;
        } 

        public List<DO_Operario> obtenerListaOperarios()
        {
            List<DO_Operario> lista = new List<DO_Operario>();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = new SqlCommand("Select * from OPERARIO",conexion);
            DataTable datatable = new DataTable();
            adaptador.Fill(datatable);

            foreach (DataRow row in datatable.Rows)
            {
                DO_Operario operario = new DO_Operario();
                operario.correo = (String)row["OPE_CORREO"];
                operario.estado = new DO_EstadoHabilitacion((String)row["EST_HAB_ESTADO"]);
                operario.nombre = (String)row["OPE_NOMBRE"];
                operario.apellidos = (String)row["OPE_APELLIDOS"];
                operario.contrasena = (String)row["OPE_CONTRASENA"];
                lista.Add(operario);
            }

            return lista;
        }

        /// <summary>
        /// Método para cambiar el estado de un determinado usuario.
        /// </summary>
        /// <param name="estado">Nuevo estado del usuario(String)</param>
        /// <param name="correo">Correo del uusario a modificar(String)</param>
        /// <returns>(True) si se modificó correctamente. (False) si no se modificó.</returns>
        public bool modificarEstado(String estado, String correo)
        {
            SqlCommand comandoActualizar = new SqlCommand("UPDATE OPERARIO SET EST_HAB_ESTADO = @nuevoEstado WHERE OPE_CORREO = @correo", conexion);
            comandoActualizar.Parameters.AddWithValue("@nuevoEstado", estado );
            comandoActualizar.Parameters.AddWithValue("@correo", correo);

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (comandoActualizar.ExecuteNonQuery() > 0)
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

            return false;

        }
    }
}
