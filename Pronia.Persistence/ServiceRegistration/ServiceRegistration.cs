using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pronia.Application.Abstractions.Helper;
using Pronia.Application.Abstractions.Repositories;
using Pronia.Application.Abstractions.Services;
using Pronia.Application.DTOs.TokenDtos;
using Pronia.Domain.Entities;
using Pronia.Persistence.Contexts;
using Pronia.Persistence.Implementations.Repositories;
using Pronia.Persistence.Implementations.Services;
using Pronia.Persistence.Interceptors;
using Pronia.Persistence.Security.Encrypting;
using Pronia.Persistence.Security.JWT;

namespace Pronia.Persistence.ServiceRegistration;

public static class ServiceRegistration
{

    public static ModelBuilder AddBaseAuditableEntityQueryFilter(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        return modelBuilder;
    }

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

        AddIdentity(services);
        
        
        AddJwtBearer(services, configuration);
        
        //Repositories
        AddRepositories(services);
        
        //Services
        AddServices(services);


        //Interceptors
        services.AddScoped<BaseEntityInterceptor>();

        return services;
    }

    private static void AddJwtBearer(IServiceCollection services, IConfiguration configuration)
    {
        TokenOptionDto tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptionDto>();
        services.AddAuthentication(options => options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
            };
        });
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenHelper, JWTHelper>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    private static void AddIdentity(IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>(opt =>
        {

            opt.Password.RequiredLength = 8;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Lockout.AllowedForNewUsers = false;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            opt.User.RequireUniqueEmail = true;


        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
    }
}
