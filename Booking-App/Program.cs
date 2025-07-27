using Booking.Application.Features.Apartments.Commands.CreateApartment;
using Booking.Application.Features.Users.Commands.CreateUser;
using Booking.Infrastructure;
using Booking.Application.Features.Owners.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateApartmentHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateOwnerHandler>());
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