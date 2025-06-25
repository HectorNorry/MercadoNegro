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
                    string.IsNullOrWhiteSpace(registroDto.Password))
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
                    Password = registroDto.Password, // En producción usar hash
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
                if (usuario == null || usuario.Password != loginDto.Password)
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

        [HttpPost("{id}/Deposito")] // Ejemplo de ruta: POST /api/Usuarios/{id}/Deposito
        public async Task<IActionResult> RealizarDeposito(int id, [FromBody] DepositoDTO depositoDto)
        {
            // 1. Validaciones iniciales
            if (id != depositoDto.UsuarioId)
            {
                return BadRequest("El ID del usuario en la URL no coincide con el ID del depósito.");
            }
            if (depositoDto.Monto <= 0)
            {
                return BadRequest("El monto del depósito debe ser mayor que cero.");
            }

            // 2. Buscar al usuario
            var usuario = await _context.Usuarios.FindAsync(id); // Asumo _context es tu DbContext
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // 3. Realizar el depósito
            usuario.Saldo += depositoDto.Monto;

            // 4. Registrar el movimiento (ejemplo básico, ajusta a tu modelo de Movimiento)
            var movimiento = new Movimiento // Asumo que tienes una clase Movimiento
            {
                UsuarioId = usuario.Id, // El usuario que recibe el dinero
                Tipo = "Deposito", // O "Ingreso", según tu enum/string en Movimiento
                Descripcion = depositoDto.Descripcion,
                Monto = depositoDto.Monto,
                Fecha = DateTime.Now,
                // No hay RemitenteId ni DestinatarioId si no es una transferencia entre usuarios
                // Puedes poner RemitenteId = null o un ID especial para "Sistema" si lo tienes en tu DB
            };
            _context.Movimientos.Add(movimiento);


            // 5. Guardar cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(usuario); // Retorna el usuario actualizado o un mensaje de éxito
        }
    }
}