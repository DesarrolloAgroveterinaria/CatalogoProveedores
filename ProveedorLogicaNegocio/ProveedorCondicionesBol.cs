using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorCondicionesBol
    {
        private ProveedorCondicionesDal proveedorCondicionesDal = new ProveedorCondicionesDal();
        private StringBuilder mensajeRespuestaSP = new StringBuilder();
        public EProveedorCondiciones consultarCondicionesByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            return proveedorCondicionesDal.GetByClave(claveProv);
        }
        public void actualizarCondiciones(EProveedorCondiciones condiciones)
        {
            if (consultarCondicionesByClaveProveedorVal(condiciones.ClaveProveedor) == null)
                proveedorCondicionesDal.agregarCondiciones(condiciones);
            else
                proveedorCondicionesDal.editarCondiciones(condiciones);
        }
    }
}
