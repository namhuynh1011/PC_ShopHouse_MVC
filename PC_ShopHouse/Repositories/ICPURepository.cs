using PC_ShopHouse.Models;

namespace PC_ShopHouse.Repositories
{
    public interface ICPURepository
    {
        Task<CPU> GetByProductIdAsync(int productId);
        Task<IEnumerable<CPU>> GetAllCPUsAsync();
        Task<CPU?> GetCPUByIdAsync(int id);
        Task AddCPUAsync(CPU cpu);
        Task UpdateCPUAsync(CPU cpu);
        Task UpdateOrCreateAsync(CPU cpu);
        Task DeleteCPUAsync(int id);
    }
}
