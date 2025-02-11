using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Domain.Entities;

namespace XoDotNet.DataAccess.Repositories;

public class GameRepository(AppDbContext db) : IGameRepository
{
    public IQueryable<Game> GetGamesAsyncQueryable()
    {
        return db.Games;
    }

    public async Task<GameState?> GetGameStateByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}