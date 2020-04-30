using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorMovimientos
    {
        public int Movimientoid { get; set; }
        public string ClaveProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string AccionDato { get; set; }
        public string CategoriaDato { get; set; }
        public string Usuario { get; set; }
        public int idCorrespondiente { get; set; }
        public DateTime Fecha { get; set; }
    }
}
