using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorPlazoCredito
    {
        public string ClaveProveedor { get; set; }
        public int Plazoid { get; set; }
        public int PrioridadDeUso { get; set; }        
        public bool EstatusActivo { get; set; }
        public bool Revisado { get; set; }
        public string PlazoCreditoDias { get; set; }
        public string DefinicionPlazo { get; set; }
        public string ProntoPago1Dias { get; set; }
        public int ProntoPago1Descuento { get; set; }
        public string VencimientoPagoFactura1 { get; set; }
        public string ObservacionesPP1 { get; set; }
        public string ProntoPago2Dias { get; set; }
        public int ProntoPago2Descuento { get; set; }
        public string VencimientoPagoFactura2 { get; set; }
        public string ObservacionesPP2 { get; set; }
        public string ProntoPago3Dias { get; set; }
        public int ProntoPago3Descuento { get; set; }
        public string VencimientoPagoFactura3 { get; set; }
        public string ObservacionesPP3 { get; set; }
        public string ProntoPago4Dias { get; set; }
        public int ProntoPago4Descuento { get; set; }
        public string VencimientoPagoFactura4 { get; set; }
        public string ObservacionesPP4 { get; set; }
        public string ProntoPago5Dias { get; set; }
        public int ProntoPago5Descuento { get; set; }
        public string VencimientoPagoFactura5 { get; set; }
        public string ObservacionesPP5 { get; set; }
        public string ObservacionesGenerales { get; set; }
    }
}
