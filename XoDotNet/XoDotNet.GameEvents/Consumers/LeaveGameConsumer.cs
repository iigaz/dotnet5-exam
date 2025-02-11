using MassTransit;
using XoDotNet.Features.Games.Commands.LeaveGame;
using XoDotNet.GameEvents.Events;
using XoDotNet.Mediator;

namespace XoDotNet.GameEvents.Consumers;

public class LeaveGameConsumer(IMediator mediator, IBus bus) : IConsumer<LeaveGameEvent>
{
    public async Task Consume(ConsumeContext<LeaveGameEvent> context)
    {
        var result = await mediator.Send(new LeaveGameCommand(context.Message.GameId, context.Message.Username));
        if (result.IsSuccess)
            await bus.Publish(new UpdateStateEvent(context.Message.GameId));
    }
}