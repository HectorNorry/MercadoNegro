
namespace MercadoNegro.Core.DTOs
{
    public class MovimientoGridItemDTO
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string Tipo { get; set; } // Puede ser "Depósito", "Transferencia Enviada", "Transferencia Recibida"
        public string OtroParticipante { get; set; } // Nombre del remitente o destinatario, si aplica
    }
}