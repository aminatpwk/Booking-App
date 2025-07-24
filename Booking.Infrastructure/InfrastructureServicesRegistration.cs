using Booking.Application.Features.Apartments;
using Booking.Application.Features.Apartments.Commands.CreateApartment;
using Booking.Application.Features.Users;
using Booking.Application.Features.Users.Commands.CreateUser;
using Booking.Application.Repositories;
using Booking.Infrastructure.GenericRepoImpl;
using Booking.Infrastructure.Users;
using Booking.Infrastructure.Apartments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Booking.Application.Features.Owners;
using Booking.Infrastructure.Owners;
using Booking.Application.Features.Owners.Commands;
namespace Booking.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<BookingContext>(options =>             {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //user scopes 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<CreateUserValidations>();

            //apartment scopes
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<CreateApartmentValidations>();

            //owner scopes
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<CreateOwnerValidations>();
            return services;

        }

    }
}
