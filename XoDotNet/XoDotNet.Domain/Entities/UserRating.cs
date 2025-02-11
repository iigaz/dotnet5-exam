namespace XoDotNet.Domain.Entities;

public class UserRating
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public int Rating { get; set; }
}