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
        public bool isDireccionesRevisado { get; set; }
        public bool isContactosRevisado { get; set; }
        public bool isDatosBancariosMXRevisado { get; set; }
        public bool isDatosBancariosEXRevisado { get; set; }
        public bool isAcuerdosRevisado { get; set; }
        public bool isCondicionesRevisado { get; set; }
        public bool isPoliticasRevisado { get; set; }
        public bool isExpedienteRevisado { get; set; }

    }
}
