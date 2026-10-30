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
        var excursion = await excursionRepository.Read(dto.ExcursionId);
        if (excursion is null)
            throw new ArgumentException($"Экскурсия с id {dto.ExcursionId} не найдена");
        var visitor = await visitorRepository.Read(dto.VisitorId);
        if (visitor is null)
            throw new ArgumentException($"Посетитель с id {dto.VisitorId} не найден");
        return dto.ToEntity(excursion, visitor);
    }

    protected override async Task ApplyChanges(Ticket entity, TicketCreateUpdateDto dto)
    {
        var excursion = await excursionRepository.Read(dto.ExcursionId);
        if (excursion is null)
            throw new ArgumentException($"Экскурсия с id {dto.ExcursionId} не найдена");
        var visitor = await visitorRepository.Read(dto.VisitorId);
        if (visitor is null)
            throw new ArgumentException($"Посетитель с id {dto.VisitorId} не найден");
        entity.UpdateFrom(dto, excursion, visitor);
    }
}
