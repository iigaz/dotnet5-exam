using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Commands.LeaveGame;

public class LeaveGameHandler(IGameRepository gameRepository, IUserRepository userRepository)
    : ICommandHandler<LeaveGameCommand>
{
    public async Task<Result> Handle(LeaveGameCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithRatingByUsernameAsync(request.Username);
        var gameState = await gameRepository.GetGameStateByIdAsync(request.GameId);
        if (gameState == null || user == null)
            return Result.Failure(new NotFoundError("Game or user were not found."));

        if (gameState.Player1?.Username == user.Username)
        {
            await gameRepository.UpdatePlayers(request.GameId, null, gameState.Player2);
            return Result.Success();
        }

        if (gameState.Player2?.Username == user.Username)
        {
            await gameRepository.UpdatePlayers(request.GameId, gameState.Player1, null);
            return Result.Success();
        }

        return Result.Failure(new ForbiddenError("This user is not playing the game."));
    }
}