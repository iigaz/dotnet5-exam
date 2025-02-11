using XoDotNet.Infrastructure.Cqrs.Queries;

namespace XoDotNet.Features.Users.Queries.GetRatings;

public class GetRatingsQuery : IQuery<List<GetRatingsDto>>
{
    private int _limit = 10;

    public GetRatingsQuery(int limit)
    {
        Limit = limit;
    }

    public int Limit
    {
        get => _limit;
        set => _limit = Math.Min(Math.Max(0, value), 500);
    }
}