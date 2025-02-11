using Microsoft.EntityFrameworkCore;
using XoDotNet.DataAccess;
using XoDotNet.DataAccess.Configuration;
using XoDotNet.DataAccess.ServicesExtensions;
using XoDotNet.Features.Helpers;
using XoDotNet.Features.ServicesExtensions;
using XoDotNet.GameEvents.Abstractions;
using XoDotNet.GameEvents.Configurations;
using XoDotNet.GameEvents.ServicesExtensions;
using XoDotNet.Main.Configuration;
using XoDotNet.Main.Hubs;
using XoDotNet.Main.Services;
using XoDotNet.Main.ServicesExtensions;
using XoDotNet.Mediator.DependencyInjectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator(AssemblyReference.Assembly);
builder.Services.AddAuth(builder.Configuration.GetSection("Jwt").Get<JwtConfig>()!);

builder.Services.AddFeatures();
builder.Services.AddDatabases(
    builder.Configuration.GetSection("MongoDatabase").Get<MongoDbConfig>()!,
    builder.Configuration.GetConnectionString("MainDatabase")!);
builder.Services.AddEvents(builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqConfig>()!);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddScoped<IGamesHubSender, GamesHubSender>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<GamesHub>("/games/hub");

if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations().Any()) context.Database.Migrate();
}

app.Run();