using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorMovimientosBol
    {
        private ProveedorMovimientosDal proveedorMovimientosDal = new ProveedorMovimientosDal();
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();
        public List<EProveedorMovimientos> consultarUltimos100Movimientos()
        {
            mensajeRespuestaSP.Clear();
            return proveedorMovimientosDal.consultarUltimos100Movimientos();
        }

        public void agregarMov(EProveedorMovimientos mov)
        {
            mensajeRespuestaSP.Clear();
            proveedorMovimientosDal.agregarMov(mov);
        }
    }
}
