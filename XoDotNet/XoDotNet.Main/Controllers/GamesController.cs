using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XoDotNet.Features.Games.Commands.CreateGame;
using XoDotNet.Features.Games.Queries.GetGame;
using XoDotNet.Features.Games.Queries.GetGames;
using XoDotNet.Main.Extensions;
using XoDotNet.Mediator;
using CreateGameDto = XoDotNet.Main.Dto.CreateGameDto;

namespace XoDotNet.Main.Controllers;

[ApiController]
[Route("/games")]
[Authorize]
public class GamesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetGames([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        var result = await mediator.Send(new GetGamesQuery(page ?? 1, pageSize ?? 20));
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(CreateGameDto dto)
    {
        var username = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
        if (username == null)
            return NotFound(new { message = "Could not get username." });
        var result = await mediator.Send(new CreateGameCommand(username, dto.MaxRating));
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return CreatedAtAction(nameof(GetGame), new { id = result.Value?.Id }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetGame(Guid id)
    {
        var result = await mediator.Send(new GetGameQuery(id));
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return Ok(result.Value);
    }
}