using Microsoft.Extensions.Logging;
using Museum.Application.Common;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Contracts.Visitors;
using Museum.Application.Mapping;
using Museum.Domain;

namespace Museum.Application.Services;

public class VisitorService(
    IRepository<Visitor> repository,
    IRepository<Ticket> ticketRepository,
    ILogger<VisitorService> logger)
    : CrudServiceBase<Visitor, VisitorDto, VisitorCreateUpdateDto>(repository, logger), IVisitorService
{
    protected override VisitorDto ToDto(Visitor entity) => entity.ToDto();

    protected override Task<Visitor> CreateEntity(VisitorCreateUpdateDto dto) =>
        Task.FromResult(dto.ToEntity());

    protected override Task ApplyChanges(Visitor entity, VisitorCreateUpdateDto dto)
    {
        entity.UpdateFrom(dto);
        return Task.CompletedTask;
    }
    public async Task<IList<TicketDto>?> GetVisitorTickets(int visitorId)
    {
        var visitor = await repository.Read(visitorId);
        if (visitor is null)
            return null;

        var tickets = await ticketRepository.ReadAll();
        return tickets
            .Where(t => t.Visitor.Id == visitorId)
            .Select(t => t.ToDto())
            .ToList();
    }
}