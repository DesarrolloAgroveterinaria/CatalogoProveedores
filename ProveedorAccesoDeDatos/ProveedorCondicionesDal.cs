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
                            TransporteEnvioAereo = Convert.ToBoolean(reader["TransporteEnvioAereo"]),
                            TransporteEnvioTerrestre = Convert.ToBoolean(reader["TransporteEnvioTerrestre"]),
                            TipoCourier = Convert.ToBoolean(reader["TipoCourier"]),
                            TipoDomicilio = Convert.ToBoolean(reader["TipoDomicilio"]),
                            FormaEntregaPersonal = Convert.ToBoolean(reader["FormaEntregaPersonal"]),
                            FormaPaqueteria = Convert.ToBoolean(reader["FormaPaqueteria"]),
                            FormaTransporteCarga = Convert.ToBoolean(reader["FormaTransporteCarga"]),
                            CargoPagado = Convert.ToBoolean(reader["CargoPagado"]),
                            CargoPorCobrar = Convert.ToBoolean(reader["CargoPorCobrar"]),
                            SucursalEntrega = Convert.ToString(reader["SucursalEntrega"]),
                            TiempoEntrega = Convert.ToString(reader["TiempoEntrega"]),
                            FormaEntrega = reader["FormaEntrega"] == DBNull.Value ? "" : Convert.ToString(reader["FormaEntrega"]),
                            ObservacionesTiempoEntrega = reader["ObservacionesTiempoEntrega"] == DBNull.Value ? "" : Convert.ToString(reader["ObservacionesTiempoEntrega"]),          
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
                                   @TransporteEnvioAereo,
                                   @TransporteEnvioTerrestre,
                                   @TipoCourier,
                                   @TipoDomicilio,
                                   @FormaEntregaPersonal,
                                   @FormaPaqueteria,
                                   @FormaTransporteCarga,
                                   @CargoPagado,
                                   @CargoPorCobrar,
                                   @TiempoEntrega,
                                   @FormaEntrega,
                                   @SucursalEntrega,
                                   @ObservacionesTiempoEntrega,
                                   @CondicionesEspecialesEntrega
	                               ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", condiciones.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@TransporteEnvioAereo", condiciones.TransporteEnvioAereo);
                    cmd.Parameters.AddWithValue("@TransporteEnvioTerrestre", condiciones.TransporteEnvioTerrestre);
                    cmd.Parameters.AddWithValue("@TipoCourier", condiciones.TipoCourier);
                    cmd.Parameters.AddWithValue("@TipoDomicilio", condiciones.TipoDomicilio);
                    cmd.Parameters.AddWithValue("@FormaEntregaPersonal", condiciones.FormaEntregaPersonal);
                    cmd.Parameters.AddWithValue("@FormaPaqueteria", condiciones.FormaPaqueteria);
                    cmd.Parameters.AddWithValue("@FormaTransporteCarga", condiciones.FormaTransporteCarga);
                    cmd.Parameters.AddWithValue("@CargoPagado", condiciones.CargoPagado);
                    cmd.Parameters.AddWithValue("@CargoPorCobrar", condiciones.CargoPorCobrar);                    
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
                                   @TransporteEnvioAereo,
                                   @TransporteEnvioTerrestre,
                                   @TipoCourier,
                                   @TipoDomicilio,
                                   @FormaEntregaPersonal,
                                   @FormaPaqueteria,
                                   @FormaTransporteCarga,
                                   @CargoPagado,
                                   @CargoPorCobrar,
                                   @TiempoEntrega,
                                   @FormaEntrega,
                                   @SucursalEntrega,
                                   @ObservacionesTiempoEntrega,
                                   @CondicionesEspecialesEntrega
	                               ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", condiciones.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@TransporteEnvioAereo", condiciones.TransporteEnvioAereo);
                    cmd.Parameters.AddWithValue("@TransporteEnvioTerrestre", condiciones.TransporteEnvioTerrestre);
                    cmd.Parameters.AddWithValue("@TipoCourier", condiciones.TipoCourier);
                    cmd.Parameters.AddWithValue("@TipoDomicilio", condiciones.TipoDomicilio);
                    cmd.Parameters.AddWithValue("@FormaEntregaPersonal", condiciones.FormaEntregaPersonal);
                    cmd.Parameters.AddWithValue("@FormaPaqueteria", condiciones.FormaPaqueteria);
                    cmd.Parameters.AddWithValue("@FormaTransporteCarga", condiciones.FormaTransporteCarga);
                    cmd.Parameters.AddWithValue("@CargoPagado", condiciones.CargoPagado);
                    cmd.Parameters.AddWithValue("@CargoPorCobrar", condiciones.CargoPorCobrar);
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
