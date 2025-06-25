using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercadoNegro.Core.DTOs;
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

        public async Task<IEnumerable<MovimientoGridItemDTO>> GetByUsuarioIdAsync(int usuarioId) 
        {
            return await _context.Movimientos
                                 .Where(m => m.UsuarioId == usuarioId) // Filtra por el ID del usuario del registro
                                 .OrderByDescending(m => m.Fecha)
                                 .Select(m => new MovimientoGridItemDTO 
                                 {
                                     Fecha = m.Fecha,
                                     Descripcion = m.Descripcion,
                                     Monto = m.Monto, // Monto se pasará como está, el signo se ajusta en el cliente
                                     Tipo = m.Tipo, // Pasamos el tipo original de la DB
                                     OtroParticipante = (m.Tipo == "Transferencia Enviada" && m.Destinatario != null) ? m.Destinatario.Nombre + " " + m.Destinatario.Apellido :
                                                        (m.Tipo == "Transferencia Recibida" && m.Remitente != null) ? m.Remitente.Nombre + " " + m.Remitente.Apellido :
                                                        null 
                                 })
                                
                                 .ToListAsync();
        }

        public async Task AddAsync(Movimiento movimiento)
        {
            await _context.Movimientos.AddAsync(movimiento);
            await _context.SaveChangesAsync();
        }
    }
}
