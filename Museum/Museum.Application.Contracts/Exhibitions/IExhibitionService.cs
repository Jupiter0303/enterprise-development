using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Excursions;

namespace Museum.Application.Contracts.Exhibitions;

/// <summary>
/// Интерфейс сервиса для работы с выставками
/// </summary>
public interface IExhibitionService : ICrudService<ExhibitionDto, ExhibitionCreateUpdateDto>
{
    /// <summary>
    /// Вернуть список экскурсий, которые включают данную выставку
    /// </summary>
    /// <param name="exhibitionId">Идентификатор выставки</param>
    /// <returns>Список экскурсий</returns>
    public Task<IList<ExcursionDto>?> GetExhibitionExcursions(int exhibitionId);
}