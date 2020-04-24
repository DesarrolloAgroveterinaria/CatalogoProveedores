using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorPlazoCreditoBol
    {
        private ProveedorPlazoCreditoDal proveedorPlazoCreditoDal = new ProveedorPlazoCreditoDal();
        //uso de stringbuilder para devolver mensajes
        public StringBuilder mensajeRespuestaSP = new StringBuilder();
        public List<EProveedorPlazoCredito> consultarPlazosByClaveProveedorVal(string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            return proveedorPlazoCreditoDal.GetByClave(claveProveedor);
        }

        public bool agregarPlazoProveedor(EProveedorPlazoCredito plazo)
        {
            mensajeRespuestaSP.Clear();
            proveedorPlazoCreditoDal.AgregarByClave(plazo);
            return true;
        }

        public bool editarPlazoByIdByClaveProveedorVal(EProveedorPlazoCredito plazo)
        {
            mensajeRespuestaSP.Clear();
            proveedorPlazoCreditoDal.EditarByIdByClave(plazo);
            return true;
        }

        public void desactivarPlazosCreditoByIdByClaveProveedorVal(int plazoid, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorPlazoCreditoDal.DesactivarByIdByClave(plazoid, claveProveedor);
        }
    }
}
