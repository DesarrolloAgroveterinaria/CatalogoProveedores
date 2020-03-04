using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;


namespace ProveedorEntidades
{
    public class EProveedorDatosBancariosMX
    {
        public string ClaveProveedor { get; set; }
        public int BancoMXid { get; set; }
        public int PrioridadDeUso { get; set; }
        public string NombreBancoDestino { get; set; }
        public string CLABE { get; set; }
        public string NumeroCuentaDestinatario { get; set; }
        public string Sucursal { get; set; }
        public string DivisaAPagar { get; set; }
        public bool EsPreferencia { get; set; }
        public bool EstatusActivo { get; set; }
    }
}
