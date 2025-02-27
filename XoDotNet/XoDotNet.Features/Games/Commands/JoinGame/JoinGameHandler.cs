using Microsoft.EntityFrameworkCore;
using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Commands.JoinGame;

public class JoinGameHandler(IGameRepository gameRepository, IUserRepository userRepository)
    : ICommandHandler<JoinGameCommand>
{
    public async Task<Result> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithRatingByUsernameAsync(request.Username);
        var gameState = await gameRepository.GetGameStateByIdAsync(request.GameId);
        var game = await gameRepository.GetGamesAsyncQueryable()
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);
        if (gameState == null || user == null || game == null)
            return Result.Failure(new NotFoundError("Game or user were not found."));
        if (game.MaxRating < user.Rating)
            return Result.Failure(new ForbiddenError("Rating too high."));
        if (gameState.Player1 == null && gameState.Player2 == null)
            return Result.Failure(new ForbiddenError("The game is ended."));

        if (gameState.Player1 == null)
        {
            await gameRepository.UpdatePlayers(request.GameId, user, gameState.Player2);
            return Result.Success();
        }

        if (gameState.Player2 == null)
        {
            await gameRepository.UpdatePlayers(request.GameId, gameState.Player1, user);
            return Result.Success();
        }

        return Result.Failure(new ForbiddenError("The game is full."));
    }
}