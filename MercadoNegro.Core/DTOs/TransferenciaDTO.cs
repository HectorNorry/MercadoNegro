using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoNegro.Core.DTOs
{
    public class TransferenciaDTO
    {
        public int RemitenteId { get; set; }
        public string CvuDestinatario { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
    }
}
