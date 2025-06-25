using System;
using System.IO;
using System.Drawing;
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
using iTextSharp.text;
using iTextSharp.text.pdf;


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
            _apiClient = new ApiClient();
            lblBienvenido.Text = $"Bienvenido {_usuarioLogueado.Nombre}";
            ConfigurarEstilos();
            CargarDatosIniciales();
        }

        private void ConfigurarEstilos()
        {
            this.Text = $"Mercado Negro - Bienvenido {_usuarioLogueado.Nombre}";
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Font = new System.Drawing.Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(600, 500);

            lblSaldoTitulo.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            lblSaldo.Font = new System.Drawing.Font("Segoe UI", 24, FontStyle.Bold);
            lblSaldo.ForeColor = Color.DarkGreen;

            btnIngresar.BackColor = Color.FromArgb(0, 123, 255);
            btnTransferir.BackColor = Color.FromArgb(40, 167, 69);
            btnCvu.BackColor = Color.FromArgb(108, 117, 125);

            foreach (var btn in new[] { btnIngresar, btnTransferir, btnCvu, btnCerrarSesion, btnGenerarPdf }) // <--- ¡Añade btnGenerarPdf aquí!
            {
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
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

        private async Task CargarDatosIniciales()
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

                var datosMostrar = movimientos.Select(m =>
                {
                    decimal displayMonto = m.Monto;
                    string displayTipo = m.Tipo; // El tipo ya viene del DTO del API

                    if (displayTipo == "Transferencia Enviada")
                    {
                        displayMonto = -displayMonto; // Si es "Enviada" (por este usuario), es negativa
                        displayTipo = $"Enviado a {m.OtroParticipante ?? "Usuario Desconocido"}";
                    }
                    else if (displayTipo == "Transferencia Recibida")
                    {
                        displayTipo = $"Recibido de {m.OtroParticipante ?? "Usuario Desconocido"}";
                    }
                    else if (displayTipo == "Deposito") // Para depósitos
                    {
                        // El monto ya es positivo y el tipo ya es "Depósito" o "Ingreso por App" del API
                        displayTipo = "Depósito";
                    }

                    return new
                    {
                        Fecha = m.Fecha.ToString("dd/MM/yyyy HH:mm"),
                        m.Descripcion,
                        Monto = displayMonto,
                        Tipo = displayTipo
                    };
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

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            if (dgvMovimientos.Rows.Count == 0)
            {
                MessageBox.Show("No hay movimientos para generar el PDF.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF Files (*.pdf)|*.pdf";
                sfd.FileName = $"Movimientos_{_usuarioLogueado.Nombre}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Crear un nuevo documento PDF
                        Document doc = new Document(PageSize.A4);
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();

                        // Añadir un título al PDF
                        Paragraph title = new Paragraph($"Extracto de Movimientos - {_usuarioLogueado.Nombre} {_usuarioLogueado.Apellido}");
                        title.Alignment = Element.ALIGN_CENTER;
                        title.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD);
                        doc.Add(title);
                        

                        // Añadir información del saldo actual
                        doc.Add(new Paragraph($"\nSaldo Actual: ${_usuarioLogueado.Saldo:N2}\n"));
                        doc.Add(new Paragraph($"Fecha de Generación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n\n"));


                        // Crear una tabla PDF con el mismo número de columnas que el DataGridView
                        PdfPTable pdfTable = new PdfPTable(dgvMovimientos.ColumnCount);
                        pdfTable.WidthPercentage = 100; // Ocupar el 100% del ancho de la página

                        // Configurar anchos de columna (opcional, ajusta según tus necesidades)
                        // float[] widths = new float[] { 1.5f, 3f, 1.5f, 2f }; // Ejemplo de anchos relativos
                        // pdfTable.SetWidths(widths);

                        // Añadir los encabezados de las columnas del DataGridView
                        foreach (DataGridViewColumn column in dgvMovimientos.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                            cell.BackgroundColor = new BaseColor(52, 58, 64); // Fondo similar al dgv
                            cell.BorderColor = BaseColor.WHITE;
                            cell.Padding = 5;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            pdfTable.AddCell(cell);
                        }

                        // Añadir los datos de las filas del DataGridView
                        foreach (DataGridViewRow row in dgvMovimientos.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string cellValue = cell.Value?.ToString() ?? "";
                                PdfPCell pdfCell = new PdfPCell(new Phrase(cellValue, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9))); pdfCell.Padding = 5;
                                pdfCell.BorderColor = BaseColor.LIGHT_GRAY;

                                // Alineación del texto basada en la columna Monto (si aplica)
                                if (dgvMovimientos.Columns[cell.ColumnIndex].Name == "Monto")
                                {
                                    pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                }
                                else if (dgvMovimientos.Columns[cell.ColumnIndex].Name == "Fecha")
                                {
                                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                }
                                else
                                {
                                    pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }
                                pdfTable.AddCell(pdfCell);
                            }
                        }

                        doc.Add(pdfTable);
                        doc.Close();

                        MessageBox.Show("PDF generado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }


}