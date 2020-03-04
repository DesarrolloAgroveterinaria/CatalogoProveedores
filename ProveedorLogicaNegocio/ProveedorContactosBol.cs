using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorContactosBol
    {
        private ProveedorContactosDal proveedorContactosDal = new ProveedorContactosDal();
        //uso de stringbuilder para devolver mensajes
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();
        //Consultar datos Proveedor Datos Primarios por Clave
        public List<EProveedorContacto> consultarContactosByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            return proveedorContactosDal.GetByClave(claveProv);
        }

        public bool agregarContacto(EProveedorContacto Contacto)
        {
            mensajeRespuestaSP.Clear();
            List<EProveedorContacto> ListaContactos = consultarContactosByClaveProveedorVal(Contacto.ClaveProveedor);

            if (ListaContactos.Count > 0)
            {
                foreach (var i in ListaContactos)
                {
                    if (Contacto.NombreCompleto == i.NombreCompleto || Contacto.NombreCompleto.Contains(i.NombreCompleto)
                        || i.NombreCompleto.Contains(Contacto.NombreCompleto))
                    {
                        mensajeRespuestaSP.Append("El Contacto ya existe.");
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append("Si deseas actualizar el siguiente Contacto presiona el bóton Editar: ");
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(i.NombreCompleto);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(i.Puesto);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(i.Categoria);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(i.TelefonoPrimario + ", " + i.Email1);
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
            proveedorContactosDal.AgregarByClave(Contacto);
            return true;         
        }

        public void priorizarContactoByIdByClaveProveedorVal(int contactoid, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorContactosDal.PriorizarByIdByClave(contactoid, claveProveedor);
        }

        public void desactivarContactoByIdByClaveProveedorVal(int contactoid, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorContactosDal.DesactivarByIdByClave(contactoid, claveProveedor);
        }
        
        public bool editarContactoByIdByClave(EProveedorContacto contacto)
        {
            mensajeRespuestaSP.Clear();
            proveedorContactosDal.EditarByIdByClave(contacto);
            return true;
        }
    }
}
