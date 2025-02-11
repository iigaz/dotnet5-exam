using MassTransit;
using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Features.Games.Queries.GetGame;
using XoDotNet.GameEvents.Abstractions;
using XoDotNet.GameEvents.Events;

namespace XoDotNet.GameEvents.Consumers;

public class UpdateStateConsumer(IGameRepository repository, IGamesHubSender sender) : IConsumer<UpdateStateEvent>
{
    public async Task Consume(ConsumeContext<UpdateStateEvent> context)
    {
        var game = await repository.GetGameStateByIdAsync(context.Message.GameId);
        if (game != null)
            await sender.UpdateState(
                context.Message.GameId,
                game.Field,
                game.Turn,
                game.Player1 != null ? new GamePlayer(game.Player1.Username, game.Player1.Rating) : null,
                game.Player2 != null ? new GamePlayer(game.Player2.Username, game.Player2.Rating) : null);
    }
}