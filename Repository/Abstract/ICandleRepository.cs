using Repository.Entities;

namespace Repository.Abstract
{
    public interface ICandleRepository
    {
        Task<IEnumerable<Candle>> GetAllAsync();
        Task<Candle> FindByIdAsync(string id);
        Task<IEnumerable<Candle>> FindByTickerAsync(string ticker);
        Task CreateAsync(Candle item);
        Task CreateRangeAsync(IEnumerable<Candle> items);
        Task UpdateAsync(Candle item);
        Task RemoveAsync(string id);
    }
}
