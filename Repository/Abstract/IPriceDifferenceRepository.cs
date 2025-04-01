using Repository.Entities;

namespace Repository.Abstract
{
    public interface IPriceDifferenceRepository
    {
        Task<IEnumerable<PriceDifference>> GetAllAsync();
        Task<PriceDifference> FindByIdAsync(string id);
        Task<IEnumerable<PriceDifference>> FindByTickerAsync(string ticker);
        Task CreateAsync(PriceDifference item);
        Task CreateRangeAsync(IEnumerable<PriceDifference> items);
        Task UpdateAsync(PriceDifference item);
        Task RemoveAsync(string id);
    }
}
