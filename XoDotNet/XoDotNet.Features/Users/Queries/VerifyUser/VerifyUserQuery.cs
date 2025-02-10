using XoDotNet.Domain.Entities;
using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Cqrs.Queries;

namespace XoDotNet.Features.Users.Queries.VerifyUser;

public record VerifyUserQuery(string Username, string Password) : IQuery<>
{
}