using XoDotNet.Infrastructure.Cqrs.Queries;

namespace XoDotNet.Features.Games.Queries.GetGames;

public record GetGamesQuery : IQuery<GetGamesDto>
{
}