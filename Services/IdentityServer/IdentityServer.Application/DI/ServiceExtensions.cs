using IdentityServer.Application.Interfaces;
using IdentityServer.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IdentityServer.Application.DI
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
