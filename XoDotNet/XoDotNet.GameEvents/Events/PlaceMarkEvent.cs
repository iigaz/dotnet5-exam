namespace XoDotNet.GameEvents.Events;

public record PlaceMarkEvent(Guid GameId, string Username, int X, int Y)
{
}