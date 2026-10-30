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

    protected override async Task<Ticket> CreateEntity(TicketCreateUpdateDto dto)
    {
        var (excursion, visitor) = await ResolveReferences(dto);
        return dto.ToEntity(excursion, visitor);
    }

    protected override async Task ApplyChanges(Ticket entity, TicketCreateUpdateDto dto)
    {
        var (excursion, visitor) = await ResolveReferences(dto);
        entity.UpdateFrom(dto, excursion, visitor);
    }
    
    private async Task<(Excursion Excursion, Visitor Visitor)> ResolveReferences(TicketCreateUpdateDto dto)
    {
        var excursion = await excursionRepository.Read(dto.ExcursionId)
            ?? throw new RelatedEntityNotFoundException(nameof(Excursion), dto.ExcursionId);

        var visitor = await visitorRepository.Read(dto.VisitorId)
            ?? throw new RelatedEntityNotFoundException(nameof(Visitor), dto.VisitorId);

        return (excursion, visitor);
    }
}
