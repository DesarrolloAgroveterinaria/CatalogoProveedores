using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveedorAccesoDeDatos;
using ProveedorEntidades;


namespace ProveedorLogicaNegocio
{
    public class ProveedorDatosBancariosEXBol
    {
        private ProveedorDireccionesBol proveedorDireccionesBol = new ProveedorDireccionesBol();
        //private List<EProveedorDatosBancariosEX> ListaDatosBancariosEX;
        private ProveedorDatosBancariosEXDal proveedorDatosBancariosEXDal = new ProveedorDatosBancariosEXDal();
        //uso de stringbuilder para devolver mensajes
        public readonly StringBuilder mensajeRespuestaSP = new StringBuilder();
        //Consultar datos Proveedor Datos Primarios por Clave
        public List<EProveedorDatosBancariosEX> consultarDatosBancariosEXByClaveProveedorVal(string claveProv)
        {
            mensajeRespuestaSP.Clear();
            return proveedorDatosBancariosEXDal.GetByClave(claveProv);
        }

        public bool agregarCuenta(EProveedorDatosBancariosEX cuentaEX, EProveedorDirecciones direccion)
        {
            mensajeRespuestaSP.Clear();
            List<EProveedorDatosBancariosEX> ListaCuentas = consultarDatosBancariosEXByClaveProveedorVal(cuentaEX.ClaveProveedor);

            if (ListaCuentas.Count > 0)
            {
                foreach (var i in ListaCuentas)
                {
                    if (cuentaEX.ClaveBancoDestino == i.ClaveBancoDestino && cuentaEX.NumeroCuentaDestinatario == i.NumeroCuentaDestinatario)
                    {
                        mensajeRespuestaSP.Append("La Cuenta Bancaria ingresada ya existe.");
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append("Si deseas actualizar la siguiente Cuenta presiona el bóton Editar: ");
                        mensajeRespuestaSP.Append(System.Environment.NewLine);
                        mensajeRespuestaSP.Append("Clave: " + i.ClaveBancoDestino);
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
            if (proveedorDireccionesBol.agregarDireccionDatosBancariosEXProveedor(direccion))
            {
                cuentaEX.NumIdDireccionDestinatario = proveedorDireccionesBol.consultarMaxId(cuentaEX.ClaveProveedor);
                proveedorDatosBancariosEXDal.AgregarByClave(cuentaEX);
                return true;
            }
            else
                return false;                       
        }

        public bool editarCuenta(EProveedorDatosBancariosEX cuentaEX, EProveedorDirecciones direccion)
        {
            mensajeRespuestaSP.Clear();
            if (proveedorDireccionesBol.editarDireccionesByIdByClaveProveedorVal(direccion))
            {
                proveedorDatosBancariosEXDal.editarCuentaByIdByClave(cuentaEX);
                return true;
            }
            else
                return false;
        }

        public void desactivarCuentaByIdByClaveProveedorVal(int bancoEXid, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorDatosBancariosEXDal.DesactivarByIdByClave(bancoEXid, claveProveedor);
        }

        public void esPreferenteCuentaByIdByClaveProveedorVal(int bancoEXid, string claveProveedor)
        {
            mensajeRespuestaSP.Clear();
            proveedorDatosBancariosEXDal.esPreferenteByIdByClave(bancoEXid, claveProveedor);
        }
    }
}
