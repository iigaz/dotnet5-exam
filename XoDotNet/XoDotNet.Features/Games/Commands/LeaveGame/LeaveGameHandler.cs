using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Commands.LeaveGame;

public class LeaveGameHandler: ICommandHandler<LeaveGameCommand>
{
    public async Task<Result> Handle(LeaveGameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}