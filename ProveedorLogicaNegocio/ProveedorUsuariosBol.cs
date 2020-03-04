using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorUsuariosBol
    {
        private ProveedorUsuariosDal proveedorUsuariosDal = new ProveedorUsuariosDal();
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();

        public string iniciarSesion(string usuario, string contra)
        {
            mensajeRespuestaSP.Clear();
            return proveedorUsuariosDal.GetNombreByUsuarioByContra(usuario, contra);
        }

        public int CambiarContrasena(string usuario, string contra)
        {
            mensajeRespuestaSP.Clear();
            return proveedorUsuariosDal.GetValorCambiarContrasenaByUsuarioByContra(usuario, contra);
        }

        public void cambiarValorContrasena(string usuario, string contra)
        {
            mensajeRespuestaSP.Clear();
            proveedorUsuariosDal.EditarContrasenaByUsuarioByContra(usuario, contra);
        }
    }
}
