namespace XoDotNet.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }

    public User Creator { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public int MaxRating { get; set; }
}