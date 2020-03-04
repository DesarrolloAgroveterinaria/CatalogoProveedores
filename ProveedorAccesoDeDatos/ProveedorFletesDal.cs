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
    public class ProveedorFletesDal
    {
        public List<EProveedorFletes> GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorFletes> FLista = new List<EProveedorFletes>();
                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllFletesEXByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorFletes F = new EProveedorFletes
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            Fleteid = Convert.ToInt32(reader["Fleteid"]),
                            FormaEntrega = Convert.ToString(reader["FormaEntrega"]),                   
                            NombreDescripcion = Convert.ToString(reader["NombreDescripcion"]),
                            NombreContacto = reader["NombreContacto"] == DBNull.Value ? "" : Convert.ToString(reader["NombreContacto"]),
                            Puesto = reader["Puesto"] == DBNull.Value ? "" : Convert.ToString(reader["Puesto"]),
                            Telefono1 = Convert.ToString(reader["Telefono1"]),
                            ExtTelefono1 = reader["ExtTelefono1"] == DBNull.Value ? "" : Convert.ToString(reader["ExtTelefono1"]),
                            Telefono2 = reader["Telefono2"] == DBNull.Value ? "" : Convert.ToString(reader["Telefono2"]),
                            ExtTelefono2 = reader["ExtTelefono2"] == DBNull.Value ? "" : Convert.ToString(reader["ExtTelefono2"]),
                            Celular1 = reader["Celular1"] == DBNull.Value ? "" : Convert.ToString(reader["Celular1"]),
                            Celular2 = reader["Celular2"] == DBNull.Value ? "" : Convert.ToString(reader["Celular2"]),
                            Email1 = reader["Email1"] == DBNull.Value ? "" : Convert.ToString(reader["Email1"]),
                            Email2 = reader["Email2"] == DBNull.Value ? "" : Convert.ToString(reader["Email2"]),
                            PedidoMin = reader["PedidoMin"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PedidoMin"]),
                            PedidoMax = reader["PedidoMax"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PedidoMax"]),
                            Unidad = reader["Unidad"] == DBNull.Value ? "" : Convert.ToString(reader["Unidad"]),
                            Origen = reader["Origen"] == DBNull.Value ? "" : Convert.ToString(reader["Origen"]),
                            Destino = reader["Destino"] == DBNull.Value ? "" : Convert.ToString(reader["Destino"]),
                            Observaciones = reader["Observaciones"] == DBNull.Value ? "" : Convert.ToString(reader["Observaciones"]),
                            CostoFleteid = Convert.ToInt32(reader["CostoFleteid"])
                        };
                        FLista.Add(F);
                    }
                    return FLista;
                }
            }
            return null;
        }
    }
}

