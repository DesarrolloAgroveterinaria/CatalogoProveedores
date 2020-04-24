using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorFletesBol
    {
        public ProveedorFletesDal proveedorFletesDal = new ProveedorFletesDal();
        private readonly StringBuilder mensajeRespuestaSP = new StringBuilder();
        public List<EProveedorFletes> consultarFletesByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            return proveedorFletesDal.GetByClave(claveProv);
        }

        public bool agregarFleteProveedor(EProveedorFletes flete)
        {
            mensajeRespuestaSP.Clear();            
            proveedorFletesDal.AgregarByClave(flete);
            return true;
        }

        public void desactivarFleteByIdByClaveProveedorVal(string fleteid, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorFletesDal.DesactivarByIdByClave(fleteid, claveProveedor);
        }

        public bool editarFleteByIdByClaveProveedorVal(EProveedorFletes flete)
        {
            mensajeRespuestaSP.Clear();
            proveedorFletesDal.EditarByIdByClave(flete);
            return true;
        }

        public void esPreferenteFleteByIdByClaveProveedorVal(string fleteid, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorFletesDal.MarcarPreferente(fleteid, claveProveedor);
        }
    }
}
