using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorProvBol
    {
        private ProveedorProvDal proveedorProvDal = new ProveedorProvDal();
        //uso de stringbuilder para devolver mensajes
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();
        public EProveedorProv consultarPaginaWebByClave(string claveProveedor)
        {
            mensajeRespuestaSP.Clear();           
            return proveedorProvDal.GetPaginaWebByClave(claveProveedor);
        }

        public void actualizarPaginaWeb(string claveProveedor, string dirPaginaWeb)
        {
            mensajeRespuestaSP.Clear();
            proveedorProvDal.actualizarPaginaWebByClave(claveProveedor, dirPaginaWeb);
        }
    }
}
