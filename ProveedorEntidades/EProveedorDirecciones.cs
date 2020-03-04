using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorDirecciones
    {
        public string ClaveProveedor { get; set; }
        public int PrioridadDeUso { get; set; }
        public int NumIdDireccion { get; set; }
        public string ConceptoUso { get; set; }
        public string CalleAveBlvr { get; set; }
        public string NumExterior { get; set; }
        public string NumInterior { get; set; }
        public string InfAdicional { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Poblacion { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public bool EstatusActivo { get; set; }
    }
}
