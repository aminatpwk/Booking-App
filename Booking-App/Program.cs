using Booking.Application.Features.Apartments.Commands.CreateApartment;
using Booking.Application.Features.Users.Commands.CreateUser;
using Booking.Infrastructure;
using Booking.Application.Features.Owners.Commands;
using Booking.Application.Features.Apartments.Queries.Get;
using Booking.Application.Features.Apartments.Commands.DeleteApartment;
using Booking.Application.Features.Apartments.Commands.UpdateApartment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Booking.Application.Common.Exceptions;
using Booking.Application.Features.Apartments.Queries.GetAllPaged;
using Booking.Application.Features.Photos.Commands.DeletePhotos;
using Booking.Application.Features.Users;
using Booking.Infrastructure.Users;
using Booking.Application.Features.Reviews.Commands.CreateReview;

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
    typeof(UpdateApartmentHandler).Assembly,
    typeof(GetAllApartmentsPagedHandler).Assembly,
    typeof(DeletePhotoHandler).Assembly,
    typeof(CreateReviewHandler).Assembly
));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecretKey"]!))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();