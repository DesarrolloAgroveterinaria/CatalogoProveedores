using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorExpediente
    {
        public string ClaveProveedor { get; set; }
        public int Expedienteid { get; set; }
        public bool hasContratoFile { get; set; }
        public bool hasPRLFile { get; set; }
        public bool hasIRLFile { get; set; }
        public bool hasCompDomicilioFile { get; set; }
        public bool hasCedulaRFCFile { get; set; }
        public bool hasCaratulaEdoCuentaFile { get; set; }
        public bool hasAvisoPrivacidadFile { get; set; }
        public bool hasPagareFile { get; set; }
    }
}
