using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MagicalProduct.Repo.Implement;
using MagicalProduct.Repo.Interfaces;
using MagicalProduct.API.Constants;
using System.Collections;
using MagicalProduct.API.Models;
using MagicalProduct.API.Services.Implements;
using MagicalProduct.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder.Extensions;

namespace MagicalProduct.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<MagicalProductContext>(options => options.UseSqlServer(GetConnectionString()));
        return services;
    }

    private static string GetConnectionString()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
         .AddEnvironmentVariables(prefix: JwtConstant.JwtEnvironment)
         .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        var strConn = config["ConnectionStrings:MyConnectionString"];

        return strConn;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<INewsService, NewsService>();
        return services;
    }

    public static IServiceCollection AddJwtValidation(this IServiceCollection services)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
         .AddEnvironmentVariables(prefix: JwtConstant.JwtEnvironment)
         .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

        // Debug: Print all environment variables
        foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
        {
            Console.WriteLine($"Key: {de.Key}, Value: {de.Value}");
        }

        var secretKey = configuration["JwtConstant:" + JwtConstant.SecretKey];
        var issuer = configuration["JwtConstant:" + JwtConstant.Issuer];

        // Debug: Print configuration values
        Console.WriteLine($"SecretKey: {secretKey}");
        Console.WriteLine($"Issuer: {issuer}");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = issuer,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        return services;
    }
}