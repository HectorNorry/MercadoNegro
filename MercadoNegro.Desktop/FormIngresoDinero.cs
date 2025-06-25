using MercadoNegro.Core.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MercadoNegro.Desktop
{
    public partial class FormIngresoDinero : Form
    {
        private readonly int _usuarioId;
        private readonly ApiClient _apiClient;

        public FormIngresoDinero(int usuarioId)
        {
            InitializeComponent();
            _usuarioId = usuarioId;
            _apiClient = new ApiClient();
            ConfigurarEstilos();
        }

        private void ConfigurarEstilos()
        {
            this.Text = "Ingresar Dinero";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            btnIngresar.BackColor = Color.FromArgb(0, 123, 255);
            btnIngresar.ForeColor = Color.White;
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.FlatAppearance.BorderSize = 0;
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido mayor a cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // ¡Ahora llamamos al nuevo método específico para depósitos!
                await _apiClient.DepositarDineroAsync(_usuarioId, monto, "Ingreso por App");

                MessageBox.Show("Dinero ingresado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Indica a FormPrincipal que la operación fue exitosa
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ingresar dinero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
