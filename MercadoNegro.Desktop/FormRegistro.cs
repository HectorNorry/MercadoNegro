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
    public partial class FormRegistro : Form
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public FormRegistro()
        {
            InitializeComponent();
            ConfigurarEstilos();
        }

        private void ConfigurarEstilos()
        {
            this.Text = "Mercado Negro - Registro";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            btnRegistrar.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnRegistrar.ForeColor = System.Drawing.Color.White;
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.FlatAppearance.BorderSize = 0;
        }

        private async void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var registroDto = new UsuarioRegistroDTO
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text
                };

                var usuario = await _apiClient.RegistrarUsuarioAsync(registroDto);
                MessageBox.Show("Registro exitoso! Por favor inicie sesión", "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
