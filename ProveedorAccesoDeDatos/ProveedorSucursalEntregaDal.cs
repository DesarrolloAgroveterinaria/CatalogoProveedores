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
    public class ProveedorSucursalEntregaDal
    {
        public List<EProveedorSucursalEntrega> GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorSucursalEntrega> SELista = new List<EProveedorSucursalEntrega>();
                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllSucursalEntregaEXByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorSucursalEntrega SE = new EProveedorSucursalEntrega
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            Condicionesid = reader["Condicionesid"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Condicionesid"]),
                            Sucursal = Convert.ToString(reader["Sucursal"]),
                            DisponibleParaEntrega = Convert.ToBoolean(reader["DisponibleParaEntrega"])
                        };
                        SELista.Add(SE);
                    }
                    return SELista;
                }
            }
            return null;
        }
    }
}
