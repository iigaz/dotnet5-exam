using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Domain.Entities;

namespace XoDotNet.DataAccess.Repositories;

public class UserRepository(AppDbContext db, IMongoCollection<UserRating> userRatingCollection) : IUserRepository
{
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await db.Users.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task CreateUserAsync(User user)
    {
        var newUser = await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        await userRatingCollection.InsertOneAsync(new UserRating
        {
            Id = newUser.Entity.Id,
            Rating = 0,
            Username = newUser.Entity.Username
        });
    }

    public async Task<UserRating?> GetUserWithRatingByUsernameAsync(string username)
    {
        var userTask = userRatingCollection.Find(user => user.Username == username).FirstOrDefaultAsync();
        if (userTask == null)
            return null;
        return await userTask;
    }

    public async Task<List<UserRating>> GetTopRatingsAsync(int limit)
    {
        var users = await userRatingCollection.Find(_ => true).SortByDescending(user => user.Rating).Limit(limit)
            .ToListAsync();
        return users ?? [];
    }
}