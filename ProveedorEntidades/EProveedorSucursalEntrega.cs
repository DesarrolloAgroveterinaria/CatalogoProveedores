using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorSucursalEntrega
    {
        public string ClaveProveedor { get; set; }
        public int Condicionesid { get; set; }
        public string Sucursal { get; set; }
        public bool DisponibleParaEntrega { get; set; }
    }
}
