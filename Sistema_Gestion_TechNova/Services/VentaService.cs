using MongoDB.Bson;
using MongoDB.Driver;
using Sistema_Gestion_TechNova.Models;
using System.Security.Cryptography;

namespace Sistema_Gestion_TechNova.Services
{
    public class VentaService : IVentaService
    {
        private readonly IMongoCollection<Venta> _ventas;
        private readonly IMongoCollection<Producto> _productos;

        public VentaService(MongoService mongo)
        {
            _ventas = mongo.Ventas;
            _productos = mongo.Productos;
        }

        public async Task<List<Venta>> GetAllAsync(string? clienteNombre = null, DateTime? desde = null, DateTime? hasta = null)
        {
            var filter = Builders<Venta>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(clienteNombre))
            {
                filter &= Builders<Venta>.Filter.Regex(
                    v => v.ClienteNombre,
                    new BsonRegularExpression(clienteNombre, "i")
                );
            }

            if (desde.HasValue)
                filter &= Builders<Venta>.Filter.Gte(v => v.Fecha, desde.Value);

            if (hasta.HasValue)
                filter &= Builders<Venta>.Filter.Lte(v => v.Fecha, hasta.Value);

            return await _ventas.Find(filter)
                .SortByDescending(v => v.Fecha)
                .ToListAsync();
        }

        public async Task<Venta> GetByIdAsync(string id)
        {
            var oid = new ObjectId(id);
            return await _ventas.Find(v => v.Id == oid).FirstOrDefaultAsync();
        }

        public async Task<string> RegistrarVentaAsync(Venta venta)
        {
            // ======================================================
            // 1. Validar stock (CORREGIDO)
            // ======================================================
            foreach (var item in venta.Items)
            {
                // Obtener el producto directamente por su ObjectId
                var producto = await _productos
                    .Find(p => p.Id == item.ProductoId)
                    .FirstOrDefaultAsync();

                if (producto == null)
                    return $"Producto no encontrado: {item.ProductoId}";

                if (item.Cantidad > producto.StockDisponible)
                    return $"Stock insuficiente para {producto.Nombre}";
            }



            // ======================================================
            // 2. Descontar stock
            // ======================================================
            foreach (var item in venta.Items)
            {
                var filter = Builders<Producto>.Filter.And(
                    Builders<Producto>.Filter.Eq(p => p.Id, item.ProductoId),
                    Builders<Producto>.Filter.Gte(p => p.StockDisponible, item.Cantidad)
                );

                var update = Builders<Producto>.Update.Inc(p => p.StockDisponible, -item.Cantidad);

                var result = await _productos.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                    return $"Error al descontar stock del producto con ID {item.ProductoId}";
            }


            // ======================================================
            // 3. Insertar venta
            // ======================================================
            await _ventas.InsertOneAsync(venta);
            return "OK";
        }
    }
}
