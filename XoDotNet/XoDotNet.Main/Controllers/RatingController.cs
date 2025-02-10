using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace XoDotNet.Main.Controllers;

[ApiController]
[Authorize]
public class RatingController : ControllerBase
{
    [HttpGet("rating")]
    public async Task<IActionResult> GetRating([FromQuery] int? limit)
    {
        throw new NotImplementedException();
    }
}