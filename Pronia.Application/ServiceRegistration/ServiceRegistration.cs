using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Pronia.Application.AutoMappers;
using Pronia.Application.Validations.CategoryValidations;
using System.Reflection;

namespace Pronia.Application.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining(typeof(CategoryPostDtoValidation)));

        return services;
    }
}
