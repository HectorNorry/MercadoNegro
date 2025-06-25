using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercadoNegro.Core.Entities;
using MercadoNegro.Core.Interfaces;
using MercadoNegro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MercadoNegro.Infrastructure.Repositories
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly AppDbContext _context;

        public MovimientoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movimiento>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Movimientos
                .Include(m => m.Remitente)
                .Include(m => m.Destinatario)
                .Where(m => m.RemitenteId == usuarioId || m.DestinatarioId == usuarioId)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync();
        }

        public async Task AddAsync(Movimiento movimiento)
        {
            await _context.Movimientos.AddAsync(movimiento);
            await _context.SaveChangesAsync();
        }
    }
}
