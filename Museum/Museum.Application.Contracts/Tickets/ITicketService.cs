using Museum.Application.Contracts.Common;


namespace Museum.Application.Contracts.Tickets;

public interface ITicketService: ICrudService<TicketDto, TicketCreateUpdateDto>
{
}
