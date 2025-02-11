using XoDotNet.Infrastructure.Cqrs.Queries;

namespace XoDotNet.Features.Users.Queries.GetUser;

public class GetUserQuery(string Username) : IQuery<GetUserDto>
{
}