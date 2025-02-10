namespace XoDotNet.Domain.Entities;

public class GameState
{
    public UserRating? Player1 { get; set; }

    public UserRating? Player2 { get; set; }

    public int Turn { get; set; }

    public string Field { get; set; } = null!;
}