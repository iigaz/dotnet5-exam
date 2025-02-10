using Microsoft.AspNetCore.Mvc;
using XoDotNet.Mediator;

namespace XoDotNet.Main.Controllers;

[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        throw new NotImplementedException();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        throw new NotImplementedException();
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        throw new NotImplementedException();
    }
}