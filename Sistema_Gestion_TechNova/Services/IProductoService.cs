using Sistema_Gestion_TechNova.Models;

namespace Sistema_Gestion_TechNova.Services
{
    public interface IProductoService
    {
        Task<List<Producto>> GetAllAsync(string search = null);
        Task<Producto> GetByIdAsync(string id);
        Task CreateAsync(Producto producto);
        Task UpdateAsync(Producto producto);
        Task DeleteAsync(string id);

        // Validaciones especiales
        Task<bool> CodigoExisteAsync(string codigo, string? ignoreId = null);
        Task DeleteAsync(string id, object oid);
    }
}
