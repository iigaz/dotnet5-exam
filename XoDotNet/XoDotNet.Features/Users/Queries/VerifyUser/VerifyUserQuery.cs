using XoDotNet.Infrastructure.Cqrs.Queries;

namespace XoDotNet.Features.Users.Queries.VerifyUser;

public record VerifyUserQuery(string Username, string Password) : IQuery<VerifyUserDto>
{
}