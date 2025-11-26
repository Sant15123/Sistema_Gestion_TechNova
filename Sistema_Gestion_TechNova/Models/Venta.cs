using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sistema_Gestion_TechNova.Models
{
    public class Venta
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId ClienteId { get; set; }
        public string? ClienteNombre { get; set; }

        public DateTime Fecha { get; set; }

        public List<VentaItem> Items { get; set; } = new();

        public decimal Total { get; set; }
    }
}
