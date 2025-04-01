using Microsoft.EntityFrameworkCore;
using Repository.Abstract;
using Repository.Context;
using Repository.Entities;

namespace Repository.Concret
{
    public class PriceDifferenceRepository : IPriceDifferenceRepository
    {
        private ApplicationDbContext _context;


        public PriceDifferenceRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<PriceDifference>> GetAllAsync()
        {
            return await _context.PriceDifferences.ToListAsync();
        }


        public async Task<PriceDifference> FindByIdAsync(string id)
        {
            return await _context.PriceDifferences.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }


        public async Task<IEnumerable<PriceDifference>> FindByTickerAsync(string ticker)
        {
            return await _context.PriceDifferences.Where(x => x.Ticker.ToLower().Trim() == ticker.ToLower().Trim()).ToListAsync();
        }


        public async Task CreateAsync(PriceDifference item)
        {
            await _context.PriceDifferences.AddAsync(item);
            await _context.SaveChangesAsync();
        }


        public async Task CreateRangeAsync(IEnumerable<PriceDifference> items)
        {
            await _context.PriceDifferences.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(PriceDifference item)
        {
            _context.PriceDifferences.Update(item);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(string id)
        {
            var item = await _context.PriceDifferences.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            if (item != null)
            {
                _context.PriceDifferences.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
