using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using ProveedorEntidades;
using ProveedorLogicaNegocio;
//using Microsoft.Live;
//using Microsoft.Live.Controls;

namespace ProveedorPrueba
{
    public partial class frmCatalogoProveedores : MetroFramework.Forms.MetroForm
    {
        public int pnlBarraSuperior_Width = 1078;
        frmNotificaciones notificaciones = new frmNotificaciones();
        //Crear instancias de Clase Entidad de todas las entidades a utilizar, así como listas para guardar datos 
        //public EProveedorUsuario eProveedorUsuario;
        private List<EProveedorUsuarioPermisos> eProveedorUsuarioPermisos;
        private List<EClaveBanco> ListaBancos;
        //private EProveedorExpediente eProveedorExpediente;
        private EProveedorDatosPrimarios eProveedorDatosPrim;
        private EProveedorProv eProveedorProv;
        private List<EProveedorDirecciones> ListaDirecciones;
        private List<EProveedorContacto> ListaContactos;
        private List<EProveedorDatosBancariosEX> ListaDatosBancariosEX;
        private EProveedorDirecciones DireccionDatosBancariosEX;
        private List<EProveedorDatosBancariosMX> ListaDatosBancariosMX;
        private List<EProveedorUbicacionArchivos> ListaUbicacionArchivos;
        private EProveedorPoliticas eProveedorPoliticas;
        private List<EProveedorCategoriaPoliticasCompra> ListaCategoriaPoliticasCompra;
        private EProveedorAcuerdos eProveedorAcuerdos;
        private EProveedorCondiciones eProveedorCondiciones;
        private List<EProveedorPlazoCredito> ListaPlazoCredito;
        private List<EProveedorFletes> ListaFletes;
        //private List<EProveedorSucursalEntrega> ListaSucursalesEntrega;
        private List<EProveedorCostoFleteLimiteCapacidad> ListaCondicionesEntregaPorSucursal;

        //Crear instancia de Clase Logica de Negocio de todas las entidades a utilizar
        //
        //Es posible tener que cambiar de readonly a todo los accesos 
        //

        private readonly ProveedorUsuariosBol proveedorUsuarioBol = new ProveedorUsuariosBol();
        private readonly ProveedorUsuarioPermisosBol proveedorUsuarioPermisosBol = new ProveedorUsuarioPermisosBol();
        private readonly ClaveBancoBol claveBancoBol= new ClaveBancoBol();
        //private readonly ProveedorExpedienteBol proveedorExpedienteBol = new ProveedorExpedienteBol();
        private readonly ProveedorDatosPrimBol proveedorDatosPrimBol = new ProveedorDatosPrimBol();
        private readonly ProveedorProvBol proveedorProvBol = new ProveedorProvBol();
        private readonly ProveedorDireccionesBol proveedorDireccionesBol = new ProveedorDireccionesBol();
        private readonly ProveedorContactosBol proveedorContactosBol = new ProveedorContactosBol();
        private readonly ProveedorDatosBancariosEXBol proveedorDatosBancariosEXBol = new ProveedorDatosBancariosEXBol();
        private readonly ProveedorDireccionesBol proveedorDireccionesDatosBancariosEXBol = new ProveedorDireccionesBol();
        private readonly ProveedorDatosBancariosMXBol proveedorDatosBancariosMXBol = new ProveedorDatosBancariosMXBol();
        private readonly ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
        private readonly ProveedorPoliticasBol proveedorPoliticasBol = new ProveedorPoliticasBol();
        private readonly ProveedorCategoriaPoliticasCompraBol proveedorCategoriaPoliticasCompraBol = new ProveedorCategoriaPoliticasCompraBol();
        private readonly ProveedorAcuerdosBol proveedorAcuerdosBol = new ProveedorAcuerdosBol();
        private readonly ProveedorCondicionesBol proveedorCondicionesBol = new ProveedorCondicionesBol();
        private readonly ProveedorPlazoCreditoBol proveedorPlazoCreditoBol = new ProveedorPlazoCreditoBol();
        private readonly ProveedorFletesBol proveedorFletesBol = new ProveedorFletesBol();
        //private readonly ProveedorSucursalEntregaBol proveedorSucursalEntregaBol = new ProveedorSucursalEntregaBol();
        private readonly ProveedorCostoFleteLimiteCapacidadBol proveedorCostoFleteLimiteCapacidadBol = new ProveedorCostoFleteLimiteCapacidadBol();

        //Borrar después de mostrar programa DEMO fase 2
        static string archivoEjemplar = "C:\\Users\\Soporte2\\Desktop\\VIMIFOSFactura.pdf";

