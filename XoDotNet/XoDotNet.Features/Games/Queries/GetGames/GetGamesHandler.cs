using Microsoft.EntityFrameworkCore;
using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Infrastructure.Cqrs.Queries;
using XoDotNet.Infrastructure.Pages;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Queries.GetGames;

public class GetGamesHandler(IGameRepository gameRepository)
    : IQueryHandler<GetGamesQuery, PageContent<List<GetGamesDto>>>
{
    public async Task<Result<PageContent<List<GetGamesDto>>>> Handle(GetGamesQuery request,
        CancellationToken cancellationToken)
    {
        var games = gameRepository.GetGamesAsyncQueryable();

        var pageBuilder = new PageContentBuilder<List<GetGamesDto>>(
            games.Count(),
            request.PageSize,
            request.Page);

        var content = await games
            .Include(game => game.Creator)
            .OrderBy(game => game.Status)
            .ThenByDescending(game => game.CreatedDateTime)
            .Skip(pageBuilder.Offset)
            .Take(pageBuilder.Limit)
            .Select(game =>
                new GetGamesDto(
                    game.Id,
                    game.Status,
                    game.CreatedDateTime,
                    game.MaxRating,
                    game.Creator.Username))
            .ToListAsync(cancellationToken);

        return Result<PageContent<List<GetGamesDto>>>.Success(pageBuilder.Build(content));
    }
}