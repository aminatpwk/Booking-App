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
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateApartmentHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateOwnerHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetApartmentQuery).Assembly,typeof(GetApartmentHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DeleteApartmentHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateApartmentHandler>());
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