using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProveedorPrueba
{
    /*
     * Esta clase conecta la aplicación con la base de datos 
     * ConexionSQLServer funciones:
     * Crea conexión con SQL Server
     * Obtener conexión
     * Abrir conexión 
     * Cerrar conexión
     * ***Es necesario agregar el conector de SQLServer***
     */
    class ConexionSQLServer
    {
        //Es una instancia privada porque ninguna otra clase debe acceder directamente a la información de la conexión por los nombres de usuario y contraseña
        private SqlConnection connection = new SqlConnection("Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password");
        
        //Regresa la conexión establecida
        public SqlConnection getConnection()
        {
            return connection;
        }

        //Abre e inicializa la conexión establecida
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        //Termina la conexión establecida
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
