using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Contracts.Visitors;

namespace Museum.Api.Controllers;

/// <summary>
/// Контроллер для запрсов, связанных с посетителями
/// </summary>
/// <param name="service">Сервис для работы с посетителями</param>
/// <param name="logger">Логгер</param>
public class VisitorController(IVisitorService service, ILogger<VisitorController> logger)
    : CrudControllerBase<VisitorDto, VisitorCreateUpdateDto>(service, logger)
{
    /// <summary>
    /// Получить список билетов посетителя
    /// </summary>
    /// <param name="id">Идентификатор посетителя</param>
    /// <returns>список билетов</returns>
    /// response code="200">Список билетов найден</response>
    /// response code="404">Посетитель не найден</response>
    [HttpGet("{id}/tickets")]
    public async Task<ActionResult<IList<TicketDto>>> GetTickets(int id)
    {
        logger.LogInformation("HTTP GET запрос: Билеты посетителя {Id}", id);

        var result = await service.GetVisitorTickets(id);
        return result == null ? NotFound(): Ok(result);
    }

}
