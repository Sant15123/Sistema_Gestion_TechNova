using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sistema_Gestion_TechNova.Models
{
    public class Producto
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }
    }
}
