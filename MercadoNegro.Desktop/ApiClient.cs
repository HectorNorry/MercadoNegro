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
        private const string BaseUrl = "https://localhost:7011/api";

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

        public async Task<Usuario> LoginAsync(string email, string contraseña)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("usuarios/login", new
                {
                    Email = email,
                    Contraseña = contraseña
                });

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
            var response = await _httpClient.PostAsJsonAsync("usuarios/registro", registroDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        public async Task<Usuario> GetUsuarioAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Usuario>($"usuarios/{id}");
        }

        public async Task<List<Movimiento>> GetMovimientosAsync(int usuarioId)
        {
            return await _httpClient.GetFromJsonAsync<List<Movimiento>>($"movimientos/usuario/{usuarioId}");
        }

        public async Task<Movimiento> RealizarTransferenciaAsync(TransferenciaDTO transferenciaDto)
        {
            var response = await _httpClient.PostAsJsonAsync("transferencias", transferenciaDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Movimiento>();
        }
    }
}