namespace MercadoNegro.Desktop
{
    partial class FormTransferencia
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
            txtCvuDestinatario = new TextBox();
            txtMonto = new TextBox();
            txtDescripcion = new TextBox();
            btnTransferir = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // txtCvuDestinatario
            // 
            txtCvuDestinatario.Location = new Point(226, 101);
            txtCvuDestinatario.Name = "txtCvuDestinatario";
            txtCvuDestinatario.Size = new Size(200, 23);
            txtCvuDestinatario.TabIndex = 0;
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(226, 141);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(200, 23);
            txtMonto.TabIndex = 1;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(226, 181);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(200, 23);
            txtDescripcion.TabIndex = 2;
            // 
            // btnTransferir
            // 
            btnTransferir.BackColor = Color.LimeGreen;
            btnTransferir.FlatAppearance.BorderColor = Color.Black;
            btnTransferir.FlatAppearance.BorderSize = 5;
            btnTransferir.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTransferir.ForeColor = SystemColors.ButtonHighlight;
            btnTransferir.Location = new Point(226, 245);
            btnTransferir.Name = "btnTransferir";
            btnTransferir.Size = new Size(172, 49);
            btnTransferir.TabIndex = 3;
            btnTransferir.Text = "Transferir";
            btnTransferir.UseVisualStyleBackColor = false;
            btnTransferir.Click += BtnTransferir_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(60, 101);
            label1.Name = "label1";
            label1.Size = new Size(160, 25);
            label1.TabIndex = 4;
            label1.Text = "CVU Destinatario:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(148, 141);
            label2.Name = "label2";
            label2.Size = new Size(72, 25);
            label2.TabIndex = 5;
            label2.Text = "Monto:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(105, 181);
            label3.Name = "label3";
            label3.Size = new Size(115, 25);
            label3.TabIndex = 6;
            label3.Text = "Descripción:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(173, 31);
            label4.Name = "label4";
            label4.Size = new Size(239, 45);
            label4.TabIndex = 7;
            label4.Text = "Enviar Dinero";
            // 
            // FormTransferencia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(580, 340);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnTransferir);
            Controls.Add(txtDescripcion);
            Controls.Add(txtMonto);
            Controls.Add(txtCvuDestinatario);
            Name = "FormTransferencia";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Transferencia";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtCvuDestinatario;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnTransferir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Label label4;
    }
}