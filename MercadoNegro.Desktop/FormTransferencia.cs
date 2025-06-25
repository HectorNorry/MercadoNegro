using MercadoNegro.Core.DTOs;
using System;
using System.Windows.Forms;

namespace MercadoNegro.Desktop
{
    public partial class FormTransferencia : Form
    {
        private readonly int _remitenteId;
        private readonly ApiClient _apiClient;

        public FormTransferencia(int remitenteId)
        {
            InitializeComponent();
            _remitenteId = remitenteId;
            _apiClient = new ApiClient();
            ConfigurarEstilos();
        }

        private void ConfigurarEstilos()
        {
            this.Text = "Realizar Transferencia";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            btnTransferir.BackColor = Color.FromArgb(40, 167, 69);
            btnTransferir.ForeColor = Color.White;
            btnTransferir.FlatStyle = FlatStyle.Flat;
            btnTransferir.FlatAppearance.BorderSize = 0;
        }

        private async void BtnTransferir_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido mayor a cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCvuDestinatario.Text))
            {
                MessageBox.Show("Ingrese el CVU del destinatario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var transferenciaDto = new TransferenciaDTO
                {
                    RemitenteId = _remitenteId,
                    CvuDestinatario = txtCvuDestinatario.Text,
                    Monto = monto,
                    Descripcion = txtDescripcion.Text
                };

                await _apiClient.RealizarTransferenciaAsync(transferenciaDto);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar transferencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}