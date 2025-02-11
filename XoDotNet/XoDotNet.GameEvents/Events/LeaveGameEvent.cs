namespace XoDotNet.GameEvents.Events;

public record LeaveGameEvent(Guid GameId, string Username)
{
}