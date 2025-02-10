using XoDotNet.Infrastructure.Cqrs.Commands;

namespace XoDotNet.Features.Games.Commands.CreateGame;

public record CreateGameCommand : ICommand<CreateGameDto>
{
}