using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Catalog.Domain.Entities;

namespace Catalog.Infastructure.Context
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options) { }

        public DbSet<WigwamsInfo> WigwamsInfos { get; set; } = null!;    
        public DbSet<SellerInfo> SellerInfos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
