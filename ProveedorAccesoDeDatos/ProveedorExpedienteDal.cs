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
    public class ProveedorExpedienteDal
    {
        //Obtener datos por busqueda de Clave
        public EProveedorExpediente GetByClave(string claveP)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetExpedienteByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveP);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EProveedorExpediente E = new EProveedorExpediente
                        {
                            ClaveProveedor = Convert.ToString(reader["ClaveProveedor"]),
                            Expedienteid = Convert.ToInt32(reader["Expedienteid"]),
                            hasContratoFile = Convert.ToBoolean(reader["hasContratoFile"]),
                            hasPRLFile = Convert.ToBoolean(reader["hasPRLFile"]),
                            hasIRLFile = Convert.ToBoolean(reader["hasIRLFile"]),
                            hasCompDomicilioFile = Convert.ToBoolean(reader["hasCompDomicilioFile"]),
                            hasCedulaRFCFile = Convert.ToBoolean(reader["hasCedulaRFCFile"]),
                            hasCaratulaEdoCuentaFile = Convert.ToBoolean(reader["hasCaratulaEdoCuentaFile"]),
                            hasAvisoPrivacidadFile = Convert.ToBoolean(reader["hasAvisoPrivacidadFile"]),
                            hasPagareFile = Convert.ToBoolean(reader["hasPagareFile"]),

                        };
                        return E;
                    }
                }
            }
            return null;
        }
    }
}
