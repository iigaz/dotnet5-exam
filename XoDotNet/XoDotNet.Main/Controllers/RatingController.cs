using Microsoft.AspNetCore.Mvc;

namespace XoDotNet.Main.Controllers;

[ApiController]
public class RatingController : ControllerBase
{
    [HttpGet("rating")]
    public async Task<IActionResult> GetRating([FromQuery] int? limit)
    {
        throw new NotImplementedException();
    }
}