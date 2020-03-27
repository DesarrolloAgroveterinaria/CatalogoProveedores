using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ProveedorEntidades;

namespace ProveedorAccesoDeDatos
{
    public class ClaveBancoDal
    {
        public List<EClaveBanco> getAllBancos()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EClaveBanco> Lista = new List<EClaveBanco>();
                const string QueryGetByNombre = "EXEC AGROCatalogoProveedoresSP_GetAllBancos";
                using (SqlCommand cmd = new SqlCommand(QueryGetByNombre, conn))
                {                    
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EClaveBanco B = new EClaveBanco
                        {
                            Banco = reader["Banco"] == DBNull.Value ? "Nombre de banco indefinido" : Convert.ToString(reader["Banco"]),
                            Institucion = Convert.ToInt32(reader["Institucion"]),
                            Clave = reader["Clave"] == DBNull.Value ? "Clave de banco indefinida" : Convert.ToString(reader["Clave"])
                        };
                        Lista.Add(B);
                    }
                    return Lista;
                }
                return null;
            }
        }
    }
}
