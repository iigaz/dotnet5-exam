using XoDotNet.Domain.Entities;
using XoDotNet.Infrastructure.Cqrs.Commands;

namespace XoDotNet.Features.Users.Commands.CreateUser;

public record CreateUserCommand(string Username, string Password) : ICommand<User>
{
}