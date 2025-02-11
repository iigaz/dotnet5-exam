namespace XoDotNet.Features.Games.Queries.GetGame;

public record GetGameDto(GamePlayer? Player1, GamePlayer? Player2, string Field, int Turn)
{
}