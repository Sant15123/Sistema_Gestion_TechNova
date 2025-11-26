using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Sistema_Gestion_TechNova.Models;

public class MongoSettings { public string ConnectionString { get; set; } public string DatabaseName { get; set; } }

public class MongoService
{
    private readonly IMongoDatabase _db;
    public MongoService(IOptions<MongoSettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _db = client.GetDatabase(options.Value.DatabaseName);
    }
    public IMongoCollection<Cliente> Clientes => _db.GetCollection<Cliente>("Clientes");
    public IMongoCollection<Producto> Productos => _db.GetCollection<Producto>("Productos");
    public IMongoCollection<Venta> Ventas => _db.GetCollection<Venta>("Ventas");
}
