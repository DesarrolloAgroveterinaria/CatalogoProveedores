using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorCondiciones
    {
        public string ClaveProveedor { get; set; }
        public int Condicionesid { get; set; }
        public string CondicionesCreditoDias { get; set; }
        public bool FletePorCobrar { get; set; }
        public string ProntoPago1Dias { get; set; }
        public int ProntoPago1Descuento { get; set; }
        public string VencimientoPagoFactura1 { get; set; }
        public string ProntoPago2Dias { get; set; }
        public int ProntoPago2Descuento { get; set; }
        public string VencimientoPagoFactura2 { get; set; }
        public string ProntoPago3Dias { get; set; }
        public int ProntoPago3Descuento { get; set; }
        public string VencimientoPagoFactura3 { get; set; }
        public string ProntoPago4Dias { get; set; }
        public int ProntoPago4Descuento { get; set; }
        public string VencimientoPagoFactura4 { get; set; }
        public string ProntoPago5Dias { get; set; }
        public int ProntoPago5Descuento { get; set; }
        public string VencimientoPagoFactura5 { get; set; }
        public string TiempoEntrega { get; set; }
        public string ObservacionesTiempoEntrega { get; set; }
        public string FormaEntrega { get; set; }
        public string SucursalEntrega { get; set; }
        public string CondicionesEspecialesEntrega { get; set; }
    }
}
