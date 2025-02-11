using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Infrastructure.Cqrs.Queries;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Users.Queries.GetRatings;

public class GetRatingsHandler(IUserRepository userRepository) : IQueryHandler<GetRatingsQuery, List<GetRatingsDto>>
{
    public async Task<Result<List<GetRatingsDto>>> Handle(GetRatingsQuery request, CancellationToken cancellationToken)
    {
        var ratings = await userRepository.GetTopRatingsAsync(request.Limit);
        return Result<List<GetRatingsDto>>.Success(ratings
            .Select(rating => new GetRatingsDto(rating.Username, rating.Rating)).ToList());
    }
}