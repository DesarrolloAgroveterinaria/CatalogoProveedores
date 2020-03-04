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
    public class ProveedorDatosBancariosEXDal
    {
        //Obtener datos por búsqueda de clave
        public List<EProveedorDatosBancariosEX> GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorDatosBancariosEX> DLista = new List<EProveedorDatosBancariosEX>();
                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllDatosBancariosEXByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorDatosBancariosEX D = new EProveedorDatosBancariosEX
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            PrioridadDeUso = Convert.ToInt32(reader["PrioridadDeUso"]),
                            BancoEXid = Convert.ToInt32(reader["BancoEXid"]),
                            NombreBancoDestino = Convert.ToString(reader["NombreBancoDestino"]),
                            ClaveBancoDestino = Convert.ToString(reader["ClaveBancoDestino"]),
                            NombreDestinatario = reader["NombreDestinatario"] == DBNull.Value ? "" : Convert.ToString(reader["NombreDestinatario"]),
                            NumeroCuentaDestinatario = Convert.ToString(reader["NumeroCuentaDestinatario"]),
                            DivisaAPagar = Convert.ToString(reader["DivisaAPagar"]),
                            MontoMaximoAPagar = reader["MontoMaximoAPagar"] == DBNull.Value ? "" : Convert.ToString(reader["MontoMaximoAPagar"]),
                            NombreBancoIntermediario = reader["NombreBancoIntermediario"] == DBNull.Value ? "" : Convert.ToString(reader["NombreBancoIntermediario"]),
                            ClaveBancoIntermediario = reader["ClaveBancoIntermediario"] == DBNull.Value ? "" : Convert.ToString(reader["ClaveBancoIntermediario"]),
                            NumIdDireccionDestinatario = reader["NumIdDireccionDestinatario"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NumIdDireccionDestinatario"]),
                            Vigencia = Convert.ToBoolean(reader["Vigencia"]),
                            FechaDeVigencia = reader["FechaDeVigencia"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FechaDeVigencia"]),
                            TipoRelacionConDestinatario = reader["TipoRelacionConDestinatario"] == DBNull.Value ? "" : Convert.ToString(reader["TipoRelacionConDestinatario"]),
                            MotivoPago = reader["MotivoPago"] == DBNull.Value ? "" : Convert.ToString(reader["MotivoPago"]),
                            EsPreferencia = Convert.ToBoolean(reader["EsPreferencia"]),
                            EstatusActivo = Convert.ToBoolean(reader["EstatusActivo"])
                        };
                        DLista.Add(D);
                    }
                    return DLista;
                }
            }
            return null;
        }

        public void DesactivarByIdByClave(int bancoEXid, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorActivacion = 0;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_DesactivarCuentaEXByIdByClaveProveedor @BancoEXid,
	                                @ClaveProveedor, @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@BancoEXid", bancoEXid);
                    cmd.Parameters.Add("@EstatusActivo", SqlDbType.Bit).Value = valorActivacion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void esPreferenteByIdByClave(int bancoEXid, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorPrioridad = 1;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_MarcarPreferenteCuentaEXByIdByClaveProveedor @BancoEXid,
	                                @ClaveProveedor, @EsPreferencia";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@BancoEXid", bancoEXid);
                    cmd.Parameters.Add("@EsPreferencia", SqlDbType.Bit).Value = valorPrioridad;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void editarCuentaByIdByClave(EProveedorDatosBancariosEX cuentaEX)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarDatosBancariosEXByClaveProveedor @ClaveProveedor,
                                    @BancoEXid,
	                                @NombreBancoDestino,
                                    @ClaveBancoDestino,
                                    @NombreDestinatario,
                                    @NumeroCuentaDestinatario,
                                    @DivisaAPagar,
                                    @MontoMaximoAPagar,
                                    @NombreBancoIntermediario,
                                    @ClaveBancoIntermediario,
                                    @Vigencia,
                                    @FechaDeVigencia,
                                    @TipoRelacionConDestinatario,
                                    @MotivoPago";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", cuentaEX.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@BancoEXid", cuentaEX.BancoEXid);
                    cmd.Parameters.AddWithValue("@NombreBancoDestino", cuentaEX.NombreBancoDestino);
                    cmd.Parameters.AddWithValue("@ClaveBancoDestino", cuentaEX.ClaveBancoDestino);
                    cmd.Parameters.AddWithValue("@NombreDestinatario", cuentaEX.NombreDestinatario);
                    cmd.Parameters.AddWithValue("@NumeroCuentaDestinatario", cuentaEX.NumeroCuentaDestinatario);
                    cmd.Parameters.AddWithValue("@DivisaAPagar", cuentaEX.DivisaAPagar);
                    cmd.Parameters.AddWithValue("@MontoMaximoAPagar", cuentaEX.MontoMaximoAPagar);
                    cmd.Parameters.AddWithValue("@NombreBancoIntermediario", cuentaEX.NombreBancoIntermediario);
                    cmd.Parameters.AddWithValue("@ClaveBancoIntermediario", cuentaEX.ClaveBancoIntermediario);
                    cmd.Parameters.AddWithValue("@Vigencia", cuentaEX.Vigencia);
                    if (cuentaEX.FechaDeVigencia == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@FechaDeVigencia", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@FechaDeVigencia", cuentaEX.FechaDeVigencia);
                    cmd.Parameters.AddWithValue("@TipoRelacionConDestinatario", cuentaEX.TipoRelacionConDestinatario);
                    cmd.Parameters.AddWithValue("@MotivoPago", cuentaEX.MotivoPago);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AgregarByClave(EProveedorDatosBancariosEX cuentaEX)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarDatosBancariosEXByClaveProveedor @ClaveProveedor,
	                                @PrioridadDeUso,
	                                @NombreBancoDestino,
                                    @ClaveBancoDestino,
                                    @NombreDestinatario,
                                    @NumeroCuentaDestinatario,
                                    @DivisaAPagar,
                                    @MontoMaximoAPagar,
                                    @NombreBancoIntermediario,
                                    @ClaveBancoIntermediario,
                                    @NumIdDireccionDestinatario,
                                    @Vigencia,
                                    @FechaDeVigencia,
                                    @TipoRelacionConDestinatario,
                                    @MotivoPago,
                                    @EsPreferencia,
                                    @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", cuentaEX.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@PrioridadDeUso", cuentaEX.PrioridadDeUso);
                    cmd.Parameters.AddWithValue("@NombreBancoDestino", cuentaEX.NombreBancoDestino);
                    cmd.Parameters.AddWithValue("@ClaveBancoDestino", cuentaEX.ClaveBancoDestino);
                    cmd.Parameters.AddWithValue("@NombreDestinatario", cuentaEX.NombreDestinatario);
                    cmd.Parameters.AddWithValue("@NumeroCuentaDestinatario", cuentaEX.NumeroCuentaDestinatario);
                    cmd.Parameters.AddWithValue("@DivisaAPagar", cuentaEX.DivisaAPagar);
                    cmd.Parameters.AddWithValue("@MontoMaximoAPagar", cuentaEX.MontoMaximoAPagar);
                    cmd.Parameters.AddWithValue("@NombreBancoIntermediario", cuentaEX.NombreBancoIntermediario);
                    cmd.Parameters.AddWithValue("@ClaveBancoIntermediario", cuentaEX.ClaveBancoIntermediario);
                    cmd.Parameters.AddWithValue("@NumIdDireccionDestinatario", cuentaEX.NumIdDireccionDestinatario);
                    cmd.Parameters.AddWithValue("@Vigencia", cuentaEX.Vigencia);
                    if (cuentaEX.FechaDeVigencia == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@FechaDeVigencia", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@FechaDeVigencia", cuentaEX.FechaDeVigencia);
                    cmd.Parameters.AddWithValue("@TipoRelacionConDestinatario", cuentaEX.TipoRelacionConDestinatario);
                    cmd.Parameters.AddWithValue("@MotivoPago", cuentaEX.MotivoPago);
                    cmd.Parameters.AddWithValue("@EsPreferencia", cuentaEX.EsPreferencia);
                    cmd.Parameters.AddWithValue("@EstatusActivo", cuentaEX.EstatusActivo);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
