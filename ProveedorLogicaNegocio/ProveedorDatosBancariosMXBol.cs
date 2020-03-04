using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;

namespace ProveedorLogicaNegocio
{
    public class ProveedorDatosBancariosMXBol
    {
        private ProveedorDatosBancariosMXDal proveedorDatosBancariosMXDal = new ProveedorDatosBancariosMXDal();
        //uso de stringbuilder para devolver mensajes
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();
        //Consultar datos Proveedor Datos Primarios por Clave
        public List<EProveedorDatosBancariosMX> consultarDatosBancariosMXByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            return proveedorDatosBancariosMXDal.GetByClave(claveProv);
        }

        public bool agregarCuenta(EProveedorDatosBancariosMX cuentaMX)
        {
            mensajeRespuestaSP.Clear();
            List<EProveedorDatosBancariosMX> ListaCuentas = consultarDatosBancariosMXByClaveProveedorVal(cuentaMX.ClaveProveedor);

            if (ListaCuentas.Count > 0)
            {
                foreach (var i in ListaCuentas)
                {
                    if (cuentaMX.CLABE == i.CLABE && cuentaMX.NumeroCuentaDestinatario == i.NumeroCuentaDestinatario)
                    {
                        mensajeRespuestaSP.Append("La Cuenta Bancaria ingresada ya existe.");
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append("Si deseas actualizar la siguiente Cuenta presiona el bóton Editar: ");
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append("CLABE: " + i.CLABE);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append("Número de Cuenta: " + i.NumeroCuentaDestinatario);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        return false;
                    }
                }
            }
            proveedorDatosBancariosMXDal.AgregarByClave(cuentaMX);
            return true;
        }

        public void esPreferenteCuentaByIdByClaveProveedorVal(int BancoMXid, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorDatosBancariosMXDal.esPreferenteByIdByClave(BancoMXid, claveProveedor);
        }

        public void desactivarCuentaByIdByClaveProveedorVal(int BancoMXid, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorDatosBancariosMXDal.DesactivarByIdByClave(BancoMXid, claveProveedor);
        }

        public bool editarCuentaByIdByClave(EProveedorDatosBancariosMX cuenta)
        {
            mensajeRespuestaSP.Clear();
            proveedorDatosBancariosMXDal.EditarByIdByClave(cuenta);
            return true;
        }
    }
}
