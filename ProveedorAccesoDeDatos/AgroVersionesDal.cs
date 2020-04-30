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
    public class AgroVersionesDal
    {
        public EAgroVersiones getAppVersion(string app)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string Query = "EXEC AGROVersionesSP_GetVersion @App";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@App", app);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EAgroVersiones V = new EAgroVersiones
                        {
                            App = reader["App"] == DBNull.Value ? "" : Convert.ToString(reader["App"]),
                            Version = reader["Version"] == DBNull.Value ? "" : Convert.ToString(reader["Version"])
                        };
                        return V;
                    }
                }
                return null;
            }
        }
    }
}
