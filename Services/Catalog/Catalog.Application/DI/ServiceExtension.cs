using Catalog.Application.Validator.PipelineBehavior;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Application.DI
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
