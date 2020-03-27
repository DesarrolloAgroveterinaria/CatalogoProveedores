using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ClaveBancoBol
    {
        private ClaveBancoDal claveBancoDal = new ClaveBancoDal();
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();

        public List<EClaveBanco> getAllBancos()
        {
            mensajeRespuestaSP.Clear();
            return claveBancoDal.getAllBancos();
        }
    }
}
