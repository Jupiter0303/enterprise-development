using Museum.Application.Contracts.Tickets;
using Museum.Domain;
using Museum.Application.Common;
using Museum.Application.Mapping;


namespace Museum.Application.Services;

public class TicketService(
    IRepository<Ticket> repository,
    IRepository<Excursion> excursionRepository,
    IRepository<Visitor> visitorRepository)
    : CrudServiceBase<Ticket, TicketDto, TicketCreateUpdateDto>(repository), ITicketService
{
    protected override TicketDto ToDto(Ticket entity) => entity.ToDto();
}
