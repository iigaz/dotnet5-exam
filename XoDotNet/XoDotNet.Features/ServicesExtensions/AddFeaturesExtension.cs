using Microsoft.Extensions.DependencyInjection;
using XoDotNet.Features.Abstractions;
using XoDotNet.Features.Services;

namespace XoDotNet.Features.ServicesExtensions;

public static class AddFeaturesExtension
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddSingleton<IGameFieldCheckerService, GameFieldCheckerService>();
        return services;
    }
}