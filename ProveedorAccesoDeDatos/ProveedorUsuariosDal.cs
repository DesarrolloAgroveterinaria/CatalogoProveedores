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
    public class ProveedorUsuariosDal
    {
        public EProveedorUsuario GetNombreByUsuarioByContra(string usuario, string contra)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                
                const string QueryGetByClave = "EXEC AGROSPgetUsuario @Usuario, @Pass";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Pass", contra);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EProveedorUsuario U = new EProveedorUsuario
                        {
                            Nombre = reader.GetString(0)
                        };
                        return U;
                    }
                    
                }
            }
            return null;
        }
    }
}
