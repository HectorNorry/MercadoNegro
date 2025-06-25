
namespace MercadoNegro.Core.DTOs
{
    public class MovimientoGridItemDTO
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string Tipo { get; set; } 
        public string OtroParticipante { get; set; } 
    }
}