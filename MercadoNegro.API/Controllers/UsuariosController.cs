using MercadoNegro.Core.DTOs;
using MercadoNegro.Core.Entities;
using MercadoNegro.Core.Interfaces;
using MercadoNegro.Core.Interfaces.MercadoNegro.Core.Interfaces;
using MercadoNegro.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MercadoNegro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppDbContext _context;

        public UsuariosController(
            IUsuarioRepository usuarioRepository,
            AppDbContext context)
        {
            _usuarioRepository = usuarioRepository;
            _context = context;
        }

        [HttpPost("registro")]
        public async Task<ActionResult<Usuario>> Registrar([FromBody] UsuarioRegistroDTO registroDto)
        {
            try
            {
                // Validación manual adicional
                if (string.IsNullOrWhiteSpace(registroDto.Nombre) ||
                    string.IsNullOrWhiteSpace(registroDto.Apellido) ||
                    string.IsNullOrWhiteSpace(registroDto.Email) ||
                    string.IsNullOrWhiteSpace(registroDto.Contraseña))
                {
                    return BadRequest("Todos los campos son obligatorios");
                }

                if (await _usuarioRepository.ExistsByEmailAsync(registroDto.Email))
                {
                    return BadRequest("El email ya está registrado");
                }

                var usuario = new Usuario
                {
                    Nombre = registroDto.Nombre,
                    Apellido = registroDto.Apellido,
                    Email = registroDto.Email,
                    Contraseña = registroDto.Contraseña, // En producción usar hash
                    Cvu = GenerarCvuUnico(),
                    Saldo = 0
                };

                await _usuarioRepository.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] UsuarioLoginDTO loginDto)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);
                if (usuario == null || usuario.Contraseña != loginDto.Contraseña)
                {
                    return Unauthorized("Credenciales inválidas");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        private string GenerarCvuUnico()
        {
            return "CVU" + Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
        }
    }
}