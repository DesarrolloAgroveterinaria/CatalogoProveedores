using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;


namespace ProveedorEntidades
{
    public class EProveedorDatosBancariosEX
    {
        public string ClaveProveedor { get; set; }
        public int PrioridadDeUso { get; set; }
        public int BancoEXid { get; set; }
        public string NombreBancoDestino { get; set; }
        public string ClaveBancoDestino { get; set; }
        public string NombreDestinatario { get; set; }
        public string NumeroCuentaDestinatario { get; set; }
        public string DivisaAPagar { get; set; }
        public string MontoMaximoAPagar { get; set; }
        public string NombreBancoIntermediario { get; set; }
        public string ClaveBancoIntermediario { get; set; }
        public int NumIdDireccionDestinatario { get; set; }
        public bool Vigencia { get; set; }
        public DateTime FechaDeVigencia { get; set; }
        public string TipoRelacionConDestinatario { get; set; }
        public string MotivoPago { get; set; }
        public bool EsPreferencia { get; set; }
        public bool EstatusActivo { get; set; }
        public bool Revisado { get; set; }

    }
}
