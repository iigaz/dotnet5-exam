using XoDotNet.Features.Games.Queries.GetGame;

namespace XoDotNet.GameEvents.Abstractions;

public interface IGamesHubSender
{
    Task UpdateState(Guid gameId, string field, int turn, GamePlayer? player1, GamePlayer? player2);
    Task DeclareWinner(Guid gameId, int winner, GamePlayer player1, GamePlayer player2);
}