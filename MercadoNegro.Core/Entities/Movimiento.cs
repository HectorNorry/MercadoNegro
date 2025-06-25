using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoNegro.Core.Entities
{
    public class Movimiento
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Descripcion { get; set; } = string.Empty;

        public int RemitenteId { get; set; }
        public Usuario Remitente { get; set; } = null!;

        public int DestinatarioId { get; set; }
        public Usuario Destinatario { get; set; } = null!;
    }
}
