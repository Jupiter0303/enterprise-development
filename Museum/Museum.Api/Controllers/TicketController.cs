using Museum.Application.Contracts.Tickets;
namespace Museum.Api.Controllers;

/// <summary>
/// Контроллер для запросов, связанных с билетами
/// </summary>
/// <param name="service">Сервис для работы с билетами</param>
/// <param name="logger">Логгер</param>
public class TicketController(ITicketService service, ILogger<TicketController> logger)
    : CrudControllerBase<TicketDto, TicketCreateUpdateDto>(service, logger)
{ }