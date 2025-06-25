using MercadoNegro.Core.DTOs;
using MercadoNegro.Core.Entities;
using System;
using System.Windows.Forms;

namespace MercadoNegro.Desktop
{
    public partial class FormLogin : Form
    {
        private readonly ApiClient _apiClient;

        public FormLogin()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
            ConfigurarEstilos();
        }

        private void ConfigurarEstilos()
        {
            this.Text = "Mercado Negro - Login";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            btnLogin.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnLogin.ForeColor = System.Drawing.Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;

            btnRegistrar.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            btnRegistrar.ForeColor = System.Drawing.Color.White;
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.FlatAppearance.BorderSize = 0;
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = await _apiClient.LoginAsync(txtEmail.Text, txtPassword.Text);
                if (usuario != null)
                {
                    this.Hide(); 

                    var formPrincipal = new FormPrincipal(usuario);
                    
                    formPrincipal.ShowDialog(); 

                    
                    this.Show(); 
                    txtEmail.Text = "";
                    txtPassword.Text = "";
                    txtEmail.Focus(); // Pone el foco en el campo de email
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            var formRegistro = new FormRegistro();
            formRegistro.Closed += (s, args) => this.Show();
            formRegistro.Show();
        }
    }
}
