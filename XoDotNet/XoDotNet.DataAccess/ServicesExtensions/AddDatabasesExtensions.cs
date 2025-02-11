using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using XoDotNet.DataAccess.Configuration;
using XoDotNet.DataAccess.Repositories;
using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Domain.Entities;

namespace XoDotNet.DataAccess.ServicesExtensions;

public static class AddDatabasesExtensions
{
    public static IServiceCollection AddDatabases(this IServiceCollection services, MongoDbConfig mongoDbConfig,
        string postgresConnectionString)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddSingleton<IMongoDatabase>(_ =>
        {
            var mongoClient = new MongoClient(
                mongoDbConfig.ConnectionString);
            return mongoClient.GetDatabase(
                mongoDbConfig.DatabaseName);
        });
        services.AddSingleton<IMongoCollection<GameState>>(svc =>
        {
            var mongoDatabase = svc.GetService<IMongoDatabase>();
            if (mongoDatabase == null)
                throw new Exception("Database not initialized.");
            return mongoDatabase.GetCollection<GameState>(
                mongoDbConfig.GameStateCollectionName);
        });
        services.AddSingleton<IMongoCollection<UserRating>>(svc =>
        {
            var mongoDatabase = svc.GetService<IMongoDatabase>();
            if (mongoDatabase == null)
                throw new Exception("Database not initialized.");
            return mongoDatabase.GetCollection<UserRating>(
                mongoDbConfig.UserRatingCollectionName);
        });

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(postgresConnectionString));
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        return services;
    }
}