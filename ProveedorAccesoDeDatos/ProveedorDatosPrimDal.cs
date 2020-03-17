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
    //para cada acción u operacíón en la base de datos se establece una nueva conexión
    public class ProveedorDatosPrimDal
    {
        //Obtener datos por busqueda de Clave
        public EProveedorDatosPrimarios GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetAllDatosPrimariosByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EProveedorDatosPrimarios P = new EProveedorDatosPrimarios
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            NombreProveedor = Convert.ToString(reader["NombreProveedor"]),
                            RFC = Convert.ToString(reader["RFC"]),
                            Categoria = Convert.ToString(reader["Categoria"]),
                            TipoProveedor = Convert.ToString(reader["TipoProveedor"]),
                            PATHImagen = reader["PATHImagen"] == DBNull.Value ? "" : Convert.ToString(reader["PATHImagen"]),
                            hasImagen = Convert.ToBoolean(reader["hasImagen"]),
                            fechaUltimaActualizacion = Convert.ToDateTime(reader["UltimaActualizacion"]),
                            isDireccionesRevisado = reader["isDireccionesRevisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["isDireccionesRevisado"]),
                            isContactosRevisado = reader["isContactosRevisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["isContactosRevisado"]),
                            isDatosBancariosMXRevisado = reader["isDatosBancariosMXRevisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["isDatosBancariosMXRevisado"]),
                            isDatosBancariosEXRevisado = reader["isDatosBancariosEXRevisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["isDatosBancariosEXRevisado"]),
                            isAcuerdosRevisado = reader["isAcuerdosRevisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAcuerdosRevisado"]),
                            isCondicionesRevisado = reader["isCondicionesRevisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["isCondicionesRevisado"]),
                            isPoliticasRevisado = reader["isPoliticasRevisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["isPoliticasRevisado"]),
                            isExpedienteRevisado = reader["isExpedienteRevisado"] == DBNull.Value ? false : Convert.ToBoolean(reader["isExpedienteRevisado"])                            
                        };
                        return P;
                    }
                }
            }
            return null;
        }
        //Obtener datos por busqueda de Nombre Proveedor
        public List<EProveedorDatosPrimarios> GetByNombreProveedor(string nombreP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorDatosPrimarios> PLista = new List<EProveedorDatosPrimarios>();
                const string QueryGetByNombre = "EXEC AGROCatalogoProveedoresSP_GetAllDatosPrimariosByNombreProveedor @NombreProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByNombre, conn))
                {
                    cmd.Parameters.AddWithValue("@NombreProveedor", nombreP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorDatosPrimarios P = new EProveedorDatosPrimarios
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            NombreProveedor = Convert.ToString(reader["NombreProveedor"]),
                            RFC = Convert.ToString(reader["RFC"]),
                            Categoria = Convert.ToString(reader["Categoria"]),
                            PATHImagen = Convert.ToString(reader["PATHImagen"]),
                            hasImagen = Convert.ToBoolean(reader["hasImagen"]),
                            fechaUltimaActualizacion = Convert.ToDateTime(reader["UltimaActualizacion"]),
                            
                        };
                        PLista.Add(P);
                    }
                    return PLista;
                }
                return null;
            }
        }

        public void editarRevision(string claveProveedor, string seccion, bool revisado)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string Query = @"EXEC AGROCatalogoProveedoresSP_EditarRevisionesByClaveProveedor @ClaveProveedor,
                                    @isDireccionesRevisado, @isContactosRevisado, @isDatosBancariosMXRevisado, 
                                    @isDatosBancariosEXRevisado, @isAcuerdosRevisado, @isCondicionesRevisado,
                                    @isExpedienteRevisado, @isPoliticasRevisado";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);

                    if(seccion == "Dirección")
                    {
                        cmd.Parameters.AddWithValue("@isDireccionesRevisado", revisado);
                        cmd.Parameters.AddWithValue("@isContactosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosMXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosEXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isAcuerdosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isCondicionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isExpedienteRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isPoliticasRevisado", DBNull.Value);
                    }                        
                    else if(seccion == "Contacto")
                    {
                        cmd.Parameters.AddWithValue("@isDireccionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isContactosRevisado", revisado);
                        cmd.Parameters.AddWithValue("@isDatosBancariosMXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosEXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isAcuerdosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isCondicionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isExpedienteRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isPoliticasRevisado", DBNull.Value);
                    }
                    else if (seccion == "Cuenta Bancaria MX")
                    {
                        cmd.Parameters.AddWithValue("@isDireccionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isContactosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosMXRevisado", revisado);
                        cmd.Parameters.AddWithValue("@isDatosBancariosEXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isAcuerdosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isCondicionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isExpedienteRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isPoliticasRevisado", DBNull.Value);
                    }
                        
                    else if (seccion == "Cuenta Bancaria EX")
                    {
                        cmd.Parameters.AddWithValue("@isDireccionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isContactosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosMXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosEXRevisado", revisado);
                        cmd.Parameters.AddWithValue("@isAcuerdosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isCondicionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isExpedienteRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isPoliticasRevisado", DBNull.Value);
                    }
                    else if (seccion == "Acuerdos")
                    {
                        cmd.Parameters.AddWithValue("@isDireccionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isContactosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosMXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosEXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isAcuerdosRevisado", revisado);
                        cmd.Parameters.AddWithValue("@isCondicionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isExpedienteRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isPoliticasRevisado", DBNull.Value);
                    }
                    else if (seccion == "Condiciones")
                    {
                        cmd.Parameters.AddWithValue("@isDireccionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isContactosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosMXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosEXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isAcuerdosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isCondicionesRevisado", revisado);
                        cmd.Parameters.AddWithValue("@isExpedienteRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isPoliticasRevisado", DBNull.Value);
                    }
                    else if (seccion == "Expediente")
                    {
                        cmd.Parameters.AddWithValue("@isDireccionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isContactosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosMXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosEXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isAcuerdosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isCondicionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isExpedienteRevisado", revisado);
                        cmd.Parameters.AddWithValue("@isPoliticasRevisado", DBNull.Value);
                    }                        
                    else if (seccion == "Políticas")
                    {
                        cmd.Parameters.AddWithValue("@isDireccionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isContactosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosMXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isDatosBancariosEXRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isAcuerdosRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isCondicionesRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isExpedienteRevisado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@isPoliticasRevisado", revisado);
                    }
                        
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void actualizarFechaDeActualizacion(string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                DateTime dateToday = DateTime.Today;

                const string Query = @"EXEC AGROCatalogoProveedoresSP_UpdateUltimaActualizacionByClaveProveedor @ClaveProveedor,
                                    @UltimaActualizacion";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@UltimaActualizacion", dateToday);                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Obtener datos por busqueda de RFC Proveedor
        public List<EProveedorDatosPrimarios> GetByRFCProveedor(string RFC)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                List<EProveedorDatosPrimarios> PLista = new List<EProveedorDatosPrimarios>();
                const string QueryGetByNombre = "EXEC AGROCatalogoProveedoresSP_GetAllDatosPrimariosByRFC @RFC";
                using (SqlCommand cmd = new SqlCommand(QueryGetByNombre, conn))
                {
                    cmd.Parameters.AddWithValue("@RFC", RFC);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EProveedorDatosPrimarios P = new EProveedorDatosPrimarios
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            NombreProveedor = Convert.ToString(reader["NombreProveedor"]),
                            RFC = Convert.ToString(reader["RFC"]),
                            Categoria = Convert.ToString(reader["Categoria"]),
                            PATHImagen = Convert.ToString(reader["PATHImagen"]),
                            hasImagen = Convert.ToBoolean(reader["hasImagen"]),
                            fechaUltimaActualizacion = Convert.ToDateTime(reader["UltimaActualizacion"]),
                            //StringUltimaActualizacion = Convert.ToString(reader["UltimaActualizacion"])
                        };
                        PLista.Add(P);
                    }
                    return PLista;
                }
                return null;
            }
        }

        //Agregar Proveedor
        public void agregarProveedor(EProveedorDatosPrimarios P)
        {
        //    SqlCommand cmd = new SqlCommand();
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("EXEC AGROSPPROVEEDORINSERTAR @PROVEEDOR, @CALLE, @CONTACTO");
        //    cmd.Parameter.Add("@Proveedor", sqltype.varchar).value = claveProveedor;
        //    sb.Append("set @si = 0");
        //    sb.Append("if exists (select * from agrousuarios where usuario = '" + usuario + "' and contra = '" + pass + "' ) ");
        //    sb.Append("begin select @si = 1 end ");
        //    sb.Append("select @si ");
        //    cmd.CommandText = sb.ToString();
        //    return Conexiones.Conexion.Executar(cmd);
            //iniciar instancia de comando para querys de base de datos
            //iniciar instancia de ingresarQuery String ingresarQuery = "INSERT INTO ";
            //iniciar conexión
            //agregar parametros de query
            //abrir o iniciar conexión con base de datos
            //if ejecutar 
            //cerrar conexión con base de datos
        }

        //Editar Proveedor
        public void editarImagen(string PATH, string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_UpdateImagenByClaveProveedor @ClaveProveedor, @PATH, @hasImagen, @UltimaActualizacion";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    cmd.Parameters.AddWithValue("@PATH", PATH);
                    cmd.Parameters.AddWithValue("@hasImagen", 1);
                    cmd.Parameters.AddWithValue("@UltimaActualizacion", DateTime.Today);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void editarProveedor(EProveedorDatosPrimarios P)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = "UPDATE AGROCatalogoProveedores_ProveedorDatosPrimarios SET NombreProveedor = @nombreProv, RFC = @rfc, Categoria = @categoria, Imagen = @imagen, hasImagen = @hasImagenBool, UltimaActualizacion = @stringUltAct WHERE ClaveProveedor = @claveProv";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreProv", P.NombreProveedor);
                    cmd.Parameters.AddWithValue("@rfc", P.RFC);
                    cmd.Parameters.AddWithValue("@categoria", P.Categoria);
                    cmd.Parameters.AddWithValue("@imagen", P.PATHImagen);
                    cmd.Parameters.AddWithValue("@hasImagenBool", P.hasImagen);
                    cmd.Parameters.AddWithValue("@stringUltAct", P.fechaUltimaActualizacion);
                    cmd.Parameters.AddWithValue("@claveProv", P.ClaveProveedor);

                    cmd.ExecuteNonQuery();
                }
            }

            //iniciar instancia de comando para querys de base de datos
            //iniciar instancia de ingresarQuery String ingresarQuery = "INSERT INTO ";
            //iniciar conexión
            //agregar parametros de query
            //abrir o iniciar conexión con base de datos
            //if ejecutar 
            //cerrar conexión con base de datos
        }
        //Eliminar Proveedor
        public void eliminarProveedor(String nombreProveedor, String claveProveedor, String RFCProveedor, String categoriaProveedor)
        {
            //iniciar instancia de comando para querys de base de datos
            //iniciar instancia de ingresarQuery String ingresarQuery = "INSERT INTO ";
            //iniciar conexión
            //agregar parametros de query
            //abrir o iniciar conexión con base de datos
            //if ejecutar 
            //cerrar conexión con base de datos
        }
        //Obtener

        //Obtener lista de Proveedores
        public String getListaProveedor()
        {
            //iniciar instancia de comando para querys de base de datos
            //iniciar instancia de ingresarQuery String ingresarQuery = "INSERT INTO ";
            //iniciar conexión
            //agregar parametros de query
            //abrir o iniciar conexión con base de datos
            //if ejecutar 
            //cerrar conexión con base de datos
            return "";
        }
        //Obtener tabla de Proveedores
        //Obtener Ultima Actualizacion

    }
}
