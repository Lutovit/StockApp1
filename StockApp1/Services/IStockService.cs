using Repository.Entities;

namespace StockApp1.Services
{
    public interface IStockService
    {
        Task<IEnumerable<Candle>> GetCandles(string symbol, string resolution, long from, long to);
    }
}
