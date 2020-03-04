using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;


namespace ProveedorEntidades
{
    public class EProveedorCostoFleteLimiteCapacidad
    {
        public string ClaveProveedor { get; set; }
        public int id { get; set; }
        public int Fleteid { get; set; }
        public string Sucursal { get; set; }
        public string Cantidad { get; set; }
        public string Observaciones { get; set; }
        public string CostoFleteOLimiteCapacidad { get; set; }

    }
}
