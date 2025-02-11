using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Domain.Entities;
using XoDotNet.Domain.Enums;

namespace XoDotNet.DataAccess.Repositories;

public class GameRepository(
    AppDbContext db,
    IMongoCollection<GameState> gameStateCollection,
    IUserRepository userRepository) : IGameRepository
{
    public IQueryable<Game> GetGamesAsyncQueryable()
    {
        return db.Games;
    }

    public async Task<GameState?> GetGameStateByIdAsync(Guid id)
    {
        return await gameStateCollection.Find(state => state.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Game> CreateGameAsync(User creator, int maxRating)
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            CreatedDateTime = DateTime.UtcNow,
            Creator = creator,
            MaxRating = maxRating,
            Status = GameStatus.Open
        };
        var newGame = await db.Games.AddAsync(game);
        await db.SaveChangesAsync();
        var user = await userRepository.GetUserWithRatingByUsernameAsync(creator.Username);
        var gameState = new GameState
        {
            Id = newGame.Entity.Id,
            Field = new string('-', 3 * 3),
            Player1 = user,
            Player2 = null,
            Turn = new Random().Next(1, 3)
        };
        await gameStateCollection.InsertOneAsync(gameState);
        return newGame.Entity;
    }

    public async Task UpdatePlayers(Guid gameId, UserRating? player1, UserRating? player2)
    {
        var state = await gameStateCollection.Find(state => state.Id == gameId).FirstOrDefaultAsync();
        var game = await db.Games.FirstOrDefaultAsync(game1 => game1.Id == gameId);
        if (state == null || game == null)
            throw new Exception("Could not find game");
        state.Player1 = player1;
        state.Player2 = player2;
        await gameStateCollection.ReplaceOneAsync(gameState => gameState.Id == gameId, state);
        if (state.Player1 == null && state.Player2 == null)
            game.Status = GameStatus.Completed;
        else if (state.Player1 == null || state.Player2 == null)
            game.Status = GameStatus.Open;
        else
            game.Status = GameStatus.Ongoing;
        db.Games.Update(game);
        await db.SaveChangesAsync();
    }

    public async Task UpdateFieldAndTurn(Guid gameId, string field, int turn)
    {
        var game = await gameStateCollection.Find(state => state.Id == gameId).FirstOrDefaultAsync();
        if (game == null)
            throw new Exception("Could not find game");
        game.Field = field;
        game.Turn = turn;
        await gameStateCollection.ReplaceOneAsync(state => state.Id == gameId, game);
    }
}