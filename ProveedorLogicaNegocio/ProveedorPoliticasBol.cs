using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorPoliticasBol
    {
        private ProveedorPoliticasDal proveedorPoliticasDal = new ProveedorPoliticasDal();
        //uso de stringbuilder para devolver mensajes
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();
        public EProveedorPoliticas consultarPoliticasByClaveProveedorVal(string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            return proveedorPoliticasDal.GetByClave(claveProveedor);
        }

        public void actualizarPoliticas(EProveedorPoliticas politicas)
        {
            if (consultarPoliticasByClaveProveedorVal(politicas.ClaveProveedor) == null)
                proveedorPoliticasDal.agregarPoliticas(politicas);
            else
                proveedorPoliticasDal.editarPoliticas(politicas);
        }
    }
}
