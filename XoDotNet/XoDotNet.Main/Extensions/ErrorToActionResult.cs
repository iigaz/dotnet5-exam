using Microsoft.AspNetCore.Mvc;
using XoDotNet.Infrastructure.Errors;

namespace XoDotNet.Main.Extensions;

public static class ErrorToActionResult
{
    public static IActionResult ToActionResult(this Error? error)
    {
        if (error is NotFoundError notFoundError)
            return new NotFoundObjectResult(new { message = notFoundError.Message });
        return new BadRequestObjectResult(new { message = error?.Message });
    }
}