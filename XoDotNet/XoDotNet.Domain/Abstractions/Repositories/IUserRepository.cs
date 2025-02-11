using XoDotNet.Domain.Entities;

namespace XoDotNet.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task CreateUser(User user);
    Task<UserRating?> GetUserWithRatingByUsernameAsync(string username);
    Task<List<UserRating>> GetTopRatings(int limit);
}