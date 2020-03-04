using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ProveedorEntidades;
//using ProveedorLogicaNegocio;

namespace ProveedorPrueba
{
    public partial class TablaResultadosProveedor : MetroFramework.Forms.MetroForm
    {
        public String valorClaveProveedor { get; set; }

        public TablaResultadosProveedor()
        {
            InitializeComponent();
        }

        private void TablaResultadosProveedor_Load(object sender, EventArgs e)
        {

        }

        private void dataGridProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnMostrarProveedorSeleccionado_Click(object sender, EventArgs e)
        {
            valorClaveProveedor = dataGridProveedores.CurrentRow.Cells[0].Value.ToString();
            this.Hide();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}