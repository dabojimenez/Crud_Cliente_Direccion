using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controlador
{
    public class LogicaDirecciones
    {
        public bool INSERTAR_DIRECCIONES(ModeloDirecciones direcciones)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlCommand sqlCommand = new SqlCommand("INSERTAR_DIRECCIONES", ConexionMaestra.Conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@IdCliente", direcciones.ID_CLIENTE);
                sqlCommand.Parameters.AddWithValue("@CallePrincipal", direcciones.CALLE_PRINCIPAL_CLI);
                sqlCommand.Parameters.AddWithValue("@CalleSecundaria", direcciones.CALLE_SECUNDARIA_CLI);
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
        public void MOSTRAR_DIRECCIONES_CLIENTE(ref DataTable dt, int IdCliente)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("MOSTRAR_DIRECCIONES_CLIENTE", ConexionMaestra.Conexion);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente);
                sqlDataAdapter.Fill(dt);
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
        public bool ACTUALIZAR_DIRECCION(ModeloDirecciones direcciones)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlCommand sqlCommand = new SqlCommand("ACTUALIZAR_DIRECCION", ConexionMaestra.Conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@IdCliente", direcciones.ID_CLIENTE);
                sqlCommand.Parameters.AddWithValue("@IdDireccion", direcciones.ID_DIR);
                sqlCommand.Parameters.AddWithValue("@CallePrincipal", direcciones.CALLE_PRINCIPAL_CLI);
                sqlCommand.Parameters.AddWithValue("@CalleSecundaria", direcciones.CALLE_SECUNDARIA_CLI);
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

        public bool ELIMINAR_DIRECCION(ModeloDirecciones direcciones)
        {
            try
            {
                ConexionMaestra.AbrirBD();
                SqlCommand sqlCommand = new SqlCommand("ELIMINAR_DIRECCION", ConexionMaestra.Conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@IdCliente", direcciones.ID_CLIENTE);
                sqlCommand.Parameters.AddWithValue("@IdDireccion", direcciones.ID_DIR);
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
