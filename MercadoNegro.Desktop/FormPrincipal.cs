using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MercadoNegro.Core.Entities;
using MercadoNegro.Core.DTOs;

namespace MercadoNegro.Desktop
{
    public partial class FormPrincipal : Form
    {
        private Usuario _usuarioLogueado;
        private readonly ApiClient _apiClient;

        public FormPrincipal(Usuario usuario)
        {
            InitializeComponent();
            _usuarioLogueado = usuario;
            ConfigurarEstilos();
            CargarDatosIniciales();
        }

        private void ConfigurarEstilos()
        {
            this.Text = $"Mercado Negro - Bienvenido {_usuarioLogueado.Nombre}";
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Font = new Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(600, 500);

            lblSaldoTitulo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblSaldo.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblSaldo.ForeColor = Color.DarkGreen;

            btnIngresar.BackColor = Color.FromArgb(0, 123, 255);
            btnTransferir.BackColor = Color.FromArgb(40, 167, 69);
            btnCvu.BackColor = Color.FromArgb(108, 117, 125);

            foreach (var btn in new[] { btnIngresar, btnTransferir, btnCvu })
            {
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btn.Size = new Size(120, 40);
            }

            dgvMovimientos.BackgroundColor = Color.White;
            dgvMovimientos.BorderStyle = BorderStyle.None;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMovimientos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dgvMovimientos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMovimientos.EnableHeadersVisualStyles = false;
            dgvMovimientos.RowHeadersVisible = false;
        }

        private async 
        Task
CargarDatosIniciales()
        {
            try
            {
                _usuarioLogueado = await _apiClient.GetUsuarioAsync(_usuarioLogueado.Id);
                ActualizarSaldo();
                await CargarMovimientos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarSaldo()
        {
            lblSaldo.Text = $"${_usuarioLogueado.Saldo:N2}";
        }

        private async Task CargarMovimientos()
        {
            try
            {
                var movimientos = await _apiClient.GetMovimientosAsync(_usuarioLogueado.Id);

                dgvMovimientos.DataSource = null;
                dgvMovimientos.Columns.Clear();

                dgvMovimientos.AutoGenerateColumns = false;

                dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Fecha",
                    HeaderText = "Fecha",
                    Name = "Fecha",
                    Width = 120
                });

                dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Descripcion",
                    HeaderText = "Descripción",
                    Name = "Descripcion",
                    Width = 200
                });

                dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Monto",
                    HeaderText = "Monto",
                    Name = "Monto",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle()
                    {
                        Format = "N2",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

                dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = "Tipo",
                    HeaderText = "Tipo",
                    Name = "Tipo",
                    Width = 150
                });

                var datosMostrar = movimientos.Select(m => new
                {
                    Fecha = m.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    m.Descripcion,
                    Monto = m.RemitenteId == _usuarioLogueado.Id ? -m.Monto : m.Monto,
                    Tipo = m.RemitenteId == _usuarioLogueado.Id ?
                           $"Enviado a {m.Destinatario.Nombre}" :
                           $"Recibido de {m.Remitente.Nombre}"
                }).ToList();

                dgvMovimientos.DataSource = datosMostrar;

                foreach (DataGridViewRow row in dgvMovimientos.Rows)
                {
                    if (row.Cells["Monto"].Value != null)
                    {
                        decimal monto = (decimal)row.Cells["Monto"].Value;
                        row.Cells["Monto"].Style.ForeColor = monto < 0 ? Color.Red : Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar movimientos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnIngresar_Click(object sender, EventArgs e)
        {
            using var form = new FormIngresoDinero(_usuarioLogueado.Id);
            if (form.ShowDialog() == DialogResult.OK)
            {
                await CargarDatosIniciales().ConfigureAwait(false);
            }
        }

        private async void BtnTransferir_Click(object sender, EventArgs e)
        {
            using var form = new FormTransferencia(_usuarioLogueado.Id);
            if (form.ShowDialog() == DialogResult.OK)
            {
                await CargarDatosIniciales().ConfigureAwait(false);
            }
        }

        private void btnCvu_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_usuarioLogueado.Cvu);
            MessageBox.Show($"Tu CVU es: {_usuarioLogueado.Cvu}\n\nSe ha copiado al portapapeles.",
                          "CVU",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }
    }

    
    
}