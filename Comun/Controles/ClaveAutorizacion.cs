using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comun.Controles
{
    public partial class ClaveAutorizacion : Form
    {
        public string Usuario { get; set; }
        public string Mov { get; set; }
        public string Almacen { get; set; }
        public ClaveAutorizacion()
        {
            InitializeComponent();
        }

        private void ClaveAutorizacion_Load(object sender, EventArgs e)
        {
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(txtClave.Text == "")
            {
                MessageBox.Show("Debe de indicar la clave de autorizacion pra continuar", "Clave Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dt = Comun.Clases.Consultas.AutorizarConClave(Mov, Almacen, txtClave.Text);
            if(dt != null && dt.Rows[0][0].ToString() == "1")
            {
                Usuario = dt.Rows[0][1].ToString();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("La clave ingresada es incorrecta, intente de nuevo", "Clave Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
