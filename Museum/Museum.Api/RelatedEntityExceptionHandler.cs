using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Museum.Application.Common;

namespace Museum.Api;


public class RelatedEntityExceptionHandler(ILogger<RelatedEntityExceptionHandler> logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,Exception exception,CancellationToken cancellationToken)
    {
        if (exception is not RelatedEntityNotFoundException)
            return false;
        
        logger.LogWarning("Ошибка связанной сущности: {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync( new
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Связанная сущность не найдена",
            Detail = exception.Message
        }, cancellationToken);

        return true;
    }
}
