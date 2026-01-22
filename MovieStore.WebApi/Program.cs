using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieStore.Application.Common.Behaviors;
using MovieStore.Application.Interfaces;
using MovieStore.Infrastructure.Data;
using MovieStore.WebApi.Common.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieStoreDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddScoped<IMovieStoreDbContext>(
    provider => provider.GetRequiredService<MovieStoreDbContext>()
);

builder.Services.AddMediatR(
    typeof(MovieStore.Application.ApplicationAssemblyMarker).Assembly
);

builder.Services.AddAutoMapper(
    typeof(MovieStore.Application.Common.Mappings.MovieProfile).Assembly
);

builder.Services.AddValidatorsFromAssembly(
    typeof(MovieStore.Application.Commands.Movie.CreateMovie.CreateMovieCommandValidator).Assembly
);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>)
);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
