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
    public class ProveedorPlazoCreditoDal
    {
        public List<EProveedorPlazoCredito> GetByClave(string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorPlazoCredito> Lista = new List<EProveedorPlazoCredito>();
                const string Query = "EXEC AGROCatalogoProveedoresSP_GetAllPlazosByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorPlazoCredito P = new EProveedorPlazoCredito
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            Plazoid = Convert.ToInt32(reader["Plazoid"]),
                            PrioridadDeUso = Convert.ToInt32(reader["PrioridadUso"]),
                            EstatusActivo = Convert.ToBoolean(reader["EstatusActivo"]),
                            Revisado = Convert.ToBoolean(reader["Revisado"]),
                            PlazoCreditoDias = Convert.ToString(reader["PlazoCreditoDias"]),
                            DefinicionPlazo = Convert.ToString(reader["DefinicionPlazo"]),
                            ProntoPago1Dias = reader["ProntoPago1Dias"] == DBNull.Value ? "" : Convert.ToString(reader["ProntoPago1Dias"]),
                            ProntoPago1Descuento = reader["ProntoPago1Descuento"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProntoPago1Descuento"]),
                            VencimientoPagoFactura1 = reader["VencimientoPagoFactura1"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura1"]),
                            ObservacionesPP1 = reader["ObservacionesPP1"] == DBNull.Value ? "" : Convert.ToString(reader["ObservacionesPP1"]),
                            ProntoPago2Dias = reader["ProntoPago2Dias"] == DBNull.Value ? "" : Convert.ToString(reader["ProntoPago2Dias"]),
                            ProntoPago2Descuento = reader["ProntoPago2Descuento"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProntoPago2Descuento"]),
                            VencimientoPagoFactura2 = reader["VencimientoPagoFactura2"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura2"]),
                            ObservacionesPP2 = reader["ObservacionesPP2"] == DBNull.Value ? "" : Convert.ToString(reader["ObservacionesPP2"]),
                            ProntoPago3Dias = reader["ProntoPago3Dias"] == DBNull.Value ? "" : Convert.ToString(reader["ProntoPago3Dias"]),
                            ProntoPago3Descuento = reader["ProntoPago3Descuento"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProntoPago3Descuento"]),
                            VencimientoPagoFactura3 = reader["VencimientoPagoFactura3"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura3"]),
                            ObservacionesPP3 = reader["ObservacionesPP3"] == DBNull.Value ? "" : Convert.ToString(reader["ObservacionesPP3"]),
                            ProntoPago4Dias = reader["ProntoPago4Dias"] == DBNull.Value ? "" : Convert.ToString(reader["ProntoPago4Dias"]),
                            ProntoPago4Descuento = reader["ProntoPago4Descuento"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProntoPago4Descuento"]),
                            VencimientoPagoFactura4 = reader["VencimientoPagoFactura4"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura4"]),
                            ObservacionesPP4 = reader["ObservacionesPP4"] == DBNull.Value ? "" : Convert.ToString(reader["ObservacionesPP4"]),
                            ProntoPago5Dias = reader["ProntoPago5Dias"] == DBNull.Value ? "" : Convert.ToString(reader["ProntoPago5Dias"]),
                            ProntoPago5Descuento = reader["ProntoPago5Descuento"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProntoPago5Descuento"]),
                            VencimientoPagoFactura5 = reader["VencimientoPagoFactura5"] == DBNull.Value ? "" : Convert.ToString(reader["VencimientoPagoFactura5"]),
                            ObservacionesPP5 = reader["ObservacionesPP5"] == DBNull.Value ? "" : Convert.ToString(reader["ObservacionesPP5"]),
                            ObservacionesGenerales = reader["ObservacionesGenerales"] == DBNull.Value ? "" : Convert.ToString(reader["ObservacionesGenerales"])
                        };
                        Lista.Add(P);
                    }
                    return Lista;
                }
            }
            return null;
        }

        public void DesactivarByIdByClave(int plazoid, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorActivacion = 0;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_DesactivarPlazoCreditoByIdByClaveProveedor @Plazoid, @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@Plazoid", plazoid);
                    cmd.Parameters.Add("@EstatusActivo", SqlDbType.Bit).Value = valorActivacion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarByIdByClave(EProveedorPlazoCredito plazo)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarPlazoByIdByClaveProveedor @ClaveProveedor,
                                    @Plazoid,
                                    @Revisado,
	                                @PlazoCreditoDias,
	                                @DefinicionPlazo,
	                                @ProntoPago1Dias,
	                                @ProntoPago1Descuento,
	                                @VencimientoPagoFactura1,
	                                @ObservacionesPP1,
	                                @ProntoPago2Dias,
	                                @ProntoPago2Descuento,
	                                @VencimientoPagoFactura2,
	                                @ObservacionesPP2,
	                                @ProntoPago3Dias,
	                                @ProntoPago3Descuento,
                                    @VencimientoPagoFactura3,
                                    @ObservacionesPP3,
                                    @ProntoPago4Dias,
	                                @ProntoPago4Descuento,
	                                @VencimientoPagoFactura4,
	                                @ObservacionesPP4,
	                                @ProntoPago5Dias,
	                                @ProntoPago5Descuento,
                                    @VencimientoPagoFactura5,
                                    @ObservacionesPP5,
                                    @ObservacionesGenerales";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", plazo.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@Plazoid", plazo.Plazoid);                    
                    cmd.Parameters.AddWithValue("@Revisado", plazo.Revisado);
                    cmd.Parameters.AddWithValue("@PlazoCreditoDias", plazo.PlazoCreditoDias);
                    cmd.Parameters.AddWithValue("@DefinicionPlazo", plazo.DefinicionPlazo);
                    cmd.Parameters.AddWithValue("@ProntoPago1Dias", plazo.ProntoPago1Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago1Descuento", plazo.ProntoPago1Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura1", plazo.VencimientoPagoFactura1);
                    cmd.Parameters.AddWithValue("@ObservacionesPP1", plazo.ObservacionesPP1);
                    cmd.Parameters.AddWithValue("@ProntoPago2Dias", plazo.ProntoPago2Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago2Descuento", plazo.ProntoPago2Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura2", plazo.VencimientoPagoFactura2);
                    cmd.Parameters.AddWithValue("@ObservacionesPP2", plazo.ObservacionesPP2);
                    cmd.Parameters.AddWithValue("@ProntoPago3Dias", plazo.ProntoPago3Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago3Descuento", plazo.ProntoPago3Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura3", plazo.VencimientoPagoFactura3);
                    cmd.Parameters.AddWithValue("@ObservacionesPP3", plazo.ObservacionesPP3);
                    cmd.Parameters.AddWithValue("@ProntoPago4Dias", plazo.ProntoPago4Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago4Descuento", plazo.ProntoPago4Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura4", plazo.VencimientoPagoFactura4);
                    cmd.Parameters.AddWithValue("@ObservacionesPP4", plazo.ObservacionesPP4);
                    cmd.Parameters.AddWithValue("@ProntoPago5Dias", plazo.ProntoPago5Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago5Descuento", plazo.ProntoPago5Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura5", plazo.VencimientoPagoFactura5);
                    cmd.Parameters.AddWithValue("@ObservacionesPP5", plazo.ObservacionesPP5);
                    cmd.Parameters.AddWithValue("@ObservacionesGenerales", plazo.ObservacionesGenerales);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AgregarByClave(EProveedorPlazoCredito plazo)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarPlazoByClaveProveedor @ClaveProveedor,
                                    @PrioridadUso,
                                    @EstatusActivo,
                                    @Revisado,
	                                @PlazoCreditoDias,
	                                @DefinicionPlazo,
	                                @ProntoPago1Dias,
	                                @ProntoPago1Descuento,
	                                @VencimientoPagoFactura1,
	                                @ObservacionesPP1,
	                                @ProntoPago2Dias,
	                                @ProntoPago2Descuento,
	                                @VencimientoPagoFactura2,
	                                @ObservacionesPP2,
	                                @ProntoPago3Dias,
	                                @ProntoPago3Descuento,
                                    @VencimientoPagoFactura3,
                                    @ObservacionesPP3,
                                    @ProntoPago4Dias,
	                                @ProntoPago4Descuento,
	                                @VencimientoPagoFactura4,
	                                @ObservacionesPP4,
	                                @ProntoPago5Dias,
	                                @ProntoPago5Descuento,
                                    @VencimientoPagoFactura5,
                                    @ObservacionesPP5,
                                    @ObservacionesGenerales";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", plazo.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@PrioridadUso", 2);
                    cmd.Parameters.AddWithValue("@EstatusActivo", 1);
                    cmd.Parameters.AddWithValue("@Revisado", 0);
                    cmd.Parameters.AddWithValue("@PlazoCreditoDias", plazo.PlazoCreditoDias);
                    cmd.Parameters.AddWithValue("@DefinicionPlazo", plazo.DefinicionPlazo);
                    cmd.Parameters.AddWithValue("@ProntoPago1Dias", plazo.ProntoPago1Dias);                    
                    cmd.Parameters.AddWithValue("@ProntoPago1Descuento", plazo.ProntoPago1Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura1", plazo.VencimientoPagoFactura1);
                    cmd.Parameters.AddWithValue("@ObservacionesPP1", plazo.ObservacionesPP1);
                    cmd.Parameters.AddWithValue("@ProntoPago2Dias", plazo.ProntoPago2Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago2Descuento", plazo.ProntoPago2Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura2", plazo.VencimientoPagoFactura2);
                    cmd.Parameters.AddWithValue("@ObservacionesPP2", plazo.ObservacionesPP2);
                    cmd.Parameters.AddWithValue("@ProntoPago3Dias", plazo.ProntoPago3Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago3Descuento", plazo.ProntoPago3Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura3", plazo.VencimientoPagoFactura3);
                    cmd.Parameters.AddWithValue("@ObservacionesPP3", plazo.ObservacionesPP3);
                    cmd.Parameters.AddWithValue("@ProntoPago4Dias", plazo.ProntoPago4Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago4Descuento", plazo.ProntoPago4Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura4", plazo.VencimientoPagoFactura4);
                    cmd.Parameters.AddWithValue("@ObservacionesPP4", plazo.ObservacionesPP4);
                    cmd.Parameters.AddWithValue("@ProntoPago5Dias", plazo.ProntoPago5Dias);
                    cmd.Parameters.AddWithValue("@ProntoPago5Descuento", plazo.ProntoPago5Descuento);
                    cmd.Parameters.AddWithValue("@VencimientoPagoFactura5", plazo.VencimientoPagoFactura5);
                    cmd.Parameters.AddWithValue("@ObservacionesPP5", plazo.ObservacionesPP5);
                    cmd.Parameters.AddWithValue("@ObservacionesGenerales", plazo.ObservacionesGenerales);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
