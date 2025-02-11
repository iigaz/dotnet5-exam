using XoDotNet.Infrastructure.Cqrs.Queries;

namespace XoDotNet.Features.Games.Queries.GetGame;

public record GetGameQuery(Guid Id) : IQuery<GetGameDto>
{
}