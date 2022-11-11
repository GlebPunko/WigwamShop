using Microsoft.EntityFrameworkCore;

namespace Catalog.Infastructure.Context
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {
        }
    }
}
