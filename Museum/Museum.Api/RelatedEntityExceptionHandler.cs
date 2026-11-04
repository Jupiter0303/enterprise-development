using Microsoft.AspNetCore.Diagnostics;
using Museum.Application.Common;

namespace Museum.Api;

/// <summary>
/// Обработчик исключений для связанных сущностей
/// </summary>
/// <param name="logger">Логгер</param>
public class RelatedEntityExceptionHandler(ILogger<RelatedEntityExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Обрабатывает исключение, если оно является RelatedEntityNotFoundException, и возвращает 400 Bad Request клиенту
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not RelatedEntityNotFoundException)
            return false;

        logger.LogWarning("Ошибка связанной сущности: {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Связанная сущность не найдена",
            Detail = exception.Message
        }, cancellationToken);

        return true;
    }
}