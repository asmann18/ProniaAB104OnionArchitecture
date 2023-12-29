using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pronia.Application.Abstractions.Helper;
using Pronia.Application.DTOs.TokenDtos;
using Pronia.Domain.Entities;
using Pronia.Infrastructure.Security.Encrypting;
using Pronia.Infrastructure.Security.JWT;
using System.Text;

namespace Pronia.Infrastructure.ServiceRegistration;

public static class ServiceRegistration
{

    public static ModelBuilder AddBaseAuditableEntityQueryFilter(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        return modelBuilder;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddJwtBearer(services, configuration);


        services.AddScoped<ITokenHelper, JWTHelper>();
        services.AddAuthorization();





        return services;
    }


    private static void AddJwtBearer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = configuration["TokenOptions:Issuer"],
                ValidAudience = configuration["TokenOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenOptions:SecurityKey"])),
                LifetimeValidator = (_, expired, token, _) => token != null ? expired > DateTime.UtcNow : false


            };
        });
    }
}
