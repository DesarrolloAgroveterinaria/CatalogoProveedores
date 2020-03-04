using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorCostoFleteLimiteCapacidadBol
    {
        public ProveedorCostoFleteLimiteCapacidadDal proveedorCostoFleteLimiteCapacidadDal = new ProveedorCostoFleteLimiteCapacidadDal();
        private readonly StringBuilder mensajeRespuestaSP = new StringBuilder();

        public List<EProveedorCostoFleteLimiteCapacidad> consultarCostoFleteLimiteCapacidadByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            return proveedorCostoFleteLimiteCapacidadDal.GetByClave(claveProv);
        }
    }
}
