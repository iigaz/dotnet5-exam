using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Infrastructure.Cqrs.Queries;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Users.Queries.GetUser;

public class GetUserHandler(IUserRepository userRepository) : IQueryHandler<GetUserQuery, GetUserDto>
{
    public async Task<Result<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithRatingByUsernameAsync(request.Username);
        if (user == null)
            return Result<GetUserDto>.Failure(new NotFoundError("User not found."));
        return Result<GetUserDto>.Success(new GetUserDto(user.Username, user.Rating));
    }
}