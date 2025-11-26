using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization.Attributes;

namespace Sistema_Gestion_TechNova.Models
{
    public class ApplicationUser : IdentityUser
    {
        [BsonId]
        public string MongoId { get; set; } = Guid.NewGuid().ToString();
    }
}
