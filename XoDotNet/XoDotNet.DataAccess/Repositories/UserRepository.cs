using Microsoft.EntityFrameworkCore;
using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Domain.Entities;

namespace XoDotNet.DataAccess.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await db.Users.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task CreateUser(User user)
    {
        // TODO: Add rating
        var newUser = await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
    }

    public async Task<UserRating?> GetUserWithRatingByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserRating>> GetTopRatings(int limit)
    {
        throw new NotImplementedException();
    }
}