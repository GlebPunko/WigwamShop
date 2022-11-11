using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Basket.Infastructure.Context;
using Microsoft.AspNetCore.Builder;
using Basket.Infastructure.Repositories.Interfaces;
using Basket.Infastructure.Repositories;

namespace Basket.Infastructure.DI
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasketDbContext>((DbContextOptionsBuilder options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            });

            services.AddDbContext<HangfireDbContext>((DbContextOptionsBuilder options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HangfireConnection"));
            });

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<DataSeeder>();

            return services;
        }

        public async static Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                var service = serviceScope.ServiceProvider.GetService<DataSeeder>();
                
                await service!.InitializeDBAsync();
            }

            return app;
        }
    }
}
