using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoNegro.Core.DTOs
{
    public class DepositoDTO
    {
        public int UsuarioId { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; } 
    }
}
