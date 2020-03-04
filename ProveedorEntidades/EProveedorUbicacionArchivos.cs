using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorUbicacionArchivos
    {
        public string ClaveProveedor { get; set; }
        public int UbicacionArchivoid { get; set; }
        public string CategoriaArchivo { get; set; }
        public string PATHArchivo { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime FechaVigencia { get; set; }
        public bool EstatusActivo { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
