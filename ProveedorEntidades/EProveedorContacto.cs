using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;


namespace ProveedorEntidades
{
    public class EProveedorContacto
    {
        public string ClaveProveedor { get; set; }
        public int Contactoid { get; set; }
        public int PrioridadDeUso { get; set; }
        public string NombreCompleto { get; set; }
        public string Puesto { get; set; }
        public string Categoria { get; set; }
        public string FuncionesContacto { get; set; }
        public string TelefonoPrimario { get; set; }
        public string ExtensionTelefonoPrimario { get; set; }
        public string TelefonoSecundario { get; set; }
        public string ExtensionTelefonoSecundario { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Comentarios { get; set; }
        public bool EstatusActivo { get; set; }
    }
}
