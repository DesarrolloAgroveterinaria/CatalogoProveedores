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
    public class ProveedorMovimientosDal
    {
        public List<EProveedorMovimientos> consultarUltimos100Movimientos()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                int numeroMov = 100;
                List<EProveedorMovimientos> MLista = new List<EProveedorMovimientos>();
                const string Query = "EXEC AGROCatalogoProveedoresSP_GetLastNumMovimientos @NumMovimientos";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@NumMovimientos", numeroMov);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorMovimientos M = new EProveedorMovimientos
                        {
                            Movimientoid = Convert.ToInt32(reader["Movimientoid"]),
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            NombreProveedor = Convert.ToString(reader["NombreProveedor"]),
                            AccionDato = Convert.ToString(reader["AccionDato"]),
                            CategoriaDato = Convert.ToString(reader["CategoriaDato"]),
                            Usuario = Convert.ToString(reader["Usuario"]),
                            idCorrespondiente = Convert.ToInt32(reader["idCorrespondiente"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"])
                        };
                        MLista.Add(M);
                    }
                    return MLista;
                }
            }
            return null;
        }

        public void agregarMov(EProveedorMovimientos mov)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarMov @ClaveProveedor,
	                                @NombreProveedor,
	                                @AccionDato,
	                                @CategoriaDato,
	                                @Usuario,
	                                @idCorrespondiente,
	                                @Fecha";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", mov.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@NombreProveedor", mov.NombreProveedor);
                    cmd.Parameters.AddWithValue("@AccionDato", mov.AccionDato);
                    cmd.Parameters.AddWithValue("@CategoriaDato", mov.CategoriaDato);
                    cmd.Parameters.AddWithValue("@Usuario", mov.Usuario);
                    cmd.Parameters.AddWithValue("@idCorrespondiente", mov.idCorrespondiente);
                    cmd.Parameters.AddWithValue("@Fecha", mov.Fecha);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
