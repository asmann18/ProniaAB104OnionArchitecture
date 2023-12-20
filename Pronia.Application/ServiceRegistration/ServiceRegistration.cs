﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Pronia.Application.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}