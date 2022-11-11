using Hangfire.SqlServer;
using Hangfire;
using SignalRChat.Context;
using Microsoft.EntityFrameworkCore;

namespace SignalRChat.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigurationAppsettings(this IConfigurationBuilder configuration)
        {
            configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: false, reloadOnChange: true)
                .Build();
        }

        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(x => x.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddHangfireConfig(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddDbContext<HangfireDbContext>((DbContextOptionsBuilder options) =>
            {
                options.UseSqlServer(configurations.GetConnectionString("HangfireConnection"));
            });

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configurations.GetConnectionString("HangfireConnection"),
            new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            services.AddHangfireServer();

            return services;
        }
    }
}
