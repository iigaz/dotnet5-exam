namespace XoDotNet.GameEvents.Events;

public record JoinGameEvent(Guid GameId, string Username)
{
}