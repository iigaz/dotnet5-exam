using XoDotNet.Features.Games.Queries.GetGame;

namespace XoDotNet.GameEvents.Abstractions;

public interface IGamesHubClient
{
    Task UpdateState(string field, int turn, GamePlayer? player1, GamePlayer? player2);
    Task DeclareWinner(int winner, GamePlayer player1, GamePlayer player2);
}