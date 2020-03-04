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
        public string GetNombreByUsuarioByContra(string usuario, string contra)
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
                        string Nombre = reader.GetString(0);
                        return Nombre;
                    }                    
                }
            }
            return null;
        }

        public void EditarContrasenaByUsuarioByContra(string usuario, string contra)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                const string Query = @"EXEC AGROSPeditarContrasena @Usuario, @Pass, @CambiarContrasena";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Pass", contra);
                    cmd.Parameters.AddWithValue("@CambiarContrasena", 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int GetValorCambiarContrasenaByUsuarioByContra(string usuario, string contra)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                int CambiarContrasena = -1;
                const string QueryGetByClave = "EXEC AGROSPgetCambiarContrasena @Usuario, @Pass";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Pass", contra);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        CambiarContrasena = reader["CambiarContrasena"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CambiarContrasena"]);

                    return CambiarContrasena;
                }
            }            
        }
    }
}
