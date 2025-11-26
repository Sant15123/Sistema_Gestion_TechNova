using MongoDB.Bson;
using MongoDB.Driver;
using Sistema_Gestion_TechNova.Models;

namespace Sistema_Gestion_TechNova.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IMongoCollection<Producto> _productos;

        public ProductoService(MongoService mongo)
        {
            _productos = mongo.Productos;
        }

        public async Task<List<Producto>> GetAllAsync(string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
                return await _productos.Find(_ => true).ToListAsync();

            var filter = Builders<Producto>.Filter.Or(
                Builders<Producto>.Filter.Regex(p => p.Nombre, new BsonRegularExpression(search, "i")),
                Builders<Producto>.Filter.Regex(p => p.Codigo, new BsonRegularExpression(search, "i"))
            );

            return await _productos.Find(filter).ToListAsync();
        }

        public async Task<Producto> GetByIdAsync(string id)
        {
            var oid = new ObjectId(id);
            return await _productos.Find(p => p.Id == oid).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Producto producto)
        {
            await _productos.InsertOneAsync(producto);
        }

        public async Task UpdateAsync(Producto producto)
        {
            var filter = Builders<Producto>.Filter.Eq(p => p.Id, producto.Id);
            await _productos.ReplaceOneAsync(filter, producto);
        }

        public async Task DeleteAsync(string id, string oid)
        {
            await _productos.DeleteOneAsync(p => p.Id == new ObjectId(id));
        }

        public async Task<bool> CodigoExisteAsync(string codigo, string? ignoreId = null)
        {
            var filter = Builders<Producto>.Filter.Eq(p => p.Codigo, codigo);

            if (ignoreId != null)
            {
                var oid = new ObjectId(ignoreId);
                filter &= Builders<Producto>.Filter.Ne(p => p.Id, oid);
            }

            return await _productos.CountDocumentsAsync(filter) > 0;
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id, object oid)
        {
            throw new NotImplementedException();
        }
    }
}
