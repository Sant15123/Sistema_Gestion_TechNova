using MongoDB.Bson;

namespace Sistema_Gestion_TechNova.Models
{
    public class VentaItem
    {
        public ObjectId ProductoId { get; set; }
        public string? ProductoNombre { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
