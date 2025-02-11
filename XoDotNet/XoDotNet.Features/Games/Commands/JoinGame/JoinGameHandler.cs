using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Commands.JoinGame;

public class JoinGameHandler: ICommandHandler<JoinGameCommand>
{
    public async Task<Result> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}