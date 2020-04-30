using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class AgroVersionesBol
    {
        private AgroVersionesDal proveedorVersionesDal = new AgroVersionesDal();
        public EAgroVersiones getAppVersion(string app)
        {
            return proveedorVersionesDal.getAppVersion(app);
        }
    }
}
