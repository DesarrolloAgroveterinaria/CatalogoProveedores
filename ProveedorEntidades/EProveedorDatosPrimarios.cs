using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorDatosPrimarios
    {
        public string ClaveProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string RFC { get; set; }
        public string Categoria { get; set; }
        public string TipoProveedor { get; set; }
        public string PATHImagen { get; set; }
        public bool hasImagen { get; set; }
        public DateTime fechaUltimaActualizacion { get; set; }
    }
}
