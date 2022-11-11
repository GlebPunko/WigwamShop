using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using SignalRChat.Context;

namespace SignalRChat.Extensions
{
    public static class WebApplicationExtensions
    {
        public async static Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<HangfireDbContext>();
                await context.Database.EnsureCreatedAsync();
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
