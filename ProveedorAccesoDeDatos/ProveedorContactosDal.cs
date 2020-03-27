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
    public class ProveedorContactosDal
    {
        public List<EProveedorContacto> GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorContacto> CLista = new List<EProveedorContacto>();
                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllContactosByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorContacto C = new EProveedorContacto
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            Contactoid = Convert.ToInt32(reader["Contactoid"]),
                            PrioridadDeUso = Convert.ToInt32(reader["PrioridadDeUso"]),
                            NombreCompleto = Convert.ToString(reader["NombreCompleto"]),
                            Puesto = Convert.ToString(reader["Puesto"]),
                            Categoria = Convert.ToString(reader["Categoría"]),
                            FuncionesContacto = Convert.ToString(reader["FuncionesContacto"]),
                            TelefonoPrimario = Convert.ToString(reader["TelefonoPrimario"]),
                            ExtensionTelefonoPrimario = reader["ExtensionTelefonoPrimario"] == DBNull.Value ? "" : Convert.ToString(reader["ExtensionTelefonoPrimario"]),
                            TelefonoSecundario = reader["TelefonoSecundario"] == DBNull.Value ? "" : Convert.ToString(reader["TelefonoSecundario"]),
                            ExtensionTelefonoSecundario = reader["ExtensionTelefonoSecundario"] == DBNull.Value ? "" : Convert.ToString(reader["ExtensionTelefonoSecundario"]),
                            Celular1 = reader["Celular1"] == DBNull.Value ? "" : Convert.ToString(reader["Celular1"]),
                            Celular2 = reader["Celular2"] == DBNull.Value ? "" : Convert.ToString(reader["Celular2"]),
                            Email1 = Convert.ToString(reader["Email1"]),
                            Email2 = reader["Email2"] == DBNull.Value ? "" : Convert.ToString(reader["Email2"]),
                            Comentarios = reader["Comentarios"] == DBNull.Value ? "" : Convert.ToString(reader["Comentarios"]),
                            EstatusActivo =  Convert.ToBoolean(reader["EstatusActivo"]),
                            Revisado = reader["Revisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["Revisado"])
                        };
                        CLista.Add(C);
                    }
                    return CLista;
                }
            }
            return null;
        }

        public void AgregarByClave(EProveedorContacto Contacto)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarContactoByClaveProveedor @ClaveProveedor,
	                                @PrioridadDeUso,
	                                @NombreCompleto,
	                                @Puesto,
	                                @Categoría,
	                                @FuncionesContacto,
	                                @TelefonoPrimario,
	                                @ExtensionTelefonoPrimario,
	                                @TelefonoSecundario,
	                                @ExtensionTelefonoSecundario,
	                                @Celular1,
	                                @Celular2,
	                                @Email1,
                                    @Email2,
                                    @Comentarios,
                                    @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", Contacto.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@PrioridadDeUso", Contacto.PrioridadDeUso);
                    cmd.Parameters.AddWithValue("@NombreCompleto", Contacto.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Puesto", Contacto.Puesto);
                    cmd.Parameters.AddWithValue("@Categoría", Contacto.Categoria);
                    cmd.Parameters.AddWithValue("@FuncionesContacto", Contacto.FuncionesContacto);
                    cmd.Parameters.AddWithValue("@TelefonoPrimario", Contacto.TelefonoPrimario);
                    cmd.Parameters.AddWithValue("@ExtensionTelefonoPrimario", Contacto.ExtensionTelefonoPrimario);
                    cmd.Parameters.AddWithValue("@TelefonoSecundario", Contacto.TelefonoSecundario);
                    cmd.Parameters.AddWithValue("@ExtensionTelefonoSecundario", Contacto.ExtensionTelefonoSecundario);
                    cmd.Parameters.AddWithValue("@Celular1", Contacto.Celular1);
                    cmd.Parameters.AddWithValue("@Celular2", Contacto.Celular2);
                    cmd.Parameters.AddWithValue("@Email1", Contacto.Email1);
                    cmd.Parameters.AddWithValue("@Email2", Contacto.Email2);
                    cmd.Parameters.AddWithValue("@Comentarios", Contacto.Comentarios);
                    cmd.Parameters.AddWithValue("@EstatusActivo", Contacto.EstatusActivo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarByIdByClave(EProveedorContacto Contacto)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {                
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarContactoByIdByClaveProveedor @ClaveProveedor,
                                    @Contactoid,
	                                @PrioridadDeUso,
	                                @NombreCompleto,
	                                @Puesto,
	                                @Categoría,
	                                @FuncionesContacto,
	                                @TelefonoPrimario,
	                                @ExtensionTelefonoPrimario,
	                                @TelefonoSecundario,
	                                @ExtensionTelefonoSecundario,
	                                @Celular1,
	                                @Celular2,
	                                @Email1,
                                    @Email2,
                                    @Comentarios,
                                    @EstatusActivo,
                                    @Revisado";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", Contacto.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@Contactoid", Contacto.Contactoid);
                    cmd.Parameters.AddWithValue("@PrioridadDeUso", Contacto.PrioridadDeUso);
                    cmd.Parameters.AddWithValue("@NombreCompleto", Contacto.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Puesto", Contacto.Puesto);
                    cmd.Parameters.AddWithValue("@Categoría", Contacto.Categoria);
                    cmd.Parameters.AddWithValue("@FuncionesContacto", Contacto.FuncionesContacto);
                    cmd.Parameters.AddWithValue("@TelefonoPrimario", Contacto.TelefonoPrimario);
                    cmd.Parameters.AddWithValue("@ExtensionTelefonoPrimario", Contacto.ExtensionTelefonoPrimario);
                    cmd.Parameters.AddWithValue("@TelefonoSecundario", Contacto.TelefonoSecundario);
                    cmd.Parameters.AddWithValue("@ExtensionTelefonoSecundario", Contacto.ExtensionTelefonoSecundario);
                    cmd.Parameters.AddWithValue("@Celular1", Contacto.Celular1);
                    cmd.Parameters.AddWithValue("@Celular2", Contacto.Celular2);
                    cmd.Parameters.AddWithValue("@Email1", Contacto.Email1);
                    cmd.Parameters.AddWithValue("@Email2", Contacto.Email2);
                    cmd.Parameters.AddWithValue("@Comentarios", Contacto.Comentarios);
                    cmd.Parameters.Add("@EstatusActivo", SqlDbType.Int).Value = Contacto.EstatusActivo;
                    cmd.Parameters.AddWithValue("@Revisado", Contacto.Revisado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DesactivarByIdByClave(int contactoid, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorActivacion = 0;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_DesactivarContactoByIdByClaveProveedor @Contactoid,
	                                @ClaveProveedor, @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@Contactoid", contactoid);
                    cmd.Parameters.Add("@EstatusActivo", SqlDbType.Bit).Value = valorActivacion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void PriorizarByIdByClave(int contactoid, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorPrioridad = 1;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_PriorizarContactoByIdByClaveProveedor @Contactoid,
	                                @ClaveProveedor, @PrioridadDeUso";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@Contactoid", contactoid);
                    cmd.Parameters.Add("@PrioridadDeUso", SqlDbType.Int).Value = valorPrioridad;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
