using Hakin.Application.Common.Interfaces.Authentication;
using Hakin.Application.Common.Interfaces.Services;
using Hakin.Infrastructure.Authentication;
using Hakin.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hakin.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();
        return services;
    }
}