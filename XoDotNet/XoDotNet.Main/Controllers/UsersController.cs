using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XoDotNet.Features.Users.Commands.CreateUser;
using XoDotNet.Features.Users.Queries.GetRatings;
using XoDotNet.Features.Users.Queries.GetUser;
using XoDotNet.Features.Users.Queries.VerifyUser;
using XoDotNet.Main.Abstractions;
using XoDotNet.Main.Dto;
using XoDotNet.Main.Extensions;
using XoDotNet.Mediator;

namespace XoDotNet.Main.Controllers;

[ApiController]
[Authorize]
public class UsersController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var result = await mediator.Send(new CreateUserCommand(dto.Username, dto.Password));
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto, [FromServices] IJwtIssuerService jwtIssuerService)
    {
        var result = await mediator.Send(new VerifyUserQuery(dto.Username, dto.Password));
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return Ok(new
        {
            access_token = await jwtIssuerService.GetTokenForUser(result.Value!.User)
        });
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var username = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
        if (username == null)
            return NotFound(new { message = "Could not get username." });
        var result = await mediator.Send(new GetUserQuery(username));
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return Ok(result.Value);
    }

    [HttpGet("rating")]
    public async Task<IActionResult> GetRating([FromQuery] int? limit)
    {
        var result = await mediator.Send(new GetRatingsQuery(limit ?? 10));
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return Ok(result.Value);
    }
}