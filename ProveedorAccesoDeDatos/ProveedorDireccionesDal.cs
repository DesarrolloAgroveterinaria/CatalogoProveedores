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
    //para cada acción u operacíón en la base de datos se establece una nueva conexión
    public class ProveedorDireccionesDal
    {
        //Obtener datos por búsqueda de clave
        public List<EProveedorDirecciones> GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorDirecciones> DLista = new List<EProveedorDirecciones>();
                const string Query = "EXEC AGROCatalogoProveedoresSP_GetAllDireccionesByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorDirecciones D = new EProveedorDirecciones
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            PrioridadDeUso = Convert.ToInt32(reader["PrioridadDeUso"]),
                            NumIdDireccion = Convert.ToInt32(reader["NumIdDireccion"]),
                            ConceptoUso = Convert.ToString(reader["ConceptoUso"]),
                            CalleAveBlvr = Convert.ToString(reader["Calle"]),
                            NumExterior = Convert.ToString(reader["NumExterior"]),
                            NumInterior = reader["NumInterior"] == DBNull.Value ? "" : Convert.ToString(reader["NumInterior"]),
                            InfAdicional = reader["InfAdicional"] == DBNull.Value ? "" : Convert.ToString(reader["InfAdicional"]),
                            Colonia = Convert.ToString(reader["Colonia"]),
                            CodigoPostal = Convert.ToString(reader["CodigoPostal"]),
                            Poblacion = Convert.ToString(reader["Poblacion"]),
                            Estado = Convert.ToString(reader["Estado"]),
                            Pais = Convert.ToString(reader["Pais"]),
                            EstatusActivo = Convert.ToBoolean(reader["EstatusActivo"])
                        };
                        DLista.Add(D);
                    }
                    return DLista;
                }
            }
            return null;
        }

        public EProveedorDirecciones GetByClaveById(string ClaveP, int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string Query = "EXEC AGROCatalogoProveedoresSP_GetDireccionesByClaveById @ClaveProveedor, @NumIdDireccion";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", ClaveP);
                    cmd.Parameters.AddWithValue("@NumIdDireccion", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EProveedorDirecciones D = new EProveedorDirecciones
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            PrioridadDeUso = Convert.ToInt32(reader["PrioridadDeUso"]),
                            NumIdDireccion = Convert.ToInt32(reader["NumIdDireccion"]),
                            ConceptoUso = Convert.ToString(reader["ConceptoUso"]),
                            CalleAveBlvr = Convert.ToString(reader["Calle"]),
                            NumExterior = Convert.ToString(reader["NumExterior"]),
                            NumInterior = reader["NumInterior"] == DBNull.Value ? "" : Convert.ToString(reader["NumInterior"]),
                            InfAdicional = reader["InfAdicional"] == DBNull.Value ? "" : Convert.ToString(reader["InfAdicional"]),
                            Colonia = Convert.ToString(reader["Colonia"]),
                            CodigoPostal = Convert.ToString(reader["CodigoPostal"]),
                            Poblacion = Convert.ToString(reader["Poblacion"]),
                            Estado = Convert.ToString(reader["Estado"]),
                            Pais = Convert.ToString(reader["Pais"]),
                            EstatusActivo = Convert.ToBoolean(reader["EstatusActivo"])
                        };
                        return D;
                    }
                }
                return null;
            }
        }
        public void AgregarByClave(EProveedorDirecciones Direccion)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_AgregarDireccionByClaveProveedor @ClaveProveedor,
	                                @PrioridadDeUso,
	                                @ConceptoUso,
	                                @Calle,
	                                @NumExterior,
	                                @NumInterior,
	                                @InfAdicional,
	                                @Colonia,
	                                @CodigoPostal,
	                                @Poblacion,
	                                @Estado,
	                                @Pais,
	                                @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", Direccion.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@PrioridadDeUso", Direccion.PrioridadDeUso);
                    cmd.Parameters.AddWithValue("@ConceptoUso", Direccion.ConceptoUso);
                    cmd.Parameters.AddWithValue("@Calle", Direccion.CalleAveBlvr);
                    cmd.Parameters.AddWithValue("@NumExterior", Direccion.NumExterior);
                    cmd.Parameters.AddWithValue("@NumInterior", Direccion.NumInterior);
                    cmd.Parameters.AddWithValue("@InfAdicional", Direccion.InfAdicional);
                    cmd.Parameters.AddWithValue("@Colonia", Direccion.Colonia);
                    cmd.Parameters.AddWithValue("@CodigoPostal", Direccion.CodigoPostal);
                    cmd.Parameters.AddWithValue("@Poblacion", Direccion.Poblacion);
                    cmd.Parameters.AddWithValue("@Estado", Direccion.Estado);
                    cmd.Parameters.AddWithValue("@Pais", Direccion.Pais);
                    cmd.Parameters.AddWithValue("@EstatusActivo", Direccion.EstatusActivo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int ConsultarMaxId(string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                int id = -1;

                const string Query = "EXEC AGROCatalogoProveedoresSP_ConsultarMaxIdDirecciones @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        id = Convert.ToInt32(reader["NumIdDireccion"]);      
                }
                return id;
            }
        }

        public void EditarByIdByClave(EProveedorDirecciones Direccion)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorActivacion = 1;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarDireccionByIdByClaveProveedor @ClaveProveedor,
                                    @NumIdDireccion,
	                                @ConceptoUso,
	                                @Calle,
	                                @NumExterior,
	                                @NumInterior,
	                                @InfAdicional,
	                                @Colonia,
	                                @CodigoPostal,
	                                @Poblacion,
	                                @Estado,
	                                @Pais,
	                                @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", Direccion.ClaveProveedor);
                    cmd.Parameters.AddWithValue("@NumIdDireccion", Direccion.NumIdDireccion);
                    cmd.Parameters.AddWithValue("@ConceptoUso", Direccion.ConceptoUso);
                    cmd.Parameters.AddWithValue("@Calle", Direccion.CalleAveBlvr);
                    cmd.Parameters.AddWithValue("@NumExterior", Direccion.NumExterior);
                    cmd.Parameters.AddWithValue("@NumInterior", Direccion.NumInterior);
                    cmd.Parameters.AddWithValue("@InfAdicional", Direccion.InfAdicional);
                    cmd.Parameters.AddWithValue("@Colonia", Direccion.Colonia);
                    cmd.Parameters.AddWithValue("@CodigoPostal", Direccion.CodigoPostal);
                    cmd.Parameters.AddWithValue("@Poblacion", Direccion.Poblacion);
                    cmd.Parameters.AddWithValue("@Estado", Direccion.Estado);
                    cmd.Parameters.AddWithValue("@Pais", Direccion.Pais);
                    cmd.Parameters.Add("@EstatusActivo", SqlDbType.Int).Value = valorActivacion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void PriorizarByIdByClave(int numIdDireccion, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorPrioridad = 1;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_PriorizarDireccionByIdByClaveProveedor @NumIdDireccion,
	                                @ClaveProveedor, @PrioridadDeUso";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@NumIdDireccion", numIdDireccion);
                    cmd.Parameters.Add("@PrioridadDeUso", SqlDbType.Int).Value = valorPrioridad;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DesactivarByIdByClave(int numIdDireccion, string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                int valorActivacion = 0;
                conn.Open();
                const string Query = @"EXEC AGROCatalogoProveedoresSP_DesactivarDireccionByIdByClaveProveedor @NumIdDireccion,
	                                @ClaveProveedor, @EstatusActivo";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@NumIdDireccion", numIdDireccion);
                    cmd.Parameters.Add("@EstatusActivo", SqlDbType.Bit).Value = valorActivacion;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
