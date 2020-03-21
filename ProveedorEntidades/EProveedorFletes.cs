using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorFletes
    {
        public string ClaveProveedor { get; set; }
        public int Fleteid { get; set; }
        public string TipoEnvio { get; set; }
        public string FormaEntrega { get; set; }        
        public string ClaveProveedorFlete { get; set; }
        public string NombreProveedor { get; set; }
        public string DescripcionProveedor { get; set; }             
        public int PedidoMin { get; set; }
        public int PedidoMax { get; set; }
        public string Unidad { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public bool EsPreferencia { get; set; }
        public string Observaciones { get; set; }
        public string CostoFleteMatriz { get; set; }
        public string CostoFleteHipodromo { get; set; }
        public string CostoFleteSanPedro { get; set; }
        public string CostoFleteMagdalena { get; set; }
        public string CostoFleteCaborca { get; set; }
        public string CostoFleteCEDIS { get; set; }
        public string CostoFleteCMA { get; set; }
    }
}
