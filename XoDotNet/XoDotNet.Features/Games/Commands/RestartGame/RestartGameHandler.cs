using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Commands.RestartGame;

public class RestartGameHandler(IGameRepository gameRepository) : ICommandHandler<RestartGameCommand>
{
    public async Task<Result> Handle(RestartGameCommand request, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetGameStateByIdAsync(request.GameId);
        if (game == null)
            return Result.Failure(new NotFoundError("Game was not found."));
        await gameRepository.UpdatePlayers(game.Id, game.Player2, game.Player1);
        await gameRepository.UpdateFieldAndTurn(game.Id, new string('-', 3 * 3), new Random().Next(1, 3));
        return Result.Success();
    }
}