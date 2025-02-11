using Microsoft.AspNetCore.SignalR;
using XoDotNet.Features.Games.Queries.GetGame;
using XoDotNet.GameEvents.Abstractions;
using XoDotNet.Main.Hubs;

namespace XoDotNet.Main.Services;

public class GamesHubSender(IHubContext<GamesHub, IGamesHubClient> hub) : IGamesHubSender
{
    public async Task UpdateState(Guid gameId, string field, int turn, GamePlayer? player1, GamePlayer? player2)
    {
        await hub.Clients.Group(gameId.ToString()).UpdateState(field, turn, player1, player2);
    }

    public async Task DeclareWinner(Guid gameId, int winner, GamePlayer player1, GamePlayer player2)
    {
        await hub.Clients.Group(gameId.ToString()).DeclareWinner(winner, player1, player2);
    }
}