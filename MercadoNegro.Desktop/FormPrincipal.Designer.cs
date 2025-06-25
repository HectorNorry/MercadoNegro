namespace MercadoNegro.Desktop
{
    partial class FormPrincipal
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
            lblBienvenido = new Label();
            lblSaldoTitulo = new Label();
            lblSaldo = new Label();
            btnIngresar = new Button();
            btnTransferir = new Button();
            btnCvu = new Button();
            dgvMovimientos = new DataGridView();
            btnCerrarSesion = new Button();
            btnGenerarPdf = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).BeginInit();
            SuspendLayout();
            // 
            // lblBienvenido
            // 
            lblBienvenido.AutoSize = true;
            lblBienvenido.Font = new Font("Segoe UI", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBienvenido.Location = new Point(302, 12);
            lblBienvenido.Name = "lblBienvenido";
            lblBienvenido.Size = new Size(337, 47);
            lblBienvenido.TabIndex = 6;
            lblBienvenido.Text = "Bienvenido {usuario}";
            // 
            // lblSaldoTitulo
            // 
            lblSaldoTitulo.AutoSize = true;
            lblSaldoTitulo.Location = new Point(30, 70);
            lblSaldoTitulo.Name = "lblSaldoTitulo";
            lblSaldoTitulo.Size = new Size(47, 15);
            lblSaldoTitulo.TabIndex = 0;
            lblSaldoTitulo.Text = "SALDO:";
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.Location = new Point(95, 60);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(37, 15);
            lblSaldo.TabIndex = 1;
            lblSaldo.Text = "$ 0.00";
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(30, 120);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(114, 40);
            btnIngresar.TabIndex = 2;
            btnIngresar.Text = "Ingresar $$";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += BtnIngresar_Click;
            // 
            // btnTransferir
            // 
            btnTransferir.Location = new Point(150, 120);
            btnTransferir.Name = "btnTransferir";
            btnTransferir.Size = new Size(107, 40);
            btnTransferir.TabIndex = 3;
            btnTransferir.Text = "Transferir";
            btnTransferir.UseVisualStyleBackColor = false;
            btnTransferir.Click += BtnTransferir_Click;
            // 
            // btnCvu
            // 
            btnCvu.Location = new Point(263, 120);
            btnCvu.Name = "btnCvu";
            btnCvu.Size = new Size(108, 40);
            btnCvu.TabIndex = 4;
            btnCvu.Text = "Mi CVU";
            btnCvu.UseVisualStyleBackColor = false;
            btnCvu.Click += btnCvu_Click;
            // 
            // dgvMovimientos
            // 
            dgvMovimientos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMovimientos.Location = new Point(30, 190);
            dgvMovimientos.Name = "dgvMovimientos";
            dgvMovimientos.Size = new Size(766, 395);
            dgvMovimientos.TabIndex = 5;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(722, 12);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(75, 23);
            btnCerrarSesion.TabIndex = 7;
            btnCerrarSesion.Text = "Salir";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // btnGenerarPdf
            // 
            btnGenerarPdf.Location = new Point(669, 156);
            btnGenerarPdf.Name = "btnGenerarPdf";
            btnGenerarPdf.Size = new Size(128, 28);
            btnGenerarPdf.TabIndex = 8;
            btnGenerarPdf.Text = "Descargar Resumen";
            btnGenerarPdf.UseVisualStyleBackColor = true;
            btnGenerarPdf.Click += btnGenerarPdf_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(826, 597);
            Controls.Add(btnGenerarPdf);
            Controls.Add(btnCerrarSesion);
            Controls.Add(lblBienvenido);
            Controls.Add(dgvMovimientos);
            Controls.Add(btnCvu);
            Controls.Add(btnTransferir);
            Controls.Add(btnIngresar);
            Controls.Add(lblSaldo);
            Controls.Add(lblSaldoTitulo);
            Name = "FormPrincipal";
            Text = "Mercado Negro";
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBienvenido; // Nuevo campo declarado
        private System.Windows.Forms.Label lblSaldoTitulo;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Button btnTransferir;
        private System.Windows.Forms.Button btnCvu;
        private System.Windows.Forms.DataGridView dgvMovimientos;
        private Button btnCerrarSesion;
        private Button btnGenerarPdf;
    }
}