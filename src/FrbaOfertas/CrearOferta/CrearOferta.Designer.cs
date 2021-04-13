namespace FrbaOfertas.CrearOferta
{
    partial class CrearOferta
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
            this.dtmFechaPublicacion = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.dtmFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.numPrecioOferta = new System.Windows.Forms.NumericUpDown();
            this.numPrecioLista = new System.Windows.Forms.NumericUpDown();
            this.numCantidadDisponible = new System.Windows.Forms.NumericUpDown();
            this.numCantidadMaxima = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioOferta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadDisponible)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadMaxima)).BeginInit();
            this.SuspendLayout();
            // 
            // dtmFechaPublicacion
            // 
            this.dtmFechaPublicacion.CustomFormat = "dd/MM/yyyy";
            this.dtmFechaPublicacion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFechaPublicacion.Location = new System.Drawing.Point(121, 66);
            this.dtmFechaPublicacion.Name = "dtmFechaPublicacion";
            this.dtmFechaPublicacion.Size = new System.Drawing.Size(152, 20);
            this.dtmFechaPublicacion.TabIndex = 64;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 63;
            this.label6.Text = "Fecha publicación:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Descripcion:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(121, 27);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(451, 20);
            this.txtDescripcion.TabIndex = 66;
            // 
            // dtmFechaVencimiento
            // 
            this.dtmFechaVencimiento.CustomFormat = "dd/MM/yyyy";
            this.dtmFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFechaVencimiento.Location = new System.Drawing.Point(420, 66);
            this.dtmFechaVencimiento.Name = "dtmFechaVencimiento";
            this.dtmFechaVencimiento.Size = new System.Drawing.Size(152, 20);
            this.dtmFechaVencimiento.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Fecha vencimiento:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Precio oferta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(305, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 71;
            this.label4.Text = "Precio de lista:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "Cantidad maxima:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 73;
            this.label7.Text = "Cantidad disponible:";
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(15, 202);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(80, 23);
            this.btnCrear.TabIndex = 78;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(497, 202);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 77;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // numPrecioOferta
            // 
            this.numPrecioOferta.Location = new System.Drawing.Point(121, 117);
            this.numPrecioOferta.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numPrecioOferta.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPrecioOferta.Name = "numPrecioOferta";
            this.numPrecioOferta.Size = new System.Drawing.Size(152, 20);
            this.numPrecioOferta.TabIndex = 79;
            this.numPrecioOferta.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numPrecioLista
            // 
            this.numPrecioLista.Location = new System.Drawing.Point(420, 117);
            this.numPrecioLista.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numPrecioLista.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPrecioLista.Name = "numPrecioLista";
            this.numPrecioLista.Size = new System.Drawing.Size(152, 20);
            this.numPrecioLista.TabIndex = 80;
            this.numPrecioLista.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numCantidadDisponible
            // 
            this.numCantidadDisponible.Location = new System.Drawing.Point(121, 159);
            this.numCantidadDisponible.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numCantidadDisponible.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidadDisponible.Name = "numCantidadDisponible";
            this.numCantidadDisponible.Size = new System.Drawing.Size(152, 20);
            this.numCantidadDisponible.TabIndex = 81;
            this.numCantidadDisponible.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numCantidadMaxima
            // 
            this.numCantidadMaxima.Location = new System.Drawing.Point(420, 159);
            this.numCantidadMaxima.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numCantidadMaxima.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidadMaxima.Name = "numCantidadMaxima";
            this.numCantidadMaxima.Size = new System.Drawing.Size(152, 20);
            this.numCantidadMaxima.TabIndex = 82;
            this.numCantidadMaxima.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CrearOferta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 242);
            this.Controls.Add(this.numCantidadMaxima);
            this.Controls.Add(this.numCantidadDisponible);
            this.Controls.Add(this.numPrecioLista);
            this.Controls.Add(this.numPrecioOferta);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtmFechaVencimiento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtmFechaPublicacion);
            this.Controls.Add(this.label6);
            this.Name = "CrearOferta";
            this.Text = "CrearOferta";
            this.Load += new System.EventHandler(this.CrearOferta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioOferta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecioLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadDisponible)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadMaxima)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtmFechaPublicacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DateTimePicker dtmFechaVencimiento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.NumericUpDown numPrecioOferta;
        private System.Windows.Forms.NumericUpDown numPrecioLista;
        private System.Windows.Forms.NumericUpDown numCantidadDisponible;
        private System.Windows.Forms.NumericUpDown numCantidadMaxima;
    }
}