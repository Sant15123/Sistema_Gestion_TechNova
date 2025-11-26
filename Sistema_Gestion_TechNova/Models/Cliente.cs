using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sistema_Gestion_TechNova.Models
{
    public class Cliente
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public object Nombre { get; internal set; }
    }
}
