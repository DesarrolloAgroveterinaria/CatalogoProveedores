using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorAcuerdosBol
    {
        private ProveedorAcuerdosDal proveedorAcuerdosDal = new ProveedorAcuerdosDal();
        //uso de stringbuilder para devolver mensajes
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();

        public EProveedorAcuerdos consultarAcuerdosByClaveProveedorVal(string claveP)
        {
            mensajeRespuestaSP.Clear();
            return proveedorAcuerdosDal.GetByClave(claveP);
        }

        public void actualizarAcuerdos(EProveedorAcuerdos acuerdos)
        {
            if (consultarAcuerdosByClaveProveedorVal(acuerdos.ClaveProveedor) == null)
                proveedorAcuerdosDal.agregarAcuerdos(acuerdos);
            else
                proveedorAcuerdosDal.editarAcuerdos(acuerdos);
        }
    }
}
