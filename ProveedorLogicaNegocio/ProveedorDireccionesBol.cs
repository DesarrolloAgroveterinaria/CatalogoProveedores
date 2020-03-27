using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorDireccionesBol
    {
        private ProveedorDireccionesDal proveedorDireccionesDal = new ProveedorDireccionesDal();
        //uso de stringbuilder para devolver mensajes
        public StringBuilder mensajeRespuestaSP = new StringBuilder();
        //Consultar datos Proveedor Datos Primarios por Clave
        public List<EProveedorDirecciones> consultarDireccionesByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            return proveedorDireccionesDal.GetByClave(claveProv);
        }
        public EProveedorDirecciones consultarDireccionesByClaveProveedorByIdVal(string claveProveedor, int id)
        {
            mensajeRespuestaSP.Clear();
             return proveedorDireccionesDal.GetByClaveById(claveProveedor, id);
        }

        //Agregar direccion de Proveedor
        public bool agregarDireccionProveedor(EProveedorDirecciones Direccion)
        {
            mensajeRespuestaSP.Clear();
            List<EProveedorDirecciones> ListaDirecciones = consultarDireccionesByClaveProveedorVal(Direccion.ClaveProveedor);

            if (ListaDirecciones.Count > 0)
            {
                foreach (var i in ListaDirecciones)
                {
                    if (Direccion.CalleAveBlvr == i.CalleAveBlvr && Direccion.NumExterior == i.NumExterior && Direccion.NumInterior == i.NumInterior
                    && Direccion.CodigoPostal == i.CodigoPostal && i.EstatusActivo)
                    {
                        mensajeRespuestaSP.Append("La Dirección ingresada ya existe.");
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append("Si deseas actualizar la siguiente Dirección presiona el bóton Editar: ");
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(i.ConceptoUso);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(i.CalleAveBlvr + " #" + (i.NumExterior == "" ? i.NumInterior + ", " : i.NumExterior + " " + i.NumInterior + ", "));
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(i.InfAdicional);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(i.Colonia + ", " + i.CodigoPostal);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        //mensajeRespuestaSP.Append("... a la siguiente Dirección?");
                        //mensajeRespuestaSP.Append(System.Environment.NewLine);
                        //mensajeRespuestaSP.Append(Direccion.ConceptoUso);
                        //mensajeRespuestaSP.Append(Direccion.CalleAveBlvr + System.Environment.NewLine);
                        //mensajeRespuestaSP.Append(Direccion.NumExterior + System.Environment.NewLine);
                        //mensajeRespuestaSP.Append(Direccion.NumInterior + System.Environment.NewLine);
                        //mensajeRespuestaSP.Append(Direccion.InfAdicional + System.Environment.NewLine);
                        //mensajeRespuestaSP.Append(Direccion.Colonia + System.Environment.NewLine);
                        //mensajeRespuestaSP.Append(Direccion.CodigoPostal + System.Environment.NewLine);
                        //mensajeRespuestaSP.Append(Direccion.Poblacion + ", " + Direccion.Estado + ", " + Direccion.Pais);
                        return false;
                    }
                }
            }
            proveedorDireccionesDal.AgregarByClave(Direccion);
            return true;
        }
        public bool agregarDireccionDatosBancariosEXProveedor(EProveedorDirecciones Direccion)
        {
            proveedorDireccionesDal.AgregarByClave(Direccion);
            return true;
        }
        //Editar direccion de Proveedor

        //Eliminar direccion de Proveedor
        public bool eliminarDireccionProveedor(String direccion, String colonia, String infAdicional, String codigoPostal, String poblacion, String estado)
        {
            //iniciar instancia de comando para querys de base de datos
            //iniciar instancia de ingresarQuery String ingresarQuery = "DELETE ";
            //iniciar conexión
            //agregar parametros de query
            //abrir o iniciar conexión con base de datos
            //if ejecutar 
            //cerrar conexión con base de datos
            return true;
        }
        //Obtener lista de Direcciones Proveedores
        public String getListaDireccionProveedor()
        {
            //iniciar instancia de comando para querys de base de datos
            //iniciar instancia de ingresarQuery String ingresarQuery = "SELECT * FROM  ";
            //iniciar conexión
            //agregar parametros de query
            //abrir o iniciar conexión con base de datos
            //if ejecutar 
            //cerrar conexión con base de datos
            return "";
        }

        public void desactivarDireccionesByIdByClaveProveedorVal(int numIdDireccion, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorDireccionesDal.DesactivarByIdByClave(numIdDireccion, claveProveedor);
        }

        public void priorizarDireccionesByIdByClaveProveedorVal(int numIdDireccion, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorDireccionesDal.PriorizarByIdByClave(numIdDireccion, claveProveedor);
        }

        public bool editarDireccionesByIdByClaveProveedorVal(EProveedorDirecciones Direccion)
        {
            mensajeRespuestaSP.Clear();
            proveedorDireccionesDal.EditarByIdByClave(Direccion);
            return true;
        }

        public int consultarMaxId(string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            return proveedorDireccionesDal.ConsultarMaxId(claveProveedor);
        }

        public void marcarDireccionRevisada(string claveProveedor, int id, string seccionDato, bool revisado)
        {
            mensajeRespuestaSP.Clear();
            proveedorDireccionesDal.marcarDireccionRevisada(claveProveedor, id, seccionDato, revisado);
        }
    }
}
