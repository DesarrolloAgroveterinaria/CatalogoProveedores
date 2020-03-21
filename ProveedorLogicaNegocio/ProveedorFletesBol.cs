using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorFletesBol
    {
        public ProveedorFletesDal proveedorFletesDal = new ProveedorFletesDal();
        private readonly StringBuilder mensajeRespuestaSP = new StringBuilder();
        public List<EProveedorFletes> consultarFletesByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            return proveedorFletesDal.GetByClave(claveProv);
        }

        public bool agregarFleteProveedor(EProveedorFletes flete)
        {
            mensajeRespuestaSP.Clear();
            //List<EProveedorDirecciones> ListaDirecciones = consultarDireccionesByClaveProveedorVal(Direccion.ClaveProveedor);

            //if (ListaDirecciones.Count > 0)
            //{
            //    foreach (var i in ListaDirecciones)
            //    {
            //        if (Direccion.CalleAveBlvr == i.CalleAveBlvr && Direccion.NumExterior == i.NumExterior && Direccion.NumInterior == i.NumInterior
            //        && Direccion.CodigoPostal == i.CodigoPostal && i.EstatusActivo)
            //        {
            //            mensajeRespuestaSP.Append("La Dirección ingresada ya existe.");
            //            mensajeRespuestaSP.Append(System.Environment.NewLine);
            //            mensajeRespuestaSP.Append("Si deseas actualizar la siguiente Dirección presiona el bóton Editar: ");
            //            mensajeRespuestaSP.Append(System.Environment.NewLine);
            //            mensajeRespuestaSP.Append(i.ConceptoUso);
            //            mensajeRespuestaSP.Append(System.Environment.NewLine);
            //            mensajeRespuestaSP.Append(i.CalleAveBlvr + " #" + (i.NumExterior == "" ? i.NumInterior + ", " : i.NumExterior + " " + i.NumInterior + ", "));
            //            mensajeRespuestaSP.Append(System.Environment.NewLine);
            //            mensajeRespuestaSP.Append(i.InfAdicional);
            //            mensajeRespuestaSP.Append(System.Environment.NewLine);
            //            mensajeRespuestaSP.Append(i.Colonia + ", " + i.CodigoPostal);
            //            mensajeRespuestaSP.Append(System.Environment.NewLine);
            //            mensajeRespuestaSP.Append(System.Environment.NewLine);
            //            mensajeRespuestaSP.Append(System.Environment.NewLine);
            //            //mensajeRespuestaSP.Append("... a la siguiente Dirección?");
            //            //mensajeRespuestaSP.Append(System.Environment.NewLine);
            //            //mensajeRespuestaSP.Append(Direccion.ConceptoUso);
            //            //mensajeRespuestaSP.Append(Direccion.CalleAveBlvr + System.Environment.NewLine);
            //            //mensajeRespuestaSP.Append(Direccion.NumExterior + System.Environment.NewLine);
            //            //mensajeRespuestaSP.Append(Direccion.NumInterior + System.Environment.NewLine);
            //            //mensajeRespuestaSP.Append(Direccion.InfAdicional + System.Environment.NewLine);
            //            //mensajeRespuestaSP.Append(Direccion.Colonia + System.Environment.NewLine);
            //            //mensajeRespuestaSP.Append(Direccion.CodigoPostal + System.Environment.NewLine);
            //            //mensajeRespuestaSP.Append(Direccion.Poblacion + ", " + Direccion.Estado + ", " + Direccion.Pais);
            //            return false;
            //        }
            //    }
            //}
            proveedorFletesDal.AgregarByClave(flete);
            return true;
        }
    }
}
