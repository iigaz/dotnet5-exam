using MassTransit;
using XoDotNet.Features.Games.Commands.JoinGame;
using XoDotNet.GameEvents.Events;
using XoDotNet.Mediator;

namespace XoDotNet.GameEvents.Consumers;

public class JoinGameConsumer(IMediator mediator, IBus bus) : IConsumer<JoinGameEvent>
{
    public async Task Consume(ConsumeContext<JoinGameEvent> context)
    {
        var result = await mediator.Send(new JoinGameCommand(context.Message.GameId, context.Message.Username));
        if (result.IsSuccess)
            await bus.Publish(new UpdateStateEvent(context.Message.GameId));
    }
}