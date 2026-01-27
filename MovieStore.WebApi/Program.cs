using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieStore.Application;
using MovieStore.Application.Common.Behaviors;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Application.Interfaces.Security;
using MovieStore.Infrastructure.Configuration;
using MovieStore.Infrastructure.Data;
using MovieStore.Infrastructure.Security;
using MovieStore.WebApi.Common.Middleware;

var builder = WebApplication.CreateBuilder(args);

// -------------------- DbContext --------------------
builder.Services.AddDbContext<MovieStoreDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddScoped<IMovieStoreDbContext>(
    provider => provider.GetRequiredService<MovieStoreDbContext>()
);


// -------------------- MediatR --------------------
builder.Services.AddMediatR(
    typeof(ApplicationAssemblyMarker).Assembly
);

// -------------------- AutoMapper --------------------
builder.Services.AddAutoMapper(
    typeof(MovieStore.Application.Common.Mappings.MovieProfile).Assembly
);

// -------------------- FluentValidation (global) --------------------
builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// -------------------- JWT SETTINGS --------------------
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings")
);

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<JwtSettings>>().Value
);

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);

// -------------------- Authentication --------------------
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


// -------------------- Middleware --------------------
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
