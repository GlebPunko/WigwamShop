using IdentityServer.Domain.Entity;
using IdentityServer.Infastructere.Context;
using IdentityServerAPI.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAPI.Extensions
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

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();
        }

        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
               opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               x => x.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

            services.AddIdentityServer()
            .AddTestUsers(IdentityConfiguration.GetTestUsers())
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            })
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<User>();
        }
    }
}
