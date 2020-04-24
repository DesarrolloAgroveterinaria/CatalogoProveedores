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
                            ClaveProveedorFlete = Convert.ToString(reader["ClaveProveedorFlete"]),
                            NombreProveedor = Convert.ToString(reader["NombreProveedor"]),
                            FormaEntrega = Convert.ToString(reader["FormaEntrega"]),
                            TipoEnvio = Convert.ToString(reader["TipoEnvio"]),
                            TransporteEnvio = Convert.ToString(reader["TransporteEnvio"]),
                            CargoEntrega = Convert.ToString(reader["CargoEntrega"]),
                            PedidoMin = reader["PedidoMin"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PedidoMin"]),
                            PedidoMax = reader["PedidoMax"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PedidoMax"]),
                            Unidad = reader["Unidad"] == DBNull.Value ? "" : Convert.ToString(reader["Unidad"]),
                            Origen = reader["Origen"] == DBNull.Value ? "" : Convert.ToString(reader["Origen"]),
                            Destino = reader["Destino"] == DBNull.Value ? "" : Convert.ToString(reader["Destino"]),
                            Observaciones = reader["Observaciones"] == DBNull.Value ? "" : Convert.ToString(reader["Observaciones"]),
                            EsPreferencia = reader["EsPreferencia"] == DBNull.Value ? false : Convert.ToBoolean(reader["EsPreferencia"]),
                            CostoFleteMatriz = reader["CostoFleteMatriz"] == DBNull.Value ? "" : Convert.ToString(reader["CostoFleteMatriz"]),
                            CostoFleteHipodromo = reader["CostoFleteHipodromo"] == DBNull.Value ? "" : Convert.ToString(reader["CostoFleteHipodromo"]),
                            CostoFleteSanPedro = reader["CostoFleteSanPedro"] == DBNull.Value ? "" : Convert.ToString(reader["CostoFleteSanPedro"]),
                            CostoFleteMagdalena = reader["CostoFleteMagdalena"] == DBNull.Value ? "" : Convert.ToString(reader["CostoFleteMagdalena"]),
                            CostoFleteCaborca = reader["CostoFleteCaborca"] == DBNull.Value ? "" : Convert.ToString(reader["CostoFleteCaborca"]),
                            CostoFleteCEDIS = reader["CostoFleteCEDIS"] == DBNull.Value ? "" : Convert.ToString(reader["CostoFleteCEDIS"]),
                            CostoFleteCMA = reader["CostoFleteCMA"] == DBNull.Value ? "" : Convert.ToString(reader["CostoFleteCMA"]),
                        };
                        FLista.Add(F);
                    }
                    return FLista;
                }
            }
            return null;
        }

        public void MarcarPreferente(string fleteid, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {                
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_EsPreferenciaFleteByIdByClaveProveedor @ClaveProveedor, @Fleteid, @EsPreferencia";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@Fleteid", fleteid);
                    cmd.Parameters.Add("@EsPreferencia", SqlDbType.Bit).Value = true;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DesactivarByIdByClave(string fleteid, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {                
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_DesactivarFleteByIdByClaveProveedor @ClaveProveedor, @Fleteid";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@Fleteid", fleteid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarByIdByClave(EProveedorFletes flete)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarFleteByIdByClaveProveedor @ClaveProveedor,
                                    @Fleteid,
                                    @ClaveProveedorFlete,
	                                @NombreProveedor,
                                    @FormaEntrega,  
                                    @TipoEnvio,
                                    @TransporteEnvio,  
                                    @CargoEntrega,	                                
	                                @PedidoMin,
	                                @PedidoMax,
	                                @Unidad,
	                                @Origen,
	                                @Destino,
	                                @Observaciones,
                                    @CostoFleteMatriz,
                                    @CostoFleteHipodromo,
                                    @CostoFleteSanPedro,
                                    @CostoFleteMagdalena,
                                    @CostoFleteCaborca,
                                    @CostoFleteCEDIS,
                                    @CostoFleteCMA";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", flete.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@Fleteid", flete.Fleteid);
                    cmd.Parameters.AddWithValue("@ClaveProveedorFlete", flete.ClaveProveedorFlete);
                    cmd.Parameters.AddWithValue("@NombreProveedor", flete.NombreProveedor);
                    cmd.Parameters.AddWithValue("@FormaEntrega", flete.FormaEntrega);
                    cmd.Parameters.AddWithValue("@TipoEnvio", flete.TipoEnvio);
                    cmd.Parameters.AddWithValue("@TransporteEnvio", flete.TransporteEnvio);
                    cmd.Parameters.AddWithValue("@CargoEntrega", flete.CargoEntrega);
                    cmd.Parameters.AddWithValue("@PedidoMin", flete.PedidoMin);
                    cmd.Parameters.AddWithValue("@PedidoMax", flete.PedidoMax);
                    cmd.Parameters.AddWithValue("@Unidad", flete.Unidad);
                    cmd.Parameters.AddWithValue("@Origen", flete.Origen);
                    cmd.Parameters.AddWithValue("@Destino", flete.Destino);
                    cmd.Parameters.AddWithValue("@Observaciones", flete.Observaciones);
                    cmd.Parameters.AddWithValue("@CostoFleteMatriz", flete.CostoFleteMatriz);
                    cmd.Parameters.AddWithValue("@CostoFleteHipodromo", flete.CostoFleteHipodromo);
                    cmd.Parameters.AddWithValue("@CostoFleteSanPedro", flete.CostoFleteSanPedro);
                    cmd.Parameters.AddWithValue("@CostoFleteMagdalena", flete.CostoFleteMagdalena);
                    cmd.Parameters.AddWithValue("@CostoFleteCaborca", flete.CostoFleteCaborca);
                    cmd.Parameters.AddWithValue("@CostoFleteCEDIS", flete.CostoFleteCEDIS);
                    cmd.Parameters.AddWithValue("@CostoFleteCMA", flete.CostoFleteCMA);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AgregarByClave(EProveedorFletes flete)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarFleteByClaveProveedor @ClaveProveedor,	                                
	                                @ClaveProveedorFlete,
	                                @NombreProveedor,
                                    @FormaEntrega,  
                                    @TipoEnvio,
                                    @TransporteEnvio,  
                                    @CargoEntrega,	                                
	                                @PedidoMin,
	                                @PedidoMax,
	                                @Unidad,
	                                @Origen,
	                                @Destino,
	                                @Observaciones,
	                                @EsPreferencia,
                                    @CostoFleteMatriz,
                                    @CostoFleteHipodromo,
                                    @CostoFleteSanPedro,
                                    @CostoFleteMagdalena,
                                    @CostoFleteCaborca,
                                    @CostoFleteCEDIS,
                                    @CostoFleteCMA";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", flete.ClaveProveedor);                    
                    cmd.Parameters.AddWithValue("@ClaveProveedorFlete", flete.ClaveProveedorFlete);
                    cmd.Parameters.AddWithValue("@NombreProveedor", flete.NombreProveedor);
                    cmd.Parameters.AddWithValue("@FormaEntrega", flete.FormaEntrega);
                    cmd.Parameters.AddWithValue("@TipoEnvio", flete.TipoEnvio);
                    cmd.Parameters.AddWithValue("@TransporteEnvio", flete.TransporteEnvio);
                    cmd.Parameters.AddWithValue("@CargoEntrega", flete.CargoEntrega);
                    cmd.Parameters.AddWithValue("@PedidoMin", flete.PedidoMin);
                    cmd.Parameters.AddWithValue("@PedidoMax", flete.PedidoMax);
                    cmd.Parameters.AddWithValue("@Unidad", flete.Unidad);
                    cmd.Parameters.AddWithValue("@Origen", flete.Origen);
                    cmd.Parameters.AddWithValue("@Destino", flete.Destino);
                    cmd.Parameters.AddWithValue("@Observaciones", flete.Observaciones);
                    cmd.Parameters.AddWithValue("@EsPreferencia", flete.EsPreferencia);
                    cmd.Parameters.AddWithValue("@CostoFleteMatriz", flete.CostoFleteMatriz);
                    cmd.Parameters.AddWithValue("@CostoFleteHipodromo", flete.CostoFleteHipodromo);
                    cmd.Parameters.AddWithValue("@CostoFleteSanPedro", flete.CostoFleteSanPedro);
                    cmd.Parameters.AddWithValue("@CostoFleteMagdalena", flete.CostoFleteMagdalena);
                    cmd.Parameters.AddWithValue("@CostoFleteCaborca", flete.CostoFleteCaborca);
                    cmd.Parameters.AddWithValue("@CostoFleteCEDIS", flete.CostoFleteCEDIS);
                    cmd.Parameters.AddWithValue("@CostoFleteCMA", flete.CostoFleteCMA);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

