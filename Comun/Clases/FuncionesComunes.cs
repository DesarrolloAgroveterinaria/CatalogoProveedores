using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Comun.Clases
{
    public class FuncionesComunes
    {
        /// <summary>
        /// Deserealiza un Xml en un objeto indicado Ejemplo. Deserializar(typeof(Objeto), XML) AS Objeto
        /// </summary>
        /// <param name="Tipo">Tipo de objeto que se desea deserealizar</param>
        /// <param name="xdoc">Documento Xml que contiene los datos del objeto</param>
        /// <returns>Obtiene un objeto deserealizado de un xml</returns>
        public static object Deserializar(Type Tipo, XmlDocument xdoc)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(Tipo);
                XmlReader xml = XmlReader.Create(new StringReader(xdoc.OuterXml));
                object objeto = serializer.Deserialize(xml);
                xml.Close();
                return objeto;
            }
            catch(Exception ex)
            {
                return null;
            }      
        }

        /// <summary>
        /// Serializa un objeto a xml
        /// </summary>
        /// <param name="tipo">Tipo del objeto que se quiere serializar</param>
        /// <param name="factura">Objeto que se quiere serializar</param>
        /// <returns></returns>
        public static XmlDocument Serializar(Type tipo, object factura)
        {
            try
            {
                var xmlserializer = new XmlSerializer(tipo);
                var stringWriter = new StringWriter();
                XmlDocument xdoc = new XmlDocument();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, factura);
                    xdoc.LoadXml( stringWriter.ToString());
                }
                return xdoc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// regresa la fecha con hora segun la fecha que se indico
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string horaActual(DateTime fecha)
        {
            string f = fecha.Year.ToString() + fecha.Month.ToString("00") + fecha.Day.ToString("00") + " " + fecha.Hour.ToString("00") + ":" + fecha.Minute.ToString("00");
            return f;
        }

        /// <summary>
        /// regresa la fecha con hora inicial segun la fecha que se indico
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string horaInicial(DateTime fecha)
        {
            string f = fecha.Year.ToString() + fecha.Month.ToString("00") + fecha.Day.ToString("00") + " " + "00:00";
            return f;
        }

        /// <summary>
        /// regresa la fecha con hora final segun la fecha que se indico
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string horaFinal(DateTime fecha)
        {
            string f = fecha.Year.ToString() + fecha.Month.ToString("00") + fecha.Day.ToString("00") + " " + "23:59";
            return f;
        }

        /// <summary>
        /// Crea una lista de items por medio de un DataTable, Donde la primer columna es la referencia del Item
        /// </summary>
        /// <param name="tabla"></param>
        /// <returns>ListViewItem[]</returns>
        public static ListViewItem[] dataTableAListViewItem(DataTable tabla)
        {
            ListViewItem[] lista = new ListViewItem[tabla.Rows.Count];
            int i = 0;
            foreach (DataRow r in tabla.Rows)
            {
                foreach (DataColumn c in tabla.Columns)
                {
                    if (lista[i] == null)
                    {
                        ListViewItem item = new ListViewItem(r[0].ToString());
                        item.Name = c.ColumnName;
                        lista[i] = item;
                    }
                    else
                    {
                        ListViewItem.ListViewSubItem item = new ListViewItem.ListViewSubItem();
                        item.Name = c.ColumnName;
                        item.Text = r[c.ColumnName.ToString()].ToString();
                        lista[i].SubItems.Add(item);
                    }
                }
                i++;
            }
            return lista;
        }

        /// <summary>
        /// Se agrega la funcion de transaccion a un query
        /// </summary>
        /// <param name="Nombre">Nombre de la transaccion</param>
        /// <param name="Query">Consulta a la base de datos</param>
        /// <returns></returns>
        public static string Transaccion(string Nombre, string Query)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BEGIN TRANSACTION "+ Nombre + " ");
            sb.Append("BEGIN TRY ");
            sb.Append(Query);
            sb.Append(" COMMIT TRANSACTION " + Nombre + " ");
            sb.Append("END TRY ");
            sb.Append("BEGIN CATCH ");
            sb.Append("ROLLBACK TRANSACTION " + Nombre + " ");
            sb.Append("DECLARE @ErrorMessage NVARCHAR(4000) ");
            sb.Append("DECLARE @ErrorSeverity INT ");
            sb.Append("DECLARE @ErrorState INT ");
            sb.Append("SET @ErrorMessage = ERROR_MESSAGE() ");
            sb.Append("SET @ErrorSeverity = ERROR_SEVERITY() ");
            sb.Append("SET @ErrorState = ERROR_STATE()");
            sb.Append("RAISERROR (@ErrorMessage, @ErrorSeverity,  @ErrorState) ");
            sb.Append("END CATCH");
            return sb.ToString();
        }

        /// <summary>
        /// Obtiene el valor para la configuracion solicitada
        /// </summary>
        /// <param name="configuracion">Nombre de la configuracion de la cual se busca el valor</param>
        /// <returns></returns>
        public static string getConfiguracion(string configuracion)
        {
            try
            {
                return ConfigurationManager.AppSettings[configuracion].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Agrega un nuevo nodo al app.config de la aplicacion del apartado appSettings
        /// </summary>
        /// <param name="nombre">Nombre del nodo</param>
        /// <param name="valor">Valor del nodo</param>
        public static void agregarAppSetting(string nombre, string valor)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Add(nombre, valor);
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// Modifica un nodo del app.config de la aplicacio del apartado appSettings
        /// </summary>
        /// <param name="nombre">Nombre del nodo</param>
        /// <param name="valor">Valor del nodo</param>
        public static void modificarAppSetting(string nombre, string valor)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[nombre].Value = valor;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings"); 
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Obtiene la impresora predeterminada de la computadora
        /// </summary>
        /// <returns></returns>
        public static string getImpresoraPorDefecto()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return string.Empty;
        }

        /// <summary>
        /// Obtiene el valor del archivo de configuracion con el nombre indicado, si no existe lo crea
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static string getAppSetting(string nombre)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[nombre] == null)
                    agregarAppSetting(nombre, "");
                return config.AppSettings.Settings[nombre].Value;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
