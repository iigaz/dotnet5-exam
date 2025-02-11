using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Commands.PlaceMark;

public class PlaceMarkHandler : ICommandHandler<PlaceMarkCommand>
{
    public async Task<Result> Handle(PlaceMarkCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}