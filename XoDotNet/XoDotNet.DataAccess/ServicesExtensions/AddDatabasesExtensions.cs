using Microsoft.Extensions.DependencyInjection;
using XoDotNet.DataAccess.Repositories;
using XoDotNet.Domain.Abstractions.Repositories;

namespace XoDotNet.DataAccess.ServicesExtensions;

public static class AddDatabasesExtensions
{
    public static IServiceCollection AddDatabases(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        return services;
    }
}