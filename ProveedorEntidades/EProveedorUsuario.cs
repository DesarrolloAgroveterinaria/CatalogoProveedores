using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;


namespace ProveedorEntidades
{
    public class EProveedorUsuario
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }

        public string Contra{ get; set; }
        public string Estacion { get; set; }
        //Agregar aquí atributos de permisos de Usuario
    }
}
