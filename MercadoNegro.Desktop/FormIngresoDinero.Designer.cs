namespace MercadoNegro.Desktop
{
    partial class FormIngresoDinero
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
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(120, 50);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(150, 23);
            this.txtMonto.TabIndex = 0;
            // 
            // btnIngresar
            // 
            this.btnIngresar.Location = new System.Drawing.Point(120, 90);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(150, 30);
            this.btnIngresar.TabIndex = 1;
            this.btnIngresar.Text = "Ingresar Dinero";
            this.btnIngresar.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Monto:";
            // 
            // FormIngresoDinero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.txtMonto);
            this.Name = "FormIngresoDinero";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ingresar Dinero";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Label label1;
    }
}