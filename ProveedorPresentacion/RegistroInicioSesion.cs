using System;
using System.Collections.Generic;
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

namespace ProveedorPrueba
{
    public partial class frmRegistroInicioSesion : MetroFramework.Forms.MetroForm
    {
        private readonly ProveedorUsuariosBol proveedorUsuariosBol = new ProveedorUsuariosBol();
        public frmRegistroInicioSesion()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pnlBarraSuperior_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void picBoxLogoAgro_Click(object sender, EventArgs e)
        {

        }
        
        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {            
            try
            {
                string inicio = proveedorUsuariosBol.iniciarSesion(txtBoxUsuario.Text, txtBoxContra.Text);
                if (inicio == "")
                {
                    lblMensajeInvalidez.Visible = true;
                    MessageBox.Show("Usuario incorrecto.", "Inicio Sesion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                lblMensajeInvalidez.Visible = false;
                this.Hide();
                frmCatalogoProveedores Catalogo = new frmCatalogoProveedores();
                Catalogo.eProveedorUsuario.Usuario = txtBoxUsuario.Text;
                Catalogo.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente problema: " + ex.Message, "Inicio Sesion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblCatalogoDeProveedores_Click(object sender, EventArgs e)
        {

        }

        private void k(object sender, EventArgs e)
        {

        }

        private void txtBoxContra_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnIniciarSesion_Click(btnIniciarSesion, new EventArgs());
            }
        }
    }
}
