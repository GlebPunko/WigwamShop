using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Catalog.Infastructure.Context;
using Catalog.Infastructure.Interfaces;
using Catalog.Infastructure.Repositories;

namespace Catalog.Infastructure.DI
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>((DbContextOptionsBuilder options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            });

            services.AddDbContext<HangfireDbContext>((DbContextOptionsBuilder options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HangfireConnection"));
            });

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
