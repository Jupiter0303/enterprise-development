using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Tickets;

namespace Museum.Application.Contracts.Visitors;

public interface IVisitorService: ICrudService<VisitorDto, VisitorCreateUpdateDto>
{
    public Task<IList<TicketDto>?> GetVisitorTickets(int visitorId);
}
