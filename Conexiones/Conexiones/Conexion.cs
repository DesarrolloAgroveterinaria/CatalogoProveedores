using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Conexiones
{
    public class Conexion
    {
        private static string getConexion()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["conexionBD"].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }
        public static string ExecutarXml(string query)
        {
            SqlConnection conexion = new SqlConnection(getConexion());
            SqlCommand cmd = new SqlCommand(query, conexion);
            try
            {
                conexion.Open();
                DataSet ds = new DataSet();
                new SqlDataAdapter(cmd).Fill(ds);
                string xml = "";
                if (ds != null && ds.Tables.Count > 0)
                    xml = ds.GetXml();
                return xml;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio el siguiente problema al executar a xml: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        public static XmlDocument ExecutarAXml(SqlCommand cmd)
        {
            SqlConnection conexion = new SqlConnection(getConexion());
            cmd.Connection = conexion;
            try
            {
                conexion.Open();
                XmlReader reader = cmd.ExecuteXmlReader();
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(reader);
                return xdoc;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio el siguiente problema al executar a xml: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        public static string Executar(SqlCommand cmd)
        {
            SqlConnection conexion = new SqlConnection(getConexion());
            cmd.Connection = conexion;
            try
            {
                conexion.Open();
                DataSet ds = new DataSet();
                new SqlDataAdapter(cmd).Fill(ds);
                if (ds.Tables.Count == 0)
                    return "";
                DataTable table = ds.Tables[0];
                return table.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio el siguiente problema al executar la consulta: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        public static DataTable ExecutarADataTable(SqlCommand cmd)
        {
            SqlConnection conexion = new SqlConnection(getConexion());
            cmd.Connection = conexion;
            try
            {
                conexion.Open();
                DataSet ds = new DataSet();
                new SqlDataAdapter(cmd).Fill(ds);
                if (ds.Tables.Count == 0)
                    return new DataTable();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio el siguiente problema al executar la consulta: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        //public static XmlDocument ExecutarAXml(string query)
        //{
        //    SqlConnection conexion = new SqlConnection(getConexion());
        //    SqlCommand cmd = new SqlCommand(query, conexion);
        //    try
        //    {
        //        conexion.Open();
        //        XmlReader reader = cmd.ExecuteXmlReader();
        //        XmlDocument xdoc = new XmlDocument();
        //        xdoc.Load(reader);
        //        return xdoc;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ocurrio el siguiente problema al executar a xml: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //}

        //public static string Executar(string query)
        //{
        //    SqlConnection conexion = new SqlConnection(getConexion());
        //    SqlCommand cmd = new SqlCommand(query, conexion);
        //    try
        //    {
        //        conexion.Open();
        //        DataSet ds = new DataSet();
        //        new SqlDataAdapter(cmd).Fill(ds);
        //        if (ds.Tables.Count == 0)
        //            return "";
        //        DataTable table = ds.Tables[0];
        //        return table.Rows[0][0].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ocurrio el siguiente problema al executar la consulta: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //}

        //public static DataTable ExecutarADataTable(string query)
        //{
        //    SqlConnection conexion = new SqlConnection(getConexion());
        //    SqlCommand cmd = new SqlCommand(query, conexion);
        //    try
        //    {
        //        conexion.Open();
        //        DataSet ds = new DataSet();
        //        new SqlDataAdapter(cmd).Fill(ds);
        //        if (ds.Tables.Count == 0)
        //            return new DataTable();
        //        return ds.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ocurrio el siguiente problema al executar la consulta: " + ex.Message);
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //}
    }
}
