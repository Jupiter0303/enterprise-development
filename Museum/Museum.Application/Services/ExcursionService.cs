using Museum.Application.Common;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Contracts.Tickets;
using Museum.Application.Mapping;
using Museum.Domain;
using Microsoft.Extensions.Logging;

namespace Museum.Application.Services;

/// <summary>
/// Сервис для работы с экскурсиями
/// </summary>
/// <param name="repository">Репозиторий экскурсий</param>
/// <param name="exhibitionRepository">Репозиторий выставок</param>
/// <param name="ticketRepository">Репозиторий билетов</param>
/// <param name="logger">Логгер</param>
public class ExcursionService(
    IRepository<Excursion> repository,
    IRepository<Exhibition> exhibitionRepository,
    IRepository<Ticket> ticketRepository,
    ILogger<ExcursionService> logger)
    : CrudServiceBase<Excursion, ExcursionDto, ExcursionCreateUpdateDto>(repository, logger), IExcursionService
{
    /// <summary>
    /// Преобразовать сущность экскурсии в DTO
    /// </summary>
    /// <param name="entity">Сущность экскурсии</param>
    /// <returns>DTO экскурсии</returns>
    protected override ExcursionDto ToDto(Excursion entity) => entity.ToDto();

    /// <summary>
    /// Создать сущность экскурсии на основе DTO
    /// </summary>
    /// <param name="dto">DTO экскурсии</param>
    /// <returns>Сущность экскурсии</returns>
    protected override async Task<Excursion> CreateEntity(ExcursionCreateUpdateDto dto)
    {
        var exhibitions = await LoadExhibitions(dto);
        return dto.ToEntity(exhibitions);
    }

    /// <summary>
    /// Обновить сущность экскурсии на основе DTO
    /// </summary>
    /// <param name="entity">Сущность экскурсии</param>
    /// <param name="dto">DTO экскурсии</param>
    protected override async Task ApplyChanges(Excursion entity, ExcursionCreateUpdateDto dto)
    {
        var exhibitions = await LoadExhibitions(dto);
        entity.UpdateFrom(dto, exhibitions);
    }

    /// <summary>
    /// Найти и загрузить связанные выставки для экскурсии на основе DTO
    /// </summary>
    /// <param name="dto">DTO экскурсии</param>
    /// <returns>Список выставок</returns>
    /// <exception cref="RelatedEntityNotFoundException">Выбрасывается, если одна из выставок не найдена</exception>
    private async Task<List<Exhibition>> LoadExhibitions(ExcursionCreateUpdateDto dto)
    {
        logger.LogInformation("Поиск выставок для экскурсии по {Count} идентификаторам", dto.ExhibitionIds.Count);

        var exhibitions = new List<Exhibition>();

        foreach (var id in dto.ExhibitionIds.Distinct())
        {
            var exhibition = await exhibitionRepository.Read(id)
                ?? throw new RelatedEntityNotFoundException(nameof(Exhibition), id);

            exhibitions.Add(exhibition);
        }

        return exhibitions;
    }

    /// <summary>
    /// Взять список билетов для экскурсии по её идентификатору
    /// </summary>
    /// <param name="excursionId">Идентификатор экскурсии</param>
    /// <returns>Список билетов</returns>
    public async Task<IList<TicketDto>?> GetExcursionTickets(int excursionId)
    {
        logger.LogInformation("Получение билетов для экскурсии с ID {ExcursionId}", excursionId);

        var excursion = await repository.Read(excursionId);
        if (excursion is null)
            return null;

        var tickets = await ticketRepository.ReadAll();
        return tickets
            .Where(t => t.Excursion.Id == excursionId)
            .Select(t => t.ToDto())
            .ToList();
    }

    /// <summary>
    /// Получить список выставок для экскурсии по её идентификатору
    /// </summary>
    /// <param name="excursionId">Идентификатор экскурсии</param>
    /// <returns>Список выставок</returns>
    public async Task<IList<ExhibitionDto>?> GetExcursionExhibitions(int excursionId)
    {
        logger.LogInformation("Получение выставок для экскурсии с ID {ExcursionId}", excursionId);

        var excursion = await repository.Read(excursionId);
        if (excursion is null)
            return null;

        return excursion.Exhibitions.Select(e => e.ToDto()).ToList();
    }
}