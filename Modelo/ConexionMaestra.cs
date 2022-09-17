using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ConexionMaestra
    {
        public static SqlConnection Conexion = new SqlConnection("Data source=DESKTOP-BRSSJE8; Initial Catalog=PTSPLENDOR; Integrated Security=true");

        public static void AbrirBD()
        {
            if (Conexion.State == ConnectionState.Closed)
            {
                Conexion.Open();
            }
        }

        public static void CerrarBD()
        {
            if (Conexion.State == ConnectionState.Open)
            {
                Conexion.Close();
            }
        }
    }
}
