using Hakin.Api.Common.Error;
using Hakin.Api.Common.Mapping;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Hakin.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, HakinProblemDetailsFactory>();
        return services;
    }
}