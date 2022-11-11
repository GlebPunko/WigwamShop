using Basket.Application.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Basket.Application.Interfaces;

namespace Basket.Application.DI
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPriceControlService, PriceControlService>();

            return services;
        }
    }
}
