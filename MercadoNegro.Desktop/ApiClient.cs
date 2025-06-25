using MercadoNegro.Core.DTOs;
using MercadoNegro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MercadoNegro.Desktop
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7011/api/";

        public ApiClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(BaseUrl),
                Timeout = TimeSpan.FromSeconds(15)
            };


        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            

            try
            {
                var response = await _httpClient.PostAsJsonAsync("usuarios/login", new
                {
                    Email = email,
                    Password = password,
                });

                var requestUri = _httpClient.BaseAddress + "usuarios/login";
                System.Diagnostics.Debug.WriteLine($"Login attempt URL: {requestUri}");

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al conectar con la API: {ex.Message}");
            }
        }
        public async Task<Usuario> RegistrarUsuarioAsync(UsuarioRegistroDTO registroDto)
        {
            try // Asegúrate de que RegistrarUsuarioAsync también tiene un try-catch para ver los errores
            {
                var requestUri = _httpClient.BaseAddress + "usuarios/registro"; // AÑADE ESTA LÍNEA
                System.Diagnostics.Debug.WriteLine($"Register attempt URL: {requestUri}"); // AÑADE ESTA LÍNEA

                var response = await _httpClient.PostAsJsonAsync("usuarios/registro", registroDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            catch (HttpRequestException ex) // Asegúrate de que capture la excepción y la muestre
            {
                throw new Exception($"Error al conectar con la API (Registro): {ex.Message}");
            }
        }

        public async Task<Usuario> GetUsuarioAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Usuario>($"usuarios/{id}");
        }

        public async Task<IEnumerable<MovimientoGridItemDTO>> GetMovimientosAsync(int usuarioId) // <-- Cambia el tipo de retorno
        {
            var response = await _httpClient.GetAsync($"Movimientos/usuario/{usuarioId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<MovimientoGridItemDTO>>() ?? new List<MovimientoGridItemDTO>();
        }

        public async Task RealizarTransferenciaAsync(TransferenciaDTO transferenciaDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Transferencias", transferenciaDto);

            // Lee el contenido de la respuesta como una cadena, sin importar si es éxito o error.
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) // Si el código de estado no es 2xx
            {
                // Si la API devuelve un mensaje de error plano (como "Saldo insuficiente")
                // o un JSON de error si lo configuraste, lo mostramos.
                throw new HttpRequestException($"Error al realizar transferencia: {responseContent}");
            }
            // Si es un código 2xx, simplemente la operación fue exitosa, no hay nada que deserializar.
            // Si la API devuelve "Transferencia realizada con éxito.", simplemente se ignora.
        }

        public async Task DepositarDineroAsync(int usuarioId, decimal monto, string descripcion)
        {
            var depositoDto = new DepositoDTO // Usa el DTO que creaste
            {
                UsuarioId = usuarioId,
                Monto = monto,
                Descripcion = descripcion
            };

            // La ruta debe coincidir con la que definiste en tu API
            var response = await _httpClient.PostAsJsonAsync($"Usuarios/{usuarioId}/Deposito", depositoDto); // Ejemplo: "api/Usuarios/{id}/Deposito"

            // Esto lanzará una HttpRequestException si el status code no es 2xx
            response.EnsureSuccessStatusCode();
        }
    }
}