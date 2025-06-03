using Microsoft.EntityFrameworkCore;
using PC_ShopHouse.Models;

namespace PC_ShopHouse.Repositories
{
    public class CPURepository : ICPURepository
    {
        private readonly ApplicationDbContext _context;
        public CPURepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<CPU>> GetAllCPUsAsync()
        {
            return await _context.CPUs.Include(p => p.Product).ToListAsync();
        }

        public async Task<CPU?> GetCPUByIdAsync(int id)
        {
            return await _context.CPUs.Include(p => p.Product).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<CPU> GetByProductIdAsync(int productId)
        {
            return await _context.CPUs.Include(p => p.Product).FirstOrDefaultAsync(p => p.ProductId == productId);
        }
        public async Task AddCPUAsync(CPU cpu)
        {
            _context.CPUs.Add(cpu);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCPUAsync(CPU cpu)
        {
            _context.CPUs.Update(cpu);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateOrCreateAsync(CPU cpu)
        {
            var existing = await _context.CPUs.FirstOrDefaultAsync(c => c.ProductId == cpu.ProductId);
            if (existing != null)
            {
                existing.Socket = cpu.Socket;
                existing.Cores = cpu.Cores;
                existing.Threads = cpu.Threads;
                existing.BaseClock = cpu.BaseClock;
                existing.BoostClock = cpu.BoostClock;
                existing.TDP = cpu.TDP;
                existing.Cache = cpu.Cache;
                existing.IntegratedGraphics = cpu.IntegratedGraphics;
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.CPUs.Add(cpu);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteCPUAsync(int id)
        {
            var cpu = await _context.CPUs.FindAsync(id);
            if (cpu != null)
            {
                _context.CPUs.Remove(cpu);
                await _context.SaveChangesAsync();
            }
        }

    }
}
