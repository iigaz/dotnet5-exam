using MassTransit;
using XoDotNet.Features.Games.Commands.PlaceMark;
using XoDotNet.GameEvents.Events;
using XoDotNet.Mediator;

namespace XoDotNet.GameEvents.Consumers;

public class PlaceMarkConsumer(IMediator mediator, IBus bus) : IConsumer<PlaceMarkEvent>
{
    public async Task Consume(ConsumeContext<PlaceMarkEvent> context)
    {
        var result = await mediator.Send(new PlaceMarkCommand(context.Message.GameId, context.Message.Username,
            context.Message.X,
            context.Message.Y));
        if (result.IsSuccess)
        {
            await bus.Publish(new UpdateStateEvent(context.Message.GameId));
            if (result.Value != null && result.Value.Winner != -1)
                await bus.Publish(new DeclareWinnerEvent(context.Message.GameId, result.Value.Winner));
        }
    }
}