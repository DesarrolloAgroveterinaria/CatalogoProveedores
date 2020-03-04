using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorCategoriaPoliticasCompraBol
    {
        private readonly StringBuilder mensajeRespuestaSP = new StringBuilder();
        private ProveedorCategoriaPoliticasCompraDal proveedorCategoriaPoliticasCompraDal = new ProveedorCategoriaPoliticasCompraDal();
        public List<EProveedorCategoriaPoliticasCompra> consultarCategoriaPoliticasCompraByClaveProveedorVal(string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            return proveedorCategoriaPoliticasCompraDal.GetByClave(claveProveedor);
        }
    }

}
