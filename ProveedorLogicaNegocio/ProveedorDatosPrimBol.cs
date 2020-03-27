using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorDatosPrimBol
    {
        private ProveedorDatosPrimDal proveedorDatosPrimDal = new ProveedorDatosPrimDal();
        protected string rootPath = "C:\\Users\\Soporte2\\Desktop\\Catalogo_De_Proveedores_Recursos\\";

        //uso de stringbuilder para devolver mensajes
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();

        //Métodos CRUD
        //Create (Agregar)
        public bool editarImagenDatosPrimVal(string PATH, string ClaveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorDatosPrimDal.editarImagen(PATH, ClaveProveedor);
            return true;            
        }
        //Agregar aquí validación de ubicación de imagen
        //Read (Consultar)
        //Consultar datos Proveedor Datos Primarios por Clave
        public EProveedorDatosPrimarios consultarProveedorDatosPrimByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            if (claveProv == "")
            {
                mensajeRespuestaSP.Append("Clave Proveedor inválida. Por favor proporcione una Clave de Proveedor válida.");
                return null;
            }
            return proveedorDatosPrimDal.GetByClave(claveProv);
  
        }

        //Consultar datos Proveedor Datos Primarios por NombreProveedor
        public List<EProveedorDatosPrimarios> consultarProveedorDatosPrimByNombreProveedorVal(string nombreProv)
        {
            mensajeRespuestaSP.Clear();
            if (nombreProv == "") mensajeRespuestaSP.Append("Nombre Proveedor inválido. Por favor proporcione un Nombre de Proveedor válido");
            if (mensajeRespuestaSP.Length == 0)
            {
                return proveedorDatosPrimDal.GetByNombreProveedor(nombreProv);
            }
            return null;
        }

        //Consultar datos Proveedor Datos Primarios por RFC
        public List<EProveedorDatosPrimarios> consultarProveedorDatosPrimByRFCProveedorVal(string RFCProv)
        {
            mensajeRespuestaSP.Clear();
            if (RFCProv == "") mensajeRespuestaSP.Append("RFC Proveedor inválido. Por favor proporcione un RFC de Proveedor válido");
            if (mensajeRespuestaSP.Length == 0)
            {
                return proveedorDatosPrimDal.GetByRFCProveedor(RFCProv);
            }
            return null;
        }

        //Editar Proveedor Datos Primarios 
        public void editarProveedorDatosPrimVal(EProveedorDatosPrimarios P)
        {
            if (ValidarProveedorDatosPrim(P))
            {
                if (proveedorDatosPrimDal.GetByClave(P.ClaveProveedor) == null)
                {
                    proveedorDatosPrimDal.agregarProveedor(P);
                }
                else
                    proveedorDatosPrimDal.editarProveedor(P);
            }
        }
        public void editarRevisionDirecciones(string claveProveedor, string seccion, bool revisado)
        {
            mensajeRespuestaSP.Clear();
            proveedorDatosPrimDal.editarRevision(claveProveedor, seccion, revisado);
        }
        public void actualizarUtlimaActualizacion(string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorDatosPrimDal.actualizarFechaDeActualizacion(claveProveedor);            
        }

        private bool ValidarProveedorDatosPrim(EProveedorDatosPrimarios P)
        {
            mensajeRespuestaSP.Clear();

            if (string.IsNullOrEmpty(P.NombreProveedor)) mensajeRespuestaSP.Append("* El campo de Nombre de Proveedor es obligatorio");
            if (string.IsNullOrEmpty(P.RFC)) mensajeRespuestaSP.Append(Environment.NewLine + "* El campo RFC es obligatorio");
            if (string.IsNullOrEmpty(P.Categoria)) mensajeRespuestaSP.Append(Environment.NewLine + "* El campo Categoria es obligatorio");
            if (string.IsNullOrEmpty(P.ClaveProveedor)) mensajeRespuestaSP.Append(Environment.NewLine + "* El campo Clave Proveedor es obligatorio");


            return mensajeRespuestaSP.Length == 0;
        }                

        //Obtener tabla de Proveedores
        //Obtener Ultima Actualizacion
    }
}
