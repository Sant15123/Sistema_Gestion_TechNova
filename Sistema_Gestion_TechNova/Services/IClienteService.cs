using Sistema_Gestion_TechNova.Models;

namespace Sistema_Gestion_TechNova.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAllAsync(string search = null);
        Task<Cliente> GetByIdAsync(string id);
        Task CreateAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(string id);
    }
}
