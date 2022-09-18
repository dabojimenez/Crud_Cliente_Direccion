using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controlador
{
    public class LogicaCliente
    {
        public bool INSERTAR_CLIENTE(ModeloCliente cliente)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlCommand sqlCommand = new SqlCommand("INSERTAR_CLIENTE", ConexionMaestra.Conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Nombre", cliente.NOMBRE_CLIENTE);
                sqlCommand.Parameters.AddWithValue("@Cedula", cliente.CEDULA_CLIENTE);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.CerrarBD();
            }
        }
        public void OBTENER_CLIENTES_PAGINADO(ref DataTable dataTable, int desde, int hasta)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("OBTENER_CLIENTES_PAGINADO", ConexionMaestra.Conexion);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Desde", desde);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                ConexionMaestra.CerrarBD();
            }
        }
        public void TOTAL_REGISTROS(ref int contador)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(ID_CLIENTE) FROM CLIENTE", ConexionMaestra.Conexion);
                contador = (int)sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                contador = 0;
            }
            finally
            {
                ConexionMaestra.CerrarBD();
            }
        }
        public void BUSCAR_CLIENTE(ref DataTable dataTable, string buscar)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("BUSCAR_CLIENTE", ConexionMaestra.Conexion);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Buscar", buscar);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                ConexionMaestra.CerrarBD();
            }
        }
        public bool ACTUALIZAR_DATOS(ModeloCliente cliente)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlCommand sqlCommand = new SqlCommand("ACTUALIZAR_DATOS", ConexionMaestra.Conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ID", cliente.ID_CLIENTE);
                sqlCommand.Parameters.AddWithValue("@Nombre", cliente.NOMBRE_CLIENTE);
                sqlCommand.Parameters.AddWithValue("@Cedula", cliente.CEDULA_CLIENTE);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.CerrarBD();
            }
        }
        public bool ELIMINAR_CLIENTE(int IdCliente)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlCommand sqlCommand = new SqlCommand("ELIMINAR_CLIENTE", ConexionMaestra.Conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ID", IdCliente);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.CerrarBD();
            }
        }
    }
}
