using Booking.Application;
using Booking.Application.Common.Model.Email;
using Booking.Application.Common.Services;
using Booking.Application.Common.Services.Notifications;
using Booking.Application.Features.Apartments;
using Booking.Application.Features.Apartments.Commands.CreateApartment;
using Booking.Application.Features.Bookings;
using Booking.Application.Features.Bookings.Commands;
using Booking.Application.Features.Emails;
using Booking.Application.Features.Owners;
using Booking.Application.Features.Owners.Commands;
using Booking.Application.Features.Photos;
using Booking.Application.Features.Reviews;
using Booking.Application.Features.Reviews.Commands.CreateReview;
using Booking.Application.Features.Users;
using Booking.Application.Features.Users.Auth;
using Booking.Application.Features.Users.Commands.CreateUser;
using Booking.Application.Repositories;
using Booking.Infrastructure.Apartments;
using Booking.Infrastructure.Bookings;
using Booking.Infrastructure.Emails;
using Booking.Infrastructure.GenericRepoImpl;
using Booking.Infrastructure.Owners;
using Booking.Infrastructure.Photos;
using Booking.Infrastructure.Reviews;
using Booking.Infrastructure.Services;
using Booking.Infrastructure.Services.Notifications;
using Booking.Infrastructure.Users;
using Booking.Infrastructure.Users.AuthImpl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hangfire;
using Hangfire.SqlServer;

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
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            //apartment scopes
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<CreateApartmentValidations>();

            //owner scopes
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<CreateOwnerValidations>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IPhotosRepository, PhotosRepository>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.Configure<EmailSenderOptions>(configuration.GetSection("EmailSettings"));

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<CreateReviewValidations>();

            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<CreateBookingValidations>();
            services.AddScoped<IBookingStatusUpdaterJob, BookingStatusUpdaterJob>();
            services.AddApplicationConfigurations();

            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ITemplateService, TemplateService>();

            services.AddScoped<ICacheService, MemoryCacheService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SecretKey"]!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/notifications"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();

            services.AddHangfire(config =>
            {
                config.UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddHangfireServer();
            services.AddScoped<IBookingStatusUpdaterJob, BookingStatusUpdaterJob>();
            services.AddSignalR();
            services.AddMemoryCache();
            services.AddResponseCaching();

            services.AddTransient<IPdfGeneratorService, PdfGeneratorService>();

            services.AddScoped<ICalculatorService, CalculatorService>();

            return services;

        }

    }
}
