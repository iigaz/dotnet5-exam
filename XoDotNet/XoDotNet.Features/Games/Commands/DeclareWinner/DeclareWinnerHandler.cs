using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Commands.DeclareWinner;

public class DeclareWinnerHandler(IGameRepository gameRepository, IUserRepository userRepository)
    : ICommandHandler<DeclareWinnerCommand>
{
    public async Task<Result> Handle(DeclareWinnerCommand request, CancellationToken cancellationToken)
    {
        var gameState = await gameRepository.GetGameStateByIdAsync(request.GameId);
        if (gameState?.Player1 == null || gameState.Player2 == null)
            return Result.Failure(new NotFoundError("Invalid game."));

        if (request.Winner == 1)
        {
            gameState.Player1.Rating += 3;
            gameState.Player2.Rating -= 1;
        }

        if (request.Winner == 2)
        {
            gameState.Player2.Rating += 3;
            gameState.Player1.Rating -= 1;
        }

        if (request.Winner > 0)
        {
            await userRepository.UpdateUserRating(gameState.Player1);
            await userRepository.UpdateUserRating(gameState.Player2);
            await gameRepository.UpdatePlayers(gameState.Id, gameState.Player1, gameState.Player2);
        }

        return Result.Success();
    }
}