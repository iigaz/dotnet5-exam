using XoDotNet.Infrastructure.Cqrs.Queries;
using XoDotNet.Infrastructure.Pages;

namespace XoDotNet.Features.Games.Queries.GetGames;

public record GetGamesQuery(int Page, int PageSize) : IQuery<PageContent<List<GetGamesDto>>>
{
}