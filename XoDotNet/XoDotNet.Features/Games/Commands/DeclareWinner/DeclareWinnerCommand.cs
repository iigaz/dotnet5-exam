using XoDotNet.Infrastructure.Cqrs.Commands;

namespace XoDotNet.Features.Games.Commands.DeclareWinner;

public record DeclareWinnerCommand(Guid GameId, int Winner) : ICommand
{
}