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
    public class ProveedorDatosBancariosMXDal
    {
        //Obtener datos por búsqueda de clave
        public List<EProveedorDatosBancariosMX> GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorDatosBancariosMX> DLista = new List<EProveedorDatosBancariosMX>();
                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllDatosBancariosMXByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorDatosBancariosMX D = new EProveedorDatosBancariosMX
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            PrioridadDeUso = Convert.ToInt32(reader["PrioridadDeUso"]),
                            BancoMXid = Convert.ToInt32(reader["BancoMXid"]),
                            NombreBancoDestino = Convert.ToString(reader["NombreBancoDestino"]),
                            NumeroCuentaDestinatario = Convert.ToString(reader["NumeroCuentaDestinatario"]),
                            DivisaAPagar = Convert.ToString(reader["DivisaAPagar"]),
                            CLABE = Convert.ToString(reader["CLABE"]),
                            Sucursal = reader["Sucursal"] == DBNull.Value ? "" : Convert.ToString(reader["Sucursal"]),
                            EsPreferencia = Convert.ToBoolean(reader["EsPreferencia"]),
                            EstatusActivo = Convert.ToBoolean(reader["EstatusActivo"]),
                            Revisado = reader["Revisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["Revisado"])
                        };
                        DLista.Add(D);
                    }
                    return DLista;
                }
            }
            return null;
        }

        public void AgregarByClave(EProveedorDatosBancariosMX cuentaMX)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarDatosBancariosMXByClaveProveedor @ClaveProveedor,
	                                @PrioridadDeUso,
	                                @NombreBancoDestino,
                                    @CLABE,
                                    @NumeroCuentaDestinatario,
                                    @Sucursal,
                                    @DivisaAPagar,
                                    @EsPreferencia,
                                    @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", cuentaMX.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@PrioridadDeUso", cuentaMX.PrioridadDeUso);
                    cmd.Parameters.AddWithValue("@NombreBancoDestino", cuentaMX.NombreBancoDestino);
                    cmd.Parameters.AddWithValue("@CLABE", cuentaMX.CLABE);
                    cmd.Parameters.AddWithValue("@NumeroCuentaDestinatario", cuentaMX.NumeroCuentaDestinatario);
                    cmd.Parameters.AddWithValue("@Sucursal", cuentaMX.Sucursal);
                    cmd.Parameters.AddWithValue("@DivisaAPagar", cuentaMX.DivisaAPagar);
                    cmd.Parameters.AddWithValue("@EsPreferencia", cuentaMX.EsPreferencia);
                    cmd.Parameters.AddWithValue("@EstatusActivo", cuentaMX.EstatusActivo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarByIdByClave(EProveedorDatosBancariosMX cuentaMX)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarDatosBancariosMXByClaveProveedor @ClaveProveedor,
                                    @BancoMXid,
	                                @PrioridadDeUso,
	                                @NombreBancoDestino,
                                    @CLABE,
                                    @NumeroCuentaDestinatario,
                                    @Sucursal,
                                    @DivisaAPagar,
                                    @EsPreferencia,
                                    @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", cuentaMX.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@BancoMXid", cuentaMX.BancoMXid);
                    cmd.Parameters.AddWithValue("@PrioridadDeUso", cuentaMX.PrioridadDeUso);
                    cmd.Parameters.AddWithValue("@NombreBancoDestino", cuentaMX.NombreBancoDestino);
                    cmd.Parameters.AddWithValue("@CLABE", cuentaMX.CLABE);
                    cmd.Parameters.AddWithValue("@NumeroCuentaDestinatario", cuentaMX.NumeroCuentaDestinatario);
                    cmd.Parameters.AddWithValue("@Sucursal", cuentaMX.Sucursal);
                    cmd.Parameters.AddWithValue("@DivisaAPagar", cuentaMX.DivisaAPagar);
                    cmd.Parameters.AddWithValue("@EsPreferencia", cuentaMX.EsPreferencia);
                    cmd.Parameters.AddWithValue("@EstatusActivo", cuentaMX.EstatusActivo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DesactivarByIdByClave(int bancoMXid, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorActivacion = 0;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_DesactivarCuentaMXByIdByClaveProveedor @BancoMXid,
	                                @ClaveProveedor, @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@BancoMXid", bancoMXid);
                    cmd.Parameters.Add("@EstatusActivo", SqlDbType.Bit).Value = valorActivacion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void esPreferenteByIdByClave(int bancoMXid, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorPrioridad = 1;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_MarcarPreferenteCuentaMXByIdByClaveProveedor @BancoMXid,
	                                @ClaveProveedor, @EsPreferencia";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@BancoMXid", bancoMXid);
                    cmd.Parameters.Add("@EsPreferencia", SqlDbType.Bit).Value = valorPrioridad;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
