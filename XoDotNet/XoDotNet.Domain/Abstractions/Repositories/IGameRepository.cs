using XoDotNet.Domain.Entities;

namespace XoDotNet.Domain.Abstractions.Repositories;

public interface IGameRepository
{
    IQueryable<Game> GetGamesAsyncQueryable();

    Task<GameState?> GetGameStateByIdAsync(Guid id);

    Task<Game> CreateGameAsync(User creator, int maxRating);

    Task UpdatePlayers(Guid gameId, UserRating? player1, UserRating? player2);

    Task UpdateFieldAndTurn(Guid gameId, string field, int turn);
}