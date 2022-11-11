using Microsoft.EntityFrameworkCore;

namespace SignalRChat.Context
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {
            
        }
    }
}