        public frmCatalogoProveedores()
        {
            InitializeComponent();
        }
        private void frmCatalogoProveedores_Load(object sender, EventArgs e)
        {
            //Hacer Todos los botones invisibles
            //Datos Generales
            //foreach (Control c in this.Controls)
            //{
            //    c.Enabled = false;
            //}

            //Cargar Lista de Bancos en ComboBox
            ListaBancos = claveBancoBol.getAllBancos();
            foreach(var i in ListaBancos)
            {
                comboBoxBancoDatosBancariosMX.Items.Add(i.Banco);
            }
            comboBoxBancoDatosBancariosMX.Items.Add("Otro");

            //Deshabilitar campos exclusivos a ciertos usuarios
            checkBoxDireccionesRevisado.Enabled = false;
            checkBoxContactosRevisado.Enabled = false;
            checkBoxDatosBancariosMXRevisado.Enabled = false;
            checkBoxDatosBancariosEXRevisado.Enabled = false;
            checkBoxAcuerdosRevisado.Enabled = false;
            checkBoxCondicionesRevisado.Enabled = false;
            checkBoxPoliticasRevisado.Enabled = false;
            checkBoxExpedienteRevisado.Enabled = false;

            //Habilitar campos para ciertos usuarios
            if (EProveedorUsuario.Usuario == "FROMO")
            {
                checkBoxDireccionesRevisado.Enabled = true;
                checkBoxContactosRevisado.Enabled = true;
                checkBoxDatosBancariosMXRevisado.Enabled = true;
                checkBoxDatosBancariosEXRevisado.Enabled = true;
                checkBoxAcuerdosRevisado.Enabled = true;
                checkBoxCondicionesRevisado.Enabled = true;
                checkBoxPoliticasRevisado.Enabled = true;
                checkBoxExpedienteRevisado.Enabled = true;
            }
            else if (EProveedorUsuario.Usuario == "CROMO")
            {
                checkBoxDireccionesRevisado.Enabled = true;
                checkBoxContactosRevisado.Enabled = true;
                checkBoxDatosBancariosMXRevisado.Enabled = true;
                checkBoxDatosBancariosEXRevisado.Enabled = true;
                checkBoxAcuerdosRevisado.Enabled = true;
                checkBoxCondicionesRevisado.Enabled = true;
                checkBoxPoliticasRevisado.Enabled = true;
                checkBoxExpedienteRevisado.Enabled = true;
            }
            else if (EProveedorUsuario.Usuario == "PMILLAN")
            {
                checkBoxDireccionesRevisado.Enabled = true;
                checkBoxContactosRevisado.Enabled = true;
                checkBoxDatosBancariosMXRevisado.Enabled = true;
                checkBoxDatosBancariosEXRevisado.Enabled = true;
            }
            else if(EProveedorUsuario.Usuario == "AMON")
            {
                checkBoxDireccionesRevisado.Enabled = true;
            }            
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }        
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnMax_Click(object sender, EventArgs e)
        {            
            double new_width = this.Width;
            //normalises window
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.pnlBarraSuperior.Width = pnlBarraSuperior_Width;
                pnlNotificaciones.Visible = true;
                tabOpciones.Dock = DockStyle.None;                                        
            }
            //maximises window
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.pnlBarraSuperior.Width = this.Width;
                pnlNotificaciones.Visible = false;
                tabOpciones.Dock = DockStyle.Fill;
                this.CenterToScreen();
            }
        }
        #region Helper Functions
        //Funciones de ayuda
        private void cargarDatosPorUsuario(string Usuario)
        {
            eProveedorUsuarioPermisos = proveedorUsuarioPermisosBol.consultarUsuarioPermisos(Usuario);
            foreach (var i in eProveedorUsuarioPermisos)
            {
                if (i.Permiso == "3.0")
                {
                    //Acceso Ilimitado a todo Catálogo de Proveedores
                    return;
                }
                else
                {
                    if (i.Permiso == "3.1")
                    {                     

                        //Consulta de Datos Generales
                        //Edición Datos Generales
                        //Agregar Datos Generales
                    }
                    if (i.Permiso == "3.1.1")
                    {
                        //Consulta de Datos Generales
                    }
                    if (i.Permiso == "3.1.2")
                    {
                        //Edición Datos Generales
                    }
                    if (i.Permiso == "3.1.3")
                    {
                        //Agregar Datos Generales
                    }
                    if (i.Permiso == "3.1.4")
                    {
                        //Eliminar Datos Generales
                    }
                    if (i.Permiso == "3.2")
                    {
                        //Acceso Ilimitado Expediente
                    }
                    if (i.Permiso == "3.2.1")
                    {
                        //Consulta Expediente
                    }
                    if (i.Permiso == "3.3")
                    {
                        //Acceso Ilimitado Acuerdos
                    }
                    if (i.Permiso == "3.3.1")
                    {
                        //Consulta Acuerdos
                    }
                    if (i.Permiso == "3.4")
                    {
                        //Acceso Ilimitado Políticas
                    }
                    if (i.Permiso == "3.4.1")
                    {
                        //Consulta Políticas
                    }
                    if (i.Permiso == "3.5")
                    {
                        //Acceso Ilimitado Condiciones
                    }
                    if (i.Permiso == "3.5.1")
                    {
                        //Consulta Condiciones
                    }
                }
            }

            
            
            //if (eProveedorUsuario.EdicionDatosGeneralesGranted)
            //{

            //}
            //if (eProveedorUsuario.EdicionPoliticasGranted)
            //{

            //}
            //if (eProveedorUsuario.EdicionExpedienteGranted)
            //{

            //}
            //if (eProveedorUsuario.EdicionCondicionesGranted)
            //{

            //}
            //if (eProveedorUsuario.EdicionAcuerdosGranted)
            //{

            //}
            //if (eProveedorUsuario.EdicionProductosPrincipalesGranted)
            //{

            //}
            //if (eProveedorUsuario.EdicionPresentacionProductosGranted)
            //{

            //}
            //if (eProveedorUsuario.EdicionListaPreciosGranted)
            //{

            //}
            //if (eProveedorUsuario.EdicionMargenUtilidadGranted)
            //{

            //}
            //if (eProveedorUsuario.EdicionHistorialPreciosGranted == true)
            //{

            //}
            //if (eProveedorUsuario.EdicionTendenciasGranted == true)
            //{

            //}

        }
        private void checkBoxDireccionesRevisado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Revisar";
                string seccionDato = "Dirección";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " Sección " + seccionDato,
                          accionDato + " Sección " + seccionDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (checkBoxDireccionesRevisado.Checked)
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, true);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como revisada.", "Revisión Sección " + seccionDato);
                }
                else
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, false);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como NO revisada.", "Revisión Sección " + seccionDato);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }            
        }
        private void checkBoxContactosRevisado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Revisar";
                string seccionDato = "Contacto";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " Sección " + seccionDato,
                          accionDato + " Sección " + seccionDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxContactosRevisado.Checked)
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, true);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como revisada.", "Revisión Sección " + seccionDato);
                }
                else
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, false);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como NO revisada.", "Revisión Sección " + seccionDato);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void checkBoxDatosBancariosMXRevisado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Revisar";
                string seccionDato = "Cuenta Bancaria MX";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " Sección " + seccionDato,
                          accionDato + " Sección " + seccionDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxDatosBancariosMXRevisado.Checked)
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, true);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como revisada.", "Revisión Sección " + seccionDato);
                }
                else
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, false);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como NO revisada.", "Revisión Sección " + seccionDato);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void checkBoxDatosBancariosEXRevisado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Revisar";
                string seccionDato = "Cuenta Bancaria EX";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " Sección " + seccionDato,
                          accionDato + " Sección " + seccionDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxDatosBancariosEXRevisado.Checked)
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, true);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como revisada.", "Revisión Sección " + seccionDato);
                }
                else
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, false);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como NO revisada.", "Revisión Sección " + seccionDato);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void checkBoxPoliticasRevisado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Revisar";
                string seccionDato = "Políticas";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " Sección " + seccionDato,
                          accionDato + " Sección " + seccionDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxPoliticasRevisado.Checked)
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, true);
                   //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como revisada.", "Revisión Sección " + seccionDato);
                }
                else
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, false);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como NO revisada.", "Revisión Sección " + seccionDato);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void checkBoxCondicionesRevisado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Revisar";
                string seccionDato = "Condiciones";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " Sección " + seccionDato,
                          accionDato + " Sección " + seccionDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxCondicionesRevisado.Checked)
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, true);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como revisada.", "Revisión Sección " + seccionDato);
                }
                else
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, false);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como NO revisada.", "Revisión Sección " + seccionDato);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void checkBoxExpedienteRevisado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Revisar";
                string seccionDato = "Expediente";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " Sección " + seccionDato,
                          accionDato + " Sección " + seccionDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxExpedienteRevisado.Checked)
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, true);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como revisada.", "Revisión Sección " + seccionDato);
                }
                else
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, false);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como NO revisada.", "Revisión Sección " + seccionDato);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void checkBoxAcuerdosRevisado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Revisar";
                string seccionDato = "Acuerdos";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " Sección " + seccionDato,
                          accionDato + " Sección " + seccionDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxAcuerdosRevisado.Checked)
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, true);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como revisada.", "Revisión Sección " + seccionDato);
                }
                else
                {
                    proveedorDatosPrimBol.editarRevisionDirecciones(eProveedorDatosPrim.ClaveProveedor, seccionDato, false);
                    //MessageBox.Show("Sección " + seccionDato + " ha sido marcada como NO revisada.", "Revisión Sección " + seccionDato);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void CargarFletes(string claveProveedor)
        {
            try
            {
                //Ignore this code, modify completely
                ListaFletes = proveedorFletesBol.consultarFletesByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                if (ListaFletes.Count == 0)
                    return;

                DataTable tablaFletes = new DataTable();
                tablaFletes.Columns.Add("ClaveProveedor");
                tablaFletes.Columns.Add("Fleteid");
                tablaFletes.Columns.Add("TipoEnvio");
                tablaFletes.Columns.Add("FormaEntrega");
                tablaFletes.Columns.Add("ClaveProveedorFlete");
                tablaFletes.Columns.Add("NombreProveedor");
                tablaFletes.Columns.Add("DescripcionProveedor");
                tablaFletes.Columns.Add("PedidoMin");
                tablaFletes.Columns.Add("PedidoMax");
                tablaFletes.Columns.Add("Unidad");
                tablaFletes.Columns.Add("Origen");
                tablaFletes.Columns.Add("Destino");
                tablaFletes.Columns.Add("Observaciones");
                tablaFletes.Columns.Add("EsPreferencia");
                tablaFletes.Columns.Add("CostoFleteMatriz");
                tablaFletes.Columns.Add("CostoFleteHipodromo");
                tablaFletes.Columns.Add("CostoFleteSanPedro");
                tablaFletes.Columns.Add("CostoFleteMagdalena");
                tablaFletes.Columns.Add("CostoFleteCaborca");
                tablaFletes.Columns.Add("CostoFleteCEDIS");
                tablaFletes.Columns.Add("CostoFleteCMA");

                foreach (var i in ListaFletes)
                {
                    tablaFletes.Rows.Add(new object[] { i.ClaveProveedor, i.Fleteid, i.TipoEnvio, i.FormaEntrega,
                    i.ClaveProveedorFlete, i.NombreProveedor, i.DescripcionProveedor, i.PedidoMin, i.PedidoMax,
                    i.Unidad, i.Origen, i.Destino, i.Observaciones, i.EsPreferencia, i.CostoFleteMatriz, i.CostoFleteHipodromo,
                        i.CostoFleteSanPedro, i.CostoFleteMagdalena, i.CostoFleteCaborca, i.CostoFleteCEDIS, i.CostoFleteCMA});
                }

                dataGridFletes.AutoGenerateColumns = true;
                dataGridFletes.DataSource = tablaFletes;
                dataGridFletes.CurrentCell = dataGridFletes.Rows[0].Cells[0];

                //Flete                
                comboBoxTipoEnvioFletes.SelectedItem = dataGridFletes.CurrentRow.Cells[2].Value.ToString();
                comboBoxFormaEntregaFletes.SelectedItem = dataGridFletes.CurrentRow.Cells[3].Value.ToString();
                txtBoxClaveProveedorFletes.Text = dataGridFletes.CurrentRow.Cells[4].Value.ToString();
                txtBoxNombreFleteCondiciones.Text = dataGridFletes.CurrentRow.Cells[5].Value.ToString();
                txtBoxPedidoMinCondiciones.Text = dataGridFletes.CurrentRow.Cells[7].Value.ToString();
                txtBoxPedidoMaxCondiciones.Text = dataGridFletes.CurrentRow.Cells[8].Value.ToString();
                comboBoxUnidadPedidoCondiciones.SelectedItem = dataGridFletes.CurrentRow.Cells[9].Value.ToString();
                txtBoxFleteOrigenCondiciones.Text = dataGridFletes.CurrentRow.Cells[10].Value.ToString();
                txtBoxFleteDestinoCondiciones.Text = dataGridFletes.CurrentRow.Cells[11].Value.ToString();
                txtBoxObservacionesFletes.Text = dataGridFletes.CurrentRow.Cells[12].Value.ToString();
                if (dataGridFletes.CurrentRow.Cells[13].Value.ToString() == "1")
                    lblFletePreferenciaFletes.Visible = true;
                txtBoxCantidadCostoSucursalMatrizFletes.Text = dataGridFletes.CurrentRow.Cells[14].Value.ToString();
                txtBoxCantidadCostoSucursalHipodromoFletes.Text = dataGridFletes.CurrentRow.Cells[15].Value.ToString();
                txtBoxCantidadCostoSucursalSanPedroFletes.Text = dataGridFletes.CurrentRow.Cells[16].Value.ToString();
                txtBoxCantidadCostoSucursalMagdalenaFletes.Text = dataGridFletes.CurrentRow.Cells[17].Value.ToString();
                txtBoxCantidadCostoSucursalCabrocaFletes.Text = dataGridFletes.CurrentRow.Cells[18].Value.ToString();
                txtBoxCantidadCostoSucursalCEDISFletes.Text = dataGridFletes.CurrentRow.Cells[19].Value.ToString();
                txtBoxCantidadCostoSucursalCMAletes.Text = dataGridFletes.CurrentRow.Cells[20].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void CargarPlazosCredito(string claveProveedor)
        {
            try
            {
                //LimpiarDireccionesDatosGenerales();               
                ListaPlazoCredito = proveedorPlazoCreditoBol.consultarPlazosByClaveProveedorVal(Convert.ToString(claveProveedor));

                if (ListaPlazoCredito.Count == 0)
                    return;

                DataTable tablaPlazoCredito = new DataTable();
                tablaPlazoCredito.Columns.Add("ClaveProveedor");
                tablaPlazoCredito.Columns.Add("Plazoid");
                tablaPlazoCredito.Columns.Add("PrioridadUso");
                tablaPlazoCredito.Columns.Add("EstatusActivoPlazo");
                tablaPlazoCredito.Columns.Add("RevisadoPlazo");
                tablaPlazoCredito.Columns.Add("PlazoCreditoDias");
                tablaPlazoCredito.Columns.Add("DefinicionPlazo");
                tablaPlazoCredito.Columns.Add("ProntoPago1Dias");
                tablaPlazoCredito.Columns.Add("ProntoPago1Descuento");
                tablaPlazoCredito.Columns.Add("VencimientoPagoFactura1");
                tablaPlazoCredito.Columns.Add("ObservacionesPP1");
                tablaPlazoCredito.Columns.Add("ProntoPago2Dias");
                tablaPlazoCredito.Columns.Add("ProntoPago2Descuento");
                tablaPlazoCredito.Columns.Add("VencimientoPagoFactura2");
                tablaPlazoCredito.Columns.Add("ObservacionesPP2");
                tablaPlazoCredito.Columns.Add("ProntoPago3Dias");
                tablaPlazoCredito.Columns.Add("ProntoPago3Descuento");
                tablaPlazoCredito.Columns.Add("VencimientoPagoFactura3");
                tablaPlazoCredito.Columns.Add("ObservacionesPP3");
                tablaPlazoCredito.Columns.Add("ProntoPago4Dias");
                tablaPlazoCredito.Columns.Add("ProntoPago4Descuento");
                tablaPlazoCredito.Columns.Add("VencimientoPagoFactura4");
                tablaPlazoCredito.Columns.Add("ObservacionesPP4");
                tablaPlazoCredito.Columns.Add("ProntoPago5Dias");
                tablaPlazoCredito.Columns.Add("ProntoPago5Descuento");
                tablaPlazoCredito.Columns.Add("VencimientoPagoFactura5");
                tablaPlazoCredito.Columns.Add("ObservacionesPP5");
                tablaPlazoCredito.Columns.Add("ObservacionesGenerales");

                foreach (var i in ListaPlazoCredito)
                {
                    if (i.EstatusActivo)
                    {
                        tablaPlazoCredito.Rows.Add(new object[] {i.ClaveProveedor, i.Plazoid, i.PrioridadDeUso, i.EstatusActivo ? "Activo" : "No activo", i.Revisado ? "Revisado": "No Revisado",
                            i.PlazoCreditoDias, i.DefinicionPlazo, i.ProntoPago1Dias, i.ProntoPago1Descuento, i.VencimientoPagoFactura1, i.ObservacionesPP1,
                            i.ProntoPago2Dias, i.ProntoPago2Descuento, i.VencimientoPagoFactura2, i.ObservacionesPP2,
                            i.ProntoPago3Dias, i.ProntoPago3Descuento, i.VencimientoPagoFactura3, i.ObservacionesPP3,
                            i.ProntoPago4Dias, i.ProntoPago4Descuento, i.VencimientoPagoFactura4, i.ObservacionesPP4,
                            i.ProntoPago5Dias, i.ProntoPago5Descuento, i.VencimientoPagoFactura5, i.ObservacionesPP5, i.ObservacionesGenerales});
                    }
                }
                dataGridPlazosCredito.DataSource = null;
                dataGridPlazosCredito.Rows.Clear();
                dataGridPlazosCredito.AutoGenerateColumns = false;
                dataGridPlazosCredito.DataSource = tablaPlazoCredito; 

                if (Convert.ToString(dataGridPlazosCredito.Rows[0].Cells[4].Value.ToString()) == "Revisado")
                    checkBoxCondicionesItem.Checked = true;
                else
                    checkBoxCondicionesItem.Checked = false;

                txtBoxCondicionesCreditoCondiciones.Text = dataGridPlazosCredito.Rows[0].Cells[5].Value.ToString();
                txtBoxDefinicionPlazoCredito.Text = dataGridPlazosCredito.Rows[0].Cells[6].Value.ToString();
                txtBoxProntoPago1Dias.Text = dataGridPlazosCredito.Rows[0].Cells[7].Value.ToString();
                comboBoxDescProntoPago1.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[8].Value.ToString();
                comboBoxVencimientoPagoFactura1.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[9].Value.ToString();
                txtBoxObservacionesProntoPago1.Text = dataGridPlazosCredito.Rows[0].Cells[10].Value.ToString();
                txtBoxProntoPago2Dias.Text = dataGridPlazosCredito.Rows[0].Cells[11].Value.ToString();
                comboBoxDescProntoPago2.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[12].Value.ToString();
                comboBoxVencimientoPagoFactura2.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[13].Value.ToString();
                txtBoxObservacionesProntoPago2.Text = dataGridPlazosCredito.Rows[0].Cells[14].Value.ToString();
                txtBoxProntoPago3Dias.Text = dataGridPlazosCredito.Rows[0].Cells[15].Value.ToString();
                comboBoxDescProntoPago3.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[16].Value.ToString();
                comboBoxVencimientoPagoFactura3.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[17].Value.ToString();
                txtBoxObservacionesProntoPago3.Text = dataGridPlazosCredito.Rows[0].Cells[18].Value.ToString();
                txtBoxProntoPago4Dias.Text = dataGridPlazosCredito.Rows[0].Cells[19].Value.ToString();
                comboBoxDescProntoPago4.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[20].Value.ToString();
                comboBoxVencimientoPagoFactura4.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[21].Value.ToString();
                txtBoxObservacionesProntoPago4.Text = dataGridPlazosCredito.Rows[0].Cells[22].Value.ToString();
                txtBoxProntoPago5Dias.Text = dataGridPlazosCredito.Rows[0].Cells[23].Value.ToString();
                comboBoxDescProntoPago5.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[24].Value.ToString();
                comboBoxVencimientoPagoFactura5.SelectedItem = dataGridPlazosCredito.Rows[0].Cells[25].Value.ToString();
                txtBoxObservacionesProntoPago5.Text = dataGridPlazosCredito.Rows[0].Cells[26].Value.ToString();
                txtBoxObservacionesGeneralesPlazo.Text = dataGridPlazosCredito.Rows[0].Cells[27].Value.ToString();

                if (txtBoxProntoPago1Dias.Text != "" || txtBoxProntoPago2Dias.Text != "" || txtBoxProntoPago3Dias.Text != "" ||
                txtBoxProntoPago4Dias.Text != "" || txtBoxProntoPago5Dias.Text != "")
                    checkBoxTieneProntoPago.Checked = true;
                else
                    checkBoxTieneProntoPago.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void CargarCondiciones(string claveP)
        {
            try
            {
                LimpiarCondiciones();
                checkBoxCondicionesRevisado.Checked = eProveedorDatosPrim.isCondicionesRevisado;
                eProveedorCondiciones = proveedorCondicionesBol.consultarCondicionesByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));
                //ListaCondicionesEntregaPorSucursal = proveedorCostoFleteLimiteCapacidadBol.consultarCostoFleteLimiteCapacidadByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));
                //ListaFletes = proveedorFletesBol.consultarFletesByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                //tab Condiciones
                txtBoxClaveCondiciones.Text = Convert.ToString(eProveedorDatosPrim.ClaveProveedor);
                txtBoxProveedorCondiciones.Text = Convert.ToString(eProveedorDatosPrim.NombreProveedor);
                txtBoxRFCCondiciones.Text = Convert.ToString(eProveedorDatosPrim.RFC);
                txtBoxCategoriaCondiciones.Text = Convert.ToString(eProveedorDatosPrim.Categoria);
                dateTimePickerCondiciones.Value = eProveedorDatosPrim.fechaUltimaActualizacion;
                if (eProveedorDatosPrim.PATHImagen != null)
                    picBoxLogoCondiciones.ImageLocation = Convert.ToString(eProveedorDatosPrim.PATHImagen);

                if (eProveedorCondiciones == null)
                    return;

                //Cargar Plazo de Cr+edito
                CargarPlazosCredito(claveP);

                //CargarFletes
                CargarFletes(claveP);

                //Condiciones 
                
                txtBoxTiempoEntregaCondiciones.Text = Convert.ToString(eProveedorCondiciones.TiempoEntrega);
                txtBoxObservacionesCondiciones.Text = Convert.ToString(eProveedorCondiciones.ObservacionesTiempoEntrega);
                txtBoxCondicionesEspecialesCondiciones.Text = Convert.ToString(eProveedorCondiciones.CondicionesEspecialesEntrega);
                

                //Sucursales de Entrega
                if (eProveedorCondiciones.SucursalEntrega.Contains(" Matriz "))
                    checkBoxMatriz.Checked = true;
                if (eProveedorCondiciones.SucursalEntrega.Contains(" Magdalena "))
                    checkBoxMagdalena.Checked = true;
                if (eProveedorCondiciones.SucursalEntrega.Contains(" San Pedro "))
                    checkBoxSanPedro.Checked = true;
                if (eProveedorCondiciones.SucursalEntrega.Contains(" Hipódromo "))
                    checkBoxHipodromo.Checked = true;
                if (eProveedorCondiciones.SucursalEntrega.Contains(" Caborca "))
                    checkBoxCaborca.Checked = true;
                if (eProveedorCondiciones.SucursalEntrega.Contains(" CEDIS "))
                    checkBoxCEDIS.Checked = true;
                if (eProveedorCondiciones.SucursalEntrega.Contains(" CMA "))
                    checkBoxCMA.Checked = true;

                //Forma de Entrega            
                //if (eProveedorCondiciones.FormaEntrega.Contains(" Paquetería Pagada por Proveedor "))
                //    checkBoxPaqueteriaPagadaProveedor.Checked = true;
                //if (eProveedorCondiciones.FormaEntrega.Contains(" Paquetería por Cobrar "))
                //    checkBoxPaqueteriaPorCobrar.Checked = true;
                //if (eProveedorCondiciones.FormaEntrega.Contains(" Transporte Contratado "))
                //    checkBoxTransporteContratado.Checked = true;
                //if (eProveedorCondiciones.FormaEntrega.Contains(" Transporte de Proveedor "))
                //    checkBoxTransporteProveedor.Checked = true;
                //if (eProveedorCondiciones.FormaEntrega.Contains(" Otra: "))
                //{
                //    string[] cadenaFormaEntrega = eProveedorCondiciones.FormaEntrega.Split(new string[] { "Otra: " }, StringSplitOptions.None);
                //    txtBoxOtraFormaEntrega.Text = cadenaFormaEntrega[1];
                //}

                //Rutas y Fletes
               

                

                //if (ListaCondicionesEntregaPorSucursal.Count > 0)
                //{
                //    //Costo Flete
                //    DataTable tablaCostoFlete = new DataTable();
                //    tablaCostoFlete.Columns.Add("NumeroFlete");
                //    tablaCostoFlete.Columns.Add("Matriz");
                //    tablaCostoFlete.Columns.Add("Hipodromo");
                //    tablaCostoFlete.Columns.Add("SanPedro");
                //    tablaCostoFlete.Columns.Add("Magdalena");
                //    tablaCostoFlete.Columns.Add("Caborca");
                //    tablaCostoFlete.Columns.Add("CEDIS");
                //    tablaCostoFlete.Columns.Add("CMA");
                //    tablaCostoFlete.Columns.Add("Observaciones");

                //    //foreach (var i in ListaCondicionesEntregaPorSucursal)
                //    //{
                //    //    if(i.CostoFleteOLimiteCapacidad == "Costo Flete")
                //    //        tablaCostoFlete.Rows.Add(new object[] { i.NumeroFlete, i.Matriz, i.Hipodromo,
                //    //            i.SanPedro, i.Magdalena, i.Caborca, i.CEDIS, i.CMA, i.Observaciones });
                //    //}

                //    //Límite Capacidad de Bodega Por Producto
                //    DataTable tablaLimiteCapacidad = new DataTable();
                //    tablaLimiteCapacidad.Columns.Add("Matriz");
                //    tablaLimiteCapacidad.Columns.Add("Hipodromo");
                //    tablaLimiteCapacidad.Columns.Add("SanPedro");
                //    tablaLimiteCapacidad.Columns.Add("Magdalena");
                //    tablaLimiteCapacidad.Columns.Add("Caborca");
                //    tablaLimiteCapacidad.Columns.Add("CEDIS");
                //    tablaLimiteCapacidad.Columns.Add("CMA");
                //    tablaLimiteCapacidad.Columns.Add("Observaciones");

                //    //foreach (var i in ListaCondicionesEntregaPorSucursal)
                //    //{
                //    //    if (i.CostoFleteOLimiteCapacidad == "Limite Capacidad")
                //    //        tablaLimiteCapacidad.Rows.Add(new object[] { i.Matriz, i.Hipodromo,
                //    //            i.SanPedro, i.Magdalena, i.Caborca, i.CEDIS, i.CMA, i.Observaciones });
                //    //}

                //    dataGridLimiteCapacidad.AutoGenerateColumns = false;
                //    dataGridLimiteCapacidad.DataSource = tablaLimiteCapacidad;
                //}
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }

        }
        private void CargarPoliticas(string claveP)
        {
            try
            {
                LimpiarPoliticas();
                checkBoxPoliticasRevisado.Checked = eProveedorDatosPrim.isPoliticasRevisado;
                eProveedorPoliticas = proveedorPoliticasBol.consultarPoliticasByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));
                ListaCategoriaPoliticasCompra = proveedorCategoriaPoliticasCompraBol.consultarCategoriaPoliticasCompraByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));
                //tab Politicas
                txtBoxClavePoliticas.Text = Convert.ToString(eProveedorDatosPrim.ClaveProveedor);
                txtBoxProveedorPoliticas.Text = Convert.ToString(eProveedorDatosPrim.NombreProveedor);
                txtBoxRFCPoliticas.Text = Convert.ToString(eProveedorDatosPrim.RFC);
                txtBoxCategoriaPoliticas.Text = Convert.ToString(eProveedorDatosPrim.Categoria);
                dateTimePickerPoliticas.Value = eProveedorDatosPrim.fechaUltimaActualizacion;
                if (eProveedorDatosPrim.PATHImagen != null)
                    picBoxLogoPoliticas.ImageLocation = Convert.ToString(eProveedorDatosPrim.PATHImagen);

                if (eProveedorPoliticas == null)
                    return;

                txtBoxCompraMinPoliticas.Text = Convert.ToString(eProveedorPoliticas.CompraMinimaMensual);

                if (ListaCategoriaPoliticasCompra.Count > 0)
                {
                    DataTable tablaCategorias = new DataTable();
                    tablaCategorias.Columns.Add("ImporteMensual");
                    tablaCategorias.Columns.Add("ImporteTrimestral");
                    tablaCategorias.Columns.Add("Beneficios");

                    foreach (var i in ListaCategoriaPoliticasCompra)
                    {
                        tablaCategorias.Rows.Add(new object[] { i.ImporteMensual, i.ImporteTrimestral, i.Beneficios });
                    }

                    //dataGridCategoriasPoliticasCompra.AutoGenerateColumns = false;
                    //dataGridCategoriasPoliticasCompra.DataSource = tablaCategorias;
                }

                if (Convert.ToString(eProveedorPoliticas.RecepcionSolicitudCompra) == "Diario")
                    radioBtnDiarioPoliticas.Checked = true;
                else if (Convert.ToString(eProveedorPoliticas.RecepcionSolicitudCompra) == "Semanal")
                    radioBtnSemanalPoliticas.Checked = true;
                else if (Convert.ToString(eProveedorPoliticas.RecepcionSolicitudCompra) == "Mensual")
                    radioBtnMensualPoliticas.Checked = true;
                else
                    radioBtnOtroPoliticas.Checked = true;

                txtBoxObservacionesSolicitudes.Text = Convert.ToString(eProveedorPoliticas.ObservacionesSolicitudCompra);
                txtBoxListaPoliticasGarantias.Text = Convert.ToString(eProveedorPoliticas.PoliticasGarantia);
                txtBoxListaPoliticasDevoluciones.Text = Convert.ToString(eProveedorPoliticas.PoliticasDevoluciones);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }

        }
        private void CargarExpediente(string claveP)
        {
            try
            {
                LimpiarExpediente();
                checkBoxExpedienteRevisado.Checked = eProveedorDatosPrim.isExpedienteRevisado;
                //tab Expediente
                txtBoxClaveExpediente.Text = Convert.ToString(eProveedorDatosPrim.ClaveProveedor);
                txtBoxProveedorExpediente.Text = Convert.ToString(eProveedorDatosPrim.NombreProveedor);
                txtBoxRFCExpediente.Text = Convert.ToString(eProveedorDatosPrim.RFC);
                txtBoxCategoriaExpediente.Text = Convert.ToString(eProveedorDatosPrim.Categoria);
                dateTimePickerExpediente.Value = eProveedorDatosPrim.fechaUltimaActualizacion;
                if (eProveedorDatosPrim.PATHImagen != null)
                    picBoxLogoExpediente.ImageLocation = Convert.ToString(eProveedorDatosPrim.PATHImagen);

                DelegarUbicacionDeArchivos(eProveedorDatosPrim.ClaveProveedor);
                ChecarDocumentacion(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void CargarAcuerdos(string claveP)
        {
            try
            {
                LimpiarAcuerdos();
                checkBoxAcuerdosRevisado.Checked = eProveedorDatosPrim.isAcuerdosRevisado;
                eProveedorAcuerdos = proveedorAcuerdosBol.consultarAcuerdosByClaveProveedorVal(eProveedorDatosPrim.ClaveProveedor);

                //tab Acuerdos
                txtBoxClaveAcuerdos.Text = Convert.ToString(eProveedorDatosPrim.ClaveProveedor);
                txtBoxProveedorAcuerdos.Text = Convert.ToString(eProveedorDatosPrim.NombreProveedor);
                txtBoxRFCAcuerdos.Text = Convert.ToString(eProveedorDatosPrim.RFC);
                txtBoxCategoriaAcuerdos.Text = Convert.ToString(eProveedorDatosPrim.Categoria);
                dateTimePickerAcuerdos.Value = eProveedorDatosPrim.fechaUltimaActualizacion;
                if (eProveedorDatosPrim.PATHImagen != null)
                    picBoxLogoAcuerdos.ImageLocation = Convert.ToString(eProveedorDatosPrim.PATHImagen);

                if (eProveedorAcuerdos == null)
                    return;
                textBoxAcuerdoCompra.Text = Convert.ToString(eProveedorAcuerdos.AcuerdoCompra);
                txtBoxAcuerdoVentaPublico.Text = Convert.ToString(eProveedorAcuerdos.AcuerdoVentaPublico);
                txtBoxAcuerdoAtencionClientes.Text = Convert.ToString(eProveedorAcuerdos.AcuerdoAtencionClientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }

        }
        private void CargarCuentasBancariasEXDatosGenerales(string claveP)
        {
            try
            {
                //LimpiarCuentasBancariasEXDatosGenerales();
                checkBoxDatosBancariosEXRevisado.Checked = eProveedorDatosPrim.isDatosBancariosEXRevisado;
                ListaDatosBancariosEX = proveedorDatosBancariosEXBol.consultarDatosBancariosEXByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                if (ListaDatosBancariosEX.Count == 0)
                    return;

                DataTable tablaDatosBancariosEX = new DataTable();
                tablaDatosBancariosEX.Columns.Add("BancoEXid");
                tablaDatosBancariosEX.Columns.Add("NombreBancoDestino");
                tablaDatosBancariosEX.Columns.Add("ClaveBancoDestino");
                tablaDatosBancariosEX.Columns.Add("NombreDestinatario");
                tablaDatosBancariosEX.Columns.Add("NumeroCuenta");
                tablaDatosBancariosEX.Columns.Add("Divisa");
                tablaDatosBancariosEX.Columns.Add("MontoMaximoAPagar");
                tablaDatosBancariosEX.Columns.Add("NombreBancoIntermediario");
                tablaDatosBancariosEX.Columns.Add("ClaveBancoIntermediario");
                tablaDatosBancariosEX.Columns.Add("NumIdDireccionDestinatario");
                tablaDatosBancariosEX.Columns.Add("Vigencia");
                tablaDatosBancariosEX.Columns.Add("FechaDeVigencia");
                tablaDatosBancariosEX.Columns.Add("TipoRelacionConDestinatario");
                tablaDatosBancariosEX.Columns.Add("MotivoPago");
                tablaDatosBancariosEX.Columns.Add("Preferente");
                tablaDatosBancariosEX.Columns.Add("EstatusActividad");
                tablaDatosBancariosEX.Columns.Add("DatoBancarioEXRevisado");


                foreach (var i in ListaDatosBancariosEX)
                {
                    if (i.EstatusActivo)
                    {
                        tablaDatosBancariosEX.Rows.Add(new object[] { i.BancoEXid, i.NombreBancoDestino, i.ClaveBancoDestino, i.NombreDestinatario,
                        i.NumeroCuentaDestinatario, i.DivisaAPagar, i.MontoMaximoAPagar, i.NombreBancoIntermediario,
                        i.ClaveBancoIntermediario, i.NumIdDireccionDestinatario, i.Vigencia, i.FechaDeVigencia,
                        i.TipoRelacionConDestinatario, i.MotivoPago, i.EsPreferencia ? "Preferente" : "No preferente",
                        i.EstatusActivo, i.Revisado ? "Revisado" : "No Revisado" });
                    }
                }

                dataGridDatosBancariosEX.AutoGenerateColumns = false;
                dataGridDatosBancariosEX.DataSource = tablaDatosBancariosEX;

                //Datos Bancarios EX
                txtBoxNombreBancoDestinoEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[1].Value.ToString());
                txtBoxClaveBancoDestinoEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[2].Value.ToString());
                txtBoxNumeroCuentaDestinatarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[4].Value.ToString());
                txtBoxNombreDestinatarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[3].Value.ToString());
                txtBoxMontoMaxEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[6].Value.ToString());
                comboBoxDivisaCuentaBancariaEX.SelectedItem = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[5].Value.ToString());
                txtBoxNombreBancoIntermediarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[7].Value.ToString());
                txtBoxClaveBancoIntermediarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[8].Value.ToString());
                if (dataGridDatosBancariosEX.CurrentRow.Cells[10].Value.ToString() == "True")
                {
                    radioBtnSi.Checked = true;
                    lblFechaVigenciaEX.Visible = true;
                    dateTimePickerDatosBancariosEX.Visible = true;
                    dateTimePickerDatosBancariosEX.Value = Convert.ToDateTime(dataGridDatosBancariosEX.CurrentRow.Cells[11].Value.ToString());

                }
                else
                {
                    radioBtnNo.Checked = true;
                    lblFechaVigenciaEX.Visible = false;
                    dateTimePickerDatosBancariosEX.Visible = false;

                }
                txtBoxTipoRelacionConDestinatarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[12].Value.ToString());
                txtBoxMotivoPagoEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[13].Value.ToString());
                DireccionDatosBancariosEX = proveedorDireccionesDatosBancariosEXBol.consultarDireccionesByClaveProveedorByIdVal(eProveedorDatosPrim.ClaveProveedor, Convert.ToInt32(dataGridDatosBancariosEX.CurrentRow.Cells[9].Value.ToString()));
                txtBoxCalleAveBlvrDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.CalleAveBlvr);
                txtBoxNumExteriorDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.NumExterior);
                txtBoxNumInteriorDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.NumInterior);
                txtBoxColoniaDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.Colonia);
                txtBoxInfAdicionalDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.InfAdicional);
                txtBoxCPDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.CodigoPostal);
                txtBoxPoblacionDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.Poblacion);
                txtBoxEstadoDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.Estado);
                txtBoxPaisDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.Pais);

                if (dataGridDatosBancariosEX.CurrentRow.Cells[14].Value.ToString() == "Preferente")
                    lblCuentaDePreferenciaClienteEX.Visible = true;
                else
                    lblCuentaDePreferenciaClienteEX.Visible = false;

                if (dataGridDatosBancariosEX.CurrentRow.Cells[16].Value.ToString() == "Revisado")
                    checkBoxDatoBancarioEXItem.Checked = true;
                else
                    checkBoxDatoBancarioEXItem.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void CargarCuentasBancariasMXDatosGenerales(string claveP)
        {
            try
            {
                //LimpiarCuentasBancariasMXDatosGenerales();
                checkBoxDatosBancariosMXRevisado.Checked = eProveedorDatosPrim.isDatosBancariosMXRevisado;
                ListaDatosBancariosMX = proveedorDatosBancariosMXBol.consultarDatosBancariosMXByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                if (ListaDatosBancariosMX.Count == 0)
                    return;

                DataTable tablaDatosBancariosMX = new DataTable();
                tablaDatosBancariosMX.Columns.Add("BancoMXid");
                tablaDatosBancariosMX.Columns.Add("EsPreferencia");
                tablaDatosBancariosMX.Columns.Add("PrioridadDeUso");
                tablaDatosBancariosMX.Columns.Add("NombreBancoDestino");
                tablaDatosBancariosMX.Columns.Add("CLABE");
                tablaDatosBancariosMX.Columns.Add("NumeroCuentaDestinatario");
                tablaDatosBancariosMX.Columns.Add("Sucursal");
                tablaDatosBancariosMX.Columns.Add("DivisaAPagar");
                tablaDatosBancariosMX.Columns.Add("EstatusActivo");
                tablaDatosBancariosMX.Columns.Add("DatoBancarioMXRevisado");


                foreach (var i in ListaDatosBancariosMX)
                {
                    if (i.EstatusActivo)
                    {
                        tablaDatosBancariosMX.Rows.Add(new object[] {i.BancoMXid, i.EsPreferencia ? "Preferente" : "No preferente",
                        i.PrioridadDeUso, i.NombreBancoDestino, i.CLABE, i.NumeroCuentaDestinatario, i.Sucursal, i.DivisaAPagar,  i.EstatusActivo,
                        i.Revisado ? "Revisado" : "No Revisado"});
                    }
                }

                dataGridViewDatosBancariosMX.AutoGenerateColumns = false;
                dataGridViewDatosBancariosMX.DataSource = tablaDatosBancariosMX;

                //Datos Bancarios MX
                comboBoxBancoDatosBancariosMX.SelectedItem = dataGridViewDatosBancariosMX.CurrentRow.Cells[3].Value.ToString();
                txtBoxCLABEDatosBancariosMX.Text = dataGridViewDatosBancariosMX.CurrentRow.Cells[4].Value.ToString();
                txtBoxNumeroCuentaDestinatarioMX.Text = dataGridViewDatosBancariosMX.CurrentRow.Cells[5].Value.ToString();
                txtBoxSucursalDatosBancariosMX.Text = dataGridViewDatosBancariosMX.CurrentRow.Cells[6].Value.ToString();
                comboBoxDivisaCuentaBancariaMX.SelectedItem = dataGridViewDatosBancariosMX.CurrentRow.Cells[7].Value.ToString();

                if (dataGridViewDatosBancariosMX.CurrentRow.Cells[1].Value.ToString() == "Preferente")
                    lblCuentaDePreferenciaClienteMX.Visible = true;
                else
                    lblCuentaDePreferenciaClienteMX.Visible = false;

                if (dataGridViewDatosBancariosMX.CurrentRow.Cells[9].Value.ToString() == "Revisado")
                    checkBoxDatoBancarioItem.Checked = true;
                else
                    checkBoxDatoBancarioItem.Checked = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }

        }
        private void CargarContactosDatosGenerales(string claveP)
        {
            try
            {                
                LimpiarContactos();
                checkBoxContactosRevisado.Checked = eProveedorDatosPrim.isContactosRevisado;
                ListaContactos = proveedorContactosBol.consultarContactosByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                if (ListaContactos.Count == 0)
                    return;

                DataTable tablaContactos = new DataTable();
                tablaContactos.Columns.Add("Contactoid");
                tablaContactos.Columns.Add("PrioridadDeUso");
                tablaContactos.Columns.Add("NombreCompleto");
                tablaContactos.Columns.Add("Puesto");
                tablaContactos.Columns.Add("Categoria");
                tablaContactos.Columns.Add("FuncionesContacto");
                tablaContactos.Columns.Add("TelefonoPrimario");
                tablaContactos.Columns.Add("ExtensionTelefonoPrimario");
                tablaContactos.Columns.Add("TelefonoSecundario");
                tablaContactos.Columns.Add("ExtensionTelefonoSecundario");
                tablaContactos.Columns.Add("Celular1");
                tablaContactos.Columns.Add("Celular2");
                tablaContactos.Columns.Add("Email1");
                tablaContactos.Columns.Add("Email2");
                tablaContactos.Columns.Add("Comentarios");
                tablaContactos.Columns.Add("ContactoRevisado");

                foreach (var i in ListaContactos)
                {
                    if (i.EstatusActivo)
                    {
                        tablaContactos.Rows.Add(new object[] { i.Contactoid, i.PrioridadDeUso, i.NombreCompleto, i.Puesto,
                    i.Categoria, i.FuncionesContacto, i.TelefonoPrimario, i.ExtensionTelefonoPrimario, i.TelefonoSecundario,
                    i.ExtensionTelefonoSecundario, i.Celular1, i.Celular2, i.Email1, i.Email2, i.Comentarios, i.Revisado ? "Revisado" : "No Revisado"});
                    }
                }

                dataGridContactos.AutoGenerateColumns = false;
                dataGridContactos.DataSource = tablaContactos;

                //Contactos
                txtBoxNombreContactos.Text = dataGridContactos.CurrentRow.Cells[2].Value.ToString();
                txtBoxPuestoContactos.Text = dataGridContactos.CurrentRow.Cells[3].Value.ToString();
                txtBoxTel1Contactos.Text = dataGridContactos.CurrentRow.Cells[6].Value.ToString();
                txtBoxExt1Contactos.Text = dataGridContactos.CurrentRow.Cells[7].Value.ToString();
                txtBoxTel2Contactos.Text = dataGridContactos.CurrentRow.Cells[8].Value.ToString();
                txtBoxExt2Contactos.Text = dataGridContactos.CurrentRow.Cells[9].Value.ToString();
                txtBoxCel1Contactos.Text = dataGridContactos.CurrentRow.Cells[10].Value.ToString();
                txtBoxCel2Contactos.Text = dataGridContactos.CurrentRow.Cells[11].Value.ToString();
                txtBoxEmail1Contactos.Text = dataGridContactos.CurrentRow.Cells[12].Value.ToString();
                txtBoxEmail2Contactos.Text = dataGridContactos.CurrentRow.Cells[13].Value.ToString();
                txtBoxCategoriaContactos.Text = dataGridContactos.CurrentRow.Cells[4].Value.ToString();
                txtBoxComentariosContacto.Text = dataGridContactos.CurrentRow.Cells[14].Value.ToString();
                comboBoxFuncionContacto.SelectedItem = dataGridContactos.CurrentRow.Cells[5].Value.ToString();
                if (dataGridContactos.CurrentRow.Cells[15].Value.ToString() == "Revisado")
                    checkBoxContactoItem.Checked = true;
                else
                    checkBoxContactoItem.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void CargarDireccionesDatosGenerales(string claveP)
        {
            try
            {
                LimpiarDirecciones();
                checkBoxDireccionesRevisado.Checked = eProveedorDatosPrim.isDireccionesRevisado;
                ListaDirecciones = proveedorDireccionesBol.consultarDireccionesByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));
                                
                if (ListaDirecciones.Count == 0)
                    return;

                DataTable tablaDirecciones = new DataTable();
                tablaDirecciones.Columns.Add("PrioridadUso");
                tablaDirecciones.Columns.Add("ConceptoUso");
                tablaDirecciones.Columns.Add("NumIdDireccion");
                tablaDirecciones.Columns.Add("CalleAveBlvr");
                tablaDirecciones.Columns.Add("NumExterior");
                tablaDirecciones.Columns.Add("NumInterior");
                tablaDirecciones.Columns.Add("InfAdicional");
                tablaDirecciones.Columns.Add("Colonia");
                tablaDirecciones.Columns.Add("CodigoPostal");
                tablaDirecciones.Columns.Add("Poblacion");
                tablaDirecciones.Columns.Add("Estado");
                tablaDirecciones.Columns.Add("Pais");
                tablaDirecciones.Columns.Add("DireccionRevisada");
                tablaDirecciones.Columns.Add("Dirección");
                tablaDirecciones.Columns.Add("Ubicación");

                foreach (var i in ListaDirecciones)
                {
                    if (i.EstatusActivo)
                    {
                        tablaDirecciones.Rows.Add(new object[] { i.PrioridadDeUso, i.ConceptoUso, i.NumIdDireccion, i.CalleAveBlvr, i.NumExterior, i.NumInterior,
                         i.InfAdicional, i.Colonia, i.CodigoPostal, i.Poblacion, i.Estado, i.Pais, i.Revisado ? "Revisado" : "No Revisado",
                        (i.CalleAveBlvr + "# " + (i.NumExterior == "" ? i.NumInterior + ", " : i.NumExterior + " " + i.NumInterior + ", ")),
                        (i.Poblacion + ", " + i.Estado + ", " + i.Pais) });
                    }
                }

                dataGridDirecciones.AutoGenerateColumns = false;
                dataGridDirecciones.DataSource = tablaDirecciones;


                comboBoxConceptoUsoDirecciones.SelectedItem = dataGridDirecciones.CurrentRow.Cells[2].Value.ToString();
                txtBoxDireccionDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[3].Value.ToString();
                txtBoxNumExteriorDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[4].Value.ToString();
                txtBoxNumInteriorDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[5].Value.ToString();
                txtBoxColoniaDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[7].Value.ToString();
                txtBoxInfAdicionalDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[6].Value.ToString();
                txtBoxCPDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[8].Value.ToString();
                txtBoxPoblacionDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[9].Value.ToString();
                txtBoxEstadoDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[10].Value.ToString();
                txtBoxPaisDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[11].Value.ToString();
                if (dataGridDirecciones.CurrentRow.Cells[12].Value.ToString() == "Revisado")
                    checkBoxDireccionItem.Checked = true;
                else
                    checkBoxDireccionItem.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void CargarDatosGenerales(string claveP)
        {
            try
            {
                LimpiarDatosGenerales();
                eProveedorDatosPrim = proveedorDatosPrimBol.consultarProveedorDatosPrimByClaveProveedorVal(eProveedorDatosPrim.ClaveProveedor);
                eProveedorProv = proveedorProvBol.consultarPaginaWebByClave(eProveedorDatosPrim.ClaveProveedor);

                //Datos Primarios
                txtBoxClaveDatosPrimarios.Text = Convert.ToString(eProveedorDatosPrim.ClaveProveedor);
                txtBoxProveedorDatosPrimarios.Text = Convert.ToString(eProveedorDatosPrim.NombreProveedor);
                txtBoxRFCDatosPrimarios.Text = Convert.ToString(eProveedorDatosPrim.RFC);
                txtBoxCategoriaDatosPrimarios.Text = Convert.ToString(eProveedorDatosPrim.Categoria);
                txtBoxTipoProveedor.Text = Convert.ToString(eProveedorDatosPrim.TipoProveedor);
                dateTimePickerDatosGen.Value = eProveedorDatosPrim.fechaUltimaActualizacion;
                if (eProveedorDatosPrim.PATHImagen != "")
                    picBoxLogoProveedor.ImageLocation = Convert.ToString(eProveedorDatosPrim.PATHImagen);
                txtBoxPaginaWebProveedorDatosPrimarios.Text = Convert.ToString(eProveedorProv.DirInternet);

                CargarDireccionesDatosGenerales(claveP);
                CargarContactosDatosGenerales(claveP);
                CargarCuentasBancariasMXDatosGenerales(claveP);
                CargarCuentasBancariasEXDatosGenerales(claveP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarPoliticas()
        {
            try
            {
                checkBoxPoliticasRevisado.Checked = false;
                picBoxLogoPoliticas.ImageLocation = "";
                txtBoxProveedorPoliticas.Clear();
                txtBoxClavePoliticas.Clear();
                txtBoxRFCPoliticas.Clear();
                txtBoxCategoriaPoliticas.Clear();
                dateTimePickerPoliticas.Value = DateTime.Today;
                txtBoxConvenioCompraPoliticas.Clear();
                txtBoxArchivosConvenioCompraPoliticas.Clear();
                comboBoxListaConveniosCompra.SelectedIndex = -1;
                comboBoxListaConveniosCompra.Items.Clear();
                comboBoxListaConveniosCompra.Text = "";
                txtBoxCompraMinPoliticas.Clear();
                radioBtnDiarioPoliticas.Checked = false;
                radioBtnSemanalPoliticas.Checked = false;
                radioBtnMensualPoliticas.Checked = false;
                radioBtnOtroPoliticas.Checked = false;
                txtBoxObservacionesSolicitudes.Clear();
                txtBoxListaPoliticasDevoluciones.Clear();
                comboBoxListaPoliticasDevoluciones.SelectedIndex = -1;
                comboBoxListaPoliticasDevoluciones.Items.Clear();
                comboBoxListaPoliticasDevoluciones.Text = "";
                txtBoxListaPoliticasGarantias.Clear();
                comboBoxListaPoliticasGarantias.SelectedIndex = -1;
                comboBoxListaPoliticasGarantias.Items.Clear();
                comboBoxListaPoliticasGarantias.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarExpediente()
        {
            try
            {
                checkBoxExpedienteRevisado.Checked = false;
                txtBoxClaveExpediente.Clear();
                txtBoxProveedorExpediente.Clear();
                txtBoxRFCExpediente.Clear();
                txtBoxCategoriaExpediente.Clear();
                dateTimePickerExpediente.Value = DateTime.Today;
                picBoxLogoExpediente.ImageLocation = "";
                checkBoxContrato.Checked = false;
                checkBoxPoderRepLegal.Checked = false;
                checkBoxIdRepLegal.Checked = false;
                checkBoxComprabanteDomicilio.Checked = false;
                checkBoxCaratatulaEdoCuenta.Checked = false;
                checkBoxAvisoPrivacidad.Checked = false;
                checkBoxCedulaRFC.Checked = false;
                checkBoxPagare.Checked = false;
                comboBoxListaContratoExpediente.SelectedIndex = -1;
                comboBoxListaContratoExpediente.Items.Clear();
                comboBoxListaContratoExpediente.Text = "";
                comboBoxListaPRLExpediente.SelectedIndex = -1;
                comboBoxListaPRLExpediente.Items.Clear();
                comboBoxListaPRLExpediente.Text = "";
                comboBoxListaIRLExpediente.SelectedIndex = -1;
                comboBoxListaIRLExpediente.Items.Clear();
                comboBoxListaIRLExpediente.Text = "";
                comboBoxListaComprobanteDomicilioExpediente.SelectedIndex = -1;
                comboBoxListaComprobanteDomicilioExpediente.Items.Clear();
                comboBoxListaComprobanteDomicilioExpediente.Text = "";
                comboBoxListaCedulaRFCExpediente.SelectedIndex = -1;
                comboBoxListaCedulaRFCExpediente.Items.Clear();
                comboBoxListaCedulaRFCExpediente.Text = "";
                comboBoxListaCaratulaEdoCuentaExpediente.SelectedIndex = -1;
                comboBoxListaCaratulaEdoCuentaExpediente.Items.Clear();
                comboBoxListaCaratulaEdoCuentaExpediente.Text = "";
                comboBoxListaAvisoPrivacidadExpediente.SelectedIndex = -1;
                comboBoxListaAvisoPrivacidadExpediente.Items.Clear();
                comboBoxListaAvisoPrivacidadExpediente.Text = "";
                comboBoxListaPagare.SelectedIndex = -1;
                comboBoxListaPagare.Items.Clear();
                comboBoxListaPagare.Text = "";
                dateTimePickerFechaActualizacionContrato.Value = DateTime.Today;
                dateTimePickerFechaVencimientoContrato.Value = DateTime.Today;
                dateTimePickerFechaActualizacionPRL.Value = DateTime.Today;
                dateTimePickerFechaVencimientoPRL.Value = DateTime.Today;
                dateTimePickerFechaActualizacionIRL.Value = DateTime.Today;
                dateTimePickerFechaVencimientoIRL.Value = DateTime.Today;
                dateTimePickerFechaActualizacionComprobanteDomicilio.Value = DateTime.Today;
                dateTimePickerFechaVencimientoComprobanteDomicilio.Value = DateTime.Today;
                dateTimePickerFechaActualizacionCaratulaEstadoCuenta.Value = DateTime.Today;
                dateTimePickerFechaVencimientoCaratulaEstadoCuenta.Value = DateTime.Today;
                dateTimePickerFechaActualizacionAvisoPrivacidad.Value = DateTime.Today;
                dateTimePickerFechaVencimientoAvisoPrivacidad.Value = DateTime.Today;
                dateTimePickerFechaActualizacionCedulaRFC.Value = DateTime.Today;
                dateTimePickerFechaVencimientoCedulaRFC.Value = DateTime.Today;
                dateTimePickerFechaActualizacionPagare.Value = DateTime.Today;
                dateTimePickerFechaVencimientoPagare.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarFletes()
        {
            try
            {                
                lblFletePreferenciaFletes.Visible = false;                
                txtBoxClaveProveedorFletes.Clear();
                txtBoxNombreFleteCondiciones.Clear();
                comboBoxFormaEntregaFletes.SelectedIndex = -1;
                comboBoxFormaEntregaFletes.Text = "";
                comboBoxTipoEnvioFletes.SelectedIndex = -1;
                comboBoxTipoEnvioFletes.Text = "";
                comboBoxTransporteEnvioFletes.SelectedIndex = -1;
                comboBoxTransporteEnvioFletes.Text = "";
                comboBoxCargoEntregaFletes.SelectedIndex = -1;
                comboBoxCargoEntregaFletes.Text = "";
                txtBoxPedidoMinCondiciones.Clear();
                txtBoxPedidoMaxCondiciones.Clear();
                comboBoxUnidadPedidoCondiciones.SelectedIndex = -1;
                comboBoxUnidadPedidoCondiciones.Text = "";
                txtBoxFleteOrigenCondiciones.Clear();
                txtBoxFleteDestinoCondiciones.Clear();                
                txtBoxObservacionesFletes.Clear();                
                txtBoxCantidadCostoSucursalMatrizFletes.Clear();
                txtBoxCantidadCostoSucursalHipodromoFletes.Clear();
                txtBoxCantidadCostoSucursalSanPedroFletes.Clear();
                txtBoxCantidadCostoSucursalMagdalenaFletes.Clear();
                txtBoxCantidadCostoSucursalCabrocaFletes.Clear();
                txtBoxCantidadCostoSucursalCEDISFletes.Clear();
                txtBoxCantidadCostoSucursalCMAletes.Clear();
                dataGridFletes.DataSource = null;
                dataGridFletes.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarPlazos()
        {
            try
            {
                dataGridPlazosCredito.DataSource = null;
                dataGridPlazosCredito.Rows.Clear();
                checkBoxCondicionesItem.Checked = false;
                checkBoxTieneProntoPago.Checked = false;
                txtBoxCondicionesCreditoCondiciones.Clear();
                txtBoxDefinicionPlazoCredito.Clear();
                txtBoxProntoPago1Dias.Clear();
                comboBoxDescProntoPago1.SelectedIndex = -1;
                comboBoxDescProntoPago1.Text = "";
                comboBoxVencimientoPagoFactura1.SelectedIndex = -1;
                comboBoxVencimientoPagoFactura1.Text = "";
                txtBoxObservacionesProntoPago1.Clear();
                txtBoxProntoPago2Dias.Clear();
                comboBoxDescProntoPago2.SelectedIndex = -1;
                comboBoxDescProntoPago2.Text = "";
                comboBoxVencimientoPagoFactura2.SelectedIndex = -1;
                comboBoxVencimientoPagoFactura2.Text = "";
                txtBoxObservacionesProntoPago2.Clear();
                txtBoxProntoPago3Dias.Clear();
                comboBoxDescProntoPago3.SelectedIndex = -1;
                comboBoxDescProntoPago3.Text = "";
                comboBoxVencimientoPagoFactura3.SelectedIndex = -1;
                comboBoxVencimientoPagoFactura3.Text = "";
                txtBoxObservacionesProntoPago3.Clear();
                txtBoxProntoPago4Dias.Clear();
                comboBoxDescProntoPago4.SelectedIndex = -1;
                comboBoxDescProntoPago4.Text = "";
                comboBoxVencimientoPagoFactura4.SelectedIndex = -1;
                comboBoxVencimientoPagoFactura4.Text = "";
                txtBoxObservacionesProntoPago4.Clear();
                txtBoxProntoPago5Dias.Clear();
                comboBoxDescProntoPago5.SelectedIndex = -1;
                comboBoxDescProntoPago5.Text = "";
                comboBoxVencimientoPagoFactura5.SelectedIndex = -1;
                comboBoxVencimientoPagoFactura5.Text = "";
                txtBoxObservacionesProntoPago5.Clear();
                txtBoxObservacionesGeneralesPlazo.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarCondiciones()
        {
            try
            {
                //tab Condiciones
                checkBoxCondicionesRevisado.Checked = false;
                txtBoxClaveCondiciones.Clear();
                txtBoxProveedorCondiciones.Clear();
                txtBoxRFCCondiciones.Clear();
                txtBoxCategoriaCondiciones.Clear();
                dateTimePickerCondiciones.Value = DateTime.Today;
                picBoxLogoCondiciones.ImageLocation = "";
                LimpiarPlazos();
                checkBoxAereoCondiciones.Checked = false;
                checkBoxTerrestreCondiciones.Checked = false;
                checkBoxCourierCondiciones.Checked = false;
                checkBoxDomicilio.Checked = false;
                checkBoxEntregaPersonal.Checked = false;
                checkBoxPaqueteria.Checked = false;
                checkBoxTransporteCarga.Checked = false;
                checkBoxPagada.Checked = false;
                checkBoxPorCobrar.Checked = false;
                checkBoxMatriz.Checked = false;
                checkBoxHipodromo.Checked = false;
                checkBoxSanPedro.Checked = false;
                checkBoxMagdalena.Checked = false;
                checkBoxCaborca.Checked = false;
                checkBoxCEDIS.Checked = false;
                checkBoxCMA.Checked = false;
                txtBoxTiempoEntregaCondiciones.Clear();
                txtBoxObservacionesCondiciones.Clear();
                comboBoxListaObservacionesFormaEntrega.SelectedIndex = -1;
                comboBoxListaObservacionesFormaEntrega.Text = "";
                dataGridLimiteCapacidad.DataSource = null;
                dataGridLimiteCapacidad.Rows.Clear();
                txtBoxCondicionesEspecialesCondiciones.Clear();
                LimpiarFletes();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarAcuerdos()
        {
            try
            {
                checkBoxAcuerdosRevisado.Checked = false;
                txtBoxClaveAcuerdos.Clear();
                txtBoxProveedorAcuerdos.Clear();
                txtBoxRFCAcuerdos.Clear();
                txtBoxCategoriaAcuerdos.Clear();
                dateTimePickerAcuerdos.Value = DateTime.Today;
                picBoxLogoAcuerdos.ImageLocation = "";
                textBoxAcuerdoCompra.Clear();
                comboBoxListaAcuerdosDeCompra.SelectedIndex = -1;
                comboBoxListaAcuerdosDeCompra.Text = "";
                txtBoxAcuerdoVentaPublico.Clear();
                comboBoxListaAcuerdosVentaPublico.SelectedIndex = -1;
                comboBoxListaAcuerdosVentaPublico.Text = "";
                txtBoxAcuerdoAtencionClientes.Clear();
                comboBoxListaAcuerdosAtencionClientes.SelectedIndex = -1;
                comboBoxListaAcuerdosAtencionClientes.Text = "";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarDatosBancariosEX()
        {
            try
            {
                //checkBoxDatosBancariosEXRevisado.Checked = false;
                checkBoxDatoBancarioEXItem.Checked = false;
                dataGridDatosBancariosEX.DataSource = null;
                dataGridDatosBancariosEX.Rows.Clear();
                txtBoxNombreBancoDestinoEX.Clear();
                txtBoxClaveBancoDestinoEX.Clear();
                txtBoxNumeroCuentaDestinatarioEX.Clear();
                txtBoxNombreDestinatarioEX.Clear();
                txtBoxMontoMaxEX.Clear();
                comboBoxDivisaCuentaBancariaEX.SelectedIndex = -1;
                txtBoxNombreBancoIntermediarioEX.Clear();
                txtBoxClaveBancoIntermediarioEX.Clear();
                radioBtnSi.Checked = false;
                lblFechaVigenciaEX.Visible = true;
                dateTimePickerDatosBancariosEX.Visible = true;
                dateTimePickerDatosBancariosEX.Value = DateTime.Today;
                txtBoxTipoRelacionConDestinatarioEX.Clear();
                txtBoxMotivoPagoEX.Clear();

                txtBoxCalleAveBlvrDatosBancariosEX.Clear();
                txtBoxNumExteriorDatosBancariosEX.Clear();
                txtBoxNumInteriorDatosBancariosEX.Clear();
                txtBoxColoniaDatosBancariosEX.Clear();
                txtBoxInfAdicionalDatosBancariosEX.Clear();
                txtBoxCPDatosBancariosEX.Clear();
                txtBoxPoblacionDatosBancariosEX.Clear();
                txtBoxEstadoDatosBancariosEX.Clear();
                txtBoxPaisDatosBancariosEX.Clear();
                lblCuentaDePreferenciaClienteEX.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarDatosBancariosMX()
        {
            try
            {
                //checkBoxDatosBancariosMXRevisado.Checked = false;
                checkBoxDatoBancarioItem.Checked = false;
                lblCuentaDePreferenciaClienteMX.Visible = false;     
                comboBoxBancoDatosBancariosMX.SelectedIndex = -1;
                comboBoxBancoDatosBancariosMX.Text = "";
                txtBoxNumeroCuentaDestinatarioMX.Clear();
                txtBoxCLABEDatosBancariosMX.Clear();
                txtBoxSucursalDatosBancariosMX.Clear();
                comboBoxDivisaCuentaBancariaMX.SelectedIndex = -1;
                comboBoxDivisaCuentaBancariaMX.Text = "";
                dataGridViewDatosBancariosMX.DataSource = null;
                dataGridViewDatosBancariosMX.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarContactos()
        {
            try
            {
                //checkBoxContactosRevisado.Checked = false;
                checkBoxContactoItem.Checked = false;
                dataGridContactos.DataSource = null;
                dataGridContactos.Rows.Clear();
                comboBoxFuncionContacto.SelectedIndex = -1;
                comboBoxFuncionContacto.Text = "";
                txtBoxCategoriaContactos.Clear();
                txtBoxNombreContactos.Clear();
                txtBoxPuestoContactos.Clear();
                txtBoxTel1Contactos.Clear();
                txtBoxExt1Contactos.Clear();
                txtBoxTel2Contactos.Clear();
                txtBoxExt2Contactos.Clear();
                txtBoxCel1Contactos.Clear();
                txtBoxCel2Contactos.Clear();
                txtBoxEmail1Contactos.Clear();
                txtBoxEmail2Contactos.Clear();
                txtBoxComentariosContacto.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarDirecciones()
        {
            try
            {
                //checkBoxDireccionesRevisado.Checked = false;
                checkBoxDireccionItem.Checked = false;
                comboBoxConceptoUsoDirecciones.SelectedIndex = -1;
                comboBoxConceptoUsoDirecciones.Text = "";
                dataGridDirecciones.DataSource = null;
                dataGridDirecciones.Rows.Clear();
                txtBoxDireccionDirecciones.Clear();
                txtBoxNumExteriorDirecciones.Clear();
                txtBoxNumInteriorDirecciones.Clear();
                txtBoxColoniaDirecciones.Clear();
                txtBoxInfAdicionalDirecciones.Clear();
                txtBoxCPDirecciones.Clear();
                txtBoxPoblacionDirecciones.Clear();
                txtBoxEstadoDirecciones.Clear();
                txtBoxPaisDirecciones.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarDatosGenerales()
        {
            try
            {
                txtBoxClaveDatosPrimarios.Clear();
                txtBoxProveedorDatosPrimarios.Clear();
                txtBoxRFCDatosPrimarios.Clear();
                txtBoxCategoriaDatosPrimarios.Clear();
                txtBoxTipoProveedor.Clear();
                dateTimePickerDatosGen.Value = DateTime.Today;
                picBoxLogoProveedor.ImageLocation = "";
                txtBoxPaginaWebProveedorDatosPrimarios.Clear();

                LimpiarDirecciones();
                LimpiarContactos();
                LimpiarDatosBancariosMX();
                LimpiarDatosBancariosEX();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void LimpiarUbicacionDeArchivos()
        {
            try
            {
                comboBoxListaConveniosCompra.Items.Clear();
                comboBoxListaConveniosCompra.SelectedIndex = -1;
                comboBoxListaPoliticasDevoluciones.Items.Clear();
                comboBoxListaPoliticasDevoluciones.SelectedIndex = -1;
                comboBoxListaPoliticasGarantias.Items.Clear();
                comboBoxListaPoliticasGarantias.SelectedIndex = -1;
                comboBoxListaAcuerdosDeCompra.Items.Clear();
                comboBoxListaAcuerdosDeCompra.SelectedIndex = -1;
                comboBoxListaAcuerdosVentaPublico.Items.Clear();
                comboBoxListaAcuerdosVentaPublico.SelectedIndex = -1;
                comboBoxListaAcuerdosAtencionClientes.Items.Clear();
                comboBoxListaAcuerdosAtencionClientes.SelectedIndex = -1;
                comboBoxListaObservacionesFormaEntrega.Items.Clear();
                comboBoxListaObservacionesFormaEntrega.SelectedIndex = -1;
                comboBoxListaContratoExpediente.Items.Clear();
                comboBoxListaContratoExpediente.SelectedIndex = -1;
                comboBoxListaPRLExpediente.Items.Clear();
                comboBoxListaPRLExpediente.SelectedIndex = -1;
                comboBoxListaIRLExpediente.Items.Clear();
                comboBoxListaIRLExpediente.SelectedIndex = -1;
                comboBoxListaComprobanteDomicilioExpediente.Items.Clear();
                comboBoxListaComprobanteDomicilioExpediente.SelectedIndex = -1;
                comboBoxListaCedulaRFCExpediente.Items.Clear();
                comboBoxListaCedulaRFCExpediente.SelectedIndex = -1;
                comboBoxListaCaratulaEdoCuentaExpediente.Items.Clear();
                comboBoxListaCaratulaEdoCuentaExpediente.SelectedIndex = -1;
                comboBoxListaAvisoPrivacidadExpediente.Items.Clear();
                comboBoxListaAvisoPrivacidadExpediente.SelectedIndex = -1;
                comboBoxListaPagare.Items.Clear();
                comboBoxListaPagare.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }   
        }        
        private void DelegarUbicacionDeArchivos(string claveP)
        {
            try
            {
                LimpiarUbicacionDeArchivos();
                ListaUbicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosByClave(claveP);
                foreach (var i in ListaUbicacionArchivos)
                {
                    if (!i.EstatusActivo)
                        continue;
                    if (i.CategoriaArchivo == "Convenios de Compra")
                        comboBoxListaConveniosCompra.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Políticas para Devoluciones")
                        comboBoxListaPoliticasDevoluciones.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Políticas de Garantías")
                        comboBoxListaPoliticasGarantias.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Acuerdo de Compra")
                        comboBoxListaAcuerdosDeCompra.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Acuerdo para Venta al Público")
                        comboBoxListaAcuerdosVentaPublico.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Acuerdo para Atención a Clientes")
                        comboBoxListaAcuerdosAtencionClientes.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Observaciones en Tiempo y Forma de Entrega")
                        comboBoxListaObservacionesFormaEntrega.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Contrato")
                        comboBoxListaContratoExpediente.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Poder Respresentante Legal")
                        comboBoxListaPRLExpediente.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Identificación Representante Legal")
                        comboBoxListaIRLExpediente.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Comprobante Domicilio")
                        comboBoxListaComprobanteDomicilioExpediente.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Cédula RFC")
                        comboBoxListaCedulaRFCExpediente.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Carátula de Estado de Cuenta")
                        comboBoxListaCaratulaEdoCuentaExpediente.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Aviso de Privacidad")
                        comboBoxListaAvisoPrivacidadExpediente.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                    else if (i.CategoriaArchivo == "Pagare")
                        comboBoxListaPagare.Items.Add(i.UbicacionArchivoid + " " + Path.GetFileName(i.PATHArchivo));
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }            
        }        
        private string copiarArchivoEnServidor(string claveProveedor, string PATHarchivo)
        {
            try
            {
                string fileName = Path.GetFileName(PATHarchivo);                
                string sourceFile = PATHarchivo;
                string targetPATH = ConfigurationManager.AppSettings["FolderCatalogoDeProveedores"].ToString() + "\\" + claveProveedor;
                string destFile = targetPATH + "\\" + fileName;

                System.IO.Directory.CreateDirectory(targetPATH);
                System.IO.File.Copy(sourceFile, destFile, true);
                return destFile;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        private void CargarUltimaActualizacion(string claveProveedor)
        {
            eProveedorDatosPrim = proveedorDatosPrimBol.consultarProveedorDatosPrimByClaveProveedorVal(claveProveedor);
            dateTimePickerDatosGen.Value = eProveedorDatosPrim.fechaUltimaActualizacion;
            dateTimePickerPoliticas.Value = eProveedorDatosPrim.fechaUltimaActualizacion;            
            dateTimePickerAcuerdos.Value = eProveedorDatosPrim.fechaUltimaActualizacion;
            dateTimePickerCondiciones.Value = eProveedorDatosPrim.fechaUltimaActualizacion;
            dateTimePickerExpediente.Value = eProveedorDatosPrim.fechaUltimaActualizacion;
        }
        #endregion
        private void linkLabelCatálogoProductos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://bit.ly/39MIBUf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabelTableroRevision_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://app.powerbi.com/view?r=eyJrIjoiYzI0OTQ1MTItNDU2Mi00MDBiLWIyMzYtNjk1MzIwYzFmNzYxIiwidCI6IjU3MDMzZTVjLWRmNTktNDZhZi1hOWJkLTY1ZjdlYTFhNGFiNSJ9");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void btnVerNotificaciones_Click(object sender, EventArgs e)
        {
            //Cargar Panel de Notificaciones
            try
            {
                notificaciones.Show();
                MessageBox.Show(string.Format("En Proceso ..."),
                "Función no implementada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (eProveedorDatosPrim != null)
                {
                    notificaciones.ClaveProveedor = eProveedorDatosPrim.ClaveProveedor;
                    notificaciones.NombreProveedor = eProveedorDatosPrim.NombreProveedor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //Seccion Datos Primarios Generales
        private void btnBuscarClave_Click(object sender, EventArgs e)
        {
            try
            {
                eProveedorDatosPrim = proveedorDatosPrimBol.consultarProveedorDatosPrimByClaveProveedorVal(txtBoxClaveDatosPrimarios.Text);

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("No se ha encontrado información con Clave " + txtBoxClaveDatosPrimarios.Text + ". Proporcione una Clave de Proveedor válida.");
                    return;
                }             //cargarDatosPorUsuario(eProveedorUsuario.Usuario);
                DelegarUbicacionDeArchivos(eProveedorDatosPrim.ClaveProveedor);
                CargarDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                CargarAcuerdos(eProveedorDatosPrim.ClaveProveedor);
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                CargarPoliticas(eProveedorDatosPrim.ClaveProveedor);
                CargarCondiciones(eProveedorDatosPrim.ClaveProveedor);
                notificaciones = new frmNotificaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void txtBoxClaveDatosPrimarios_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    btnBuscarClave_Click(btnBuscarClave, new EventArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                List<EProveedorDatosPrimarios> ListaProveedorDatosPrim = proveedorDatosPrimBol.consultarProveedorDatosPrimByNombreProveedorVal(txtBoxProveedorDatosPrimarios.Text);

                if (ListaProveedorDatosPrim == null)
                {
                    MessageBox.Show(Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP));
                    return;
                }

                if (ListaProveedorDatosPrim.Count > 0)
                {
                    DataTable TablaProveedorDatosPrim = new DataTable();
                    TablaProveedorDatosPrim.Columns.Add("Clave");
                    TablaProveedorDatosPrim.Columns.Add("Nombre");
                    TablaProveedorDatosPrim.Columns.Add("RFC");
                    TablaProveedorDatosPrim.Columns.Add("Categoria");
                    foreach (var i in ListaProveedorDatosPrim)
                    {
                        TablaProveedorDatosPrim.Rows.Add(new object[] { i.ClaveProveedor, i.NombreProveedor, i.RFC, i.Categoria });
                    }

                    TablaResultadosProveedor Resultados = new TablaResultadosProveedor();
                    Resultados.dataGridProveedores.AutoGenerateColumns = false;
                    Resultados.dataGridProveedores.DataSource = TablaProveedorDatosPrim;
                    if (Resultados.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxClaveDatosPrimarios.Text = Resultados.valorClaveProveedor;
                        btnBuscarClave_Click(this, e);
                    }
                }
                else
                    MessageBox.Show("Proveedor con nombre " + txtBoxProveedorDatosPrimarios.Text + " no existe.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void btnBuscarRFC_Click(object sender, EventArgs e)
        {
            try
            {
                List<EProveedorDatosPrimarios> ListaProveedorDatosPrim = proveedorDatosPrimBol.consultarProveedorDatosPrimByRFCProveedorVal(txtBoxRFCDatosPrimarios.Text);
                if (ListaProveedorDatosPrim == null)
                {
                    MessageBox.Show(Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP));
                    return;
                }

                if (ListaProveedorDatosPrim.Count > 0)
                {
                    DataTable TablaProveedorDatosPrim = new DataTable();
                    TablaProveedorDatosPrim.Columns.Add("Clave");
                    TablaProveedorDatosPrim.Columns.Add("Nombre");
                    TablaProveedorDatosPrim.Columns.Add("RFC");
                    TablaProveedorDatosPrim.Columns.Add("Categoria");
                    foreach (var i in ListaProveedorDatosPrim)
                    {
                        TablaProveedorDatosPrim.Rows.Add(new object[] { i.ClaveProveedor, i.NombreProveedor, i.RFC, i.Categoria });
                    }

                    TablaResultadosProveedor Resultados = new TablaResultadosProveedor();
                    Resultados.dataGridProveedores.AutoGenerateColumns = false;
                    Resultados.dataGridProveedores.DataSource = TablaProveedorDatosPrim;
                    if (Resultados.ShowDialog() == DialogResult.OK)
                    {
                        txtBoxClaveDatosPrimarios.Text = Resultados.valorClaveProveedor;
                        btnBuscarClave_Click(this, e);
                    }
                }
                else
                    MessageBox.Show("Proveedor con RFC " + txtBoxRFCDatosPrimarios.Text + " no existe.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void btnLimpiarCamposDatosPrim_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxClaveDatosPrimarios.Text = "";
                txtBoxProveedorDatosPrimarios.Text = "";
                txtBoxRFCDatosPrimarios.Text = "";
                txtBoxCategoriaDatosPrimarios.Text = "";
                txtBoxTipoProveedor.Text = "";
                picBoxLogoProveedor.ImageLocation = "";
                txtBoxPaginaWebProveedorDatosPrimarios.Clear();
                dateTimePickerDatosGen.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void btnLimpiarCamposDirecciones_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxDireccionDirecciones.Text = "";
                txtBoxInfAdicionalDirecciones.Text = "";
                txtBoxNumExteriorDirecciones.Text = "";
                txtBoxNumInteriorDirecciones.Text = "";
                txtBoxColoniaDirecciones.Text = "";
                txtBoxCPDirecciones.Text = "";
                txtBoxPoblacionDirecciones.Text = "";
                txtBoxEstadoDirecciones.Text = "";
                txtBoxPaisDirecciones.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            try
            {                
                string categoriaDato = "Imagen";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| jpeg files(*.jpeg)|*.jpeg| PNG files(*.png)|*.png| All files(*.*)|*.*";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorDatosPrimBol.editarImagenDatosPrimVal(pathLocation, eProveedorDatosPrim.ClaveProveedor))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }  
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Agregar";
                string categoriaDato = "Dirección";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxConceptoUsoDirecciones.SelectedItem) == "" || txtBoxDireccionDirecciones.Text == "" || txtBoxColoniaDirecciones.Text == "" ||
                txtBoxNumExteriorDirecciones.Text == "" || txtBoxCPDirecciones.Text == "" || txtBoxPoblacionDirecciones.Text == "" || txtBoxEstadoDirecciones.Text == "" ||
                txtBoxPaisDirecciones.Text == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nLos campos requeridos son: \r\n*Direccion\r\n" +
                        "*Colonia\r\n*Num Exterior\r\n*Código Postal\r\n*Población\r\n*Estado\r\n*País", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                
                EProveedorDirecciones Direccion = new EProveedorDirecciones
                {
                    ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                    PrioridadDeUso = Convert.ToInt32(2),
                    //NumIdDireccion = Convert.ToInt32(reader["NumIdDireccion"]),
                    ConceptoUso = Convert.ToString(comboBoxConceptoUsoDirecciones.SelectedItem),
                    CalleAveBlvr = Convert.ToString(txtBoxDireccionDirecciones.Text),
                    NumExterior = Convert.ToString(txtBoxNumExteriorDirecciones.Text),
                    NumInterior = Convert.ToString(txtBoxNumInteriorDirecciones.Text),
                    InfAdicional = Convert.ToString(txtBoxInfAdicionalDirecciones.Text),
                    Colonia = Convert.ToString(txtBoxColoniaDirecciones.Text),
                    CodigoPostal = Convert.ToString(txtBoxCPDirecciones.Text),
                    Poblacion = Convert.ToString(txtBoxPoblacionDirecciones.Text),
                    Estado = Convert.ToString(txtBoxEstadoDirecciones.Text),
                    Pais = Convert.ToString(txtBoxPaisDirecciones.Text),
                    EstatusActivo = Convert.ToBoolean(1)
                };               

                if (proveedorDireccionesBol.agregarDireccionProveedor(Direccion))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarDireccionesDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido agregada exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + proveedorDireccionesBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDesactivarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Desactivar";
                string categoriaDato = "Dirección";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ListaDirecciones = proveedorDireccionesBol.consultarDireccionesByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                foreach (var i in ListaDirecciones)
                {
                    if (Convert.ToInt32(dataGridDirecciones.CurrentRow.Cells[0].Value) == i.NumIdDireccion)
                    {
                        proveedorDireccionesBol.desactivarDireccionesByIdByClaveProveedorVal(i.NumIdDireccion, i.ClaveProveedor);
                        MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                        proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        CargarDireccionesDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                        CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        return;
                    }
                }
            }            
             catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridDirecciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Dirección",
                          "Seleccionar" + " " + "Dirección", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                comboBoxConceptoUsoDirecciones.SelectedItem = dataGridDirecciones.CurrentRow.Cells[2].Value.ToString();
                txtBoxDireccionDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[3].Value.ToString();
                txtBoxNumExteriorDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[4].Value.ToString();
                txtBoxNumInteriorDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[5].Value.ToString();
                txtBoxColoniaDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[7].Value.ToString();
                txtBoxInfAdicionalDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[6].Value.ToString();
                txtBoxCPDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[8].Value.ToString();
                txtBoxPoblacionDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[9].Value.ToString();
                txtBoxEstadoDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[10].Value.ToString();
                txtBoxPaisDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[11].Value.ToString();
                if (dataGridDirecciones.CurrentRow.Cells[12].Value.ToString() == "Revisado")
                    checkBoxDireccionItem.Checked = true;
                else
                    checkBoxDireccionItem.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnPriorizarDirecciones_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Priorizar";
                string categoriaDato = "Dirección";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ListaDirecciones = proveedorDireccionesBol.consultarDireccionesByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                foreach (var i in ListaDirecciones)
                {
                    if (Convert.ToInt32(dataGridDirecciones.CurrentRow.Cells[0].Value) == i.NumIdDireccion)
                    {
                        proveedorDireccionesBol.priorizarDireccionesByIdByClaveProveedorVal(i.NumIdDireccion, i.ClaveProveedor);
                        MessageBox.Show(categoriaDato + " ha sido priorizada exitosamente.", accionDato + " " + categoriaDato,
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                        proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        CargarDireccionesDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                        CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        return;
                    }
                }
            }           
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEditarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Editar";
                string categoriaDato = "Dirección";
                bool revisado = false;

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxConceptoUsoDirecciones.SelectedItem) == "" || txtBoxDireccionDirecciones.Text == "" || txtBoxColoniaDirecciones.Text == "" ||
                txtBoxNumExteriorDirecciones.Text == "" || txtBoxCPDirecciones.Text == "" || txtBoxPoblacionDirecciones.Text == "" || txtBoxEstadoDirecciones.Text == "" ||
                txtBoxPaisDirecciones.Text == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nLos campos requeridos son: \r\n*Direccion\r\n" +
                        "*Colonia\r\n*Num Exterior\r\n*Código Postal\r\n*Población\r\n*Estado\r\n*País", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (checkBoxDireccionItem.Checked)
                    revisado = true;

                EProveedorDirecciones Direccion = new EProveedorDirecciones
                {
                        ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                        //PrioridadDeUso = Convert.ToInt32(2),
                        NumIdDireccion = Convert.ToInt32(dataGridDirecciones.CurrentRow.Cells[0].Value.ToString()),
                        ConceptoUso = Convert.ToString(comboBoxConceptoUsoDirecciones.SelectedItem),
                        CalleAveBlvr = Convert.ToString(txtBoxDireccionDirecciones.Text),
                        NumExterior = Convert.ToString(txtBoxNumExteriorDirecciones.Text),
                        NumInterior = Convert.ToString(txtBoxNumInteriorDirecciones.Text),
                        InfAdicional = Convert.ToString(txtBoxInfAdicionalDirecciones.Text),
                        Colonia = Convert.ToString(txtBoxColoniaDirecciones.Text),
                        CodigoPostal = Convert.ToString(txtBoxCPDirecciones.Text),
                        Poblacion = Convert.ToString(txtBoxPoblacionDirecciones.Text),
                        Estado = Convert.ToString(txtBoxEstadoDirecciones.Text),
                        Pais = Convert.ToString(txtBoxPaisDirecciones.Text),
                        EstatusActivo = Convert.ToBoolean(1),
                        Revisado = revisado
                };               

                if (proveedorDireccionesBol.editarDireccionesByIdByClaveProveedorVal(Direccion))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarDireccionesDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido editada exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser editada. " + System.Environment.NewLine + proveedorDireccionesBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnAgregarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Agregar";
                string categoriaDato = "Contacto";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtBoxCategoriaContactos.Text == "" || txtBoxNombreContactos.Text == "" || txtBoxPuestoContactos.Text == ""
                    || txtBoxTel1Contactos.Text == "" || txtBoxEmail1Contactos.Text == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nLos campos requeridos son: \r\n*Categoría\r\n" +
                        "*Nombre Completo\r\n*Puesto\r\n*Teléfono 1\r\n*Email 1", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }   

                EProveedorContacto Contacto = new EProveedorContacto
                {
                    ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                    PrioridadDeUso = Convert.ToInt32(2),
                    //Contactoid = Convert.ToInt32(reader["Contactoid"]),
                    NombreCompleto = Convert.ToString(txtBoxNombreContactos.Text),
                    Puesto = Convert.ToString(txtBoxPuestoContactos.Text),
                    Categoria = Convert.ToString(txtBoxCategoriaContactos.Text),
                    FuncionesContacto = Convert.ToString(comboBoxFuncionContacto.SelectedItem),
                    TelefonoPrimario = Convert.ToString(txtBoxTel1Contactos.Text),
                    ExtensionTelefonoPrimario = Convert.ToString(txtBoxExt1Contactos.Text),
                    TelefonoSecundario = Convert.ToString(txtBoxTel2Contactos.Text),
                    ExtensionTelefonoSecundario = Convert.ToString(txtBoxExt2Contactos.Text),
                    Celular1 = Convert.ToString(txtBoxCel1Contactos.Text),
                    Celular2 = Convert.ToString(txtBoxCel2Contactos.Text),
                    Email1 = Convert.ToString(txtBoxEmail1Contactos.Text),
                    Email2 = Convert.ToString(txtBoxEmail2Contactos.Text),
                    Comentarios = Convert.ToString(txtBoxComentariosContacto.Text),
                    EstatusActivo = Convert.ToBoolean(1)
                };

                if (proveedorContactosBol.agregarContacto(Contacto))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarContactosDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido agregado exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser agregado. " + System.Environment.NewLine + proveedorContactosBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpiarCamposContactos_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxCategoriaContactos.Text = "";
                txtBoxNombreContactos.Text = "";
                txtBoxPuestoContactos.Text = "";
                txtBoxTel1Contactos.Text = "";
                txtBoxExt1Contactos.Text = "";
                txtBoxTel2Contactos.Text = "";
                txtBoxCel1Contactos.Text = "";
                txtBoxCel2Contactos.Text = "";
                txtBoxExt2Contactos.Text = "";
                txtBoxEmail1Contactos.Text = "";
                txtBoxEmail2Contactos.Text = "";               
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnPriorizarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Priorizar";
                string categoriaDato = "Contacto";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ListaContactos = proveedorContactosBol.consultarContactosByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                foreach (var i in ListaContactos)
                {
                    if (Convert.ToInt32(dataGridContactos.CurrentRow.Cells[0].Value) == i.Contactoid)
                    {
                        proveedorContactosBol.priorizarContactoByIdByClaveProveedorVal(i.Contactoid, i.ClaveProveedor);
                        proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        MessageBox.Show(categoriaDato + " ha sido priorizado exitosamente.", accionDato + " " + categoriaDato,
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarContactosDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                        CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        return;
                    }
                }
            }            
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDesactivarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Desactivar";
                string categoriaDato = "Contacto";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ListaContactos = proveedorContactosBol.consultarContactosByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                foreach (var i in ListaContactos)
                {
                    if (Convert.ToInt32(dataGridContactos.CurrentRow.Cells[0].Value) == i.Contactoid)
                    {
                        proveedorContactosBol.desactivarContactoByIdByClaveProveedorVal(i.Contactoid, i.ClaveProveedor);
                        MessageBox.Show(categoriaDato + " ha sido desactivado exitosamente.", accionDato + " " + categoriaDato,
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                        proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        CargarContactosDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                        CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnEditarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Editar";
                string categoriaDato = "Contacto";
                bool revisado = false;

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtBoxCategoriaContactos.Text == "" || txtBoxNombreContactos.Text == "" || txtBoxPuestoContactos.Text == ""
                    || txtBoxTel1Contactos.Text == "" || txtBoxEmail1Contactos.Text == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nLos campos requeridos son: \r\n*Categoría\r\n" +
                        "*Nombre Completo\r\n*Puesto\r\n*Teléfono 1\r\n*Email 1", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (checkBoxContactoItem.Checked)
                    revisado = true;

                EProveedorContacto Contacto = new EProveedorContacto
                {
                    ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                    PrioridadDeUso = Convert.ToInt32(2),
                    Contactoid = Convert.ToInt32(dataGridContactos.CurrentRow.Cells[0].Value),
                    NombreCompleto = Convert.ToString(txtBoxNombreContactos.Text),
                    Puesto = Convert.ToString(txtBoxPuestoContactos.Text),
                    Categoria = Convert.ToString(txtBoxCategoriaContactos.Text),
                    FuncionesContacto = Convert.ToString(comboBoxFuncionContacto.SelectedItem),
                    TelefonoPrimario = Convert.ToString(txtBoxTel1Contactos.Text),
                    ExtensionTelefonoPrimario = Convert.ToString(txtBoxExt1Contactos.Text),
                    TelefonoSecundario = Convert.ToString(txtBoxTel2Contactos.Text),
                    ExtensionTelefonoSecundario = Convert.ToString(txtBoxExt2Contactos.Text),
                    Celular1 = Convert.ToString(txtBoxCel1Contactos.Text),
                    Celular2 = Convert.ToString(txtBoxCel2Contactos.Text),
                    Email1 = Convert.ToString(txtBoxEmail1Contactos.Text),
                    Email2 = Convert.ToString(txtBoxEmail2Contactos.Text),
                    Comentarios = Convert.ToString(txtBoxComentariosContacto.Text),
                    EstatusActivo = Convert.ToBoolean(1),
                    Revisado = revisado
                };
                
                if (proveedorContactosBol.editarContactoByIdByClave(Contacto))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarContactosDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido editado exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser editado. " + System.Environment.NewLine + proveedorContactosBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridContactos_Click(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Contacto",
                          "Seleccionar" + " " + "Contacto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtBoxNombreContactos.Text = dataGridContactos.CurrentRow.Cells[2].Value.ToString();
                txtBoxPuestoContactos.Text = dataGridContactos.CurrentRow.Cells[3].Value.ToString();
                txtBoxTel1Contactos.Text = dataGridContactos.CurrentRow.Cells[6].Value.ToString();
                txtBoxExt1Contactos.Text = dataGridContactos.CurrentRow.Cells[7].Value.ToString();
                txtBoxTel2Contactos.Text = dataGridContactos.CurrentRow.Cells[8].Value.ToString();
                txtBoxExt2Contactos.Text = dataGridContactos.CurrentRow.Cells[9].Value.ToString();
                txtBoxCel1Contactos.Text = dataGridContactos.CurrentRow.Cells[10].Value.ToString();
                txtBoxCel2Contactos.Text = dataGridContactos.CurrentRow.Cells[11].Value.ToString();
                txtBoxEmail1Contactos.Text = dataGridContactos.CurrentRow.Cells[12].Value.ToString();
                txtBoxEmail2Contactos.Text = dataGridContactos.CurrentRow.Cells[13].Value.ToString();
                txtBoxCategoriaContactos.Text = dataGridContactos.CurrentRow.Cells[4].Value.ToString();
                txtBoxComentariosContacto.Text = dataGridContactos.CurrentRow.Cells[14].Value.ToString();
                comboBoxFuncionContacto.SelectedItem = dataGridContactos.CurrentRow.Cells[5].Value.ToString();
                if (dataGridContactos.CurrentRow.Cells[15].Value.ToString() == "Revisado")
                    checkBoxContactoItem.Checked = true;
                else
                    checkBoxContactoItem.Checked = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnLimpiarCamposDatosBancariosEX_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxNombreBancoDestinoEX.Text = "";
                txtBoxClaveBancoDestinoEX.Text = "";
                txtBoxNombreDestinatarioEX.Text = "";
                txtBoxNumeroCuentaDestinatarioEX.Text = "";
                comboBoxDivisaCuentaBancariaEX.SelectedItem = "Dólares (EEUU)";
                txtBoxMontoMaxEX.Text = "";
                txtBoxNombreBancoIntermediarioEX.Text = "";
                txtBoxClaveBancoIntermediarioEX.Text = "";
                txtBoxNombreBancoDestinoEX.Text = "";
                dateTimePickerDatosBancariosEX.Visible = true;
                dateTimePickerDatosBancariosEX.Value = DateTime.Today;
                lblFechaVigenciaEX.Visible = true;
                radioBtnNo.Checked = false;
                radioBtnSi.Checked = false;
                txtBoxTipoRelacionConDestinatarioEX.Text = "";
                txtBoxMotivoPagoEX.Text = "";
                txtBoxCalleAveBlvrDatosBancariosEX.Text = "";
                txtBoxNumExteriorDatosBancariosEX.Text = "";
                txtBoxCPDatosBancariosEX.Text = "";
                txtBoxColoniaDatosBancariosEX.Text = "";
                txtBoxNumInteriorDatosBancariosEX.Text = "";
                txtBoxInfAdicionalDatosBancariosEX.Text = "";
                txtBoxPoblacionDatosBancariosEX.Text = "";
                txtBoxEstadoDatosBancariosEX.Text = "";
                txtBoxPaisDatosBancariosEX.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnAgregarCuentaMX_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Agregar";
                string categoriaDato = "Cuenta Bancaria";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxBancoDatosBancariosMX.SelectedItem) == "" || txtBoxNumeroCuentaDestinatarioMX.Text == "" ||
                    txtBoxCLABEDatosBancariosMX.Text == "" || Convert.ToString(comboBoxDivisaCuentaBancariaMX.SelectedItem) == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nLos campos requeridos son: \r\n*Nombre de Banco Destino\r\n" +
                        "*Número de Cuenta Destinatario\r\n*CLABE Interbancaria\r\n*Divisa", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }                

                EProveedorDatosBancariosMX CuentaMX = new EProveedorDatosBancariosMX
                {
                    ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                    PrioridadDeUso = Convert.ToInt32(2),
                    //BancoMXid = Convert.ToInt32(BancoMXid),
                    NombreBancoDestino = Convert.ToString(comboBoxBancoDatosBancariosMX.SelectedItem),
                    CLABE = Convert.ToString(txtBoxCLABEDatosBancariosMX.Text),
                    NumeroCuentaDestinatario = Convert.ToString(txtBoxNumeroCuentaDestinatarioMX.Text),
                    Sucursal = Convert.ToString(txtBoxSucursalDatosBancariosMX.Text),
                    DivisaAPagar = Convert.ToString(comboBoxDivisaCuentaBancariaMX.SelectedItem),
                    EsPreferencia = Convert.ToBoolean(0),
                    EstatusActivo = Convert.ToBoolean(1)
                };

                if (proveedorDatosBancariosMXBol.agregarCuenta(CuentaMX))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarCuentasBancariasMXDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido agregada exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + proveedorDatosBancariosMXBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEsPreferenciaDatosBancariosMX_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Cuenta de Preferencia";
                string categoriaDato = "Cuenta Bancaria MX";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                proveedorDatosBancariosMXBol.esPreferenteCuentaByIdByClaveProveedorVal(Convert.ToInt32(dataGridViewDatosBancariosMX.CurrentRow.Cells[0].Value.ToString()),
                                                                                    eProveedorDatosPrim.ClaveProveedor);
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarCuentasBancariasMXDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " ha sido marcada como preferente exitosamente.", accionDato + " - " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDesactivarCuentaMX_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Desactivar";
                string categoriaDato = "Cuenta Bancaria MX";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                proveedorDatosBancariosMXBol.desactivarCuentaByIdByClaveProveedorVal(Convert.ToInt32(dataGridViewDatosBancariosMX.CurrentRow.Cells[0].Value.ToString()),
                                                                                    eProveedorDatosPrim.ClaveProveedor);               
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarCuentasBancariasMXDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEditarCuentaMX_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Editar";
                string categoriaDato = "Cuenta Bancaria MX";
                bool revisado = false;

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxBancoDatosBancariosMX.SelectedItem) == "" || txtBoxCLABEDatosBancariosMX.Text == "" || txtBoxNumeroCuentaDestinatarioMX.Text == ""
                    || Convert.ToString(comboBoxDivisaCuentaBancariaMX.SelectedItem) == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nLos campos requeridos son: \r\n*Nombre de Banco\r\n" +
                        "*CLABE\r\n*Número de Cuenta\r\n*Divisa", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }  
                
                if(checkBoxDatoBancarioItem.Checked)
                    revisado = true;

                EProveedorDatosBancariosMX Cuenta = new EProveedorDatosBancariosMX
                {
                    ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                    PrioridadDeUso = Convert.ToInt32(dataGridViewDatosBancariosMX.CurrentRow.Cells[2].Value.ToString()),
                    BancoMXid = Convert.ToInt32(dataGridViewDatosBancariosMX.CurrentRow.Cells[0].Value.ToString()),
                    NombreBancoDestino = Convert.ToString(comboBoxBancoDatosBancariosMX.SelectedItem),
                    CLABE = Convert.ToString(txtBoxCLABEDatosBancariosMX.Text),
                    NumeroCuentaDestinatario = Convert.ToString(txtBoxNumeroCuentaDestinatarioMX.Text),
                    Sucursal = Convert.ToString(txtBoxSucursalDatosBancariosMX.Text),
                    DivisaAPagar = Convert.ToString(comboBoxDivisaCuentaBancariaMX.SelectedItem),
                    EsPreferencia = Convert.ToBoolean(dataGridViewDatosBancariosMX.CurrentRow.Cells[1].Value.ToString() == "Preferente" ? true : false),
                    EstatusActivo = Convert.ToBoolean(1),
                    Revisado = revisado
                };

                if (proveedorDatosBancariosMXBol.editarCuentaByIdByClave(Cuenta))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarCuentasBancariasMXDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido editada exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser editada. " + System.Environment.NewLine + proveedorContactosBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpiarCamposDatosBancariosMX_Click(object sender, EventArgs e)
        {
            try
            {
                comboBoxBancoDatosBancariosMX.SelectedItem = "";
                txtBoxCLABEDatosBancariosMX.Text = "";
                txtBoxNumeroCuentaDestinatarioMX.Text = "";
                txtBoxSucursalDatosBancariosMX.Text = "";
                txtBoxNumeroCuentaDestinatarioMX.Text = "";
                comboBoxDivisaCuentaBancariaMX.SelectedItem = "Pesos (MX)";
                lblCuentaDePreferenciaClienteMX.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
        private void dataGridViewDatosBancariosMX_Click(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Dato Bancario",
                         "Seleccionar" + " " + "Dato Bancario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                comboBoxBancoDatosBancariosMX.SelectedItem = dataGridViewDatosBancariosMX.CurrentRow.Cells[3].Value.ToString();
                txtBoxCLABEDatosBancariosMX.Text = dataGridViewDatosBancariosMX.CurrentRow.Cells[4].Value.ToString();
                txtBoxNumeroCuentaDestinatarioMX.Text = dataGridViewDatosBancariosMX.CurrentRow.Cells[5].Value.ToString();
                txtBoxSucursalDatosBancariosMX.Text = dataGridViewDatosBancariosMX.CurrentRow.Cells[6].Value.ToString();
                comboBoxDivisaCuentaBancariaMX.SelectedItem = dataGridViewDatosBancariosMX.CurrentRow.Cells[7].Value.ToString();

                if (dataGridViewDatosBancariosMX.CurrentRow.Cells[1].Value.ToString() == "Preferente")
                    lblCuentaDePreferenciaClienteMX.Visible = true;
                else
                    lblCuentaDePreferenciaClienteMX.Visible = false;

                if (dataGridViewDatosBancariosMX.CurrentRow.Cells[9].Value.ToString() == "Revisado")
                    checkBoxDatoBancarioItem.Checked = true;
                else
                    checkBoxDatoBancarioItem.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }        
        private void btnAgregarCuentaEX_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Agregar";
                string categoriaDato = "Cuenta Bancaria";
                int id = -1;

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(txtBoxNombreBancoDestinoEX.Text) == "" || txtBoxClaveBancoDestinoEX.Text == "" ||
                    txtBoxNumeroCuentaDestinatarioEX.Text == "" || Convert.ToString(comboBoxDivisaCuentaBancariaEX.SelectedItem) == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nLos campos requeridos son: \r\n*Nombre de Banco Destino\r\n" +
                        "*Número de Cuenta Destinatario\r\n*Clave de Banco\r\n*Divisa", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                
                EProveedorDirecciones D = new EProveedorDirecciones
                {
                    ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                    PrioridadDeUso = Convert.ToInt32(2),
                    //NumIdDireccion = Convert.ToInt32(reader["NumIdDireccion"]),
                    ConceptoUso = Convert.ToString(categoriaDato),
                    CalleAveBlvr = Convert.ToString(txtBoxCalleAveBlvrDatosBancariosEX.Text),
                    NumExterior = Convert.ToString(txtBoxNumExteriorDatosBancariosEX.Text),
                    NumInterior =Convert.ToString(txtBoxNumInteriorDatosBancariosEX.Text),
                    InfAdicional = Convert.ToString(txtBoxInfAdicionalDatosBancariosEX.Text),
                    Colonia = Convert.ToString(txtBoxColoniaDatosBancariosEX.Text),
                    CodigoPostal = Convert.ToString(txtBoxCPDatosBancariosEX.Text),
                    Poblacion = Convert.ToString(txtBoxPoblacionDatosBancariosEX.Text),
                    Estado = Convert.ToString(txtBoxEstadoDatosBancariosEX.Text),
                    Pais = Convert.ToString(txtBoxPaisDatosBancariosEX.Text),
                    EstatusActivo = Convert.ToBoolean(1)
                };
                EProveedorDatosBancariosEX CuentaEX = new EProveedorDatosBancariosEX
                {
                    ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                    PrioridadDeUso = Convert.ToInt32(2),
                    //BancoEXid = Convert.ToInt32(reader["BancoEXid"]),
                    NombreBancoDestino = Convert.ToString(txtBoxNombreBancoDestinoEX.Text),
                    ClaveBancoDestino = Convert.ToString(txtBoxClaveBancoDestinoEX.Text),
                    NombreDestinatario = Convert.ToString(txtBoxNombreDestinatarioEX.Text),
                    NumeroCuentaDestinatario = Convert.ToString(txtBoxNumeroCuentaDestinatarioEX.Text),
                    DivisaAPagar = Convert.ToString(comboBoxDivisaCuentaBancariaEX.SelectedItem),
                    MontoMaximoAPagar = Convert.ToString(txtBoxMontoMaxEX.Text),
                    NombreBancoIntermediario = Convert.ToString(txtBoxNombreBancoIntermediarioEX.Text),
                    ClaveBancoIntermediario = Convert.ToString(txtBoxClaveBancoIntermediarioEX.Text),
                    NumIdDireccionDestinatario = Convert.ToInt32(id),
                    Vigencia = Convert.ToBoolean(radioBtnSi.Checked),
                    FechaDeVigencia = Convert.ToDateTime(radioBtnSi.Checked ? dateTimePickerDatosBancariosEX.Value : DateTime.MinValue),
                    TipoRelacionConDestinatario = Convert.ToString(txtBoxTipoRelacionConDestinatarioEX.Text),
                    MotivoPago = Convert.ToString(txtBoxMotivoPagoEX.Text),
                    EsPreferencia = Convert.ToBoolean(0),
                    EstatusActivo = Convert.ToBoolean(1)
                };

                if (proveedorDatosBancariosEXBol.agregarCuenta(CuentaEX, D))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarDireccionesDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarCuentasBancariasEXDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido agregada exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + proveedorDatosBancariosEXBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEditarCuentaEX_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Editar";
                string categoriaDato = "Cuenta Bancaria";
                bool revisado = false;

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(txtBoxNombreBancoDestinoEX.Text) == "" || txtBoxClaveBancoDestinoEX.Text == "" ||
                    txtBoxNumeroCuentaDestinatarioEX.Text == "" || Convert.ToString(comboBoxDivisaCuentaBancariaEX.SelectedItem) == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nLos campos requeridos son: \r\n*Nombre de Banco Destino\r\n" +
                        "*Número de Cuenta Destinatario\r\n*Clave de Banco\r\n*Divisa", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (checkBoxDatoBancarioEXItem.Checked)
                    revisado = true;

                EProveedorDirecciones D = new EProveedorDirecciones
                {
                    ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                    //PrioridadDeUso = Convert.ToInt32(dataGridDatosBancariosEX.CurrentRow.Cells[9].Value.ToString()),
                    NumIdDireccion = Convert.ToInt32(dataGridDatosBancariosEX.CurrentRow.Cells[9].Value.ToString()),
                    ConceptoUso = Convert.ToString(categoriaDato),
                    CalleAveBlvr = Convert.ToString(txtBoxCalleAveBlvrDatosBancariosEX.Text),
                    NumExterior = Convert.ToString(txtBoxNumExteriorDatosBancariosEX.Text),
                    NumInterior = Convert.ToString(txtBoxNumInteriorDatosBancariosEX.Text),
                    InfAdicional = Convert.ToString(txtBoxInfAdicionalDatosBancariosEX.Text),
                    Colonia = Convert.ToString(txtBoxColoniaDatosBancariosEX.Text),
                    CodigoPostal = Convert.ToString(txtBoxCPDatosBancariosEX.Text),
                    Poblacion = Convert.ToString(txtBoxPoblacionDatosBancariosEX.Text),
                    Estado = Convert.ToString(txtBoxEstadoDatosBancariosEX.Text),
                    Pais = Convert.ToString(txtBoxPaisDatosBancariosEX.Text),
                    Revisado = revisado
                    //EstatusActivo = Convert.ToBoolean(1)                    
                };
                EProveedorDatosBancariosEX CuentaEX = new EProveedorDatosBancariosEX
                {
                    ClaveProveedor = Convert.ToString(eProveedorDatosPrim.ClaveProveedor),
                    //PrioridadDeUso = Convert.ToInt32(2),
                    BancoEXid = Convert.ToInt32(dataGridDatosBancariosEX.CurrentRow.Cells[0].Value.ToString()),
                    NombreBancoDestino = Convert.ToString(txtBoxNombreBancoDestinoEX.Text),
                    ClaveBancoDestino = Convert.ToString(txtBoxClaveBancoDestinoEX.Text),
                    NombreDestinatario = Convert.ToString(txtBoxNombreDestinatarioEX.Text),
                    NumeroCuentaDestinatario = Convert.ToString(txtBoxNumeroCuentaDestinatarioEX.Text),
                    DivisaAPagar = Convert.ToString(comboBoxDivisaCuentaBancariaEX.SelectedItem),
                    MontoMaximoAPagar = Convert.ToString(txtBoxMontoMaxEX.Text),
                    NombreBancoIntermediario = Convert.ToString(txtBoxNombreBancoIntermediarioEX.Text),
                    ClaveBancoIntermediario = Convert.ToString(txtBoxClaveBancoIntermediarioEX.Text),
                    //NumIdDireccionDestinatario = Convert.ToInt32(dataGridDatosBancariosEX.CurrentRow.Cells[9].Value.ToString()),
                    Vigencia = Convert.ToBoolean(radioBtnSi.Checked),
                    FechaDeVigencia = Convert.ToDateTime(radioBtnSi.Checked ? dateTimePickerDatosBancariosEX.Value : DateTime.MinValue),
                    TipoRelacionConDestinatario = Convert.ToString(txtBoxTipoRelacionConDestinatarioEX.Text),
                    MotivoPago = Convert.ToString(txtBoxMotivoPagoEX.Text),
                    Revisado = revisado
                    //EsPreferencia = Convert.ToBoolean(0),
                    //EstatusActivo = Convert.ToBoolean(1)
                };

                if (proveedorDatosBancariosEXBol.editarCuenta(CuentaEX, D))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarDireccionesDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarCuentasBancariasEXDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido editada exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser editada. " + System.Environment.NewLine + proveedorDatosBancariosEXBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDesactivarCuentaEX_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Desactivar";
                string categoriaDato = "Cuenta Bancaria EX";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                proveedorDatosBancariosEXBol.desactivarCuentaByIdByClaveProveedorVal(Convert.ToInt32(dataGridDatosBancariosEX.CurrentRow.Cells[0].Value.ToString()),
                                                                                    eProveedorDatosPrim.ClaveProveedor);                
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarCuentasBancariasEXDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEsPreferenciaDatosBancariosEX_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Cuenta de Preferencia";
                string categoriaDato = "Cuenta Bancaria EX";
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                proveedorDatosBancariosEXBol.esPreferenteCuentaByIdByClaveProveedorVal(Convert.ToInt32(dataGridDatosBancariosEX.CurrentRow.Cells[0].Value.ToString()),
                                                                                    eProveedorDatosPrim.ClaveProveedor);                
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarCuentasBancariasEXDatosGenerales(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " ha sido marcada como preferente exitosamente.", accionDato + " - " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridDatosBancariosEX_Click(object sender, EventArgs e)
        {
            try
            {
                //Datos Bancarios EX
                txtBoxNombreBancoDestinoEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[1].Value.ToString());
                txtBoxClaveBancoDestinoEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[2].Value.ToString());
                txtBoxNumeroCuentaDestinatarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[4].Value.ToString());
                txtBoxNombreDestinatarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[3].Value.ToString());
                txtBoxMontoMaxEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[6].Value.ToString());
                comboBoxDivisaCuentaBancariaEX.SelectedItem = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[5].Value.ToString());
                txtBoxNombreBancoIntermediarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[7].Value.ToString());
                txtBoxClaveBancoIntermediarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[8].Value.ToString());
                if (dataGridDatosBancariosEX.CurrentRow.Cells[10].Value.ToString() == "True")
                {
                    radioBtnSi.Checked = true;
                    lblFechaVigenciaEX.Visible = true;
                    dateTimePickerDatosBancariosEX.Visible = true;
                    dateTimePickerDatosBancariosEX.Value = Convert.ToDateTime(dataGridDatosBancariosEX.CurrentRow.Cells[11].Value.ToString());

                }
                else
                {
                    radioBtnNo.Checked = true;
                    lblFechaVigenciaEX.Visible = false;
                    dateTimePickerDatosBancariosEX.Visible = false;

                }
                txtBoxTipoRelacionConDestinatarioEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[12].Value.ToString());
                txtBoxMotivoPagoEX.Text = Convert.ToString(dataGridDatosBancariosEX.CurrentRow.Cells[13].Value.ToString());
                DireccionDatosBancariosEX = proveedorDireccionesDatosBancariosEXBol.consultarDireccionesByClaveProveedorByIdVal(eProveedorDatosPrim.ClaveProveedor, Convert.ToInt32(dataGridDatosBancariosEX.CurrentRow.Cells[9].Value.ToString()));
                txtBoxCalleAveBlvrDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.CalleAveBlvr);
                txtBoxNumExteriorDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.NumExterior);
                txtBoxNumInteriorDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.NumInterior);
                txtBoxColoniaDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.Colonia);
                txtBoxInfAdicionalDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.InfAdicional);
                txtBoxCPDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.CodigoPostal);
                txtBoxPoblacionDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.Poblacion);
                txtBoxEstadoDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.Estado);
                txtBoxPaisDatosBancariosEX.Text = Convert.ToString(DireccionDatosBancariosEX.Pais);

                if (dataGridDatosBancariosEX.CurrentRow.Cells[14].Value.ToString() == "Preferente")
                    lblCuentaDePreferenciaClienteEX.Visible = true;
                else
                    lblCuentaDePreferenciaClienteEX.Visible = false;

                if (dataGridDatosBancariosEX.CurrentRow.Cells[16].Value.ToString() == "Revisado")
                    checkBoxDatoBancarioEXItem.Checked = true;
                else
                    checkBoxDatoBancarioEXItem.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
        private void radioBtnSi_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePickerDatosBancariosEX.Visible = true;
                lblFechaVigenciaEX.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void radioBtnNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblFechaVigenciaEX.Visible = false;
                dateTimePickerDatosBancariosEX.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        //Seccion Expediente
        private void ChecarDocumentacion(string claveProveedor)
        {
            try
            {
                if (comboBoxListaContratoExpediente.Items.Count > 0)
                    checkBoxContrato.Checked = true;
                else
                    checkBoxContrato.Checked = false;
                if (comboBoxListaPRLExpediente.Items.Count > 0)
                    checkBoxPoderRepLegal.Checked = true;
                else
                    checkBoxPoderRepLegal.Checked = false;
                if (comboBoxListaIRLExpediente.Items.Count > 0)
                    checkBoxIdRepLegal.Checked = true;
                else
                    checkBoxIdRepLegal.Checked = false;
                if (comboBoxListaComprobanteDomicilioExpediente.Items.Count > 0)
                    checkBoxComprabanteDomicilio.Checked = true;
                else
                    checkBoxComprabanteDomicilio.Checked = false;
                if (comboBoxListaCedulaRFCExpediente.Items.Count > 0)
                    checkBoxCedulaRFC.Checked = true;
                else
                    checkBoxCedulaRFC.Checked = false;
                if (comboBoxListaCaratulaEdoCuentaExpediente.Items.Count > 0)
                    checkBoxCaratatulaEdoCuenta.Checked = true;
                else
                    checkBoxCaratatulaEdoCuenta.Checked = false;
                if (comboBoxListaAvisoPrivacidadExpediente.Items.Count > 0)
                    checkBoxAvisoPrivacidad.Checked = true;
                else
                    checkBoxAvisoPrivacidad.Checked = false;
                if (comboBoxListaPagare.Items.Count > 0)
                    checkBoxPagare.Checked = true;
                else
                    checkBoxPagare.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnAbrirContrato_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaContratoExpediente.SelectedItem) == "" || comboBoxListaContratoExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.",  MessageBoxButtons.OK);
                    return;
                }
                
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaContratoExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));         
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);
                     
                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoContrato_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Contrato";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxListaContratoExpediente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Archivo",
                          "Seleccionar" + " " + "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxListaContratoExpediente.SelectedIndex == -1)
                    return;
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaContratoExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                dateTimePickerFechaActualizacionContrato.Value = ubicacionArchivos.FechaActualizacion;
                dateTimePickerFechaVencimientoContrato.Value = ubicacionArchivos.FechaVigencia;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarContrato_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Contrato";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaContratoExpediente.SelectedItem) == "" || comboBoxListaContratoExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK); 
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaContratoExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaContratoExpediente.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAbrirIdRepLegal_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaIRLExpediente.SelectedItem) == "" || comboBoxListaIRLExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaIRLExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoIRL_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Identificación Representante Legal";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxListaIRLExpediente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Archivo",
                          "Seleccionar" + " " + "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxListaIRLExpediente.SelectedIndex == -1)
                    return;
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaIRLExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                dateTimePickerFechaActualizacionIRL.Value = ubicacionArchivos.FechaActualizacion;
                dateTimePickerFechaVencimientoIRL.Value = ubicacionArchivos.FechaVigencia;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarIdRepLegal_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Identificación Representante Legal";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaIRLExpediente.SelectedItem) == "" || comboBoxListaIRLExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaIRLExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaIRLExpediente.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        private void btnAbrirPoderRepLegal_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaPRLExpediente.SelectedItem) == "" || comboBoxListaPRLExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPRLExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoPRL_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Poder Respresentante Legal";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxListaPRLExpediente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Archivo",
                          "Seleccionar" + " " + "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxListaPRLExpediente.SelectedIndex == -1)
                    return;
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPRLExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                dateTimePickerFechaActualizacionPRL.Value = ubicacionArchivos.FechaActualizacion;
                dateTimePickerFechaVencimientoPRL.Value = ubicacionArchivos.FechaVigencia;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarPoderRepLegal_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Poder Respresentante Legal";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaPRLExpediente.SelectedItem) == "" || comboBoxListaPRLExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPRLExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaPRLExpediente.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);               
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        private void btnAbrirCedulaRFC_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaCedulaRFCExpediente.SelectedItem) == "" || comboBoxListaCedulaRFCExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaCedulaRFCExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoRFC_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Cédula RFC";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxListaCedulaRFCExpediente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Archivo",
                          "Seleccionar" + " " + "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxListaCedulaRFCExpediente.SelectedIndex == -1)
                    return;
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaCedulaRFCExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                dateTimePickerFechaActualizacionCedulaRFC.Value = ubicacionArchivos.FechaActualizacion;
                dateTimePickerFechaVencimientoCedulaRFC.Value = ubicacionArchivos.FechaVigencia;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarCedulaRFC_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Cédula RFC";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaCedulaRFCExpediente.SelectedItem) == "" || comboBoxListaCedulaRFCExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaCedulaRFCExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaCedulaRFCExpediente.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAbrirCaratulaEstadoCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaCaratulaEdoCuentaExpediente.SelectedItem) == "" || comboBoxListaCaratulaEdoCuentaExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaCaratulaEdoCuentaExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoCaratulaEstadoCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Carátula de Estado de Cuenta";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxListaCaratulaEdoCuentaExpediente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Archivo",
                          "Seleccionar" + " " + "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxListaCaratulaEdoCuentaExpediente.SelectedIndex == -1)
                    return;
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaCaratulaEdoCuentaExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                dateTimePickerFechaActualizacionCaratulaEstadoCuenta.Value = ubicacionArchivos.FechaActualizacion;
                dateTimePickerFechaVencimientoCaratulaEstadoCuenta.Value = ubicacionArchivos.FechaVigencia;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarCaratulaEstadoCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Carátula de Estado de Cuenta";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaCaratulaEdoCuentaExpediente.SelectedItem) == "" || comboBoxListaCaratulaEdoCuentaExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaCaratulaEdoCuentaExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaCaratulaEdoCuentaExpediente.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAbrirAvisoPrivacidad_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaAvisoPrivacidadExpediente.SelectedItem) == "" || comboBoxListaAvisoPrivacidadExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaAvisoPrivacidadExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoAvisoPrivacidad_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Aviso de Privacidad";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxListaAvisoPrivacidadExpediente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Archivo",
                          "Seleccionar" + " " + "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxListaAvisoPrivacidadExpediente.SelectedIndex == -1)
                    return;
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaAvisoPrivacidadExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                dateTimePickerFechaActualizacionAvisoPrivacidad.Value = ubicacionArchivos.FechaActualizacion;
                dateTimePickerFechaVencimientoAvisoPrivacidad.Value = ubicacionArchivos.FechaVigencia;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarAvisoPrivacidad_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Aviso de Privacidad";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaAvisoPrivacidadExpediente.SelectedItem) == "" || comboBoxListaAvisoPrivacidadExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaAvisoPrivacidadExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                comboBoxListaAvisoPrivacidadExpediente.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAbrirComprobanteDomicilio_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaComprobanteDomicilioExpediente.SelectedItem) == "" || comboBoxListaComprobanteDomicilioExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaComprobanteDomicilioExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoComprobanteDomicilio_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Comprobante Domicilio";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxListaComprobanteDomicilioExpediente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Archivo",
                          "Seleccionar" + " " + "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxListaComprobanteDomicilioExpediente.SelectedIndex == -1)
                    return;
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaComprobanteDomicilioExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                dateTimePickerFechaActualizacionComprobanteDomicilio.Value = ubicacionArchivos.FechaActualizacion;
                dateTimePickerFechaVencimientoComprobanteDomicilio.Value = ubicacionArchivos.FechaVigencia;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarComprobanteDomicilio_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Comprobante Domicilio";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaComprobanteDomicilioExpediente.SelectedItem) == "" || comboBoxListaComprobanteDomicilioExpediente.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaComprobanteDomicilioExpediente.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaComprobanteDomicilioExpediente.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonAbrirPagare_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaPagare.SelectedItem) == "" || comboBoxListaPagare.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPagare.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoPagare_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Pagare";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxListaPagare_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Archivo",
                          "Seleccionar" + " " + "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxListaPagare.SelectedIndex == -1)
                    return;
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPagare.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                dateTimePickerFechaActualizacionPagare.Value = ubicacionArchivos.FechaActualizacion;
                dateTimePickerFechaVencimientoPagare.Value = ubicacionArchivos.FechaVigencia;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarPagare_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Pagare";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaPagare.SelectedItem) == "" || comboBoxListaPagare.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPagare.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaPagare.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Seccion Acuerdos 
        private void btnGuardarCambiosSeccionAcuerdos1_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Acuerdos";
                string accionDato = "Actualizar";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                EProveedorAcuerdos Acuerdos = new EProveedorAcuerdos
                {
                    ClaveProveedor = eProveedorDatosPrim.ClaveProveedor,
                    //Acuerdoid
                    AcuerdoCompra = textBoxAcuerdoCompra.Text,
                    AcuerdoVentaPublico = txtBoxAcuerdoVentaPublico.Text,
                    AcuerdoAtencionClientes = txtBoxAcuerdoAtencionClientes.Text
                };

                proveedorAcuerdosBol.actualizarAcuerdos(Acuerdos);
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " han sido actualizado exitosamente.", accionDato + " " + categoriaDato,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarAcuerdos(eProveedorDatosPrim.ClaveProveedor);
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnGuardarCambiosSeccionAcuerdos2_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Acuerdos";
                string accionDato = "Actualizar";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                EProveedorAcuerdos Acuerdos = new EProveedorAcuerdos
                {
                    ClaveProveedor = eProveedorDatosPrim.ClaveProveedor,
                    //Acuerdoid
                    AcuerdoCompra = textBoxAcuerdoCompra.Text,
                    AcuerdoVentaPublico = txtBoxAcuerdoVentaPublico.Text,
                    AcuerdoAtencionClientes = txtBoxAcuerdoAtencionClientes.Text
                };

                proveedorAcuerdosBol.actualizarAcuerdos(Acuerdos);
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " han sido actualizado exitosamente.", accionDato + " " + categoriaDato,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarAcuerdos(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpiarCampoAcuerdoCompra_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxAcuerdoCompra.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnLimpiarCampoAcuerdoVentaPublico_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxAcuerdoVentaPublico.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnLimpiarCampoAcuerdoAtencionClientes_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxAcuerdoAtencionClientes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnAbrirAcuerdoCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaAcuerdosDeCompra.SelectedItem) == "" || comboBoxListaAcuerdosDeCompra.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaAcuerdosDeCompra.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoAcuerdoCompra_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Acuerdo de Compra";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarAcuerdoCompra_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Acuerdo de Compra";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaAcuerdosDeCompra.SelectedItem) == "" || comboBoxListaAcuerdosDeCompra.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaAcuerdosDeCompra.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaAcuerdosDeCompra.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAbrirAcuerdoVentaPublico_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaAcuerdosVentaPublico.SelectedItem) == "" || comboBoxListaAcuerdosVentaPublico.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaAcuerdosVentaPublico.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoAcuerdoVentaPublico_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Acuerdo para Venta al Público";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarAcuerdoVentaPublico_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Acuerdo para Venta al Público";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaAcuerdosVentaPublico.SelectedItem) == "" || comboBoxListaAcuerdosVentaPublico.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaAcuerdosVentaPublico.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaAcuerdosVentaPublico.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAbrirAcuerdoAtencionClientes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaAcuerdosAtencionClientes.SelectedItem) == "" || comboBoxListaAcuerdosAtencionClientes.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaAcuerdosAtencionClientes.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnSubirArchivoAcuerdoAtencionClientes_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Acuerdo para Atención a Clientes";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarAtencionClientes_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Acuerdo para Atención a Clientes";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaAcuerdosAtencionClientes.SelectedItem) == "" || comboBoxListaAcuerdosAtencionClientes.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaAcuerdosAtencionClientes.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);


                comboBoxListaAcuerdosAtencionClientes.SelectedIndex = -1; 
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Seccion Condiciones
        private void btnGuardarCambiosSeccionCondiciones1_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Condiciones de Pago y Entrega";
                string accionDato = "Actualizar";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                StringBuilder formaDeEntrega = new StringBuilder("");
                StringBuilder sucursalesDeEntrega = new StringBuilder("");                

                //if (checkBoxPaqueteriaPagadaProveedor.Checked)
                //    formaDeEntrega.Append(" Paquetería Pagada por Proveedor ");
                //if (checkBoxPaqueteriaPorCobrar.Checked)
                //    formaDeEntrega.Append(" Paquetería por Cobrar ");
                //if (checkBoxTransporteContratado.Checked)
                //    formaDeEntrega.Append(" Transporte Contratado ");
                //if (checkBoxTransporteProveedor.Checked)
                //    formaDeEntrega.Append(" Transporte de Proveedor ");
                //if (txtBoxOtraFormaEntrega.Text != "")
                //    formaDeEntrega.Append(" Otra: " + txtBoxOtraFormaEntrega.Text);
                
                if (checkBoxMatriz.Checked)
                    sucursalesDeEntrega.Append(" Matriz ");
                if (checkBoxMagdalena.Checked)
                    sucursalesDeEntrega.Append(" Magdalena ");
                if (checkBoxSanPedro.Checked)
                    sucursalesDeEntrega.Append(" San Pedro ");
                if (checkBoxHipodromo.Checked)
                    sucursalesDeEntrega.Append(" Hipódromo ");
                if (checkBoxCaborca.Checked)
                    sucursalesDeEntrega.Append(" Caborca ");
                if (checkBoxCEDIS.Checked)
                    sucursalesDeEntrega.Append(" CEDIS ");
                if (checkBoxCMA.Checked)
                    sucursalesDeEntrega.Append(" CMA ");

                EProveedorCondiciones C = new EProveedorCondiciones
                {
                    ClaveProveedor = eProveedorDatosPrim.ClaveProveedor,
                    //Condicionesid
                    CondicionesCreditoDias = txtBoxCondicionesCreditoCondiciones.Text,
                    //FletePorCobrar = null,
                    ProntoPago1Dias = txtBoxProntoPago1Dias.Text,
                    ProntoPago2Dias = txtBoxProntoPago2Dias.Text,
                    ProntoPago3Dias = txtBoxProntoPago3Dias.Text,
                    ProntoPago4Dias = txtBoxProntoPago4Dias.Text,
                    ProntoPago5Dias = txtBoxProntoPago5Dias.Text,
                    ProntoPago1Descuento = Convert.ToInt32(comboBoxDescProntoPago1.SelectedItem),
                    ProntoPago2Descuento = Convert.ToInt32(comboBoxDescProntoPago2.SelectedItem),
                    ProntoPago3Descuento = Convert.ToInt32(comboBoxDescProntoPago3.SelectedItem),
                    ProntoPago4Descuento = Convert.ToInt32(comboBoxDescProntoPago4.SelectedItem),
                    ProntoPago5Descuento = Convert.ToInt32(comboBoxDescProntoPago5.SelectedItem),
                    VencimientoPagoFactura1 = Convert.ToString(comboBoxVencimientoPagoFactura1.SelectedItem),
                    VencimientoPagoFactura2 = Convert.ToString(comboBoxVencimientoPagoFactura2.SelectedItem),
                    VencimientoPagoFactura3 = Convert.ToString(comboBoxVencimientoPagoFactura3.SelectedItem),
                    VencimientoPagoFactura4 = Convert.ToString(comboBoxVencimientoPagoFactura4.SelectedItem),
                    VencimientoPagoFactura5 = Convert.ToString(comboBoxVencimientoPagoFactura5.SelectedItem),
                    TiempoEntrega = txtBoxTiempoEntregaCondiciones.Text,
                    ObservacionesTiempoEntrega = txtBoxObservacionesCondiciones.Text,
                    FormaEntrega = Convert.ToString(formaDeEntrega),
                    SucursalEntrega = Convert.ToString(sucursalesDeEntrega),
                    CondicionesEspecialesEntrega = txtBoxCondicionesEspecialesCondiciones.Text
                };                

                proveedorCondicionesBol.actualizarCondiciones(C);
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " han sido actualizado exitosamente.", accionDato + " " + categoriaDato,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);  
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGuardarCambiosSeccionCondiciones2_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Condiciones de Pago y Entrega";
                string accionDato = "Actualizar";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                StringBuilder formaDeEntrega = new StringBuilder("");
                StringBuilder sucursalesDeEntrega = new StringBuilder("");

                //if (checkBoxPaqueteriaPagadaProveedor.Checked)
                //    formaDeEntrega.Append(" Paquetería Pagada por Proveedor ");
                //if (checkBoxPaqueteriaPorCobrar.Checked)
                //    formaDeEntrega.Append(" Paquetería por Cobrar ");
                //if (checkBoxTransporteContratado.Checked)
                //    formaDeEntrega.Append(" Transporte Contratado ");
                //if (checkBoxTransporteProveedor.Checked)
                //    formaDeEntrega.Append(" Transporte de Proveedor ");
                //if (txtBoxOtraFormaEntrega.Text != "")
                //    formaDeEntrega.Append(" Otra: " + txtBoxOtraFormaEntrega.Text);

                if (checkBoxMatriz.Checked)
                    sucursalesDeEntrega.Append(" Matriz ");
                if (checkBoxMagdalena.Checked)
                    sucursalesDeEntrega.Append(" Magdalena ");
                if (checkBoxSanPedro.Checked)
                    sucursalesDeEntrega.Append(" San Pedro ");
                if (checkBoxHipodromo.Checked)
                    sucursalesDeEntrega.Append(" Hipódromo ");
                if (checkBoxCaborca.Checked)
                    sucursalesDeEntrega.Append(" Caborca ");
                if (checkBoxCEDIS.Checked)
                    sucursalesDeEntrega.Append(" CEDIS ");
                if (checkBoxCMA.Checked)
                    sucursalesDeEntrega.Append(" CMA ");

                EProveedorCondiciones C = new EProveedorCondiciones
                {
                    ClaveProveedor = eProveedorDatosPrim.ClaveProveedor,
                    //Condicionesid
                    CondicionesCreditoDias = txtBoxCondicionesCreditoCondiciones.Text,
                    //FletePorCobrar = null,
                    ProntoPago1Dias = txtBoxProntoPago1Dias.Text,
                    ProntoPago2Dias = txtBoxProntoPago2Dias.Text,
                    ProntoPago3Dias = txtBoxProntoPago3Dias.Text,
                    ProntoPago4Dias = txtBoxProntoPago4Dias.Text,
                    ProntoPago5Dias = txtBoxProntoPago5Dias.Text,
                    ProntoPago1Descuento = Convert.ToInt32(comboBoxDescProntoPago1.SelectedItem),
                    ProntoPago2Descuento = Convert.ToInt32(comboBoxDescProntoPago2.SelectedItem),
                    ProntoPago3Descuento = Convert.ToInt32(comboBoxDescProntoPago3.SelectedItem),
                    ProntoPago4Descuento = Convert.ToInt32(comboBoxDescProntoPago4.SelectedItem),
                    ProntoPago5Descuento = Convert.ToInt32(comboBoxDescProntoPago5.SelectedItem),
                    VencimientoPagoFactura1 = Convert.ToString(comboBoxVencimientoPagoFactura1.SelectedItem),
                    VencimientoPagoFactura2 = Convert.ToString(comboBoxVencimientoPagoFactura2.SelectedItem),
                    VencimientoPagoFactura3 = Convert.ToString(comboBoxVencimientoPagoFactura3.SelectedItem),
                    VencimientoPagoFactura4 = Convert.ToString(comboBoxVencimientoPagoFactura4.SelectedItem),
                    VencimientoPagoFactura5 = Convert.ToString(comboBoxVencimientoPagoFactura5.SelectedItem),
                    TiempoEntrega = txtBoxTiempoEntregaCondiciones.Text,
                    ObservacionesTiempoEntrega = txtBoxObservacionesCondiciones.Text,
                    FormaEntrega = Convert.ToString(formaDeEntrega),
                    SucursalEntrega = Convert.ToString(sucursalesDeEntrega),
                    CondicionesEspecialesEntrega = txtBoxCondicionesEspecialesCondiciones.Text
                };

                proveedorCondicionesBol.actualizarCondiciones(C);
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " han sido actualizado exitosamente.", accionDato + " " + categoriaDato,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridPlazosCredito_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(dataGridPlazosCredito.CurrentRow.Cells[4].Value.ToString()) == "Revisado")
                    checkBoxCondicionesItem.Checked = true;
                else
                    checkBoxCondicionesItem.Checked = false;

                txtBoxCondicionesCreditoCondiciones.Text = dataGridPlazosCredito.CurrentRow.Cells[5].Value.ToString();
                txtBoxDefinicionPlazoCredito.Text = dataGridPlazosCredito.CurrentRow.Cells[6].Value.ToString();
                txtBoxProntoPago1Dias.Text = dataGridPlazosCredito.CurrentRow.Cells[7].Value.ToString();
                comboBoxDescProntoPago1.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[8].Value.ToString();
                comboBoxVencimientoPagoFactura1.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[9].Value.ToString();
                txtBoxObservacionesProntoPago1.Text = dataGridPlazosCredito.CurrentRow.Cells[10].Value.ToString();
                txtBoxProntoPago2Dias.Text = dataGridPlazosCredito.CurrentRow.Cells[11].Value.ToString();
                comboBoxDescProntoPago2.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[12].Value.ToString();
                comboBoxVencimientoPagoFactura2.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[13].Value.ToString();
                txtBoxObservacionesProntoPago2.Text = dataGridPlazosCredito.CurrentRow.Cells[14].Value.ToString();
                txtBoxProntoPago3Dias.Text = dataGridPlazosCredito.CurrentRow.Cells[15].Value.ToString();
                comboBoxDescProntoPago3.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[16].Value.ToString();
                comboBoxVencimientoPagoFactura3.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[17].Value.ToString();
                txtBoxObservacionesProntoPago3.Text = dataGridPlazosCredito.CurrentRow.Cells[18].Value.ToString();
                txtBoxProntoPago4Dias.Text = dataGridPlazosCredito.CurrentRow.Cells[19].Value.ToString();
                comboBoxDescProntoPago4.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[20].Value.ToString();
                comboBoxVencimientoPagoFactura4.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[21].Value.ToString();
                txtBoxObservacionesProntoPago4.Text = dataGridPlazosCredito.CurrentRow.Cells[22].Value.ToString();
                txtBoxProntoPago5Dias.Text = dataGridPlazosCredito.CurrentRow.Cells[23].Value.ToString();
                comboBoxDescProntoPago5.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[24].Value.ToString();
                comboBoxVencimientoPagoFactura5.SelectedItem = dataGridPlazosCredito.CurrentRow.Cells[25].Value.ToString();
                txtBoxObservacionesProntoPago5.Text = dataGridPlazosCredito.CurrentRow.Cells[26].Value.ToString();
                txtBoxObservacionesGeneralesPlazo.Text = dataGridPlazosCredito.CurrentRow.Cells[27].Value.ToString();

                if (txtBoxProntoPago1Dias.Text != "" || txtBoxProntoPago2Dias.Text != "" || txtBoxProntoPago3Dias.Text != "" ||
                 txtBoxProntoPago4Dias.Text != "" || txtBoxProntoPago5Dias.Text != "")
                    checkBoxTieneProntoPago.Checked = true;
                else
                    checkBoxTieneProntoPago.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAgregarPlazo_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Agregar";
                string categoriaDato = "Plazo de Crédito";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtBoxCondicionesCreditoCondiciones.Text == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nEl campo requerido es: \r\n*Días de Plazo de Crédito",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                EProveedorPlazoCredito Plazo = new EProveedorPlazoCredito
                {            
                    ClaveProveedor = eProveedorDatosPrim.ClaveProveedor,
                    PlazoCreditoDias = txtBoxCondicionesCreditoCondiciones.Text,
                    DefinicionPlazo = txtBoxDefinicionPlazoCredito.Text,
                    ProntoPago1Dias = txtBoxProntoPago1Dias.Text,
                    ProntoPago1Descuento = Convert.ToInt32(comboBoxDescProntoPago1.SelectedItem),
                    VencimientoPagoFactura1 = Convert.ToString(comboBoxVencimientoPagoFactura1.SelectedItem),
                    ObservacionesPP1 = txtBoxObservacionesProntoPago1.Text,
                    ProntoPago2Dias = txtBoxProntoPago2Dias.Text,
                    ProntoPago2Descuento = Convert.ToInt32(comboBoxDescProntoPago2.SelectedItem),
                    VencimientoPagoFactura2 = Convert.ToString(comboBoxVencimientoPagoFactura2.SelectedItem),
                    ObservacionesPP2 = txtBoxObservacionesProntoPago2.Text,
                    ProntoPago3Dias = txtBoxProntoPago3Dias.Text,
                    ProntoPago3Descuento = Convert.ToInt32(comboBoxDescProntoPago3.SelectedItem),
                    VencimientoPagoFactura3 = Convert.ToString(comboBoxVencimientoPagoFactura3.SelectedItem),
                    ObservacionesPP3 = txtBoxObservacionesProntoPago3.Text,
                    ProntoPago4Dias = txtBoxProntoPago4Dias.Text,
                    ProntoPago4Descuento = Convert.ToInt32(comboBoxDescProntoPago4.SelectedItem),
                    VencimientoPagoFactura4 = Convert.ToString(comboBoxVencimientoPagoFactura4.SelectedItem),
                    ObservacionesPP4 = txtBoxObservacionesProntoPago4.Text,
                    ProntoPago5Dias = txtBoxProntoPago5Dias.Text,
                    ProntoPago5Descuento = Convert.ToInt32(comboBoxDescProntoPago5.SelectedItem),
                    VencimientoPagoFactura5 = Convert.ToString(comboBoxVencimientoPagoFactura5.SelectedItem),
                    ObservacionesPP5 = txtBoxObservacionesProntoPago5.Text,
                    ObservacionesGenerales = txtBoxObservacionesGeneralesPlazo.Text
                };

                if (proveedorPlazoCreditoBol.agregarPlazoProveedor(Plazo))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarPlazosCredito(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido agregado exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser agregado. " + System.Environment.NewLine + proveedorDireccionesBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEditarPlazo_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Editar";
                string categoriaDato = "Plazo de Crédito";
                bool revisado = false;

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtBoxCondicionesCreditoCondiciones.Text == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nEl campo requerido es: \r\n*Días de Plazo de Crédito",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (checkBoxCondicionesItem.Checked)
                    revisado = true;

                EProveedorPlazoCredito Plazo = new EProveedorPlazoCredito
                {
                    ClaveProveedor = eProveedorDatosPrim.ClaveProveedor,
                    Plazoid = Convert.ToInt32(dataGridPlazosCredito.CurrentRow.Cells[1].Value.ToString()),
                    Revisado = revisado,
                    PlazoCreditoDias = txtBoxCondicionesCreditoCondiciones.Text,
                    DefinicionPlazo = txtBoxDefinicionPlazoCredito.Text,
                    ProntoPago1Dias = txtBoxProntoPago1Dias.Text,
                    ProntoPago1Descuento = Convert.ToInt32(comboBoxDescProntoPago1.SelectedItem),
                    VencimientoPagoFactura1 = Convert.ToString(comboBoxVencimientoPagoFactura1.SelectedItem),
                    ObservacionesPP1 = txtBoxObservacionesProntoPago1.Text,
                    ProntoPago2Dias = txtBoxProntoPago2Dias.Text,
                    ProntoPago2Descuento = Convert.ToInt32(comboBoxDescProntoPago2.SelectedItem),
                    VencimientoPagoFactura2 = Convert.ToString(comboBoxVencimientoPagoFactura2.SelectedItem),
                    ObservacionesPP2 = txtBoxObservacionesProntoPago2.Text,
                    ProntoPago3Dias = txtBoxProntoPago3Dias.Text,
                    ProntoPago3Descuento = Convert.ToInt32(comboBoxDescProntoPago3.SelectedItem),
                    VencimientoPagoFactura3 = Convert.ToString(comboBoxVencimientoPagoFactura3.SelectedItem),
                    ObservacionesPP3 = txtBoxObservacionesProntoPago3.Text,
                    ProntoPago4Dias = txtBoxProntoPago4Dias.Text,
                    ProntoPago4Descuento = Convert.ToInt32(comboBoxDescProntoPago4.SelectedItem),
                    VencimientoPagoFactura4 = Convert.ToString(comboBoxVencimientoPagoFactura4.SelectedItem),
                    ObservacionesPP4 = txtBoxObservacionesProntoPago4.Text,
                    ProntoPago5Dias = txtBoxProntoPago5Dias.Text,
                    ProntoPago5Descuento = Convert.ToInt32(comboBoxDescProntoPago5.SelectedItem),
                    VencimientoPagoFactura5 = Convert.ToString(comboBoxVencimientoPagoFactura5.SelectedItem),
                    ObservacionesPP5 = txtBoxObservacionesProntoPago5.Text,
                    ObservacionesGenerales = txtBoxObservacionesGeneralesPlazo.Text
                };

                if (proveedorPlazoCreditoBol.editarPlazoByIdByClaveProveedorVal(Plazo))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarPlazosCredito(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido editado exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser editada. " + System.Environment.NewLine + proveedorDireccionesBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpiarCamposPlazoCredito_Click(object sender, EventArgs e)
        {
            checkBoxCondicionesItem.Checked = false;
            checkBoxTieneProntoPago.Checked = false;
            txtBoxCondicionesCreditoCondiciones.Clear();
            txtBoxDefinicionPlazoCredito.Clear();
            txtBoxProntoPago1Dias.Clear();
            comboBoxDescProntoPago1.SelectedIndex = -1;
            comboBoxVencimientoPagoFactura1.SelectedIndex = -1;
            txtBoxObservacionesProntoPago1.Clear();
            txtBoxProntoPago2Dias.Clear();
            comboBoxDescProntoPago2.SelectedIndex = -1;
            comboBoxVencimientoPagoFactura2.SelectedIndex = -1;
            txtBoxObservacionesProntoPago2.Clear();
            txtBoxProntoPago3Dias.Clear();
            comboBoxDescProntoPago3.SelectedIndex = -1;
            comboBoxVencimientoPagoFactura3.SelectedIndex = -1;
            txtBoxObservacionesProntoPago3.Clear();
            txtBoxProntoPago4Dias.Clear();
            comboBoxDescProntoPago4.SelectedIndex = -1;
            comboBoxVencimientoPagoFactura4.SelectedIndex = -1;
            txtBoxObservacionesProntoPago4.Clear();
            txtBoxProntoPago5Dias.Clear();
            comboBoxDescProntoPago5.SelectedIndex = -1;
            comboBoxVencimientoPagoFactura5.SelectedIndex = -1;
            txtBoxObservacionesProntoPago5.Clear();
            txtBoxObservacionesGeneralesPlazo.Clear();              
        }
        private void btnLimpiarCampoObservacionesTiempoEntregaCondiciones_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxObservacionesCondiciones.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnAbrirArchivoObservacionesTiempoFormaEntrega_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaObservacionesFormaEntrega.SelectedItem) == "" || comboBoxListaObservacionesFormaEntrega.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaObservacionesFormaEntrega.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }
        private void btnEliminarArchivoObservacionesTiempoFormaEntrega_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Observaciones en Tiempo y Forma de Entrega";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaObservacionesFormaEntrega.SelectedItem) == "" || comboBoxListaObservacionesFormaEntrega.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaObservacionesFormaEntrega.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaObservacionesFormaEntrega.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSeleccionarArchivoObservacionesFormaEntrega_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Observaciones en Tiempo y Forma de Entrega";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpiarCampoCondicionesEspecialesEntrega_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxCondicionesEspecialesCondiciones.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void dataGridFletes_Click(object sender, EventArgs e)
        {
            try
            {
                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + "Seleccionar" + " " + "Flete",
                          "Seleccionar" + " " + "Flete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                comboBoxTipoEnvioFletes.SelectedItem = dataGridFletes.CurrentRow.Cells[2].Value.ToString();
                comboBoxFormaEntregaFletes.SelectedItem = dataGridFletes.CurrentRow.Cells[3].Value.ToString();
                txtBoxClaveProveedorFletes.Text = dataGridFletes.CurrentRow.Cells[4].Value.ToString();
                txtBoxNombreFleteCondiciones.Text = dataGridFletes.CurrentRow.Cells[5].Value.ToString();
                txtBoxPedidoMinCondiciones.Text = dataGridFletes.CurrentRow.Cells[7].Value.ToString();
                txtBoxPedidoMaxCondiciones.Text = dataGridFletes.CurrentRow.Cells[8].Value.ToString();
                comboBoxUnidadPedidoCondiciones.SelectedItem = dataGridFletes.CurrentRow.Cells[9].Value.ToString();
                txtBoxFleteOrigenCondiciones.Text = dataGridFletes.CurrentRow.Cells[10].Value.ToString();
                txtBoxFleteDestinoCondiciones.Text = dataGridFletes.CurrentRow.Cells[11].Value.ToString();
                txtBoxObservacionesFletes.Text = dataGridFletes.CurrentRow.Cells[12].Value.ToString();
                if (dataGridFletes.CurrentRow.Cells[13].Value.ToString() == "1")
                    lblFletePreferenciaFletes.Visible = true;
                txtBoxCantidadCostoSucursalMatrizFletes.Text = dataGridFletes.CurrentRow.Cells[14].Value.ToString();
                txtBoxCantidadCostoSucursalHipodromoFletes.Text = dataGridFletes.CurrentRow.Cells[15].Value.ToString();
                txtBoxCantidadCostoSucursalSanPedroFletes.Text = dataGridFletes.CurrentRow.Cells[16].Value.ToString();
                txtBoxCantidadCostoSucursalMagdalenaFletes.Text = dataGridFletes.CurrentRow.Cells[17].Value.ToString();
                txtBoxCantidadCostoSucursalCabrocaFletes.Text = dataGridFletes.CurrentRow.Cells[18].Value.ToString();
                txtBoxCantidadCostoSucursalCEDISFletes.Text = dataGridFletes.CurrentRow.Cells[19].Value.ToString();
                txtBoxCantidadCostoSucursalCMAletes.Text = dataGridFletes.CurrentRow.Cells[20].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAgregarFlete_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Agregar";
                string categoriaDato = "Flete";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtBoxClaveProveedorFletes.Text == "" || txtBoxNombreFleteCondiciones.Text == "" || 
                    Convert.ToString(comboBoxFormaEntregaFletes.SelectedItem) == "" || Convert.ToString(comboBoxTipoEnvioFletes.SelectedItem) == "")
                {
                    MessageBox.Show("Alguno de los campos requeridos está vacio. \r\nLos campos requeridos son: \r\n*Nombre\r\n" +
                        "*Clave\r\n*Forma de Entrega\r\n*Tipo de Envío", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                int valorAEntero;
                if (!int.TryParse(txtBoxPedidoMinCondiciones.Text, out valorAEntero) || !int.TryParse(txtBoxPedidoMaxCondiciones.Text, out valorAEntero))
                {
                    MessageBox.Show("Los siguientes campos solo aceptan números: \r\n \r\n*Pedido Min\r\n" +
                        "*Pedido Max", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                EProveedorFletes Flete = new EProveedorFletes()
                {
                    ClaveProveedor = eProveedorDatosPrim.ClaveProveedor,
                    TipoEnvio = Convert.ToString(comboBoxTipoEnvioFletes.SelectedItem),
                    FormaEntrega = Convert.ToString(comboBoxFormaEntregaFletes.SelectedItem),
                    ClaveProveedorFlete = txtBoxClaveProveedorFletes.Text,
                    NombreProveedor = txtBoxNombreFleteCondiciones.Text,
                    DescripcionProveedor = "",
                    PedidoMin = Convert.ToInt32(txtBoxPedidoMinCondiciones.Text),
                    PedidoMax = Convert.ToInt32(txtBoxPedidoMaxCondiciones.Text),
                    Unidad = Convert.ToString(comboBoxUnidadPedidoCondiciones.SelectedItem),
                    Origen = txtBoxFleteOrigenCondiciones.Text,
                    Destino = txtBoxFleteDestinoCondiciones.Text,
                    EsPreferencia = false,
                    Observaciones = txtBoxObservacionesFletes.Text,
                    CostoFleteMatriz = txtBoxCantidadCostoSucursalMatrizFletes.Text,
                    CostoFleteHipodromo = txtBoxCantidadCostoSucursalHipodromoFletes.Text,
                    CostoFleteSanPedro = txtBoxCantidadCostoSucursalSanPedroFletes.Text,
                    CostoFleteMagdalena = txtBoxCantidadCostoSucursalMagdalenaFletes.Text,
                    CostoFleteCaborca = txtBoxCantidadCostoSucursalCabrocaFletes.Text,
                    CostoFleteCEDIS = txtBoxCantidadCostoSucursalCEDISFletes.Text,
                    CostoFleteCMA = txtBoxCantidadCostoSucursalCMAletes.Text
                };

                if (proveedorFletesBol.agregarFleteProveedor(Flete))
                {
                    proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    CargarCondiciones(eProveedorDatosPrim.ClaveProveedor);
                    CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                    MessageBox.Show(categoriaDato + " ha sido agregado exitosamente.", accionDato + " " + categoriaDato,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(categoriaDato + " NO ha podido ser agregado. " + System.Environment.NewLine + proveedorDireccionesBol.mensajeRespuestaSP,
                       accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminarFletes_Click(object sender, EventArgs e)
        {
            try
            {
                string accionDato = "Desactivar";
                string categoriaDato = "Flete";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ListaFletes = proveedorFletesBol.consultarFletesByClaveProveedorVal(Convert.ToString(eProveedorDatosPrim.ClaveProveedor));

                foreach (var i in ListaFletes)
                {
                    if (Convert.ToInt32(dataGridFletes.CurrentRow.Cells[0].Value) == i.Fleteid)
                    {
                        proveedorFletesBol.desactivarFleteByIdByClaveProveedorVal(i.Fleteid, i.ClaveProveedor);
                        MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                        proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        CargarCondiciones(eProveedorDatosPrim.ClaveProveedor);
                        CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void VerInformacionFletes_Click(object sender, EventArgs e)
        {
            try
            {
                string clave = txtBoxClaveProveedorFletes.Text;
                frmCatalogoProveedores nuevaVentana = new frmCatalogoProveedores();
                nuevaVentana.Show();
                nuevaVentana.btnCerrar.Visible = false;
                nuevaVentana.btnVerInformacionFletes.Visible = false;
                //nuevaVentana.eProveedorDatosPrim = proveedorDatosPrimBol.consultarProveedorDatosPrimByClaveProveedorVal(clave);
                nuevaVentana.txtBoxClaveDatosPrimarios.Text = clave;
                nuevaVentana.btnBuscarClave_Click(nuevaVentana, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        //Seccion Politicas
        private void btnLimpiarCamposObservacionesSolicitudCompra_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxObservacionesSolicitudes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnLimpiarCampoPoliticasDevoluciones_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxListaPoliticasDevoluciones.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpiarCampoPoliticasGarantias_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxListaPoliticasGarantias.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGuardarCambiosSeccionPoliticas1_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Políticas de Compra";
                string accionDato = "Actualizar";
                string recepcionSolicitudCompra = "";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (radioBtnMensualPoliticas.Checked)
                    recepcionSolicitudCompra = "Mensual";
                else if (radioBtnDiarioPoliticas.Checked)
                    recepcionSolicitudCompra = "Diario";
                else if (radioBtnSemanalPoliticas.Checked)
                    recepcionSolicitudCompra = "Semanal";
                else if (radioBtnOtroPoliticas.Checked)
                    recepcionSolicitudCompra = "Otro";

                EProveedorPoliticas P = new EProveedorPoliticas
                {
                    ClaveProveedor = eProveedorDatosPrim.ClaveProveedor,
                    //Politicasid
                    PoliticasGarantia = txtBoxListaPoliticasGarantias.Text,
                    PoliticasDevoluciones = txtBoxListaPoliticasDevoluciones.Text,
                    CompraMinimaMensual = txtBoxCompraMinPoliticas.Text,
                    RecepcionSolicitudCompra = recepcionSolicitudCompra,
                    ObservacionesSolicitudCompra = txtBoxObservacionesSolicitudes.Text
                };
                proveedorPoliticasBol.actualizarPoliticas(P);
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " han sido actualizado exitosamente.", accionDato + " " + categoriaDato,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGuardarCambiosSeccionPoliticas2_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Políticas de Compra";
                string accionDato = "Actualizar";
                string recepcionSolicitudCompra = "";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (radioBtnMensualPoliticas.Checked)
                    recepcionSolicitudCompra = "Mensual";
                else if (radioBtnDiarioPoliticas.Checked)
                    recepcionSolicitudCompra = "Diario";
                else if (radioBtnSemanalPoliticas.Checked)
                    recepcionSolicitudCompra = "Semanal";
                else if (radioBtnOtroPoliticas.Checked)
                    recepcionSolicitudCompra = "Otro";

                EProveedorPoliticas P = new EProveedorPoliticas
                {
                    ClaveProveedor = eProveedorDatosPrim.ClaveProveedor,
                    //Politicasid
                    PoliticasGarantia = txtBoxListaPoliticasGarantias.Text,
                    PoliticasDevoluciones = txtBoxListaPoliticasDevoluciones.Text,
                    CompraMinimaMensual = txtBoxCompraMinPoliticas.Text,
                    RecepcionSolicitudCompra = recepcionSolicitudCompra,
                    ObservacionesSolicitudCompra = txtBoxObservacionesSolicitudes.Text
                };
                proveedorPoliticasBol.actualizarPoliticas(P);
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                MessageBox.Show(categoriaDato + " han sido actualizado exitosamente.", accionDato + " " + categoriaDato,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAbrirDocPoliticasDevoluciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaPoliticasDevoluciones.SelectedItem) == "" || comboBoxListaPoliticasDevoluciones.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPoliticasDevoluciones.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }

        }
        private void btnEliminarArchivoPoliticasDevoluciones_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Políticas para Devoluciones";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaPoliticasDevoluciones.SelectedItem) == "" || comboBoxListaPoliticasDevoluciones.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPoliticasDevoluciones.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);


                comboBoxListaPoliticasDevoluciones.SelectedIndex = -1; 
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSubirArchivoPoliticasDevoluciones_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Políticas para Devoluciones";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAbrirDocPoliticasGarantias_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(comboBoxListaPoliticasGarantias.SelectedItem) == "" || comboBoxListaPoliticasGarantias.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }

                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPoliticasGarantias.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));
                System.Diagnostics.Process.Start(ubicacionArchivos.PATHArchivo);

                MessageBox.Show("El archivo seleccionado está abriéndose en el programa predeterminado.", "Abrir Archivo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: Es necesario verificar que se ha seleccionado un archivo.", ex.Message, MessageBoxButtons.OK);
            }
        }        
        private void btnEliminarArchivoPoliticasGarantias_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Políticas de Garantías";
                string accionDato = "Desactivar Uso de ";

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToString(comboBoxListaPoliticasGarantias.SelectedItem) == "" || comboBoxListaPoliticasGarantias.SelectedIndex == -1)
                {
                    MessageBox.Show("Es necesario verificar el origen del archivo.", "Archivo no encontrado.", MessageBoxButtons.OK);
                    return;
                }
                EProveedorUbicacionArchivos ubicacionArchivos = new EProveedorUbicacionArchivos();
                ProveedorUbicacionArchivosBol proveedorUbicacionArchivosBol = new ProveedorUbicacionArchivosBol();
                string[] cadenaArchivo = null;
                string nombreArchivo = comboBoxListaPoliticasGarantias.SelectedItem.ToString();
                cadenaArchivo = nombreArchivo.Split();
                int id = Convert.ToInt32(cadenaArchivo[0]);
                ubicacionArchivos = proveedorUbicacionArchivosBol.consultarUbicacionArchivosById(Convert.ToInt32(cadenaArchivo[0]));

                proveedorUbicacionArchivosBol.desactivarUbicacionArchivoByIdVal(Convert.ToInt32(ubicacionArchivos.UbicacionArchivoid));
                MessageBox.Show(categoriaDato + " ha sido desactivada exitosamente.", accionDato + " " + categoriaDato,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                comboBoxListaPoliticasGarantias.SelectedIndex = -1;
                proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);                
                CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSubirArchivoPoliticasGarantias_Click(object sender, EventArgs e)
        {
            try
            {
                string categoriaDato = "Políticas de Garantías";
                string accionDato = "Agregar";
                string imageLocation = "";
                string newLocation = "";
                OpenFileDialog dialog = new OpenFileDialog();

                if (eProveedorDatosPrim == null)
                {
                    MessageBox.Show("Ingresar datos de Proveedor para poder " + accionDato + " " + categoriaDato,
                          accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dialog.Filter = "All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    newLocation = copiarArchivoEnServidor(eProveedorDatosPrim.ClaveProveedor, imageLocation);

                    if (!(newLocation == ""))
                    {
                        string pathLocation = System.IO.Path.GetFullPath(newLocation);
                        if (!proveedorUbicacionArchivosBol.agregarUbicacionArchivo(pathLocation, eProveedorDatosPrim.ClaveProveedor, categoriaDato))
                        {
                            MessageBox.Show(categoriaDato + " NO ha podido ser agregada. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proveedorDatosPrimBol.actualizarUtlimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarUltimaActualizacion(eProveedorDatosPrim.ClaveProveedor);
                            CargarExpediente(eProveedorDatosPrim.ClaveProveedor);
                            MessageBox.Show(categoriaDato + " ha sido agregada exitosamente. " + System.Environment.NewLine + Convert.ToString(proveedorDatosPrimBol.mensajeRespuestaSP),
                           accionDato + " " + categoriaDato, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Otras       
        private void btnActualizarRecepcionSolicitudCompra_Click(object sender, EventArgs e)
        {
            if ((radioBtnDiarioPoliticas.Checked && eProveedorPoliticas.RecepcionSolicitudCompra == "Diario") ||
                (radioBtnSemanalPoliticas.Checked && eProveedorPoliticas.RecepcionSolicitudCompra == "Semanal") ||
                (radioBtnMensualPoliticas.Checked && eProveedorPoliticas.RecepcionSolicitudCompra == "Mensual") ||
                (radioBtnOtroPoliticas.Checked && eProveedorPoliticas.RecepcionSolicitudCompra == "Otro"))
                return;
            //Realizar edicion o UPDATE de campo
        }
        private void btnActualizarDiasCondicionesCredito_Click(object sender, EventArgs e)
        {
            try
            {
                int diasCondicionesCredito = Convert.ToInt32(txtBoxCondicionesCreditoCondiciones.Text);
                if(diasCondicionesCredito < 0)
                {
                    MessageBox.Show("La cantidad de días debe ser igual o mayor a 0");
                    return;
                }
                else
                {
                    //Realizar edicion o UPDATE de campo
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: {0}", ex.Message);
            }
        }
        private void txtBoxCondicionesCreditoCondiciones_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtBoxInstruccionesSubirArchivoPoliticas_TextChanged(object sender, EventArgs e)
        {

        }

        //
        //Funciones sin definición sin uso
        //

        private void checkBoxDireccionItem_CheckedChanged(object sender, EventArgs e)
        {
            //sin uso
        }
        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            //sin uso
        }
        private void pnlDatosPrimExpediente_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }
        private void lblCompraMinMensualSimbolo_Click(object sender, EventArgs e)
        {
            //Sin uso
        }
        private void txtBoxInstruccionesResultados_TextChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Sin uso
        }
        private void dateTimePickerDatosGen_ValueChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void panel2_Paint_2(object sender, PaintEventArgs e)
        {
            //Sin uso
        }
        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sin uso
        }
        private void pnlFleterosCondiciones_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }

        private void checkBoxPaqueteriaPagadaProveedor_CheckedChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Sin uso
        }        
        private void flowPnlDatosGen_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }

        private void pnlContactos_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }
        private void pnlContacto1_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }
        private void pnlDatosBancarios_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void btnSubirCaratulaEstadoCuenta_Click(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void txtBoxProveedorPoliticas_TextChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void pnlTablaCategorias_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Sin uso
        }

        private void lblFrecuenciaSolicitudesPoliticas_Click(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void pnlPoliticasCompra_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }

        private void panel9_Paint_1(object sender, PaintEventArgs e)
        {
            //Sin uso
        }

        private void label22_Click(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void label36_Click(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }
        //Solo muestra dos opciones posibles Sí o No
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        //Solo muestra valores del 0 al 100
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        //Acepta solamente números enteros positivos
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        //Agrega Sucursal a base de datos y añade un checkBox con el Nombre de Sucursal en el panel Padre
        private void btnAgregarSucursal_Click(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void label28_Click(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void btnEliminarFleteroCondiciones_Click(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            //Sin uso
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void lblListaPrecios_Click(object sender, EventArgs e)
        {
            //Sin uso
        }
        private void comboBoxMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void frmCatalogoEditor_Load(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void lblSSNDatosBancariosEU_Click(object sender, EventArgs e)
        {
            //Sin uso
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Sin uso
        }
        private void picBoxLogoProveedor_Click(object sender, EventArgs e)
        {
            //Sin uso
        }
        private void txtBoxCategoriaMargenUtilidad_TextChanged(object sender, EventArgs e)
        {
            //Sin uso
        }
        private void pnlDatosPrimarios_Paint(object sender, PaintEventArgs e)
        {
            //Sin uso
        }
        private void txtBoxProveedorDatosPrimarios_TextChanged(object sender, EventArgs e)
        {
            //sin uso
        }
        private void pnlDirecciones_Paint(object sender, PaintEventArgs e)
        {
            //sin uso
        }
        //
        //Funciones definidas sin utilizar
        //
        private void dataGridDirecciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //comboBoxConceptoUsoDirecciones.SelectedValue = dataGridDirecciones.CurrentRow.Cells[1].Value.ToString();
            //txtBoxDireccionDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[3].Value.ToString();
            //txtBoxNumExteriorDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[4].Value.ToString();
            //txtBoxNumInteriorDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[5].Value.ToString();
            //txtBoxColoniaDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[7].Value.ToString();
            //txtBoxInfAdicionalDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[6].Value.ToString();
            //txtBoxCPDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[8].Value.ToString();
            //txtBoxPoblacionDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[9].Value.ToString();
            //txtBoxEstadoDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[10].Value.ToString();
            //txtBoxPaisDirecciones.Text = dataGridDirecciones.CurrentRow.Cells[11].Value.ToString();
        }
        //private void btnSiguienteDirecciones_Click(object sender, EventArgs e)
        //{
        //    if (ListaDirecciones.Count == (iDireccion + 1))
        //    {
        //        return;
        //    }
        //    txtBoxDireccionDirecciones.Text = Convert.ToString(ListaDirecciones[++iDireccion].CalleAveBlvr);
        //    txtBoxNumExteriorDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].NumExterior);
        //    txtBoxNumInteriorDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].NumInterior);
        //    txtBoxColoniaDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].Colonia);
        //    txtBoxInfAdicionalDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].InfAdicional);
        //    txtBoxCPDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].CodigoPostal);
        //    txtBoxPoblacionDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].Poblacion);
        //    txtBoxEstadoDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].Estado);
        //    txtBoxPaisDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].Pais);

        //    txtBoxDireccion1.Clear();
        //    txtBoxDireccion1.Text += txtBoxDireccionDirecciones.Text + " #" +
        //        txtBoxNumExteriorDirecciones.Text + " " +
        //        txtBoxNumInteriorDirecciones.Text + " " +
        //        txtBoxColoniaDirecciones.Text +
        //        txtBoxInfAdicionalDirecciones.Text + System.Environment.NewLine + " " +
        //        txtBoxCPDirecciones.Text + System.Environment.NewLine + " " +
        //        txtBoxPoblacionDirecciones.Text.Replace("\n", String.Empty) + ", " +
        //        txtBoxEstadoDirecciones.Text.Replace("\n", String.Empty) + ", " +
        //        txtBoxPaisDirecciones.Text.Replace("\n", String.Empty);
        //}

        //private void btnAnteriorDirecciones_Click(object sender, EventArgs e)
        //{
        //    if (iDireccion == 0)
        //    {
        //        return;
        //    }
        //    txtBoxDireccionDirecciones.Text = Convert.ToString(ListaDirecciones[--iDireccion].CalleAveBlvr);
        //    txtBoxNumExteriorDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].NumExterior);
        //    txtBoxNumInteriorDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].NumInterior);
        //    txtBoxColoniaDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].Colonia);
        //    txtBoxInfAdicionalDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].InfAdicional);
        //    txtBoxCPDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].CodigoPostal);
        //    txtBoxPoblacionDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].Poblacion);
        //    txtBoxEstadoDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].Estado);
        //    txtBoxPaisDirecciones.Text = Convert.ToString(ListaDirecciones[iDireccion].Pais);

        //    txtBoxDireccion1.Clear();
        //    txtBoxDireccion1.Text += txtBoxDireccionDirecciones.Text + " #" +
        //        txtBoxNumExteriorDirecciones.Text + " " +
        //        txtBoxNumInteriorDirecciones.Text + " " +
        //        txtBoxColoniaDirecciones.Text +
        //        txtBoxInfAdicionalDirecciones.Text + System.Environment.NewLine + " " +
        //        txtBoxCPDirecciones.Text + System.Environment.NewLine + " " +
        //        txtBoxPoblacionDirecciones.Text.Replace("\n", String.Empty) + ", " +
        //        txtBoxEstadoDirecciones.Text.Replace("\n", String.Empty) + ", " +
        //        txtBoxPaisDirecciones.Text.Replace("\n", String.Empty);
        //}
        private void getListaDatosBancariosGenerales()
        {
            //List<EProveedorDatosBancariosMX> SortedListaDatosBancariosMX = ListaDatosBancariosMX.OrderBy(o => o.PrioridadDeUso).ToList();
            //List<EProveedorDatosBancariosEX> SortedListaDatosBancariosEX = ListaDatosBancariosEX.OrderBy(o => o.PrioridadDeUso).ToList();
            //foreach (var i in SortedListaDatosBancariosMX)
            //{

            //}
            return;
        }

        private void comboBoxConceptoUsoDirecciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxPaisDirecciones_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnActualizarPaginaWebProveedorDatosPrimarios_Click(object sender, EventArgs e)
        {
        }

        private void btnLimpiarCamposFletes_Click(object sender, EventArgs e)
        {

        }

        private void pnlAcuerdoCompra_Paint(object sender, PaintEventArgs e)
        {

        }        

        private void pnlListaDocumentos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblFormaEntregaCondiciones_Click(object sender, EventArgs e)
        {

        }

        private void pnlCondicionesPagoEntrega_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlDireccion1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridPlazosCredito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBoxCondicionesItem_CheckedChanged(object sender, EventArgs e)
        {

        }

       


        //private void btnSiguienteRutas_Click(object sender, EventArgs e)
        //{
        //    if (iRutas == ListaRutas.Count - 1)
        //    {
        //        return;
        //    }
        //    txtBoxOrigenRutaCondiciones.Text = Convert.ToString(ListaRutas[++iRutas].Origen);
        //    txtBoxDestinoRutaCondiciones.Text = Convert.ToString(ListaRutas[iRutas].Destino);

        //    txtBoxListaRutas.Clear();
        //    txtBoxListaRutas.Text += "De: " + txtBoxOrigenRutaCondiciones.Text + System.Environment.NewLine +
        //        "A: " + txtBoxDestinoRutaCondiciones.Text;
        //}

        //private void btnAnteriorRutas_Click(object sender, EventArgs e)
        //{
        //    if (iRutas == 0)
        //    {
        //        return;
        //    }
        //    txtBoxOrigenRutaCondiciones.Text = Convert.ToString(ListaRutas[--iRutas].Origen);
        //    txtBoxDestinoRutaCondiciones.Text = Convert.ToString(ListaRutas[iRutas].Destino);

        //    txtBoxListaRutas.Clear();
        //    txtBoxListaRutas.Text += "De: " + txtBoxOrigenRutaCondiciones.Text + System.Environment.NewLine +
        //        "A: " + txtBoxDestinoRutaCondiciones.Text;

        //}

        //private void btnSiguienteFletes_Click(object sender, EventArgs e)
        //{
        //    if (iFletes == ListaFletes.Count - 1)
        //    {
        //        return;
        //    }
        //    txtBoxNombreFleteCondiciones.Text = Convert.ToString(ListaFletes[++iFletes].NombreDescripcion);
        //    txtBoxTel1FleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].TelefonoPrimario);
        //    txtBoxTel2FleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].TelefonoSecundario);
        //    txtBoxPuestoFleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].Puesto);
        //    txtBoxEmail1FleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].Email);
        //    txtBoxContactoFleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].Contacto);

        //    txtBoxListaFletes.Clear();
        //    txtBoxListaFletes.Text += txtBoxNombreFleteCondiciones.Text + System.Environment.NewLine +
        //        "Contacto: " + txtBoxContactoFleteCondiciones.Text + System.Environment.NewLine +
        //        txtBoxPuestoFleteCondiciones.Text + System.Environment.NewLine +
        //        txtBoxTel1FleteCondiciones.Text + System.Environment.NewLine +
        //        txtBoxTel2FleteCondiciones.Text + System.Environment.NewLine +
        //        txtBoxEmail1FleteCondiciones.Text + System.Environment.NewLine;
        //}

        //private void btnAnteriorFletes_Click(object sender, EventArgs e)
        //{
        //    if (iFletes == 0)
        //    {
        //        return;
        //    }
        //    txtBoxNombreFleteCondiciones.Text = Convert.ToString(ListaFletes[--iFletes].NombreDescripcion);
        //    txtBoxTel1FleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].TelefonoPrimario);
        //    txtBoxTel2FleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].TelefonoSecundario);
        //    txtBoxPuestoFleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].Puesto);
        //    txtBoxEmail1FleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].Email);
        //    txtBoxContactoFleteCondiciones.Text = Convert.ToString(ListaFletes[iFletes].Contacto);

        //    txtBoxListaFletes.Clear();
        //    txtBoxListaFletes.Text += txtBoxNombreFleteCondiciones.Text + System.Environment.NewLine +
        //        "Contacto: " + txtBoxContactoFleteCondiciones.Text + System.Environment.NewLine +
        //        txtBoxPuestoFleteCondiciones.Text + System.Environment.NewLine +
        //        txtBoxTel1FleteCondiciones.Text + System.Environment.NewLine +
        //        txtBoxTel2FleteCondiciones.Text + System.Environment.NewLine +
        //        txtBoxEmail1FleteCondiciones.Text + System.Environment.NewLine;
        //}


        //private void btnAnteriorDatosBancarios_Click(object sender, EventArgs e)
        //{
        //    if (iDatosBancariosMX == 0)
        //    {
        //        return;
        //    }
        //    txtBoxBancoDatosBancariosMX.Text = Convert.ToString(ListaDatosBancariosMX[--iDatosBancariosMX].NombreBancoDestino);
        //    txtBoxCLABEDatosBancariosMX.Text = Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].CLABE);
        //    txtBoxNumeroCuentaDestinatarioMX.Text = Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].NumeroCuentaDestinatario);
        //    txtBoxSucursalDatosBancariosMX.Text = Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].Sucursal);
        //    comboBoxDivisaCuentaBancariaMX.SelectedItem = Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].DivisaAPagar);

        //    txtBoxCuentaBancariaMX.Clear();
        //    txtBoxCuentaBancariaMX.Text += "Banco: " + txtBoxBancoDatosBancariosMX.Text + System.Environment.NewLine +
        //        "CLABE: " + txtBoxCLABEDatosBancariosMX.Text + System.Environment.NewLine +
        //        "Número de Cuenta: " + txtBoxNumeroCuentaDestinatarioMX.Text + System.Environment.NewLine +
        //        "Sucursal: " + txtBoxSucursalDatosBancariosMX.Text + System.Environment.NewLine +
        //        "Divisa: " + Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].DivisaAPagar);
        //}

        //private void btnSiguienteDatosBancarios_Click(object sender, EventArgs e)
        //{
        //    if (iDatosBancariosMX == (ListaDatosBancariosMX.Count - 1))
        //    {
        //        return;
        //    }
        //    txtBoxBancoDatosBancariosMX.Text = Convert.ToString(ListaDatosBancariosMX[++iDatosBancariosMX].NombreBancoDestino);
        //    txtBoxCLABEDatosBancariosMX.Text = Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].CLABE);
        //    txtBoxNumeroCuentaDestinatarioMX.Text = Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].NumeroCuentaDestinatario);
        //    txtBoxSucursalDatosBancariosMX.Text = Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].Sucursal);
        //    comboBoxDivisaCuentaBancariaMX.SelectedItem = Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].DivisaAPagar);

        //    txtBoxCuentaBancariaMX.Clear();
        //    txtBoxCuentaBancariaMX.Text += "Banco: " + txtBoxBancoDatosBancariosMX.Text + System.Environment.NewLine +
        //        "CLABE: " + txtBoxCLABEDatosBancariosMX.Text + System.Environment.NewLine +
        //        "Número de Cuenta: " + txtBoxNumeroCuentaDestinatarioMX.Text + System.Environment.NewLine +
        //        "Sucursal: " + txtBoxSucursalDatosBancariosMX.Text + System.Environment.NewLine +
        //        "Divisa: " + Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].DivisaAPagar);
        //}

        //private void btnAnteriorContactos_Click(object sender, EventArgs e)
        //{
        //    if (iContacto == 0)
        //    {
        //        return;
        //    }
        //    txtBoxNombreContactos.Text = Convert.ToString(ListaContactos[--iContacto].NombreCompleto);
        //    txtBoxPuestoContactos.Text = Convert.ToString(ListaContactos[iContacto].Puesto);
        //    txtBoxTel1Contactos.Text = Convert.ToString(ListaContactos[iContacto].TelefonoPrimario);
        //    txtBoxTel2Contactos.Text = Convert.ToString(ListaContactos[iContacto].TelefonoSecundario);
        //    txtBoxCategoriaContactos.Text = Convert.ToString(ListaContactos[iContacto].Categoría);
        //    txtBoxEmail1Contactos.Text = Convert.ToString(ListaContactos[iContacto].Email);

        //    txtBoxContacto1.Clear();
        //    txtBoxContacto1.Text += txtBoxNombreContactos.Text + System.Environment.NewLine +
        //        " " + txtBoxPuestoContactos.Text + System.Environment.NewLine +
        //        " " + txtBoxTel1Contactos.Text + System.Environment.NewLine +
        //        " " + txtBoxTel2Contactos.Text + System.Environment.NewLine +
        //        " " + txtBoxCategoriaContactos.Text + System.Environment.NewLine +
        //        " " + txtBoxEmail1Contactos.Text + System.Environment.NewLine +
        //        " " + Convert.ToString(ListaContactos[iContacto].Comentarios) + System.Environment.NewLine;
        //}
        //private void btnSiguienteContactos_Click(object sender, EventArgs e)
        //{
        //    if (iContacto == (ListaContactos.Count - 1))
        //    {
        //        return;
        //    }
        //    txtBoxNombreContactos.Text = Convert.ToString(ListaContactos[++iContacto].NombreCompleto);
        //    txtBoxPuestoContactos.Text = Convert.ToString(ListaContactos[iContacto].Puesto);
        //    txtBoxTel1Contactos.Text = Convert.ToString(ListaContactos[iContacto].TelefonoPrimario);
        //    txtBoxTel2Contactos.Text = Convert.ToString(ListaContactos[iContacto].TelefonoSecundario);
        //    txtBoxCategoriaContactos.Text = Convert.ToString(ListaContactos[iContacto].Categoría);
        //    txtBoxEmail1Contactos.Text = Convert.ToString(ListaContactos[iContacto].Email);

        //    txtBoxContacto1.Clear();
        //    txtBoxContacto1.Text += txtBoxNombreContactos.Text + System.Environment.NewLine +
        //        " " + txtBoxPuestoContactos.Text + System.Environment.NewLine +
        //        " " + txtBoxTel1Contactos.Text + System.Environment.NewLine +
        //        " " + txtBoxTel2Contactos.Text + System.Environment.NewLine +
        //        " " + txtBoxCategoriaContactos.Text + System.Environment.NewLine +
        //        " " + txtBoxEmail1Contactos.Text + System.Environment.NewLine +
        //        " " + Convert.ToString(ListaContactos[iContacto].Comentarios) + System.Environment.NewLine;
        //}
        //private void btnAnteriorDatosBancariosEX_Click(object sender, EventArgs e)
        //{
        //    if (iDatosBancariosEX == 0)
        //    {
        //        return;
        //    }
        //    txtBoxNombreBancoDestinoEX.Text = Convert.ToString(ListaDatosBancariosEX[--iDatosBancariosEX].NombreBancoDestino);
        //    txtBoxClaveBancoDestinoEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].ClaveBancoDestino);
        //    txtBoxNumeroCuentaDestinatarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].NumeroCuentaDestinatario);
        //    txtBoxNombreDestinatarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].NombreDestinatario);
        //    txtBoxMontoMaxEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].MontoMaximoAPagar);
        //    comboBoxDivisaCuentaBancariaEX.SelectedItem = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].DivisaAPagar);
        //    txtBoxNombreBancoIntermediarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].NombreBancoIntermediario);
        //    txtBoxClaveBancoIntermediarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].ClaveBancoIntermediario);
        //    if (ListaDatosBancariosEX[iDatosBancariosEX].Vigencia == 1)
        //    {
        //        radioBtnSi.Checked = true;
        //        lblFechaVigenciaEX.Visible = true;
        //        dateTimePickerDatosBancariosEX.Visible = true;
        //        dateTimePickerDatosBancariosEX.Value = Convert.ToDateTime(ListaDatosBancariosEX[iDatosBancariosEX].FechaDeVigencia);
        //        vigenciaDatosBancariosEX = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].StringFechaDeVigencia);
        //    }
        //    else
        //    {
        //        radioBtnNo.Checked = true;
        //        lblFechaVigenciaEX.Visible = false;
        //        dateTimePickerDatosBancariosEX.Visible = false;
        //        vigenciaDatosBancariosEX = "No";
        //    }
        //    txtBoxTipoRelacionConDestinatarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].TipoRelacionConDestinatario);
        //    txtBoxMotivoPagoEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].MotivoPago);
        //    //txtBoxCalleAveBlvrEX.Text = Convert.ToString(ListaDirecciones[iDireccion].CalleAveBlvr);
        //    //txtBoxNumExteriorEX.Text = Convert.ToString(ListaDirecciones[iDireccion].NumExterior);
        //    //txtBoxNumInteriorEX.Text = Convert.ToString(ListaDirecciones[iDireccion].NumInterior);
        //    //txtBoxColoniaEX.Text = Convert.ToString(ListaDirecciones[iDireccion].Colonia);
        //    //txtBoxInfAdicionalEX.Text = Convert.ToString(ListaDirecciones[iDireccion].InfAdicional);
        //    //txtBoxCPEX.Text = Convert.ToString(ListaDirecciones[iDireccion].CodigoPostal);
        //    //txtBoxPoblacionEX.Text = Convert.ToString(ListaDirecciones[iDireccion].Poblacion);
        //    //txtBoxEstadoEX.Text = Convert.ToString(ListaDirecciones[iDireccion].Estado);
        //    //txtBoxPaisEX.Text = Convert.ToString(ListaDirecciones[iDireccion].Pais);

        //    txtBoxCuentaBancariaEX.Clear();
        //    txtBoxCuentaBancariaEX.Text += "Banco: " + txtBoxNombreBancoDestinoEX.Text + System.Environment.NewLine +
        //        "Clave Banco: " + txtBoxClaveBancoDestinoEX.Text + System.Environment.NewLine +
        //        "Número de Cuenta: " + txtBoxNumeroCuentaDestinatarioEX.Text + System.Environment.NewLine +
        //        "Destinatario: " + txtBoxNombreDestinatarioEX.Text + System.Environment.NewLine +
        //        "Divisa: " + Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].DivisaAPagar) + System.Environment.NewLine +
        //        "Monto Máximo a Pagar: " + txtBoxMontoMaxEX.Text + System.Environment.NewLine +
        //        "Intermediario:" + System.Environment.NewLine +
        //        txtBoxNombreBancoIntermediarioEX.Text + System.Environment.NewLine +
        //        txtBoxClaveBancoIntermediarioEX.Text + System.Environment.NewLine +
        //        "Relacion: " + txtBoxTipoRelacionConDestinatarioEX.Text + System.Environment.NewLine +
        //        "Motivo de Pago: " + txtBoxMotivoPagoEX.Text + System.Environment.NewLine +
        //        "Vigencia: " + vigenciaDatosBancariosEX;
        //}

        //private void btnSiguienteDatosBancariosEX_Click(object sender, EventArgs e)
        //{
        //    if (iDatosBancariosEX == ListaDatosBancariosEX.Count - 1)
        //    {
        //        return;
        //    }
        //    txtBoxNombreBancoDestinoEX.Text = Convert.ToString(ListaDatosBancariosEX[++iDatosBancariosEX].NombreBancoDestino);
        //    txtBoxClaveBancoDestinoEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].ClaveBancoDestino);
        //    txtBoxNumeroCuentaDestinatarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].NumeroCuentaDestinatario);
        //    txtBoxNombreDestinatarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].NombreDestinatario);
        //    txtBoxMontoMaxEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].MontoMaximoAPagar);
        //    comboBoxDivisaCuentaBancariaEX.SelectedItem = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].DivisaAPagar);
        //    txtBoxNombreBancoIntermediarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].NombreBancoIntermediario);
        //    txtBoxClaveBancoIntermediarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].ClaveBancoIntermediario);
        //    if (ListaDatosBancariosEX[iDatosBancariosEX].Vigencia == 1)
        //    {
        //        radioBtnSi.Checked = true;
        //        lblFechaVigenciaEX.Visible = true;
        //        dateTimePickerDatosBancariosEX.Visible = true;
        //        dateTimePickerDatosBancariosEX.Value = Convert.ToDateTime(ListaDatosBancariosEX[iDatosBancariosEX].FechaDeVigencia);
        //        vigenciaDatosBancariosEX = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].StringFechaDeVigencia);
        //    }
        //    else
        //    {
        //        radioBtnNo.Checked = true;
        //        lblFechaVigenciaEX.Visible = false;
        //        dateTimePickerDatosBancariosEX.Visible = false;
        //        vigenciaDatosBancariosEX = "No";
        //    }
        //    txtBoxTipoRelacionConDestinatarioEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].TipoRelacionConDestinatario);
        //    txtBoxMotivoPagoEX.Text = Convert.ToString(ListaDatosBancariosEX[iDatosBancariosEX].MotivoPago);
        //    //txtBoxCalleAveBlvrEX.Text = Convert.ToString(ListaDirecciones[iDireccion].CalleAveBlvr);
        //    //txtBoxNumExteriorEX.Text = Convert.ToString(ListaDirecciones[iDireccion].NumExterior);
        //    //txtBoxNumInteriorEX.Text = Convert.ToString(ListaDirecciones[iDireccion].NumInterior);
        //    //txtBoxColoniaEX.Text = Convert.ToString(ListaDirecciones[iDireccion].Colonia);
        //    //txtBoxInfAdicionalEX.Text = Convert.ToString(ListaDirecciones[iDireccion].InfAdicional);
        //    //txtBoxCPEX.Text = Convert.ToString(ListaDirecciones[iDireccion].CodigoPostal);
        //    //txtBoxPoblacionEX.Text = Convert.ToString(ListaDirecciones[iDireccion].Poblacion);
        //    //txtBoxEstadoEX.Text = Convert.ToString(ListaDirecciones[iDireccion].Estado);
        //    //txtBoxPaisEX.Text = Convert.ToString(ListaDirecciones[iDireccion].Pais);

        //    txtBoxCuentaBancariaEX.Clear();
        //    txtBoxCuentaBancariaEX.Text += "Banco: " + txtBoxNombreBancoDestinoEX.Text + System.Environment.NewLine +
        //        "Clave Banco: " + txtBoxClaveBancoDestinoEX.Text + System.Environment.NewLine +
        //        "Número de Cuenta: " + txtBoxNumeroCuentaDestinatarioEX.Text + System.Environment.NewLine +
        //        "Destinatario: " + txtBoxNombreDestinatarioEX.Text + System.Environment.NewLine +
        //        "Divisa: " + Convert.ToString(ListaDatosBancariosMX[iDatosBancariosMX].DivisaAPagar) + System.Environment.NewLine +
        //        "Monto Máximo a Pagar: " + txtBoxMontoMaxEX.Text + System.Environment.NewLine +
        //        "Intermediario:" + System.Environment.NewLine +
        //        txtBoxNombreBancoIntermediarioEX.Text + System.Environment.NewLine +
        //        txtBoxClaveBancoIntermediarioEX.Text + System.Environment.NewLine +
        //        "Relacion: " + txtBoxTipoRelacionConDestinatarioEX.Text + System.Environment.NewLine +
        //        "Motivo de Pago: " + txtBoxMotivoPagoEX.Text + System.Environment.NewLine +
        //        "Vigencia: " + vigenciaDatosBancariosEX;
        //}
    }
}
