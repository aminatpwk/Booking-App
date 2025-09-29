using Booking.Application.Common.Exceptions;
using Booking.Application.Common.Services;
using Booking.Application.Features.Apartments.Commands.CreateApartment;
using Booking.Application.Features.Apartments.Commands.DeleteApartment;
using Booking.Application.Features.Apartments.Commands.UpdateApartment;
using Booking.Application.Features.Apartments.Queries.Get;
using Booking.Application.Features.Apartments.Queries.GetAllPaged;
using Booking.Application.Features.Bookings.Commands;
using Booking.Application.Features.Bookings.Commands.CancelBooking;
using Booking.Application.Features.Bookings.Commands.ConfirmBooking;
using Booking.Application.Features.Bookings.Queries.GetAll;
using Booking.Application.Features.Owners.Commands;
using Booking.Application.Features.Photos.Commands.DeletePhotos;
using Booking.Application.Features.Reviews.Commands.CreateReview;
using Booking.Application.Features.Users.Commands.CreateUser;
using Booking.Infrastructure;
using Hangfire;
using Booking.Shared.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

//remove origin cors if not necessary or adjust by frontend url
var specificOrigins = "_specificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: specificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateUserHandler).Assembly,        
    typeof(CreateApartmentHandler).Assembly,   
    typeof(CreateOwnerHandler).Assembly,       
    typeof(GetApartmentHandler).Assembly,      
    typeof(DeleteApartmentHandler).Assembly,   
    typeof(UpdateApartmentHandler).Assembly,
    typeof(GetAllApartmentsPagedHandler).Assembly,
    typeof(DeletePhotoHandler).Assembly,
    typeof(CreateReviewHandler).Assembly,
    typeof(CreateBookingHandler).Assembly,
    typeof(GetAllBookingsHandler).Assembly,
    typeof(ConfirmBookingHandler).Assembly,
    typeof(CancelBookingHandler).Assembly
));
builder.Logging.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(specificOrigins);
app.UseMiddleware<ExceptionMiddleware>();
app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard("/hangfire");
RecurringJob.AddOrUpdate<IBookingStatusUpdaterJob>(
    "update-completed-bookings",
    job => job.UpdateCompletedBookings(),
    "1 0 * * *"
    );
app.MapControllers();
app.MapHub<NotificationHub>("/hubs/notifications");
app.UseStaticFiles();
app.Run();