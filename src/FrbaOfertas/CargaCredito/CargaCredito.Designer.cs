namespace FrbaOfertas.CargaCredito
{
    partial class CargaCredito
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
            this.labelUsuario = new System.Windows.Forms.Label();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAño = new System.Windows.Forms.TextBox();
            this.txtMes = new System.Windows.Forms.TextBox();
            this.txtNumeroTarjeta4 = new System.Windows.Forms.TextBox();
            this.txtNumeroTarjeta3 = new System.Windows.Forms.TextBox();
            this.txtNumeroTarjeta2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCodigoSeguridad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNumeroTarjeta1 = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPagar = new System.Windows.Forms.Button();
            this.numMonto = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMonto)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Location = new System.Drawing.Point(12, 28);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(42, 13);
            this.labelUsuario.TabIndex = 39;
            this.labelUsuario.Text = "Cliente:";
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Location = new System.Drawing.Point(120, 25);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.ReadOnly = true;
            this.txtNombreCliente.Size = new System.Drawing.Size(165, 20);
            this.txtNombreCliente.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Fecha:";
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(120, 64);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(165, 20);
            this.txtFecha.TabIndex = 42;
            // 
            // cmbTipoPago
            // 
            this.cmbTipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPago.FormattingEnabled = true;
            this.cmbTipoPago.Location = new System.Drawing.Point(120, 144);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(165, 21);
            this.cmbTipoPago.TabIndex = 44;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Tipo de pago:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Monto:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtAño);
            this.groupBox1.Controls.Add(this.txtMes);
            this.groupBox1.Controls.Add(this.txtNumeroTarjeta4);
            this.groupBox1.Controls.Add(this.txtNumeroTarjeta3);
            this.groupBox1.Controls.Add(this.txtNumeroTarjeta2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCodigoSeguridad);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtNumeroTarjeta1);
            this.groupBox1.Location = new System.Drawing.Point(12, 180);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 147);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos tarjeta";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(225, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "/";
            // 
            // txtAño
            // 
            this.txtAño.Location = new System.Drawing.Point(238, 65);
            this.txtAño.Name = "txtAño";
            this.txtAño.Size = new System.Drawing.Size(24, 20);
            this.txtAño.TabIndex = 63;
            this.txtAño.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMes
            // 
            this.txtMes.Location = new System.Drawing.Point(198, 65);
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(24, 20);
            this.txtMes.TabIndex = 62;
            this.txtMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNumeroTarjeta4
            // 
            this.txtNumeroTarjeta4.Location = new System.Drawing.Point(228, 26);
            this.txtNumeroTarjeta4.Name = "txtNumeroTarjeta4";
            this.txtNumeroTarjeta4.Size = new System.Drawing.Size(34, 20);
            this.txtNumeroTarjeta4.TabIndex = 61;
            this.txtNumeroTarjeta4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNumeroTarjeta3
            // 
            this.txtNumeroTarjeta3.Location = new System.Drawing.Point(188, 26);
            this.txtNumeroTarjeta3.Name = "txtNumeroTarjeta3";
            this.txtNumeroTarjeta3.Size = new System.Drawing.Size(34, 20);
            this.txtNumeroTarjeta3.TabIndex = 60;
            this.txtNumeroTarjeta3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNumeroTarjeta2
            // 
            this.txtNumeroTarjeta2.Location = new System.Drawing.Point(148, 26);
            this.txtNumeroTarjeta2.Name = "txtNumeroTarjeta2";
            this.txtNumeroTarjeta2.Size = new System.Drawing.Size(34, 20);
            this.txtNumeroTarjeta2.TabIndex = 59;
            this.txtNumeroTarjeta2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Código seguridad:";
            // 
            // txtCodigoSeguridad
            // 
            this.txtCodigoSeguridad.Location = new System.Drawing.Point(217, 105);
            this.txtCodigoSeguridad.Name = "txtCodigoSeguridad";
            this.txtCodigoSeguridad.PasswordChar = '*';
            this.txtCodigoSeguridad.Size = new System.Drawing.Size(45, 20);
            this.txtCodigoSeguridad.TabIndex = 58;
            this.txtCodigoSeguridad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "Fecha vencimiento (MM/AA):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Número tarjeta:";
            // 
            // txtNumeroTarjeta1
            // 
            this.txtNumeroTarjeta1.Location = new System.Drawing.Point(108, 26);
            this.txtNumeroTarjeta1.Name = "txtNumeroTarjeta1";
            this.txtNumeroTarjeta1.Size = new System.Drawing.Size(34, 20);
            this.txtNumeroTarjeta1.TabIndex = 54;
            this.txtNumeroTarjeta1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(210, 342);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 48;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnPagar
            // 
            this.btnPagar.Location = new System.Drawing.Point(15, 342);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(80, 23);
            this.btnPagar.TabIndex = 49;
            this.btnPagar.Text = "Pagar";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // numMonto
            // 
            this.numMonto.Location = new System.Drawing.Point(120, 107);
            this.numMonto.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numMonto.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMonto.Name = "numMonto";
            this.numMonto.Size = new System.Drawing.Size(165, 20);
            this.numMonto.TabIndex = 50;
            this.numMonto.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CargaCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 377);
            this.Controls.Add(this.numMonto);
            this.Controls.Add(this.btnPagar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTipoPago);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.labelUsuario);
            this.Controls.Add(this.txtNombreCliente);
            this.Name = "CargaCredito";
            this.Text = "CargaDeCredito";
            this.Load += new System.EventHandler(this.CargaCredito_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMonto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.ComboBox cmbTipoPago;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCodigoSeguridad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNumeroTarjeta1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.TextBox txtAño;
        private System.Windows.Forms.TextBox txtMes;
        private System.Windows.Forms.TextBox txtNumeroTarjeta4;
        private System.Windows.Forms.TextBox txtNumeroTarjeta3;
        private System.Windows.Forms.TextBox txtNumeroTarjeta2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numMonto;
    }
}