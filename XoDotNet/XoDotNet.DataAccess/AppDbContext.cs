using Microsoft.EntityFrameworkCore;
using XoDotNet.Domain.Entities;

namespace XoDotNet.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
}