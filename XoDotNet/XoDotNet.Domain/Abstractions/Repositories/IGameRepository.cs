using XoDotNet.Domain.Entities;

namespace XoDotNet.Domain.Abstractions.Repositories;

public interface IGameRepository
{
    IQueryable<Game> GetGamesAsyncQueryable();

    Task<GameState?> GetGameStateByIdAsync(Guid id);
}