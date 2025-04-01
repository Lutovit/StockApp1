using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.Context
{
    public class ApplicationDbContext : DbContext
    { 
        public DbSet<Candle> Candles { get; set; }
        public DbSet<PriceDifference> PriceDifferences { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {  
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
