using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using XoDotNet.Features.Abstractions;

namespace XoDotNet.Features.Services;

public class PasswordService : IPasswordService
{
    public Task<string> HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(128 / 8);
        return Task.FromResult(HashPasswordWithSalt(password, salt));
    }

    public Task<bool> CheckPassword(string password, string passwordHash)
    {
        var split = passwordHash.Split(':');
        if (split.Length != 2)
            throw new ArgumentException("Invalid hashed password");
        var salt = Convert.FromBase64String(split[0]);
        var hashed = HashPasswordWithSalt(password, salt);
        return Task.FromResult(hashed == passwordHash);
    }

    private string HashPasswordWithSalt(string password, byte[] salt)
    {
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8));
        return Convert.ToBase64String(salt) + ':' + hashed;
    }
}