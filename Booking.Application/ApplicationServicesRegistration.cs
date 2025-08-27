using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using Booking.Application.Common.Behaviors;

namespace Booking.Application
{
    public static class ApplicationServicesRegistration
    {
       public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => 
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
