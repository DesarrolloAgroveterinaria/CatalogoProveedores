using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProveedorPrueba
{
    public partial class frmNotificaciones : Form
    {
        public string ClaveProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public frmNotificaciones()
        {
            InitializeComponent();            
            //CargarActividad();
            //CargarProximasActualizacionesIndividual();
            //CargarProximasActualizacionesGeneral();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void CargarActividad()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarProximasActualizacionesIndividual()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarProximasActualizacionesGeneral()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
