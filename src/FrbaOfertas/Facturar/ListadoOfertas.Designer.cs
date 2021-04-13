namespace FrbaOfertas.Facturar
{
    partial class ListadoOfertas
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
            this.dgvOfertas = new System.Windows.Forms.DataGridView();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOfertas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOfertas
            // 
            this.dgvOfertas.AllowUserToAddRows = false;
            this.dgvOfertas.AllowUserToDeleteRows = false;
            this.dgvOfertas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOfertas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOfertas.Location = new System.Drawing.Point(12, 12);
            this.dgvOfertas.Name = "dgvOfertas";
            this.dgvOfertas.ReadOnly = true;
            this.dgvOfertas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOfertas.Size = new System.Drawing.Size(799, 222);
            this.dgvOfertas.TabIndex = 64;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(12, 254);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(129, 23);
            this.btnGenerar.TabIndex = 66;
            this.btnGenerar.Text = "Generar Facturación";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(730, 254);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 65;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // ListadoOfertas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 289);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.dgvOfertas);
            this.Name = "ListadoOfertas";
            this.Text = "ListadoOfertas";
            this.Load += new System.EventHandler(this.ListadoOfertas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOfertas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOfertas;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnVolver;
    }
}