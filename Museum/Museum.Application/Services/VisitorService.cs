using Microsoft.Extensions.Logging;
using Museum.Application.Common;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Contracts.Visitors;
using Museum.Application.Mapping;
using Museum.Domain;

namespace Museum.Application.Services;

/// <summary>
/// Сервис для раюботы с посетителями
/// </summary>
/// <param name="repository">Репозиторий посетителей</param>
/// <param name="ticketRepository">Репозиторий билетов</param>
/// <param name="logger">Логгер</param>
public class VisitorService(
    IRepository<Visitor> repository,
    IRepository<Ticket> ticketRepository,
    ILogger<VisitorService> logger)
    : CrudServiceBase<Visitor, VisitorDto, VisitorCreateUpdateDto>(repository, logger), IVisitorService
{
    /// <summary>
    /// Преобразовать сущность посетителя в DTO
    /// </summary>
    /// <param name="entity">Сущность посетителя</param>
    /// <returns>DTO посетителя</returns>
    protected override VisitorDto ToDto(Visitor entity) => entity.ToDto();

    /// <summary>
    /// Создать сущность посетителя на основе DTO
    /// </summary>
    /// <param name="dto">DTO посетителя</param>
    /// <returns>Сущность посетителя</returns>
    protected override Task<Visitor> CreateEntity(VisitorCreateUpdateDto dto) =>
        Task.FromResult(dto.ToEntity());

    /// <summary>
    /// Обновить сущность посетителя на основе DTO
    /// </summary>
    /// <param name="entity">Сущность посетителя</param>
    /// <param name="dto">DTO посетителя</param>
    protected override Task ApplyChanges(Visitor entity, VisitorCreateUpdateDto dto)
    {
        entity.UpdateFrom(dto);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Получить список билетов для посетителя по его идентификатору
    /// </summary>
    /// <param name="visitorId">Идентификатор посетителя</param>
    /// <returns>Список билетов посетителя</returns>
    public async Task<IList<TicketDto>?> GetVisitorTickets(int visitorId)
    {
        logger.LogInformation("Получение билетов для посетителя с ID {VisitorId}", visitorId);

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