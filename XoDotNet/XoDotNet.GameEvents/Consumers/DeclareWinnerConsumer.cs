using MassTransit;
using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Features.Games.Commands.DeclareWinner;
using XoDotNet.Features.Games.Queries.GetGame;
using XoDotNet.GameEvents.Abstractions;
using XoDotNet.GameEvents.Events;
using XoDotNet.Mediator;

namespace XoDotNet.GameEvents.Consumers;

public class DeclareWinnerConsumer(
    IMediator mediator,
    IGameRepository repository,
    IGamesHubSender sender,
    IMessageScheduler messageScheduler
) : IConsumer<DeclareWinnerEvent>
{
    public async Task Consume(ConsumeContext<DeclareWinnerEvent> context)
    {
        var result = await mediator.Send(
            new DeclareWinnerCommand(context.Message.GameId, context.Message.Winner)
        );
        if (result.IsSuccess)
        {
            var game = await repository.GetGameStateByIdAsync(context.Message.GameId);
            if (game is { Player1: not null, Player2: not null })
                await sender.DeclareWinner(
                    context.Message.GameId,
                    context.Message.Winner,
                    new GamePlayer(game.Player1.Username, game.Player1.Rating),
                    new GamePlayer(game.Player2.Username, game.Player2.Rating)
                );
            await messageScheduler.SchedulePublish(
                DateTime.UtcNow.AddSeconds(3),
                new RestartGameEvent(context.Message.GameId)
            );
        }
    }
}
