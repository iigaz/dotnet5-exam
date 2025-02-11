using XoDotNet.Infrastructure.Cqrs.Commands;

namespace XoDotNet.Features.Games.Commands.JoinGame;

public class JoinGameCommand(Guid GameId, string Username) : ICommand
{
}