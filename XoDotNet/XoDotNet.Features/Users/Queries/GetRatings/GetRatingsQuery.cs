using XoDotNet.Infrastructure.Cqrs.Queries;

namespace XoDotNet.Features.Users.Queries.GetRatings;

public record GetRatingsQuery(int Limit) : IQuery<List<GetRatingsDto>>
{
}