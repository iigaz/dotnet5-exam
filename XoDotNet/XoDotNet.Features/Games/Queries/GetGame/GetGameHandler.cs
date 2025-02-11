using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Infrastructure.Cqrs.Queries;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Queries.GetGame;

public class GetGameHandler(IGameRepository gameRepository) : IQueryHandler<GetGameQuery, GetGameDto>
{
    public async Task<Result<GetGameDto>> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetGameStateByIdAsync(request.Id);
        if (game == null)
            return Result<GetGameDto>.Failure(new NotFoundError("Game was not found."));
        return Result<GetGameDto>.Success(new GetGameDto(
            game.Player1 != null ? new GamePlayer(game.Player1.Username, game.Player1.Rating) : null,
            game.Player2 != null ? new GamePlayer(game.Player2.Username, game.Player2.Rating) : null,
            game.Field,
            game.Turn));
    }
}