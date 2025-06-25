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
            this.txtCvuDestinatario = new System.Windows.Forms.TextBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnTransferir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // txtCvuDestinatario
            this.txtCvuDestinatario.Location = new System.Drawing.Point(120, 30);
            this.txtCvuDestinatario.Name = "txtCvuDestinatario";
            this.txtCvuDestinatario.Size = new System.Drawing.Size(200, 23);
            this.txtCvuDestinatario.TabIndex = 0;

            // txtMonto
            this.txtMonto.Location = new System.Drawing.Point(120, 70);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(200, 23);
            this.txtMonto.TabIndex = 1;

            // txtDescripcion
            this.txtDescripcion.Location = new System.Drawing.Point(120, 110);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(200, 23);
            this.txtDescripcion.TabIndex = 2;

            // btnTransferir
            this.btnTransferir.Location = new System.Drawing.Point(150, 150);
            this.btnTransferir.Name = "btnTransferir";
            this.btnTransferir.Size = new System.Drawing.Size(120, 30);
            this.btnTransferir.TabIndex = 3;
            this.btnTransferir.Text = "Transferir";
            this.btnTransferir.UseVisualStyleBackColor = true;
            this.btnTransferir.Click += new System.EventHandler(this.BtnTransferir_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "CVU Destinatario:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Monto:";

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Descripción:";

            // FormTransferencia
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTransferir);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.txtCvuDestinatario);
            this.Name = "FormTransferencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transferencia";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtCvuDestinatario;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnTransferir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}