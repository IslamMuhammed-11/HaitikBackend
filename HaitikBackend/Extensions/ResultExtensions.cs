using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace HaitikBackend.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }

        return MapErrorToActionResult(result.Error);
    }

    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        return MapErrorToActionResult(result.Error);
    }

    private static IActionResult MapErrorToActionResult(Error? error)
    {
        if (error is null)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        var statusCode = error.ErrorTypes switch
        {
            enErrorTypes.Validation => StatusCodes.Status400BadRequest,
            enErrorTypes.Unauthorized => StatusCodes.Status401Unauthorized,
            enErrorTypes.InvalidCreds => StatusCodes.Status401Unauthorized,
            enErrorTypes.ForBidden => StatusCodes.Status403Forbidden,
            enErrorTypes.NotFound => StatusCodes.Status404NotFound,
            enErrorTypes.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var errorResponse = new
        {
            code = error.Code,
            message = error.Message,
            errorType = error.ErrorTypes.ToString()
        };

        return new ObjectResult(errorResponse)
        {
            StatusCode = statusCode
        };
    }
}
