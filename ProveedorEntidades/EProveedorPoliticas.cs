using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorPoliticas
    {
        public string ClaveProveedor { get; set; }
        public int Politicasid { get; set; }
        public string ConvenioCompra { get; set; }
        public string PoliticasGarantia { get; set; }
        public string PoliticasDevoluciones { get; set; }
        public string CompraMinimaMensual { get; set; }
        public string RecepcionSolicitudCompra { get; set; }
        public string ObservacionesSolicitudCompra { get; set; }

    }
}
