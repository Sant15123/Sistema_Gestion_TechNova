using MongoDB.Bson;
using MongoDB.Driver;
using Sistema_Gestion_TechNova.Models;
using Sistema_Gestion_TechNova.Services;

namespace Sistema_Gestion_TechNova.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IMongoCollection<Cliente> _clientes;

        public ClienteService(MongoService mongo)
        {
            _clientes = mongo.Clientes;
        }

        public async Task<List<Cliente>> GetAllAsync(string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
                return await _clientes.Find(_ => true).ToListAsync();

            // Filtro por nombre o correo usando Regex
            var filter = Builders<Cliente>.Filter.Or(
                Builders<Cliente>.Filter.Regex(c => c.Nombre, new BsonRegularExpression(search, "i")),
                Builders<Cliente>.Filter.Regex(c => c.Correo, new BsonRegularExpression(search, "i"))
            );

            return await _clientes.Find(filter).ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(string id)
        {
            var oid = new ObjectId(id);
            return await _clientes.Find(c => c.Id == oid).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Cliente cliente)
        {
            await _clientes.InsertOneAsync(cliente);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            var filter = Builders<Cliente>.Filter.Eq(c => c.Id, cliente.Id);
            await _clientes.ReplaceOneAsync(filter, cliente);
        }

        public async Task DeleteAsync(string id)
        {
            var oid = new ObjectId(id);
            await _clientes.DeleteOneAsync(c => c.Id == oid);
        }
    }


}
