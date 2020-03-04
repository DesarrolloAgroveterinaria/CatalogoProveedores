using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorUbicacionArchivosBol
    {
        private ProveedorUbicacionArchivosDal proveedorUbicacionArchivosDal = new ProveedorUbicacionArchivosDal();
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();

        public List<EProveedorUbicacionArchivos> consultarUbicacionArchivosByClave(string claveP)
        {
            mensajeRespuestaSP.Clear();
            return proveedorUbicacionArchivosDal.GetByClave(claveP);
        }

        public bool agregarUbicacionArchivo(string pathLocation, string claveProveedor, string categoriaDato)
        {
            mensajeRespuestaSP.Clear();
            proveedorUbicacionArchivosDal.AgregarUbicacionArchivo(pathLocation, claveProveedor, categoriaDato);
            return true;
        }

        public EProveedorUbicacionArchivos consultarUbicacionArchivosById(int id)
        {
            mensajeRespuestaSP.Clear();
            return proveedorUbicacionArchivosDal.GetById(id);
        }

        public void desactivarUbicacionArchivoByIdVal(int id)
        {
            mensajeRespuestaSP.Clear();
            proveedorUbicacionArchivosDal.DesactivarById(id);
        }
    }
}
