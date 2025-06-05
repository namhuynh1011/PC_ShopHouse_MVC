using Microsoft.EntityFrameworkCore;
using PC_ShopHouse.Models;

namespace PC_ShopHouse.Repositories
{
    public class MainboardRepository : IMainboardRepository
    {
        private readonly ApplicationDbContext _context;
        public MainboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Mainboard>> GetAllAsync()
        {
            return await _context.Mainboards
                .Include(m => m.Product)
                .ToListAsync();
        }
        public Task<Mainboard> GetByIdAsync(int id)
        {
            return _context.Mainboards
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public Task AddAsync(Mainboard mainboard)
        {
            _context.Mainboards.Add(mainboard);
            return _context.SaveChangesAsync();
        }
        public Task UpdateAsync(Mainboard mainboard)
        {
            _context.Mainboards.Update(mainboard);
            return _context.SaveChangesAsync();
        }
        public Task DeleteAsync(int id)
        {
            var mainboard = _context.Mainboards.Find(id);
            if (mainboard != null)
            {
                _context.Mainboards.Remove(mainboard);
                return _context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }
    }
}
