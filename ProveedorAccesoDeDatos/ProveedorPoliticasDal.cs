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
    public class ProveedorPoliticasDal
    {
        //Obtener datos por busqueda de Clave
        public EProveedorPoliticas GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllPoliticasByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EProveedorPoliticas P = new EProveedorPoliticas
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            Politicasid = Convert.ToInt32(reader["Politicasid"]),
                            PoliticasGarantia = Convert.ToString(reader["PoliticasGarantia"]),                            
                            PoliticasDevoluciones = Convert.ToString(reader["PoliticasDevoluciones"]),                            
                            CompraMinimaMensual = reader["CompraMinimaMensual"] == DBNull.Value ? "0" : Convert.ToString(reader["CompraMinimaMensual"]),
                            ObservacionesSolicitudCompra = reader["ObservacionesSolicitudCompra"] == DBNull.Value ? "" : Convert.ToString(reader["ObservacionesSolicitudCompra"]),
                            RecepcionSolicitudCompra = Convert.ToString(reader["RecepcionSolicitudCompra"])
                        };
                        return P;
                    }
                }
            }
            return null;
        }

        public void editarPoliticas(EProveedorPoliticas politicas)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarPoliticasByClaveProveedor @ClaveProveedor, 
                                   @PoliticasGarantia,
                                   @PoliticasDevoluciones,
                                   @CompraMinimaMensual,
                                   @ObservacionesSolicitudCompra,
                                   @RecepcionSolicitudCompra
	                               ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", politicas.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@PoliticasGarantia", politicas.PoliticasGarantia);
                    cmd.Parameters.AddWithValue("@PoliticasDevoluciones", politicas.PoliticasDevoluciones);
                    cmd.Parameters.AddWithValue("@CompraMinimaMensual", politicas.CompraMinimaMensual);
                    cmd.Parameters.AddWithValue("@ObservacionesSolicitudCompra", politicas.ObservacionesSolicitudCompra);
                    cmd.Parameters.AddWithValue("@RecepcionSolicitudCompra", politicas.RecepcionSolicitudCompra);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void agregarPoliticas(EProveedorPoliticas politicas)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarPoliticasByClaveProveedor @ClaveProveedor, 
                                   @PoliticasGarantia,
                                   @PoliticasDevoluciones,
                                   @CompraMinimaMensual,
                                   @ObservacionesSolicitudCompra,
                                   @RecepcionSolicitudCompra
	                               ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", politicas.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@PoliticasGarantia", politicas.PoliticasGarantia);
                    cmd.Parameters.AddWithValue("@PoliticasDevoluciones", politicas.PoliticasDevoluciones);
                    cmd.Parameters.AddWithValue("@CompraMinimaMensual", politicas.CompraMinimaMensual);
                    cmd.Parameters.AddWithValue("@ObservacionesSolicitudCompra", politicas.ObservacionesSolicitudCompra);
                    cmd.Parameters.AddWithValue("@RecepcionSolicitudCompra", politicas.RecepcionSolicitudCompra);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
