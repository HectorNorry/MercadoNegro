using MercadoNegro.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MercadoNegro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientoRepository;

        public MovimientosController(IMovimientoRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuarioId(int usuarioId)
        {
            var movimientos = await _movimientoRepository.GetByUsuarioIdAsync(usuarioId);
            return Ok(movimientos);
        }
    }
}
