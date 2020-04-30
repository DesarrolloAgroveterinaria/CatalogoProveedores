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
        public bool TransporteEnvioAereo { get; set; }
        public bool TransporteEnvioTerrestre { get; set; }
        public bool TipoCourier { get; set; }
        public bool TipoDomicilio { get; set; }
        public bool FormaEntregaPersonal { get; set; }
        public bool FormaPaqueteria { get; set; }
        public bool FormaTransporteCarga { get; set; }
        public bool CargoPagado { get; set; }
        public bool CargoPorCobrar { get; set; }
        public string SucursalEntrega { get; set; }
        public string TiempoEntrega { get; set; }
        public string FormaEntrega { get; set; }
        public string ObservacionesTiempoEntrega { get; set; }
        public string CondicionesEspecialesEntrega { get; set; }
    }
}
