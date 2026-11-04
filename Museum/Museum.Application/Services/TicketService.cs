using Museum.Application.Contracts.Tickets;
using Museum.Domain;
using Microsoft.Extensions.Logging;
using Museum.Application.Common;
using Museum.Application.Mapping;


namespace Museum.Application.Services;

/// <summary>
/// Сервис для работы с билетами
/// </summary>
/// <param name="repository">Репозиторий билетов</param>
/// <param name="excursionRepository">Репозиторий экскурсий</param>
/// <param name="visitorRepository">Репозиторий посетителей</param>
/// <param name="logger">Логгер</param>
public class TicketService(
    IRepository<Ticket> repository,
    IRepository<Excursion> excursionRepository,
    IRepository<Visitor> visitorRepository,
    ILogger<TicketService> logger)
    : CrudServiceBase<Ticket, TicketDto, TicketCreateUpdateDto>(repository, logger), ITicketService
{
    /// <summary>
    /// Преобразовать сущность билета в DTO
    /// </summary>
    /// <param name="entity">Сущность билета</param>
    /// <returns>DTO билета</returns>
    protected override TicketDto ToDto(Ticket entity) => entity.ToDto();

    /// <summary>
    /// Создать сущность билета на основе DTO
    /// </summary>
    /// <param name="dto">DTO билета</param>
    /// <returns>Сущность билета</returns>
    protected override async Task<Ticket> CreateEntity(TicketCreateUpdateDto dto)
    {
        var (excursion, visitor) = await ResolveReferences(dto);
        return dto.ToEntity(excursion, visitor);
    }

    /// <summary>
    /// Обновить сущность билета на основе DTO
    /// </summary>
    /// <param name="entity">Сущность билета</param>
    /// <param name="dto">DTO билета</param>
    protected override async Task ApplyChanges(Ticket entity, TicketCreateUpdateDto dto)
    {
        var (excursion, visitor) = await ResolveReferences(dto);
        entity.UpdateFrom(dto, excursion, visitor);
    }

    /// <summary>
    /// Найти экскурсию и посетителя по идентификаторам из DTO билета
    /// </summary>
    /// <param name="dto">DTO билета</param>
    /// <returns>Кортеж из экскурсии и посетителя</returns>
    /// <exception cref="RelatedEntityNotFoundException">Выбрасывается в случае отсуствия сущности</exception>
    private async Task<(Excursion Excursion, Visitor Visitor)> ResolveReferences(TicketCreateUpdateDto dto)
    {
        logger.LogInformation("Поиск связанных сущностей для билета: Экскурсия {ExcursionId}, Посетитель {VisitorId}",
            dto.ExcursionId, dto.VisitorId);

        var excursion = await excursionRepository.Read(dto.ExcursionId)
            ?? throw new RelatedEntityNotFoundException(nameof(Excursion), dto.ExcursionId);

        var visitor = await visitorRepository.Read(dto.VisitorId)
            ?? throw new RelatedEntityNotFoundException(nameof(Visitor), dto.VisitorId);

        return (excursion, visitor);
    }
}