using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Domain.Entities;
using XoDotNet.Features.Abstractions;
using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Users.Commands.CreateUser;

public class CreateUserHandler(IUserRepository userRepository, IPasswordService passwordService)
    : ICommandHandler<CreateUserCommand, CreateUserDto>
{
    public async Task<Result<CreateUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByUsernameAsync(request.Username);
        if (user != null)
            return Result<CreateUserDto>.Failure(
                new ForbiddenError("User with this username already exists."));
        user = new User
        {
            Id = Guid.NewGuid(),
            PasswordHash = await passwordService.HashPassword(request.Password),
            Username = request.Username
        };
        await userRepository.CreateUserAsync(user);
        return Result<CreateUserDto>.Success(new CreateUserDto());
    }
}