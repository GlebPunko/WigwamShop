using Microsoft.EntityFrameworkCore;

namespace Basket.Infastructure.Context
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {
            
        }
    }
}
