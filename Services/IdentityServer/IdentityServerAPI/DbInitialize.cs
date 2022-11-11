using IdentityServer.Infastructere.Context;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerAPI.IdentityServer;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAPI
{
    public static class DbInitialize
    {
        public static void Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();

            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Database.Migrate();

            if (!context.Clients.Any())
            {
                foreach (var client in IdentityConfiguration.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityConfiguration.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in IdentityConfiguration.GetApiScopes())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in IdentityConfiguration.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }
        }
    }
}
