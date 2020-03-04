namespace ProveedorPrueba
{
    partial class frmRegistroInicioSesion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroInicioSesion));
            this.pnlBarraSuperior = new System.Windows.Forms.Panel();
            this.lblIncioSesion = new System.Windows.Forms.Label();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.picBoxLogoAgro = new System.Windows.Forms.PictureBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblContra = new System.Windows.Forms.Label();
            this.txtBoxUsuario = new System.Windows.Forms.TextBox();
            this.lblCatalogoDeProveedores = new System.Windows.Forms.Label();
            this.txtBoxContra = new System.Windows.Forms.TextBox();
            this.lblMensajeInvalidez = new System.Windows.Forms.Label();
            this.btnIniciarSesion = new System.Windows.Forms.Button();
            this.dragControl1 = new MisControles.DragControl();
            this.pnlBarraSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogoAgro)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBarraSuperior
            // 
            this.pnlBarraSuperior.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.pnlBarraSuperior.Controls.Add(this.lblIncioSesion);
            this.pnlBarraSuperior.Controls.Add(this.btnMin);
            this.pnlBarraSuperior.Controls.Add(this.btnCerrar);
            this.pnlBarraSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlBarraSuperior.Name = "pnlBarraSuperior";
            this.pnlBarraSuperior.Size = new System.Drawing.Size(657, 30);
            this.pnlBarraSuperior.TabIndex = 3;
            this.pnlBarraSuperior.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBarraSuperior_Paint);
            // 
            // lblIncioSesion
            // 
            this.lblIncioSesion.AutoSize = true;
            this.lblIncioSesion.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncioSesion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblIncioSesion.Location = new System.Drawing.Point(15, 9);
            this.lblIncioSesion.Name = "lblIncioSesion";
            this.lblIncioSesion.Size = new System.Drawing.Size(91, 17);
            this.lblIncioSesion.TabIndex = 5;
            this.lblIncioSesion.Text = "Iniciar Sesión";
            // 
            // btnMin
            // 
            this.btnMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnMin.Location = new System.Drawing.Point(587, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(35, 30);
            this.btnMin.TabIndex = 3;
            this.btnMin.Text = "-";
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.Location = new System.Drawing.Point(622, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(35, 30);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // picBoxLogoAgro
            // 
            this.picBoxLogoAgro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picBoxLogoAgro.ErrorImage = null;
            this.picBoxLogoAgro.Image = ((System.Drawing.Image)(resources.GetObject("picBoxLogoAgro.Image")));
            this.picBoxLogoAgro.InitialImage = null;
            this.picBoxLogoAgro.Location = new System.Drawing.Point(229, 63);
            this.picBoxLogoAgro.Name = "picBoxLogoAgro";
            this.picBoxLogoAgro.Size = new System.Drawing.Size(200, 200);
            this.picBoxLogoAgro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxLogoAgro.TabIndex = 4;
            this.picBoxLogoAgro.TabStop = false;
            this.picBoxLogoAgro.Click += new System.EventHandler(this.picBoxLogoAgro_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft PhagsPa", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUsuario.Location = new System.Drawing.Point(160, 343);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(55, 17);
            this.lblUsuario.TabIndex = 29;
            this.lblUsuario.Text = "Usuario";
            // 
            // lblContra
            // 
            this.lblContra.AutoSize = true;
            this.lblContra.Font = new System.Drawing.Font("Microsoft PhagsPa", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContra.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblContra.Location = new System.Drawing.Point(140, 379);
            this.lblContra.Name = "lblContra";
            this.lblContra.Size = new System.Drawing.Size(77, 17);
            this.lblContra.TabIndex = 30;
            this.lblContra.Text = "Contraseña";
            // 
            // txtBoxUsuario
            // 
            this.txtBoxUsuario.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxUsuario.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtBoxUsuario.Location = new System.Drawing.Point(223, 343);
            this.txtBoxUsuario.Name = "txtBoxUsuario";
            this.txtBoxUsuario.Size = new System.Drawing.Size(241, 21);
            this.txtBoxUsuario.TabIndex = 0;
            // 
            // lblCatalogoDeProveedores
            // 
            this.lblCatalogoDeProveedores.AutoSize = true;
            this.lblCatalogoDeProveedores.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatalogoDeProveedores.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCatalogoDeProveedores.Location = new System.Drawing.Point(167, 278);
            this.lblCatalogoDeProveedores.Name = "lblCatalogoDeProveedores";
            this.lblCatalogoDeProveedores.Size = new System.Drawing.Size(335, 35);
            this.lblCatalogoDeProveedores.TabIndex = 34;
            this.lblCatalogoDeProveedores.Text = "Catálogo de Proveedores";
            this.lblCatalogoDeProveedores.Click += new System.EventHandler(this.lblCatalogoDeProveedores_Click);
            // 
            // txtBoxContra
            // 
            this.txtBoxContra.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxContra.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtBoxContra.Location = new System.Drawing.Point(223, 375);
            this.txtBoxContra.Name = "txtBoxContra";
            this.txtBoxContra.Size = new System.Drawing.Size(241, 21);
            this.txtBoxContra.TabIndex = 1;
            this.txtBoxContra.UseSystemPasswordChar = true;
            this.txtBoxContra.TextChanged += new System.EventHandler(this.k);
            this.txtBoxContra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxContra_KeyDown);
            // 
            // lblMensajeInvalidez
            // 
            this.lblMensajeInvalidez.AutoSize = true;
            this.lblMensajeInvalidez.ForeColor = System.Drawing.Color.Red;
            this.lblMensajeInvalidez.Location = new System.Drawing.Point(220, 409);
            this.lblMensajeInvalidez.Name = "lblMensajeInvalidez";
            this.lblMensajeInvalidez.Size = new System.Drawing.Size(155, 13);
            this.lblMensajeInvalidez.TabIndex = 36;
            this.lblMensajeInvalidez.Text = "Contraseña o Usuario Inválido. ";
            this.lblMensajeInvalidez.Visible = false;
            this.lblMensajeInvalidez.Click += new System.EventHandler(this.lblMensajeInvalidez_Click);
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIniciarSesion.Font = new System.Drawing.Font("Microsoft PhagsPa", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniciarSesion.Location = new System.Drawing.Point(223, 436);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(241, 25);
            this.btnIniciarSesion.TabIndex = 2;
            this.btnIniciarSesion.Text = "Iniciar Sesión";
            this.btnIniciarSesion.UseVisualStyleBackColor = true;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            // 
            // dragControl1
            // 
            this.dragControl1.SelectControl = this.pnlBarraSuperior;
            // 
            // frmRegistroInicioSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(656, 561);
            this.Controls.Add(this.btnIniciarSesion);
            this.Controls.Add(this.lblMensajeInvalidez);
            this.Controls.Add(this.txtBoxContra);
            this.Controls.Add(this.lblCatalogoDeProveedores);
            this.Controls.Add(this.txtBoxUsuario);
            this.Controls.Add(this.lblContra);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.picBoxLogoAgro);
            this.Controls.Add(this.pnlBarraSuperior);
            this.Name = "frmRegistroInicioSesion";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.pnlBarraSuperior.ResumeLayout(false);
            this.pnlBarraSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLogoAgro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBarraSuperior;
        private System.Windows.Forms.Label lblIncioSesion;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.PictureBox picBoxLogoAgro;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblContra;
        private System.Windows.Forms.TextBox txtBoxUsuario;
        private System.Windows.Forms.Label lblCatalogoDeProveedores;
        private System.Windows.Forms.TextBox txtBoxContra;
        private System.Windows.Forms.Label lblMensajeInvalidez;
        private System.Windows.Forms.Button btnIniciarSesion;
        private MisControles.DragControl dragControl1;
    }
}