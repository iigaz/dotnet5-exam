namespace XoDotNet.Domain.Entities;

public class UserRating
{
    public User User { get; set; } = null!;

    public int Rating { get; set; }
}