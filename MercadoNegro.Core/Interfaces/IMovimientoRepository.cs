using MercadoNegro.Core.DTOs;
using MercadoNegro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoNegro.Core.Interfaces
{
    public interface IMovimientoRepository
    {
        Task<IEnumerable<MovimientoGridItemDTO>> GetByUsuarioIdAsync(int usuarioId);
        Task AddAsync(Movimiento movimiento);
    }
}
