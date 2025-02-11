using XoDotNet.Infrastructure.Cqrs.Commands;

namespace XoDotNet.Features.Games.Commands.JoinGame;

public record JoinGameCommand(Guid GameId, string Username) : ICommand
{
}