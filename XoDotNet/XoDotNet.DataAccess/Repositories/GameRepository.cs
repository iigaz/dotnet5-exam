using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Domain.Entities;
using XoDotNet.Domain.Enums;

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

    public async Task<Game> CreateGameAsync(User creator, int maxRating)
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            CreatedDateTime = DateTime.Now,
            Creator = creator,
            MaxRating = maxRating,
            Status = GameStatus.Open
        };
        var newGame = await db.Games.AddAsync(game);
        await db.SaveChangesAsync();
        // TODO: Add game state
        return newGame.Entity;
    }

    public async Task UpdatePlayers(Guid gameId, UserRating? player1, UserRating? player2)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateFieldAndTurn(Guid gameId, string field, int turn)
    {
        throw new NotImplementedException();
    }
}