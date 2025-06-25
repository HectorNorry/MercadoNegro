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
            txtMonto = new TextBox();
            btnIngresar = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(187, 187);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(150, 23);
            txtMonto.TabIndex = 0;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(187, 227);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(150, 30);
            btnIngresar.TabIndex = 1;
            btnIngresar.Text = "Ingresar Dinero";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(127, 190);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 2;
            label1.Text = "Monto:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(170, 69);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 3;
            label2.Text = "Ingresar Dinero";
            // 
            // FormIngresoDinero
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 342);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnIngresar);
            Controls.Add(txtMonto);
            Name = "FormIngresoDinero";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingresar Dinero";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Label label1;
        private Label label2;
    }
}