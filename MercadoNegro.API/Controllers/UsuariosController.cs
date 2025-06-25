using MercadoNegro.Core.DTOs;
using MercadoNegro.Core.Entities;
using MercadoNegro.Core.Interfaces;
using MercadoNegro.Core.Interfaces.MercadoNegro.Core.Interfaces;
using MercadoNegro.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace MercadoNegro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("registro")]
        public async Task<ActionResult<Usuario>> Registrar([FromBody] UsuarioRegistroDTO registroDto)
        {
            if (await _usuarioRepository.ExistsByEmailAsync(registroDto.Email))
                return BadRequest("El email ya está registrado");

            var usuario = new Usuario
            {
                Nombre = registroDto.Nombre,
                Apellido = registroDto.Apellido,
                Email = registroDto.Email,
                Contraseña = registroDto.Contraseña,
                Cvu = GenerarCvuUnico(),
                Saldo = 0
            };

            await _usuarioRepository.AddAsync(usuario);

            // Devuelve el usuario creado con su ID generado
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }


        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] UsuarioLoginDTO loginDto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);
            if (usuario == null || usuario.Contraseña != loginDto.Contraseña)
                return Unauthorized("Credenciales inválidas");

            return Ok(usuario);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        private string GenerarCvuUnico()
        {
            return "CVU" + Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
        }
    }
}
