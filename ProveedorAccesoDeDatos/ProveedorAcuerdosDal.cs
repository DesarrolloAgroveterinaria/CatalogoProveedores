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
    public class ProveedorAcuerdosDal
    {
        //Obtener datos por busqueda de Clave
        public EProveedorAcuerdos GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllAcuerdosByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EProveedorAcuerdos A = new EProveedorAcuerdos
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            Acuerdoid = Convert.ToInt32(reader["Acuerdoid"]),
                            AcuerdoCompra = reader["AcuerdoCompra"] == DBNull.Value ? "" : Convert.ToString(reader["AcuerdoCompra"]),
                            AcuerdoVentaPublico = reader["AcuerdoVentaPublico"] == DBNull.Value ? "" : Convert.ToString(reader["AcuerdoVentaPublico"]),
                            AcuerdoAtencionClientes = reader["AcuerdoAtencioClientes"] == DBNull.Value ? "" : Convert.ToString(reader["AcuerdoAtencioClientes"]),
                            ListaPrecios = reader["ListaPrecios"] == DBNull.Value ? "" : Convert.ToString(reader["ListaPrecios"])
                        };
                        return A;
                    }
                    return null;
                }
            }            
        }
        public void editarAcuerdos(EProveedorAcuerdos acuerdos)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarAcuerdosByClaveProveedor @ClaveProveedor,
	                               @AcuerdoCompra,
                                   @AcuerdoVentaPublico,
                                   @AcuerdoAtencioClientes,
                                   @ListaPrecios";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", acuerdos.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@AcuerdoCompra", acuerdos.AcuerdoCompra);
                    cmd.Parameters.AddWithValue("@AcuerdoVentaPublico", acuerdos.AcuerdoVentaPublico);
                    cmd.Parameters.AddWithValue("@AcuerdoAtencioClientes", acuerdos.AcuerdoAtencionClientes);
                    cmd.Parameters.AddWithValue("@ListaPrecios", acuerdos.ListaPrecios);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void agregarAcuerdos(EProveedorAcuerdos acuerdos)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarAcuerdosByClaveProveedor @ClaveProveedor,
	                               @AcuerdoCompra,
                                   @AcuerdoVentaPublico,
                                   @AcuerdoAtencioClientes
                                   @ListaPrecios";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", acuerdos.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@AcuerdoCompra", acuerdos.AcuerdoCompra);
                    cmd.Parameters.AddWithValue("@AcuerdoVentaPublico", acuerdos.AcuerdoVentaPublico);
                    cmd.Parameters.AddWithValue("@AcuerdoAtencioClientes", acuerdos.AcuerdoAtencionClientes);
                    cmd.Parameters.AddWithValue("@ListaPrecios", acuerdos.ListaPrecios);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
