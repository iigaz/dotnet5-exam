using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Features.Abstractions;
using XoDotNet.Infrastructure.Cqrs.Queries;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Users.Queries.VerifyUser;

public class VerifyUserHandler(IUserRepository userRepository, IPasswordService passwordService)
    : IQueryHandler<VerifyUserQuery, VerifyUserDto>
{
    public async Task<Result<VerifyUserDto>> Handle(VerifyUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByUsernameAsync(request.Username);
        var passwordCorrect =
            user != null && await passwordService.CheckPassword(request.Password, user.PasswordHash);
        if (!passwordCorrect || user == null)
            return Result<VerifyUserDto>.Failure(new NotFoundError("User not found or password is incorrect."));
        return Result<VerifyUserDto>.Success(new VerifyUserDto(user));
    }
}