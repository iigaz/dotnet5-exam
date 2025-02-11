using XoDotNet.Infrastructure.Cqrs.Commands;

namespace XoDotNet.Features.Games.Commands.CreateGame;

public record CreateGameCommand(string Username, int MaxRating) : ICommand<CreateGameDto>
{
}