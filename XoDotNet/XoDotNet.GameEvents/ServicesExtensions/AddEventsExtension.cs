using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using XoDotNet.GameEvents.Configurations;
using XoDotNet.GameEvents.Consumers;

namespace XoDotNet.GameEvents.ServicesExtensions;

public static class AddEventsExtension
{
    public static IServiceCollection AddEvents(this IServiceCollection services, RabbitMqConfig config)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<DeclareWinnerConsumer>();
            x.AddConsumer<JoinGameConsumer>();
            x.AddConsumer<LeaveGameConsumer>();
            x.AddConsumer<PlaceMarkConsumer>();
            x.AddConsumer<UpdateStateConsumer>();
            x.AddConsumer<RestartGameConsumer>();

            x.AddDelayedMessageScheduler();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseDelayedMessageScheduler();

                cfg.Host(config.Hostname, h =>
                {
                    h.Username(config.Username);
                    h.Password(config.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}