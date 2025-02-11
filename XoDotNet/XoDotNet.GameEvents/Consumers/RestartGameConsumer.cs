using MassTransit;
using XoDotNet.Features.Games.Commands.RestartGame;
using XoDotNet.GameEvents.Events;
using XoDotNet.Mediator;

namespace XoDotNet.GameEvents.Consumers;

public class RestartGameConsumer(IMediator mediator, IBus bus) : IConsumer<RestartGameEvent>
{
    public async Task Consume(ConsumeContext<RestartGameEvent> context)
    {
        var result = await mediator.Send(new RestartGameCommand(context.Message.GameId));
        if (result.IsSuccess)
            await bus.Publish(new UpdateStateEvent(context.Message.GameId));
    }
}