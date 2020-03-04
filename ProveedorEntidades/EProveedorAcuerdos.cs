using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorAcuerdos
    {
        public string ClaveProveedor { get; set; }
        public int Acuerdoid { get; set; }
        public string AcuerdoCompra { get; set; }        
        public string AcuerdoVentaPublico { get; set; }
        public string AcuerdoAtencionClientes { get; set; }
    }
}
