using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Contracts.Visitors;

namespace Museum.Api.Controllers;

public class VisitorController(IVisitorService service, ILogger<VisitorController> logger)
    : CrudControllerBase<VisitorDto, VisitorCreateUpdateDto>(service, logger)
{
    [HttpGet("{id}/tickets")]
    public async Task<ActionResult<IList<TicketDto>>> GetTickets(int id)
    {
        logger.LogInformation("HTTP GET запрос: Билеты посетителя {Id}", id);

        var result = await service.GetVisitorTickets(id);
        return result == null ? NotFound(): Ok(result);
    }

}
