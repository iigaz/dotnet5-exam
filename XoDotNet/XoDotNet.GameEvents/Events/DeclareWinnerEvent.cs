namespace XoDotNet.GameEvents.Events;

public record DeclareWinnerEvent(Guid GameId, int Winner)
{
}