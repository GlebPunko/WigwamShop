using Basket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Basket.Infastructure.Context
{
    public class BasketDbContext : DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
        { }

        public DbSet<Seller> Sellers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
