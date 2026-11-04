using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Contracts.Exhibitions;
using Microsoft.AspNetCore.Mvc;

namespace Museum.Api.Controllers;

/// <summary>
/// Контроллер для запросов, связанных с экскурсиями
/// </summary>
/// <param name="service">Сервис для работы с экскурсиями</param>
/// <param name="logger">Логгер</param>
public class ExcursionController(IExcursionService service, ILogger<ExcursionController> logger)
    : CrudControllerBase<ExcursionDto, ExcursionCreateUpdateDto>(service, logger)
{
    /// <summary>
    /// Получить список билетов экскурсии
    /// </summary>
    /// <param name="id">Идентификатор экскурсии</param>
    /// <response code="200">Список билетов экскурсии</response>
    /// <response code="404">Экскурсия не найдена</response>
    [HttpGet("{id}/tickets")]
    public async Task<ActionResult<IList<TicketDto>>> GetTickets(int id)
    {
        logger.LogInformation("HTTP GET запрос: Билеты экскурсии {Id}", id);
        var result = await service.GetExcursionTickets(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Получить список выставок экскурсии
    /// </summary>
    /// <param name="id">Идентификатор экскурсии</param>
    /// <response code="200">Список выставок экскурсии</response>
    /// <response code="404">Экскурсия не найдена</response>
    [HttpGet("{id}/exhibitions")]
    public async Task<ActionResult<IList<ExhibitionDto>>> GetExhibitions(int id)
    {
        logger.LogInformation("HTTP GET запрос: Выставки экскурсии {Id}", id);
        var result = await service.GetExcursionExhibitions(id);
        return result == null ? NotFound() : Ok(result);
    }
}