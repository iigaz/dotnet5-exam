using XoDotNet.Domain.Enums;

namespace XoDotNet.Features.Games.Queries.GetGames;

public record GetGamesDto(Guid Id, string Status, DateTime CreatedAt, int MaxRating, string Creator)
{
}