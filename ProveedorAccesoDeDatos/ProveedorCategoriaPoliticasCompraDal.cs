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
    public class ProveedorCategoriaPoliticasCompraDal
    {
        public List<EProveedorCategoriaPoliticasCompra> GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                List<EProveedorCategoriaPoliticasCompra> ListaCP = new List<EProveedorCategoriaPoliticasCompra>();
                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllCategoriaPoliticasCompraByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorCategoriaPoliticasCompra CP = new EProveedorCategoriaPoliticasCompra
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            Politicasid = Convert.ToInt32(reader["Politicasid"]),
                            Categoria = Convert.ToString(reader["Categoria"]),
                            ImporteMensual = reader["ImporteMensual"] == DBNull.Value ? "" : Convert.ToString(reader["ImporteMensual"]),
                            ImporteTrimestral = reader["ImporteTrimestral"] == DBNull.Value ? "" : Convert.ToString(reader["ImporteTrimestral"]),
                            Beneficios = Convert.ToString(reader["Beneficios"])
                        };
                        ListaCP.Add(CP);
                    }
                    return ListaCP;
                }
            }
            return null;
        }
    }
}
