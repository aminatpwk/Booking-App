using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
            return services;
        }
    }
}
