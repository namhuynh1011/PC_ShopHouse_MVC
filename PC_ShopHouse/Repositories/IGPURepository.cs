using PC_ShopHouse.Models;

namespace PC_ShopHouse.Repositories
{
    public interface IGPURepository
    {
        Task<GPU?> GetGPUByIdAsync(int id);
        Task<IEnumerable<GPU>> GetAllGPUsAsync();
        Task CreateGPUAsync(GPU gpu);
        Task UpdateGPUAsync(GPU gpu);
        Task DeleteGPUAsync(int id);
    }
}
