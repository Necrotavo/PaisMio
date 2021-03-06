﻿using System;
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

        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ProductionConnection);

        private string queryInsertar = "";

        public DAO_Administrador() {
            this.queryInsertar= " INSERT INTO ADMINISTRADOR (OPE_CORREO) VALUES (@correo)";
        }

        /// <summary>
        /// Método encargado de insertar Operarios en la tabla OPERARIO de la base de datos
        /// </summary>
        /// <param name="correo"> correo del administrador</param>
        /// <param name="estado"> estado del administrador, HABILITADO o DESHABILITADO</param>
        /// <param name="nombre"> nombre del administrador</param>
        /// <param name="apellidos"> apellidos del administrador</param>
        /// <param name="contrasena"> contrasena del administrador</param>
        /// <param name="querySupervisor">string que contiene el query de operario y supervisor</param>
        /// <returns>true si se agregó correctamente, false si ocurrió algún error</returns>
        public string agregarAdministrador(DO_Operario doOperario, string querySupervisor) {


            SqlCommand comandoInsertar = new SqlCommand("BEGIN TRANSACTION BEGIN TRY " + querySupervisor+queryInsertar+ " COMMIT END TRY BEGIN CATCH ROLLBACK END CATCH", conexion);

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
