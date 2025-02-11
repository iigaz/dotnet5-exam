using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using XoDotNet.Domain.Entities;
using XoDotNet.Main.Abstractions;
using XoDotNet.Main.Configuration;

namespace XoDotNet.Main.Services;

public class JwtIssuerService(JwtConfig config) : IJwtIssuerService
{
    public Task<string> GetTokenForUser(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username)
        };

        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            config.Issuer,
            config.Audience,
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromMinutes(config.LifetimeMinutes)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecretKey)), SecurityAlgorithms.HmacSha256));
        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}