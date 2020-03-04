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
    public class ProveedorUbicacionArchivosDal
    {
        public List<EProveedorUbicacionArchivos> GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorUbicacionArchivos> UALista = new List<EProveedorUbicacionArchivos>();
                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllUbicacionArchivosByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorUbicacionArchivos UA = new EProveedorUbicacionArchivos
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            UbicacionArchivoid = Convert.ToInt32(reader["UbicacionArchivoid"]),
                            CategoriaArchivo = Convert.ToString(reader["CategoriaArchivo"]),
                            PATHArchivo = Convert.ToString(reader["PATHArchivo"]),
                            FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"]),
                            FechaVigencia = Convert.ToDateTime(reader["FechaVigencia"]),
                            EstatusActivo = reader["EstatusActivo"] == DBNull.Value ? false : Convert.ToBoolean(reader["EstatusActivo"]),
                            UsuarioModificacion = reader["UsuarioModificacion"] == DBNull.Value ? "" : Convert.ToString(reader["UsuarioModificacion"])

                        };
                       UALista.Add(UA);
                    }
                    return UALista;
                }
            }
            return null;
        }

        public void DesactivarById(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorActivacion = 0;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_DesactivarUbicacionArchivoById @UbicacionArchivoid, @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@UbicacionArchivoid", id);


                    cmd.Parameters.Add("@EstatusActivo", SqlDbType.Bit).Value = valorActivacion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public EProveedorUbicacionArchivos GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetUbicacionArchivosById @UbicacionArchivoid";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@UbicacionArchivoid", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EProveedorUbicacionArchivos U = new EProveedorUbicacionArchivos
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            UbicacionArchivoid = Convert.ToInt32(reader["UbicacionArchivoid"]),
                            CategoriaArchivo = Convert.ToString(reader["CategoriaArchivo"]),
                            PATHArchivo = Convert.ToString(reader["PATHArchivo"]),
                            FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"]),
                            FechaVigencia = Convert.ToDateTime(reader["FechaVigencia"]),
                            EstatusActivo = reader["EstatusActivo"] == DBNull.Value ? false : Convert.ToBoolean(reader["EstatusActivo"]),
                            UsuarioModificacion = reader["UsuarioModificacion"] == DBNull.Value ? "" : Convert.ToString(reader["UsuarioModificacion"])
                        };
                        return U;
                    }
                    return null;
                }
            }            
        }

        public void AgregarUbicacionArchivo(string pathLocation, string claveProveedor, string categoriaDato)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                DateTime dateToday = DateTime.Today;
                DateTime finalDate = dateToday.AddYears(1);

                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarUbicacionArchivoByClaveProveedor @ClaveProveedor,
	                                @CategoriaArchivo,
                                    @PATHArchivo,
                                    @FechaActualizacion,
                                    @FechaVigencia,
                                    @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@CategoriaArchivo", categoriaDato);
                    cmd.Parameters.AddWithValue("@PATHArchivo", pathLocation);
                    cmd.Parameters.AddWithValue("@FechaActualizacion", dateToday);
                    cmd.Parameters.AddWithValue("@FechaVigencia", finalDate);
                    cmd.Parameters.AddWithValue("@EstatusActivo", 1);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
