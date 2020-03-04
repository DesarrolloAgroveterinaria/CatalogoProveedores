using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorSucursalEntregaBol
    {
        private ProveedorSucursalEntregaDal proveedorSucursalEntregaDal = new ProveedorSucursalEntregaDal();
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();

        public List<EProveedorSucursalEntrega> consultarSucursalEntregaByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            return proveedorSucursalEntregaDal.GetByClave(claveProv);
        }
    }
}
