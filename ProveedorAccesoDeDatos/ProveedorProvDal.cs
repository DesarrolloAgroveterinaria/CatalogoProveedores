﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ProveedorEntidades;

namespace ProveedorAccesoDeDatos
{
    public class ProveedorProvDal
    {   
        public EProveedorProv GetPaginaWebByClave(string claveProveedor)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();

                const string QueryGetByClave = "EXEC AGROCatalogoProveedoresSP_GetDirInternetProvByClaveProveedor @ClaveProveedor";
                using (SqlCommand cmd = new SqlCommand(QueryGetByClave, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        EProveedorProv P = new EProveedorProv
                        {
                            DirInternet = reader["DirInternet"] == DBNull.Value ? "" : Convert.ToString(reader["DirInternet"])
                        };
                        return P;
                    }
                }
                return null;
            }            
        }

        public void actualizarPaginaWebByClave(string claveProveedor, string dirPaginaWeb)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionBD"].ToString()))
            {
                conn.Open();
                DateTime dateToday = DateTime.Today;

                const string Query = @"EXEC AGROCatalogoProveedoresSP_UpdatePaginaWebByClaveProveedor @ClaveProveedor,
                                    @DirInternet";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClaveProveedor", claveProveedor);
                    cmd.Parameters.AddWithValue("@DirInternet", dirPaginaWeb);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
