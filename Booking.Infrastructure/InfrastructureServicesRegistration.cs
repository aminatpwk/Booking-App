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
using Booking.Application.Features.Users.Auth;
using Booking.Infrastructure.Users.AuthImpl;
using Booking.Application.Features.Photos;
using Booking.Infrastructure.Photos;
using Booking.Application.Features.Emails;
using Booking.Infrastructure.Emails;
using Booking.Application.Common.Model;
using Booking.Application.Features.Reviews;
using Booking.Domain.Reviews;
using Booking.Infrastructure.Reviews;
using Booking.Application.Features.Reviews.Commands.CreateReview;
using Booking.Application.Features.Bookings;
using Booking.Infrastructure.Bookings;
using Booking.Application;

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

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IPhotosRepository, PhotosRepository>();

            services.AddTransient<IEmailService, EmailService>();
            services.Configure<EmailSenderOptions>(configuration.GetSection("EmailSettings"));

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<CreateReviewValidations>();

            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddApplicationConfigurations();
            return services;

        }

    }
}
