using MongoDB.Driver;
using Sistema_Gestion_TechNova.Data;
using Sistema_Gestion_TechNova.Models;
using Microsoft.Extensions.Options;

namespace Sistema_Gestion_TechNova.Services
{
    public class MongoService
    {
        private readonly IMongoDatabase _db;

        public MongoService(IOptions<MongoSettings> settings, IMongoClient client)
        {
            _db = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Cliente> Clientes => _db.GetCollection<Cliente>("Clientes");
        public IMongoCollection<Producto> Productos => _db.GetCollection<Producto>("Productos");
        public IMongoCollection<Venta> Ventas => _db.GetCollection<Venta>("Ventas");
    }
}
