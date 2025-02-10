using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace XoDotNet.Main.Controllers;

[ApiController]
[Route("/games")]
[Authorize]
public class GamesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetGames([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        throw new NotImplementedException();
    }
}