using Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Clases
{
    public class Consultas
    {
        public static DataTable AutorizarConClave(string Mov, string Almacen, string Clave)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                SqlCommand cmd = new SqlCommand();
                sb.Append("Exec AGROSPClaveAutorizacion @Mov, @Almacen, @Clave ");
                cmd.Parameters.Add("@Mov", SqlDbType.VarChar).Value = Mov;
                cmd.Parameters.Add("@Almacen", SqlDbType.VarChar).Value = Almacen;
                cmd.Parameters.Add("@Clave", SqlDbType.VarChar).Value = Clave;
                cmd.CommandText = sb.ToString();
                DataTable dt = Conexion.ExecutarADataTable(cmd);
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
