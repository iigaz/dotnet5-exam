using XoDotNet.Infrastructure.Cqrs.Commands;

namespace XoDotNet.Features.Games.Commands.LeaveGame;

public record LeaveGameCommand(Guid GameId, string Username) : ICommand
{
}