using Sistema_Gestion_TechNova.Models;

namespace Sistema_Gestion_TechNova.Services
{
    public interface IVentaService
    {
        Task<List<Venta>> GetAllAsync(string clienteNombre = null, DateTime? desde = null, DateTime? hasta = null);
        Task<Venta> GetByIdAsync(string id);
        Task<string> RegistrarVentaAsync(Venta venta);
    }
}
