using Museum.Application.Contracts.Tickets;
namespace Museum.Api.Controllers;

public class TicketController(ITicketService service, ILogger<TicketController> logger)
    : CrudControllerBase<TicketDto, TicketCreateUpdateDto>(service, logger)
{ }