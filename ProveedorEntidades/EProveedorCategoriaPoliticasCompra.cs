using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorCategoriaPoliticasCompra
    {
        public int Politicasid { get; set; }
        public string ClaveProveedor { get; set; }
        public string Categoria { get; set; }
        public string ImporteMensual { get; set; }
        public string ImporteTrimestral { get; set; }
        public string Beneficios { get; set; }
    }
}
