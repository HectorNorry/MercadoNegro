namespace MercadoNegro.Desktop
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSaldoTitulo = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.btnIngresar.Click += new System.EventHandler(this.BtnIngresar_Click);
            this.btnTransferir.Click += new System.EventHandler(this.BtnTransferir_Click);
            this.btnCvu = new System.Windows.Forms.Button();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();

            this.SuspendLayout();
            // 
            // lblSaldoTitulo
            // 
            this.lblSaldoTitulo.AutoSize = true;
            this.lblSaldoTitulo.Location = new System.Drawing.Point(30, 30);
            this.lblSaldoTitulo.Name = "lblSaldoTitulo";
            this.lblSaldoTitulo.Size = new System.Drawing.Size(42, 15);
            this.lblSaldoTitulo.TabIndex = 0;
            this.lblSaldoTitulo.Text = "SALDO:";
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Location = new System.Drawing.Point(80, 25);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(60, 15);
            this.lblSaldo.TabIndex = 1;
            this.lblSaldo.Text = "$ 0.00";
            // 
            // btnIngresar
            // 
            this.btnIngresar.Location = new System.Drawing.Point(30, 70);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(100, 40);
            this.btnIngresar.TabIndex = 2;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;

            // 
            // btnTransferir
            // 
            this.btnTransferir.Location = new System.Drawing.Point(150, 70);
            this.btnTransferir.Name = "btnTransferir";
            this.btnTransferir.Size = new System.Drawing.Size(100, 40);
            this.btnTransferir.TabIndex = 3;
            this.btnTransferir.Text = "Transferir";
            this.btnTransferir.UseVisualStyleBackColor = false;
            // 
            // btnCvu
            // 
            this.btnCvu.Location = new System.Drawing.Point(270, 70);
            this.btnCvu.Name = "btnCvu";
            this.btnCvu.Size = new System.Drawing.Size(100, 40);
            this.btnCvu.TabIndex = 4;
            this.btnCvu.Text = "CVU";
            this.btnCvu.UseVisualStyleBackColor = false;
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Location = new System.Drawing.Point(30, 130);
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.RowTemplate.Height = 25;
            this.dgvMovimientos.Size = new System.Drawing.Size(740, 300);
            this.dgvMovimientos.TabIndex = 5;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvMovimientos);
            this.Controls.Add(this.btnCvu);
            this.Controls.Add(this.btnTransferir);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.lblSaldoTitulo);
            this.Name = "FormPrincipal";
            this.Text = "Mercado Negro";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblSaldoTitulo;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Button btnTransferir;
        private System.Windows.Forms.Button btnCvu;
        private System.Windows.Forms.DataGridView dgvMovimientos;
    }
}