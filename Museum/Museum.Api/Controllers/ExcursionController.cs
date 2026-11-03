using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Contracts.Exhibitions;
using Microsoft.AspNetCore.Mvc;

namespace Museum.Api.Controllers;

public class ExcursionController(IExcursionService service, ILogger<ExcursionController> logger)
    : CrudControllerBase<ExcursionDto, ExcursionCreateUpdateDto>(service, logger)
{
    [HttpGet("{id}/tickets")]
    public async Task<ActionResult<IList<TicketDto>?>> GetTickets(int id)
    {
        logger.LogInformation("HTTP GET запрос: Билеты экскурсии {Id}", id);
        var result = await service.GetExcursionTickets(id);
        return result == null ? NotFound() : Ok(result);
    }
    public async Task<ActionResult<IList<ExhibitionDto>?>> GetExhibitions(int id)
    {
        logger.LogInformation("HTTP GET запрос: Выставки экскурсии {Id}", id);
        var result = await service.GetExcursionExhibitions(id);
        return result == null ? NotFound() : Ok(result);
    }

}