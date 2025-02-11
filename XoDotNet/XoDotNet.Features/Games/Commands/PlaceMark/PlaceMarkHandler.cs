using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Features.Abstractions;
using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Commands.PlaceMark;

public class PlaceMarkHandler(
    IGameRepository gameRepository,
    IUserRepository userRepository,
    IGameFieldCheckerService fieldCheckerService)
    : ICommandHandler<PlaceMarkCommand, PlaceMarkDto>
{
    public async Task<Result<PlaceMarkDto>> Handle(PlaceMarkCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithRatingByUsernameAsync(request.Username);
        var gameState = await gameRepository.GetGameStateByIdAsync(request.GameId);
        if (gameState == null || user == null)
            return Result<PlaceMarkDto>.Failure(new NotFoundError("Game or user were not found."));
        var field = gameState.Field.ToCharArray();
        if (field.Length != 9)
            throw new ArgumentException("Field was not 3x3");
        if (gameState.Player1 == null || gameState.Player2 == null)
            return Result<PlaceMarkDto>.Failure("Some players are missing.");

        if (gameState.Player1.Username == user.Username && gameState.Turn == 1)
        {
            var indexer = request.Y * 3 + request.X;
            if (field[indexer] != '-')
                return Result<PlaceMarkDto>.Failure("This cell is already taken.");
            field[indexer] = 'x';
            var fieldCheck = fieldCheckerService.CheckFieldForWinners(field);
            var turn = fieldCheck == -1 ? 2 : 0;
            await gameRepository.UpdateFieldAndTurn(request.GameId, new string(field), turn);
            return Result<PlaceMarkDto>.Success(new PlaceMarkDto(fieldCheck));
        }

        if (gameState.Player2.Username == user.Username && gameState.Turn == 2)
        {
            var indexer = request.Y * 3 + request.X;
            if (field[indexer] != '-')
                return Result<PlaceMarkDto>.Failure("This cell is already taken.");
            field[indexer] = 'o';
            var fieldCheck = fieldCheckerService.CheckFieldForWinners(field);
            var turn = fieldCheck == -1 ? 1 : 0;
            await gameRepository.UpdateFieldAndTurn(request.GameId, new string(field), turn);
            return Result<PlaceMarkDto>.Success(new PlaceMarkDto(fieldCheck));
        }

        return Result<PlaceMarkDto>.Failure(new ForbiddenError("You cannot make a turn."));
    }
}