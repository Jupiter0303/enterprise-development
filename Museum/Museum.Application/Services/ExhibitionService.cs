using Museum.Application.Common;
using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Contracts.Excursions;
using Microsoft.Extensions.Logging;
using Museum.Application.Mapping;
using Museum.Domain;

namespace Museum.Application.Services;

/// <summary>
/// Сервис для работы с выставками
/// </summary>
/// <param name="repository">Репозиторий выставок</param>
/// <param name="excursionRepository">Репозиторий экскурсий</param>
/// <param name="logger">Логгер</param>
public class ExhibitionService(IRepository<Exhibition> repository, IRepository<Excursion> excursionRepository, ILogger<ExhibitionService> logger)
    : CrudServiceBase<Exhibition, ExhibitionDto, ExhibitionCreateUpdateDto>(repository, logger), IExhibitionService
{
    /// <summary>
    /// Преобразовать сущность выставки в DTO
    /// </summary>
    /// <param name="entity">Сущность выставки</param>
    /// <returns>DTO выставки</returns>
    protected override ExhibitionDto ToDto(Exhibition entity) => entity.ToDto();

    /// <summary>
    /// Создать сущность выставки на основе DTO
    /// </summary>
    /// <param name="dto">DTO выставки</param>
    /// <returns>Сущность выставки</returns>
    protected override Task<Exhibition> CreateEntity(ExhibitionCreateUpdateDto dto)
        => Task.FromResult(dto.ToEntity());

    /// <summary>
    /// Обновить сущность выставки на основе DTO
    /// </summary>
    /// <param name="entity">Сущность выставки</param>
    /// <param name="dto">DTO выставки</param>
    protected override Task ApplyChanges(Exhibition entity, ExhibitionCreateUpdateDto dto)
    {
        entity.UpdateFrom(dto);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Получить список экскурсий для выставки по её идентификатору
    /// </summary>
    /// <param name="exhibitionId">Идентификатор выставки</param>
    /// <returns>Список DTO экскурсий или null, если выставка не найдена</returns>
    public async Task<IList<ExcursionDto>?> GetExhibitionExcursions(int exhibitionId)
    {
        logger.LogInformation("Получение экскурсий для выставки с ID {ExhibitionId}", exhibitionId);

        var exhibition = await repository.Read(exhibitionId);
        if (exhibition is null) return null;

        var excurions = await excursionRepository.ReadAll();
        return excurions.
                Where(exc => exc.Exhibitions.Any(exh => exh.Id == exhibition.Id))
                .Select(exc => exc.ToDto())
                .ToList();
    }
}