using Microsoft.EntityFrameworkCore;
using Repository.Abstract;
using Repository.Context;
using Repository.Entities;


namespace Repository.Concret
{
    public class CandleRepository : ICandleRepository
    {
        private ApplicationDbContext _context;


        public CandleRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Candle>> GetAllAsync()
        {
            return await _context.Candles.ToListAsync();
        }


        public async Task<Candle> FindByIdAsync(string id)
        {
            return await _context.Candles.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }


        public async Task<IEnumerable<Candle>> FindByTickerAsync(string ticker)
        {
            return await _context.Candles.Where(x => x.Ticker.ToLower().Trim() == ticker.ToLower().Trim()).ToListAsync();
        }


        public async Task CreateAsync(Candle item)
        {
            await _context.Candles.AddAsync(item);
            await _context.SaveChangesAsync();
        }


        public async Task CreateRangeAsync(IEnumerable<Candle> items)
        {
            await _context.Candles.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Candle item)
        {
            _context.Candles.Update(item);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(string id)
        {
            var item = await _context.Candles.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            if (item != null)
            {
                _context.Candles.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

    }
}
