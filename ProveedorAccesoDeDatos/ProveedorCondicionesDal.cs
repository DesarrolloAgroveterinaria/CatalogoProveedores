using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient; //Se utiliza al agregar referencia desde Propiedades
                             //using Conexiones; //Utilizar esta clase en sustitución de los archivos previos
using ProveedorEntidades;

namespace ProveedorAccesoDeDatos
{
    public class ProveedorCondicionesDal
    {
        public EProveedorCondiciones GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllCondicionesByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EProveedorCondiciones C = new EProveedorCondiciones
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            Condicionesid = Convert.ToInt32(reader["Condicionesid"]),
                            CondicionesCreditoDias = Convert.ToString(reader["CondicionesCreditoDias"]),
                            FletePorCobrar = reader["FletePorCobrar"] == DBNull.Value ? false : Convert.ToBoolean(reader["FletePorCobrar"]),
                            ProntoPago1Dias = Convert.ToString(reader["ProntoPago1Dias"]),
                            ProntoPago1Descuento = Convert.ToInt32(reader["ProntoPago1Descuento"]),
                            VencimientoPagoFactura1 = reader["VencimientoPagoFactura1"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura1"]),
                            ProntoPago2Dias = reader["ProntoPago2Dias"] == DBNull.Value ? "" : Convert.ToString(reader["ProntoPago2Dias"]),                            
                            ProntoPago2Descuento = reader["ProntoPago2Descuento"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProntoPago2Descuento"]),
                            VencimientoPagoFactura2 = reader["VencimientoPagoFactura2"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura2"]),
                            ProntoPago3Dias = reader["ProntoPago3Dias"] == DBNull.Value ? "" : Convert.ToString(reader["ProntoPago3Dias"]),
                            ProntoPago3Descuento = reader["ProntoPago3Descuento"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProntoPago3Descuento"]),
                            VencimientoPagoFactura3 = reader["VencimientoPagoFactura3"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura3"]),
                            ProntoPago4Dias = reader["ProntoPago4Dias"] == DBNull.Value ? "" : Convert.ToString(reader["ProntoPago4Dias"]),
                            ProntoPago4Descuento = reader["ProntoPago4Descuento"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProntoPago4Descuento"]),
                            VencimientoPagoFactura4 = reader["VencimientoPagoFactura4"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura4"]),
                            ProntoPago5Dias = reader["ProntoPago5Dias"] == DBNull.Value ? "" : Convert.ToString(reader["ProntoPago5Dias"]),
                            ProntoPago5Descuento = reader["ProntoPago5Descuento"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProntoPago5Descuento"]),
                            VencimientoPagoFactura5 = reader["VencimientoPagoFactura5"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura5"]),
                            TiempoEntrega = Convert.ToString(reader["TiempoEntrega"]),
                            ObservacionesTiempoEntrega = reader["ObservacionesTiempoEntrega"] == DBNull.Value ? "" : Convert.ToString(reader["ObservacionesTiempoEntrega"]),          
                            FormaEntrega = reader["FormaEntrega"] == DBNull.Value ? "" : Convert.ToString(reader["FormaEntrega"]),
                            SucursalEntrega = Convert.ToString(reader["SucursalEntrega"]),
                            CondicionesEspecialesEntrega = reader["CondicionesEspecialesEntrega"] == DBNull.Value ? "" : Convert.ToString(reader["CondicionesEspecialesEntrega"])
                        };
                        return C;
                    }
                    return null;
                }
            }
            
        }

        public void editarCondiciones(EProveedorCondiciones condiciones)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarCondicionesByClaveProveedor @ClaveProveedor, 
                                   @CondicionesCreditoDias,
                                   @ProntoPago1Dias,
                                   @ProntoPago1Descuento,
                                   @VencimientoPagoFactura1,
                                   @ProntoPago2Dias,
                                   @ProntoPago2Descuento,
                                   @VencimientoPagoFactura2,
                                   @ProntoPago3Dias,
                                   @ProntoPago3Descuento,
                                   @VencimientoPagoFactura3,
                                   @ProntoPago4Dias,
                                   @ProntoPago4Descuento,
                                   @VencimientoPagoFactura4,
                                   @ProntoPago5Dias,
                                   @ProntoPago5Descuento,
                                   @VencimientoPagoFactura5,
                                   @TiempoEntrega,
                                   @FormaEntrega,
                                   @SucursalEntrega,
                                   @ObservacionesTiempoEntrega,
                                   @CondicionesEspecialesEntrega
	                               ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", condiciones.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@CondicionesCreditoDias", condiciones.CondicionesCreditoDias);
                    cmd.Parameters.AddWithValue("@ProntoPago1Dias", condiciones.ProntoPago1Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago1Descuento", condiciones.ProntoPago1Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura1", condiciones.VencimientoPagoFactura1);
                    cmd.Parameters.AddWithValue("@ProntoPago2Dias", condiciones.ProntoPago2Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago2Descuento", condiciones.ProntoPago2Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura2", condiciones.VencimientoPagoFactura2);
                    cmd.Parameters.AddWithValue("@ProntoPago3Dias", condiciones.ProntoPago3Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago3Descuento", condiciones.ProntoPago3Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura3", condiciones.VencimientoPagoFactura3);
                    cmd.Parameters.AddWithValue("@ProntoPago4Dias", condiciones.ProntoPago4Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago4Descuento", condiciones.ProntoPago4Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura4", condiciones.VencimientoPagoFactura4);
                    cmd.Parameters.AddWithValue("@ProntoPago5Dias", condiciones.ProntoPago5Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago5Descuento", condiciones.ProntoPago5Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura5", condiciones.VencimientoPagoFactura5);
                    cmd.Parameters.AddWithValue("@TiempoEntrega", condiciones.TiempoEntrega);
                    cmd.Parameters.AddWithValue("@FormaEntrega", condiciones.FormaEntrega);
                    cmd.Parameters.AddWithValue("@SucursalEntrega", condiciones.SucursalEntrega);
                    cmd.Parameters.AddWithValue("@ObservacionesTiempoEntrega", condiciones.ObservacionesTiempoEntrega);
                    cmd.Parameters.AddWithValue("@CondicionesEspecialesEntrega", condiciones.CondicionesEspecialesEntrega);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void agregarCondiciones(EProveedorCondiciones condiciones)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarCondicionesByClaveProveedor @ClaveProveedor, 
                                   @CondicionesCreditoDias,
                                   @ProntoPago1Dias,
                                   @ProntoPago1Descuento,
                                   @VencimientoPagoFactura1,
                                   @ProntoPago2Dias,
                                   @ProntoPago2Descuento,
                                   @VencimientoPagoFactura2,
                                   @ProntoPago3Dias,
                                   @ProntoPago3Descuento,
                                   @VencimientoPagoFactura3,
                                   @ProntoPago4Dias,
                                   @ProntoPago4Descuento,
                                   @VencimientoPagoFactura4,
                                   @ProntoPago5Dias,
                                   @ProntoPago5Descuento,
                                   @VencimientoPagoFactura5,
                                   @TiempoEntrega,
                                   @FormaEntrega,
                                   @SucursalEntrega,
                                   @ObservacionesTiempoEntrega,
                                   @CondicionesEspecialesEntrega
	                               ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", condiciones.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@CondicionesCreditoDias", condiciones.CondicionesCreditoDias);
                    cmd.Parameters.AddWithValue("@ProntoPago1Dias", condiciones.ProntoPago1Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago1Descuento", condiciones.ProntoPago1Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura1", condiciones.VencimientoPagoFactura1);
                    cmd.Parameters.AddWithValue("@ProntoPago2Dias", condiciones.ProntoPago2Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago2Descuento", condiciones.ProntoPago2Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura2", condiciones.VencimientoPagoFactura2);
                    cmd.Parameters.AddWithValue("@ProntoPago3Dias", condiciones.ProntoPago3Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago3Descuento", condiciones.ProntoPago3Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura3", condiciones.VencimientoPagoFactura3);
                    cmd.Parameters.AddWithValue("@ProntoPago4Dias", condiciones.ProntoPago4Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago4Descuento", condiciones.ProntoPago4Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura4", condiciones.VencimientoPagoFactura4);
                    cmd.Parameters.AddWithValue("@ProntoPago5Dias", condiciones.ProntoPago5Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago5Descuento", condiciones.ProntoPago5Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura5", condiciones.VencimientoPagoFactura5);
                    cmd.Parameters.AddWithValue("@TiempoEntrega", condiciones.TiempoEntrega);
                    cmd.Parameters.AddWithValue("@FormaEntrega", condiciones.FormaEntrega);
                    cmd.Parameters.AddWithValue("@SucursalEntrega", condiciones.SucursalEntrega);
                    cmd.Parameters.AddWithValue("@ObservacionesTiempoEntrega", condiciones.ObservacionesTiempoEntrega);
                    cmd.Parameters.AddWithValue("@CondicionesEspecialesEntrega", condiciones.CondicionesEspecialesEntrega);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
