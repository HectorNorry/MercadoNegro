using MercadoNegro.Core.DTOs;
using MercadoNegro.Core.Entities;
using MercadoNegro.Core.Interfaces.MercadoNegro.Core.Interfaces;
using MercadoNegro.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System; 

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

                // Validar que no sea transferencia a si mismo (opcional, pero buena práctica)
                if (remitente.Id == destinatario.Id)
                {
                    return BadRequest("No se puede transferir dinero a uno mismo.");
                }

                if (remitente.Saldo < transferenciaDto.Monto)
                    return BadRequest("Saldo insuficiente");

                if (transferenciaDto.Monto <= 0)
                    return BadRequest("Monto debe ser positivo");

                remitente.Saldo -= transferenciaDto.Monto;
                destinatario.Saldo += transferenciaDto.Monto;

                // Crear movimiento para el REMITENTE
                var movimientoRemitente = new Movimiento
                {
                    UsuarioId = remitente.Id, // El usuario que envió el dinero
                    Tipo = "Transferencia Enviada", // ¡Asigna el tipo aquí!
                    Monto = transferenciaDto.Monto,
                    Descripcion = transferenciaDto.Descripcion,
                    RemitenteId = remitente.Id, // Se auto-referencia como remitente
                    DestinatarioId = destinatario.Id, // ID del destinatario
                    Fecha = DateTime.Now
                };
                await _movimientoRepository.AddAsync(movimientoRemitente);

                // Crear movimiento para el DESTINATARIO (opcional, pero es bueno registrarlo para ambos)
                var movimientoDestinatario = new Movimiento
                {
                    UsuarioId = destinatario.Id, // El usuario que recibió el dinero
                    Tipo = "Transferencia Recibida", // ¡Asigna el tipo aquí!
                    Monto = transferenciaDto.Monto,
                    Descripcion = transferenciaDto.Descripcion,
                    RemitenteId = remitente.Id, // ID del remitente
                    DestinatarioId = destinatario.Id, // Se auto-referencia como destinatario
                    Fecha = DateTime.Now
                };
                await _movimientoRepository.AddAsync(movimientoDestinatario);


                // Actualizar saldos de los usuarios (asegúrate de que los repositorios los actualicen en la DB)
                await _usuarioRepository.UpdateAsync(remitente);
                await _usuarioRepository.UpdateAsync(destinatario);

                // Si tu IMovimientoRepository.AddAsync no guarda los cambios,
                // puede que necesites un SaveChanges en tu UnitOfWork o DbContext directamente aquí,
                // pero si tus repositorios lo manejan internamente, está bien.

                return Ok("Transferencia realizada con éxito."); // Retorna un mensaje de éxito
            }
            catch (Exception ex)
            {
                // Para depuración, puedes loguear la InnerException también
                // Console.WriteLine($"Error interno en transferencia: {ex.InnerException?.Message}");
                return BadRequest(ex.Message); // Esto enviará el mensaje de la excepción al cliente
            }
        }
    }
}
