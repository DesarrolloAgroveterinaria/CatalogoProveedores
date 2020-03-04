namespace ProveedorPrueba
{
    partial class TablaResultadosProveedor
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
            this.lblBusquedaProveedorForm = new System.Windows.Forms.Label();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnMostrarProveedorSeleccionado = new System.Windows.Forms.Button();
            this.pnlDatosPrimarios = new System.Windows.Forms.Panel();
            this.dataGridProveedores = new System.Windows.Forms.DataGridView();
            this.ClaveProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlInstruccionesResultados = new System.Windows.Forms.Panel();
            this.txtBoxInstruccionesResultados = new System.Windows.Forms.TextBox();
            this.lblProveedoresResultados = new System.Windows.Forms.Label();
            this.txtBoxProveedoresResultados = new System.Windows.Forms.TextBox();
            this.pnlBarraSuperior.SuspendLayout();
            this.pnlDatosPrimarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProveedores)).BeginInit();
            this.pnlInstruccionesResultados.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBarraSuperior
            // 
            this.pnlBarraSuperior.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.pnlBarraSuperior.Controls.Add(this.lblBusquedaProveedorForm);
            this.pnlBarraSuperior.Controls.Add(this.btnMin);
            this.pnlBarraSuperior.Controls.Add(this.btnCerrar);
            this.pnlBarraSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlBarraSuperior.Name = "pnlBarraSuperior";
            this.pnlBarraSuperior.Size = new System.Drawing.Size(1037, 29);
            this.pnlBarraSuperior.TabIndex = 4;
            // 
            // lblBusquedaProveedorForm
            // 
            this.lblBusquedaProveedorForm.AutoSize = true;
            this.lblBusquedaProveedorForm.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusquedaProveedorForm.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblBusquedaProveedorForm.Location = new System.Drawing.Point(15, 9);
            this.lblBusquedaProveedorForm.Name = "lblBusquedaProveedorForm";
            this.lblBusquedaProveedorForm.Size = new System.Drawing.Size(157, 17);
            this.lblBusquedaProveedorForm.TabIndex = 3;
            this.lblBusquedaProveedorForm.Text = "Resultados de Búsqueda";
            // 
            // btnMin
            // 
            this.btnMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnMin.Location = new System.Drawing.Point(967, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(35, 29);
            this.btnMin.TabIndex = 4;
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
            this.btnCerrar.Location = new System.Drawing.Point(1002, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(35, 29);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnMostrarProveedorSeleccionado
            // 
            this.btnMostrarProveedorSeleccionado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMostrarProveedorSeleccionado.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnMostrarProveedorSeleccionado.Font = new System.Drawing.Font("Microsoft PhagsPa", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMostrarProveedorSeleccionado.Location = new System.Drawing.Point(388, 368);
            this.btnMostrarProveedorSeleccionado.Name = "btnMostrarProveedorSeleccionado";
            this.btnMostrarProveedorSeleccionado.Size = new System.Drawing.Size(241, 25);
            this.btnMostrarProveedorSeleccionado.TabIndex = 39;
            this.btnMostrarProveedorSeleccionado.Text = "Mostrar Datos";
            this.btnMostrarProveedorSeleccionado.UseVisualStyleBackColor = true;
            this.btnMostrarProveedorSeleccionado.Click += new System.EventHandler(this.btnMostrarProveedorSeleccionado_Click);
            // 
            // pnlDatosPrimarios
            // 
            this.pnlDatosPrimarios.Controls.Add(this.dataGridProveedores);
            this.pnlDatosPrimarios.Location = new System.Drawing.Point(18, 160);
            this.pnlDatosPrimarios.Name = "pnlDatosPrimarios";
            this.pnlDatosPrimarios.Size = new System.Drawing.Size(984, 186);
            this.pnlDatosPrimarios.TabIndex = 40;
            // 
            // dataGridProveedores
            // 
            this.dataGridProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProveedores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClaveProveedor,
            this.NombreProveedor,
            this.RFC,
            this.Categoria});
            this.dataGridProveedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridProveedores.Location = new System.Drawing.Point(0, 0);
            this.dataGridProveedores.Name = "dataGridProveedores";
            this.dataGridProveedores.Size = new System.Drawing.Size(984, 186);
            this.dataGridProveedores.TabIndex = 0;
            this.dataGridProveedores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridProveedores_CellContentClick);
            // 
            // ClaveProveedor
            // 
            this.ClaveProveedor.DataPropertyName = "Clave";
            this.ClaveProveedor.Frozen = true;
            this.ClaveProveedor.HeaderText = "Clave";
            this.ClaveProveedor.Name = "ClaveProveedor";
            // 
            // NombreProveedor
            // 
            this.NombreProveedor.DataPropertyName = "Nombre";
            this.NombreProveedor.Frozen = true;
            this.NombreProveedor.HeaderText = "Nombre";
            this.NombreProveedor.Name = "NombreProveedor";
            this.NombreProveedor.Width = 350;
            // 
            // RFC
            // 
            this.RFC.DataPropertyName = "RFC";
            this.RFC.Frozen = true;
            this.RFC.HeaderText = "RFC";
            this.RFC.Name = "RFC";
            this.RFC.Width = 200;
            // 
            // Categoria
            // 
            this.Categoria.DataPropertyName = "Categoria";
            this.Categoria.Frozen = true;
            this.Categoria.HeaderText = "Categoria";
            this.Categoria.Name = "Categoria";
            this.Categoria.Width = 300;
            // 
            // pnlInstruccionesResultados
            // 
            this.pnlInstruccionesResultados.Controls.Add(this.txtBoxInstruccionesResultados);
            this.pnlInstruccionesResultados.Location = new System.Drawing.Point(801, 45);
            this.pnlInstruccionesResultados.Name = "pnlInstruccionesResultados";
            this.pnlInstruccionesResultados.Size = new System.Drawing.Size(216, 109);
            this.pnlInstruccionesResultados.TabIndex = 41;
            // 
            // txtBoxInstruccionesResultados
            // 
            this.txtBoxInstruccionesResultados.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtBoxInstruccionesResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxInstruccionesResultados.Location = new System.Drawing.Point(0, 0);
            this.txtBoxInstruccionesResultados.Multiline = true;
            this.txtBoxInstruccionesResultados.Name = "txtBoxInstruccionesResultados";
            this.txtBoxInstruccionesResultados.Size = new System.Drawing.Size(216, 109);
            this.txtBoxInstruccionesResultados.TabIndex = 1;
            this.txtBoxInstruccionesResultados.Text = "Instrucciones:\r\n\r\n1. Verificar información de Proveedor \r\n2. Seleccionar Proveedo" +
    "r de la tabla\r\n3. Dar Click en Mostrar Datos\r\n";
            // 
            // lblProveedoresResultados
            // 
            this.lblProveedoresResultados.AutoSize = true;
            this.lblProveedoresResultados.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedoresResultados.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProveedoresResultados.Location = new System.Drawing.Point(23, 71);
            this.lblProveedoresResultados.Name = "lblProveedoresResultados";
            this.lblProveedoresResultados.Size = new System.Drawing.Size(179, 35);
            this.lblProveedoresResultados.TabIndex = 42;
            this.lblProveedoresResultados.Text = "Proveedores:";
            // 
            // txtBoxProveedoresResultados
            // 
            this.txtBoxProveedoresResultados.Location = new System.Drawing.Point(29, 118);
            this.txtBoxProveedoresResultados.Name = "txtBoxProveedoresResultados";
            this.txtBoxProveedoresResultados.Size = new System.Drawing.Size(469, 20);
            this.txtBoxProveedoresResultados.TabIndex = 43;
            // 
            // TablaResultadosProveedor
            // 
            this.AcceptButton = this.btnMostrarProveedorSeleccionado;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 416);
            this.Controls.Add(this.txtBoxProveedoresResultados);
            this.Controls.Add(this.lblProveedoresResultados);
            this.Controls.Add(this.pnlInstruccionesResultados);
            this.Controls.Add(this.pnlDatosPrimarios);
            this.Controls.Add(this.btnMostrarProveedorSeleccionado);
            this.Controls.Add(this.pnlBarraSuperior);
            this.Name = "TablaResultadosProveedor";
            this.Load += new System.EventHandler(this.TablaResultadosProveedor_Load);
            this.pnlBarraSuperior.ResumeLayout(false);
            this.pnlBarraSuperior.PerformLayout();
            this.pnlDatosPrimarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProveedores)).EndInit();
            this.pnlInstruccionesResultados.ResumeLayout(false);
            this.pnlInstruccionesResultados.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel pnlBarraSuperior;
        public System.Windows.Forms.Label lblBusquedaProveedorForm;
        public System.Windows.Forms.Button btnMin;
        public System.Windows.Forms.Button btnCerrar;
        public System.Windows.Forms.Button btnMostrarProveedorSeleccionado;
        public System.Windows.Forms.Panel pnlDatosPrimarios;
        public System.Windows.Forms.Panel pnlInstruccionesResultados;
        public System.Windows.Forms.TextBox txtBoxInstruccionesResultados;
        public System.Windows.Forms.Label lblProveedoresResultados;
        public System.Windows.Forms.DataGridView dataGridProveedores;
        public System.Windows.Forms.TextBox txtBoxProveedoresResultados;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClaveProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
    }
}