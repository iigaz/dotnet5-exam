namespace XoDotNet.Features.Games.Queries.GetGames;

public record GetGamesDto(Guid Id, GameStatus Status, DateTime CreatedAt, int MaxRating, string Creator)
{
}