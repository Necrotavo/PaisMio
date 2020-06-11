using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAO
{
    /// <summary>
    /// En esta clase se trabaja el acceso a base de datos para la creaci[on de reportes
    /// </summary>
    public class DAO_Reporte
    {
        private SqlConnection conexion = new SqlConnection(DAO.Properties.Settings.Default.ConnectionString);

        //public List<DO_InsumoEnBodega> obtenerListaInsumosConsumidos(String inicio, String final) {
        //    try
        //    {
        //        //Formato del string para la fecha 2020-05-30
        //        SqlDataAdapter adaptador = new SqlDataAdapter();
        //        DataTable datatable = new DataTable();
        //        List<DO_InsumoEnBodega> listaInsumosConsumidos = new List<DO_InsumoEnBodega>();

        //        adaptador.SelectCommand = new SqlCommand("Select INS_CODIGO, SUM(ACS_CANTIDAD) AS TOTAL_POR_INSUMO from SOL_A_CONSUMIR_INS " +
        //            "INNER JOIN (Select SOL_CODIGO from SOLICITUD_INSUMO " +
        //            "where SOL_FECHA BETWEEN CONVERT(datetime, @fechaInicio) AND CONVERT(datetime, @fechaFinal)) as temporal " +
        //            "ON SOL_A_CONSUMIR_INS.SOL_CODIGO = temporal.SOL_CODIGO GROUP BY INS_CODIGO; ", conexion);

        //        adaptador.SelectCommand.Parameters.AddWithValue("@fechaInicio", inicio);
        //        adaptador.SelectCommand.Parameters.AddWithValue("@fechaFinal", final);

        //        if (conexion.State != ConnectionState.Open)
        //        {
        //            conexion.Open();
        //        }

        //        adaptador.Fill(datatable);

        //        foreach (DataRow fila in datatable.Rows)
        //        {
        //            DO_InsumoEnBodega insumoConsumido = new DO_InsumoEnBodega();
        //            insumoConsumido.insumo = new DO_Insumo();

        //            insumoConsumido.insumo.codigo = Convert.ToInt32(fila["INS_CODIGO"]);
        //            insumoConsumido.cantidadDisponible = Convert.ToInt32(fila["TOTAL_POR_INSUMO"]);
                    
        //            listaInsumosConsumidos.Add(insumoConsumido);

        //        }
        //        return listaInsumosConsumidos;
        //    }
        //    catch (SqlException)
        //    {
        //        return null;
        //    }
        //    finally {
        //        if (conexion.State != ConnectionState.Closed)
        //        {
        //            conexion.Close();
        //        }
        //    }
        //}

        //public List<DO_InsumoEnBodega> obtenerListaInsumosDescartados(String inicio, String final)
        //{
        //    try
        //    {
        //        //Formato del string para la fecha 2020-05-30
        //        SqlDataAdapter adaptador = new SqlDataAdapter();
        //        DataTable datatable = new DataTable();
        //        List<DO_InsumoEnBodega> listaInsumosDescartados = new List<DO_InsumoEnBodega>();

        //        adaptador.SelectCommand = new SqlCommand("Select INS_CODIGO, SUM(PDS_CANTIDAD) AS TOTAL_POR_INSUMO from POR_DESCARTE " +
        //            "INNER JOIN (Select SOL_CODIGO from SOLICITUD_INSUMO " +
        //            "where SOL_FECHA BETWEEN CONVERT(datetime, @fechaInicio) AND CONVERT(datetime, @fechaFinal)) as temporal " +
        //            "ON POR_DESCARTE.SOL_CODIGO = temporal.SOL_CODIGO GROUP BY INS_CODIGO; ", conexion);

        //        adaptador.SelectCommand.Parameters.AddWithValue("@fechaInicio", inicio);
        //        adaptador.SelectCommand.Parameters.AddWithValue("@fechaFinal", final);

        //        if (conexion.State != ConnectionState.Open)
        //        {
        //            conexion.Open();
        //        }

        //        adaptador.Fill(datatable);

        //        foreach (DataRow fila in datatable.Rows)
        //        {
        //            DO_InsumoEnBodega insumoConsumido = new DO_InsumoEnBodega();
        //            insumoConsumido.insumo = new DO_Insumo();

        //            insumoConsumido.insumo.codigo = Convert.ToInt32(fila["INS_CODIGO"]);
        //            insumoConsumido.cantidadDisponible = Convert.ToInt32(fila["TOTAL_POR_INSUMO"]);

        //            listaInsumosDescartados.Add(insumoConsumido);

        //        }
        //        return listaInsumosDescartados;
        //    }
        //    catch (SqlException)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        if (conexion.State != ConnectionState.Closed)
        //        {
        //            conexion.Close();
        //        }
        //    }
        //}

        public List<DO_ReporteInsumos> reporteInsumos(String inicio, String final) {
            try
            {
                //Formato del string para la fecha 2020-05-30
                SqlDataAdapter adaptador = new SqlDataAdapter();
                DataTable datatable = new DataTable();
                List<DO_ReporteInsumos> listaReportados = new List<DO_ReporteInsumos>();

                adaptador.SelectCommand = new SqlCommand("SELECT * FROM (Select INS_CODIGO AS INS_CODIGO_CONSUMIR, SUM(ACS_CANTIDAD) AS TOTAL_CONSUMIDO from SOL_A_CONSUMIR_INS " +
                    "INNER JOIN (Select SOL_CODIGO from SOLICITUD_INSUMO where SOL_FECHA BETWEEN CONVERT(datetime, @fechaInicio) AND CONVERT(datetime, @fechaFinal)) as temporal " +
                    "ON SOL_A_CONSUMIR_INS.SOL_CODIGO = temporal.SOL_CODIGO GROUP BY INS_CODIGO) AS CONSUMO " +
                    "FULL JOIN " +
                    "(Select INS_CODIGO AS INS_CODIGO_DESCARTE, SUM(PDS_CANTIDAD) AS TOTAL_DESCARTADO from POR_DESCARTE " +
                    "INNER JOIN (Select SOL_CODIGO from SOLICITUD_INSUMO where SOL_FECHA BETWEEN CONVERT(datetime, @fechaInicio) AND CONVERT(datetime, @fechaFinal)) as temporal " +
                    "ON POR_DESCARTE.SOL_CODIGO = temporal.SOL_CODIGO GROUP BY INS_CODIGO) AS DESCARTE ON CONSUMO.INS_CODIGO_CONSUMIR = DESCARTE.INS_CODIGO_DESCARTE", conexion);

                adaptador.SelectCommand.Parameters.AddWithValue("@fechaInicio", inicio);
                adaptador.SelectCommand.Parameters.AddWithValue("@fechaFinal", final);

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                adaptador.Fill(datatable);

                foreach (DataRow fila in datatable.Rows)
                {
                    DO_ReporteInsumos insumoConsumido = new DO_ReporteInsumos();
                    insumoConsumido.insumo = new DO_Insumo();
                    Object codigoConsumir = fila["INS_CODIGO_CONSUMIR"];
                    if (!(codigoConsumir is DBNull))
                    {
                        insumoConsumido.insumo.codigo = Convert.ToInt32(codigoConsumir);
                    }
                    else {
                        insumoConsumido.insumo.codigo = Convert.ToInt32(fila["INS_CODIGO_DESCARTE"]);
                    }
                    Object cantConsumida = fila["TOTAL_CONSUMIDO"];
                    if (!(cantConsumida is DBNull))
                    {
                        insumoConsumido.cantidadConsumida = Convert.ToInt32(cantConsumida);
                    }
                    Object cantDescartada = fila["TOTAL_DESCARTADO"];
                    if (!(cantDescartada is DBNull))
                    {
                        insumoConsumido.cantidadDescartada = Convert.ToInt32(cantDescartada);
                    }

                    DAO_Insumo daoInsumo = new DAO_Insumo();
                    insumoConsumido.insumo.nombre = daoInsumo.obtenerNombreInsumo(insumoConsumido.insumo.codigo);
                    insumoConsumido.total = insumoConsumido.cantidadConsumida + insumoConsumido.cantidadDescartada;

                    listaReportados.Add(insumoConsumido);
                }
                return listaReportados;
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
        /// Método para obtener los pedidos despachados en un mes determinado.
        /// </summary>
        /// <param name="mes">Mes de los pedidos a buscar (int)</param>
        /// <param name="anho">Año del pedido a buscar(int)</param>
        /// <returns>Lista de pedidos en el mes especificado</returns>
        public List<DO_ReportePedido> reportePedidos(Int32 mes, Int32 anho)
        {

            List<DO_ReportePedido> listaReportes = new List<DO_ReportePedido>();
            SqlCommand comandoBuscar = new SqlCommand("SELECT PEDIDO.PED_CODIGO, CLIENTE.CLI_NOMBRE, PEDIDO.ESTADO, PEDIDO.OPE_CORREO,PEDIDO.ADM_OPE_CORREO," +
                                                    "PEDIDO.PED_FECHA_INGRESO,PEDIDO.PED_FECHA_DESPACHO FROM PEDIDO, CLIENTE WHERE PEDIDO.CLI_CEDULA = CLIENTE.CLI_CEDULA AND (MONTH(PEDIDO.PED_FECHA_DESPACHO) = @mes AND YEAR(PEDIDO.PED_FECHA_DESPACHO) = @anho)",conexion);

            comandoBuscar.Parameters.AddWithValue("@mes", mes);
            comandoBuscar.Parameters.AddWithValue("@anho", anho);

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
                        DO_ReportePedido reportePedido = new DO_ReportePedido();

                        reportePedido.codigo = Convert.ToInt32(lector["PED_CODIGO"]);
                        reportePedido.nombreCliente = (string)lector["CLI_NOMBRE"];
                        reportePedido.nombreAdminIngreso = (string)lector["OPE_CORREO"];
                        reportePedido.nombreAdminDespacho = (string)lector["ADM_OPE_CORREO"];
                        reportePedido.fechaIngreso = (DateTime)lector["PED_FECHA_INGRESO"];
                        reportePedido.fechaDespacho = (DateTime)lector["PED_FECHA_DESPACHO"];
                        

                        listaReportes.Add(reportePedido);
                    }
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

            obtenerProductos(listaReportes); //Se envía la lista de pedidos al método encargado de asignar los respectivos productos con los pedidos.

            return listaReportes;
        }

        /// <summary>
        /// Método para obtener agregar los productos en la lista de pedidos de reporte
        /// </summary>
        /// <param name="listaReportes"></param>
        public void obtenerProductos(List<DO_ReportePedido> listaReportes)
        {
                      
            foreach (DO_ReportePedido pedido in listaReportes)
            {
                SqlCommand comandoBuscarProductos = new SqlCommand("SELECT PRODUCTO.PRO_NOMBRE, PRODUCTO.PRO_CODIGO,PRODUCTO.PRO_DESCRIPCION,PRODUCTO.EST_HAB_ESTADO, PED_POSEE_PRO.PPP_CANTIDAD " +
                "FROM PED_POSEE_PRO,PRODUCTO WHERE PED_POSEE_PRO.PRO_CODIGO = PRODUCTO.PRO_CODIGO AND (PED_POSEE_PRO.PED_CODIGO = @codigoPedido)", conexion);
                comandoBuscarProductos.Parameters.AddWithValue("@codigoPedido", pedido.codigo);
                List<DO_ProductoEnPedido> listaPedidos = new List<DO_ProductoEnPedido>();

                try
                {
                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }

                    SqlDataReader lector = comandoBuscarProductos.ExecuteReader();
                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            DO_ProductoEnPedido productoEnPedido = new DO_ProductoEnPedido();
                            DO_Producto producto = new DO_Producto();
                            

                            producto.codigo = Convert.ToInt32(lector["PRO_CODIGO"]);
                            producto.nombre = (String)lector["PRO_NOMBRE"];
                            producto.descripcion = (String)lector["PRO_DESCRIPCION"];
                            producto.estado = (String)lector["EST_HAB_ESTADO"];

                            productoEnPedido.producto = producto;
                            productoEnPedido.cantidad = Convert.ToInt32(lector["PPP_CANTIDAD"]);

                            listaPedidos.Add(productoEnPedido);
                        }
                    }
                }
                catch (SqlException)
                {
                  
                }
                finally
                {
                    if (conexion.State != ConnectionState.Closed)
                    {
                        conexion.Close();
                    }
                }

                pedido.productos = listaPedidos;
            }


        }
    }
}
