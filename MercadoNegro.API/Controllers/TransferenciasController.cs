using MercadoNegro.Core.DTOs;
using MercadoNegro.Core.Entities;
using MercadoNegro.Core.Interfaces.MercadoNegro.Core.Interfaces;
using MercadoNegro.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MercadoNegro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciasController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMovimientoRepository _movimientoRepository;

        public TransferenciasController(
            IUsuarioRepository usuarioRepository,
            IMovimientoRepository movimientoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _movimientoRepository = movimientoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Transferir([FromBody] TransferenciaDTO transferenciaDto)
        {
            try
            {
                var remitente = await _usuarioRepository.GetByIdAsync(transferenciaDto.RemitenteId);
                if (remitente == null)
                    return BadRequest("Remitente no encontrado");

                var destinatario = await _usuarioRepository.GetByCvuAsync(transferenciaDto.CvuDestinatario);
                if (destinatario == null)
                    return BadRequest("Destinatario no encontrado");

                if (remitente.Saldo < transferenciaDto.Monto)
                    return BadRequest("Saldo insuficiente");

                if (transferenciaDto.Monto <= 0)
                    return BadRequest("Monto debe ser positivo");

                remitente.Saldo -= transferenciaDto.Monto;
                destinatario.Saldo += transferenciaDto.Monto;

                var movimiento = new Movimiento
                {
                    Monto = transferenciaDto.Monto,
                    Descripcion = transferenciaDto.Descripcion,
                    RemitenteId = remitente.Id,
                    DestinatarioId = destinatario.Id,
                    Fecha = DateTime.Now
                };

                await _usuarioRepository.UpdateAsync(remitente);
                await _usuarioRepository.UpdateAsync(destinatario);
                await _movimientoRepository.AddAsync(movimiento);

                return Ok(movimiento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
