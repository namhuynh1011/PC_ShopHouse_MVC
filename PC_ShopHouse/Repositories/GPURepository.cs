using Microsoft.EntityFrameworkCore;
using PC_ShopHouse.Models;

namespace PC_ShopHouse.Repositories
{
    public class GPURepository : IGPURepository
    {
        private readonly ApplicationDbContext _context;
        public GPURepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GPU>> GetAllGPUsAsync()
        {
            return await _context.GPUs.Include(p => p.Product).ToListAsync();
        }
        public async Task<GPU?> GetGPUByIdAsync(int id)
        {
            return await _context.GPUs.Include(p => p.Product).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task CreateGPUAsync(GPU gpu)
        {
            _context.GPUs.Add(gpu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGPUAsync(GPU gpu)
        {
            _context.GPUs.Update(gpu);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGPUAsync(int id)
        {
            var gpu = await _context.GPUs.FindAsync(id);
            if (gpu != null)
            {
                _context.GPUs.Remove(gpu);
                await _context.SaveChangesAsync();
            }

        }
    }
}
