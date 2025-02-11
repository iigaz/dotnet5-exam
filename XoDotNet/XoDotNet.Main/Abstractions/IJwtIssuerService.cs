using XoDotNet.Domain.Entities;

namespace XoDotNet.Main.Abstractions;

public interface IJwtIssuerService
{
    Task<string> GetTokenForUser(User user);
}