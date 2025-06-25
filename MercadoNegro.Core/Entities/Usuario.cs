using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoNegro.Core.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Cvu { get; set; } = string.Empty; // Clave única para transferencias
        public decimal Saldo { get; set; } = 0;

        public ICollection<Movimiento> MovimientosEnviados { get; set; } = new List<Movimiento>();
        public ICollection<Movimiento> MovimientosRecibidos { get; set; } = new List<Movimiento>();
    }
}