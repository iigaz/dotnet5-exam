using Microsoft.EntityFrameworkCore;
using XoDotNet.Domain.Entities;

namespace XoDotNet.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .Property(user => user.Username)
            .HasMaxLength(128)
            .IsRequired();
        modelBuilder
            .Entity<User>()
            .HasAlternateKey(user => user.Username);
    }
}