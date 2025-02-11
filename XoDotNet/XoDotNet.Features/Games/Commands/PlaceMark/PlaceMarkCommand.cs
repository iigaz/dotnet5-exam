using XoDotNet.Infrastructure.Cqrs.Commands;

namespace XoDotNet.Features.Games.Commands.PlaceMark;

public record PlaceMarkCommand(Guid GameId, string Username, int X, int Y) : ICommand<PlaceMarkDto>
{
}