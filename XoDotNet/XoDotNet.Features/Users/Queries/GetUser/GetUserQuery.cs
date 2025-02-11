using XoDotNet.Infrastructure.Cqrs.Queries;

namespace XoDotNet.Features.Users.Queries.GetUser;

public record GetUserQuery(string Username) : IQuery<GetUserDto>
{
}