using Hangfire.Dashboard.BasicAuthorization;
using Hangfire;
using Catalog.Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Extensions
{
    public static class WebApplicationExtensions
    {
        public async static void MigrateDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var contextCatalog = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
                var contextHangfire = scope.ServiceProvider.GetRequiredService<HangfireDbContext>();

                await contextCatalog.Database.MigrateAsync();
                await contextHangfire.Database.EnsureCreatedAsync();
            }
        }

        public static void UseCustomHangfireDashboard(this WebApplication app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                {
                    RequireSsl = false,
                    SslRedirect = false,
                    LoginCaseSensitive = true,
                    Users = new []
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login = "admin",
                            PasswordClear =  "admin"
                        }
                    }
                })}
            });
        }
    }
}
