namespace ProveedorPrueba
{
    partial class frmCambiarContrasena
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBarraSuperior = new System.Windows.Forms.Panel();
            this.lblCambiarContrasena = new System.Windows.Forms.Label();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblMensajeCambiarContrasena = new System.Windows.Forms.Label();
            this.lblMensajeInvalidez = new System.Windows.Forms.Label();
            this.txtBoxContrasena2 = new System.Windows.Forms.TextBox();
            this.lblContrasena2 = new System.Windows.Forms.Label();
            this.textBoxContrasena1 = new System.Windows.Forms.TextBox();
            this.lblContrasena1 = new System.Windows.Forms.Label();
            this.txtBoxUsuario = new System.Windows.Forms.TextBox();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.pnlBarraSuperior.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBarraSuperior
            // 
            this.pnlBarraSuperior.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.pnlBarraSuperior.Controls.Add(this.lblCambiarContrasena);
            this.pnlBarraSuperior.Controls.Add(this.btnMin);
            this.pnlBarraSuperior.Controls.Add(this.btnCerrar);
            this.pnlBarraSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlBarraSuperior.Name = "pnlBarraSuperior";
            this.pnlBarraSuperior.Size = new System.Drawing.Size(393, 30);
            this.pnlBarraSuperior.TabIndex = 4;
            this.pnlBarraSuperior.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBarraSuperior_Paint);
            // 
            // lblCambiarContrasena
            // 
            this.lblCambiarContrasena.AutoSize = true;
            this.lblCambiarContrasena.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCambiarContrasena.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCambiarContrasena.Location = new System.Drawing.Point(15, 9);
            this.lblCambiarContrasena.Name = "lblCambiarContrasena";
            this.lblCambiarContrasena.Size = new System.Drawing.Size(133, 17);
            this.lblCambiarContrasena.TabIndex = 5;
            this.lblCambiarContrasena.Text = "Cambiar Contraseña";
            // 
            // btnMin
            // 
            this.btnMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnMin.Location = new System.Drawing.Point(323, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(35, 30);
            this.btnMin.TabIndex = 3;
            this.btnMin.Text = "-";
            this.btnMin.UseVisualStyleBackColor = true;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.Location = new System.Drawing.Point(358, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(35, 30);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // lblMensajeCambiarContrasena
            // 
            this.lblMensajeCambiarContrasena.AutoSize = true;
            this.lblMensajeCambiarContrasena.Font = new System.Drawing.Font("Microsoft PhagsPa", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeCambiarContrasena.Location = new System.Drawing.Point(88, 91);
            this.lblMensajeCambiarContrasena.Name = "lblMensajeCambiarContrasena";
            this.lblMensajeCambiarContrasena.Size = new System.Drawing.Size(224, 17);
            this.lblMensajeCambiarContrasena.TabIndex = 5;
            this.lblMensajeCambiarContrasena.Text = "Es necesario cambiar tu contraseña";
            // 
            // lblMensajeInvalidez
            // 
            this.lblMensajeInvalidez.AutoSize = true;
            this.lblMensajeInvalidez.ForeColor = System.Drawing.Color.Red;
            this.lblMensajeInvalidez.Location = new System.Drawing.Point(103, 255);
            this.lblMensajeInvalidez.Name = "lblMensajeInvalidez";
            this.lblMensajeInvalidez.Size = new System.Drawing.Size(130, 13);
            this.lblMensajeInvalidez.TabIndex = 39;
            this.lblMensajeInvalidez.Text = "Contraseñas no coinciden";
            this.lblMensajeInvalidez.Visible = false;
            // 
            // txtBoxContrasena2
            // 
            this.txtBoxContrasena2.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxContrasena2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtBoxContrasena2.Location = new System.Drawing.Point(106, 221);
            this.txtBoxContrasena2.Name = "txtBoxContrasena2";
            this.txtBoxContrasena2.Size = new System.Drawing.Size(241, 21);
            this.txtBoxContrasena2.TabIndex = 2;
            this.txtBoxContrasena2.UseSystemPasswordChar = true;
            this.txtBoxContrasena2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxContrasena2_KeyDown);
            // 
            // lblContrasena2
            // 
            this.lblContrasena2.AutoSize = true;
            this.lblContrasena2.Font = new System.Drawing.Font("Microsoft PhagsPa", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContrasena2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblContrasena2.Location = new System.Drawing.Point(23, 225);
            this.lblContrasena2.Name = "lblContrasena2";
            this.lblContrasena2.Size = new System.Drawing.Size(77, 17);
            this.lblContrasena2.TabIndex = 38;
            this.lblContrasena2.Text = "Contraseña";
            // 
            // textBoxContrasena1
            // 
            this.textBoxContrasena1.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxContrasena1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxContrasena1.Location = new System.Drawing.Point(106, 185);
            this.textBoxContrasena1.Name = "textBoxContrasena1";
            this.textBoxContrasena1.Size = new System.Drawing.Size(241, 21);
            this.textBoxContrasena1.TabIndex = 1;
            this.textBoxContrasena1.UseSystemPasswordChar = true;
            // 
            // lblContrasena1
            // 
            this.lblContrasena1.AutoSize = true;
            this.lblContrasena1.Font = new System.Drawing.Font("Microsoft PhagsPa", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContrasena1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblContrasena1.Location = new System.Drawing.Point(23, 189);
            this.lblContrasena1.Name = "lblContrasena1";
            this.lblContrasena1.Size = new System.Drawing.Size(77, 17);
            this.lblContrasena1.TabIndex = 41;
            this.lblContrasena1.Text = "Contraseña";
            // 
            // txtBoxUsuario
            // 
            this.txtBoxUsuario.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxUsuario.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtBoxUsuario.Location = new System.Drawing.Point(106, 111);
            this.txtBoxUsuario.Name = "txtBoxUsuario";
            this.txtBoxUsuario.Size = new System.Drawing.Size(178, 21);
            this.txtBoxUsuario.TabIndex = 0;
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarCambios.Font = new System.Drawing.Font("Microsoft PhagsPa", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarCambios.Location = new System.Drawing.Point(80, 321);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(241, 25);
            this.btnGuardarCambios.TabIndex = 3;
            this.btnGuardarCambios.Text = "Guardar Cambios";
            this.btnGuardarCambios.UseVisualStyleBackColor = true;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // frmCambiarContrasena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(392, 450);
            this.Controls.Add(this.btnGuardarCambios);
            this.Controls.Add(this.txtBoxUsuario);
            this.Controls.Add(this.textBoxContrasena1);
            this.Controls.Add(this.lblContrasena1);
            this.Controls.Add(this.lblMensajeInvalidez);
            this.Controls.Add(this.txtBoxContrasena2);
            this.Controls.Add(this.lblContrasena2);
            this.Controls.Add(this.lblMensajeCambiarContrasena);
            this.Controls.Add(this.pnlBarraSuperior);
            this.Name = "frmCambiarContrasena";
            this.pnlBarraSuperior.ResumeLayout(false);
            this.pnlBarraSuperior.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBarraSuperior;
        private System.Windows.Forms.Label lblCambiarContrasena;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblMensajeCambiarContrasena;
        private System.Windows.Forms.Label lblMensajeInvalidez;
        private System.Windows.Forms.TextBox txtBoxContrasena2;
        private System.Windows.Forms.Label lblContrasena2;
        private System.Windows.Forms.TextBox textBoxContrasena1;
        private System.Windows.Forms.Label lblContrasena1;
        public System.Windows.Forms.TextBox txtBoxUsuario;
        private System.Windows.Forms.Button btnGuardarCambios;
    }
}