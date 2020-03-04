using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient; //Se utiliza al agregar referencia desde Propiedades
                             //using Conexiones; //Utilizar esta clase en sustitución de los archivos previos
using ProveedorEntidades;

namespace ProveedorAccesoDeDatos
{
    public class ProveedorCostoFleteLimiteCapacidadDal
    {
        public List<EProveedorCostoFleteLimiteCapacidad> GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorCostoFleteLimiteCapacidad> SLista = new List<EProveedorCostoFleteLimiteCapacidad>();
                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllCostoFleteLimiteCapacidadByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorCostoFleteLimiteCapacidad S = new EProveedorCostoFleteLimiteCapacidad
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            id = Convert.ToInt32(reader["id"]),
                            Fleteid = Convert.ToInt32(reader["Fleteid"]),
                            Sucursal = Convert.ToString(reader["Sucursal"]),
                            Cantidad = reader["Cantidad"] == DBNull.Value ? "" : Convert.ToString(reader["Cantidad"]),                            
                            Observaciones = reader["Observaciones"] == DBNull.Value ? "N/A" : Convert.ToString(reader["Observaciones"]),
                            CostoFleteOLimiteCapacidad = Convert.ToString(reader["CostoFleteOLimiteCapacidad"])
                        };
                        SLista.Add(S);
                    }
                    return SLista;
                }
            }
            return null;
        }
    }
}
