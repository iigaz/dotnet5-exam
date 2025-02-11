using MassTransit;
using XoDotNet.Features.Games.Commands.DeclareWinner;
using XoDotNet.GameEvents.Events;
using XoDotNet.Mediator;

namespace XoDotNet.GameEvents.Consumers;

public class DeclareWinnerConsumer(IMediator mediator) : IConsumer<DeclareWinnerEvent>
{
    public async Task Consume(ConsumeContext<DeclareWinnerEvent> context)
    {
        var result = await mediator.Send(new DeclareWinnerCommand(context.Message.GameId, context.Message.Winner));
        if (result.IsSuccess)
            // TODO: send declare winner message to hub
            await context.SchedulePublish(DateTime.Now.AddSeconds(3), new RestartGameEvent(context.Message.GameId));
    }
}