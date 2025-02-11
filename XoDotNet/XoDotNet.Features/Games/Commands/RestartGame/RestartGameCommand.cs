using XoDotNet.Infrastructure.Cqrs.Commands;

namespace XoDotNet.Features.Games.Commands.RestartGame;

public record RestartGameCommand(Guid GameId) : ICommand
{
}