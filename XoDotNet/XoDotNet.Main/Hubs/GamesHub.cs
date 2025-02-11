using System.Security.Claims;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using XoDotNet.GameEvents.Abstractions;
using XoDotNet.GameEvents.Events;

namespace XoDotNet.Main.Hubs;

[Authorize]
public class GamesHub(IPublishEndpoint publishEndpoint) : Hub<IGamesHubClient>
{
    public async Task Join(Guid gameId)
    {
        var username = Context.User?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
        if (username == null)
        {
            Context.Abort();
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        await publishEndpoint.Publish(new JoinGameEvent(gameId, username));
    }

    public async Task Leave(Guid gameId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
        var username = Context.User?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
        if (username == null)
            Context.Abort();
        else
            await publishEndpoint.Publish(new LeaveGameEvent(gameId, username));
    }

    public async Task Spectate(Guid gameId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
    }

    public async Task PlaceMark(Guid gameId, int x, int y)
    {
        var username = Context.User?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
        if (username == null)
        {
            Context.Abort();
            return;
        }

        await publishEndpoint.Publish(new PlaceMarkEvent(gameId, username, x, y));
    }
}