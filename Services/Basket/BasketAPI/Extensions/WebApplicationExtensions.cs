using Basket.Infastructure.Context;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.EntityFrameworkCore;

namespace BasketAPI.Extensions
{
    public static class WebApplicationExtensions
    {
        public async static Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var contextBasket = scope.ServiceProvider.GetRequiredService<BasketDbContext>();
                var contextHangfire = scope.ServiceProvider.GetRequiredService<HangfireDbContext>();

                await contextBasket!.Database.MigrateAsync();
                await contextHangfire.Database.EnsureCreatedAsync();
            }

            return app;
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
