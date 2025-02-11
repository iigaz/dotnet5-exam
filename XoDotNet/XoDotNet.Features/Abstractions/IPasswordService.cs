namespace XoDotNet.Features.Abstractions;

public interface IPasswordService
{
    Task<string> HashPassword(string password);
    Task<bool> CheckPassword(string password, string passwordHash);
}