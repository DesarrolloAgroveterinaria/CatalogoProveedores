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

    }
}
