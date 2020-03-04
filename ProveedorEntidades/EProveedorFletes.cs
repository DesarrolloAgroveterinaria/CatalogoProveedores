using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ProveedorEntidades
{
    public class EProveedorFletes
    {
        public string ClaveProveedor { get; set; }
        public int Fleteid { get; set; }
        public string FormaEntrega { get; set; }
        public string NombreContacto { get; set; }
        public string NombreDescripcion { get; set; }
        public string Puesto { get; set; }
        public string Telefono1 { get; set; }
        public string ExtTelefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string ExtTelefono2 { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int PedidoMin { get; set; }
        public int PedidoMax { get; set; }
        public string Unidad { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Observaciones { get; set; }
        public int CostoFleteid { get; set; }

    }
}
