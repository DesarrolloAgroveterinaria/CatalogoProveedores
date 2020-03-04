using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProveedorEntidades;
using ProveedorLogicaNegocio;

namespace ProveedorPrueba
{
    public partial class frmCambiarContrasena : MetroFramework.Forms.MetroForm
    {
        public frmCambiarContrasena()
        {
            InitializeComponent();
        }

        private void pnlBarraSuperior_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxContrasena1.Text != txtBoxContrasena2.Text)
                {
                    lblMensajeInvalidez.Visible = true;
                    MessageBox.Show("Verificar que las contraseñas coincidan.", "Cambiar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (textBoxContrasena1.Text == "1")
                        MessageBox.Show("Cambiar Contraseña a una diferente de la Contraseña predeterminada", "Cambiar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ProveedorUsuariosBol proveedorUsuarioBol = new ProveedorUsuariosBol();
                    proveedorUsuarioBol.cambiarValorContrasena(txtBoxUsuario.Text, textBoxContrasena1.Text);
                    lblMensajeInvalidez.Visible = false;
                    MessageBox.Show("Ya puedes iniciar sesión con la nueva contraseña.", "Cambiar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
            }            
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente problema: " + ex.Message, "Inicio Sesion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBoxContrasena2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnGuardarCambios_Click(btnGuardarCambios, new EventArgs());
            }
        }
    }
}
