using XoDotNet.Domain.Abstractions.Repositories;
using XoDotNet.Infrastructure.Cqrs.Commands;
using XoDotNet.Infrastructure.Errors;
using XoDotNet.Infrastructure.Results;

namespace XoDotNet.Features.Games.Commands.CreateGame;

public class CreateGameHandler(IGameRepository gameRepository, IUserRepository userRepository)
    : ICommandHandler<CreateGameCommand, CreateGameDto>
{
    public async Task<Result<CreateGameDto>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var userRating = await userRepository.GetUserWithRatingByUsernameAsync(request.Username);
        var user = await userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || userRating == null)
            return Result<CreateGameDto>.Failure(new NotFoundError("Could not find username."));
        var game = await gameRepository.CreateGameAsync(user, Math.Max(userRating.Rating, request.MaxRating));
        return Result<CreateGameDto>.Success(new CreateGameDto(game.Id));
    }
}