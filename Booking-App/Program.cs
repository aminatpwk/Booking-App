using Booking.Application.Features.Apartments.Commands.CreateApartment;
using Booking.Application.Features.Users.Commands.CreateUser;
using Booking.Infrastructure;
using Booking.Application.Features.Owners.Commands;
using Booking.Application.Features.Apartments.Queries.Get;
using Booking.Application.Features.Apartments.Commands.DeleteApartment;
using Booking.Application.Features.Apartments.Commands.UpdateApartment;

var builder = WebApplication.CreateBuilder(args);

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
    typeof(UpdateApartmentHandler).Assembly    
));
builder.Services.AddAuthorization();

var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();