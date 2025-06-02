using PC_ShopHouse.Models;

namespace PC_ShopHouse.Repositories
{
    public interface IMainboardRepository
    {
        Task<IEnumerable<Mainboard>> GetAllAsync();
        Task<Mainboard> GetByIdAsync(int id);
        Task AddMainAsync(Mainboard mainboard);
        Task UpdateAsync(Mainboard mainboard);
        Task DeleteAsync(int id);
    }
}
