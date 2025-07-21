using Booking.Application.Features.Users;
using Booking.Application.Features.Users.Commands.CreateUser;
using Booking.Infrastructure;
using Booking.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserHandler>());

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