using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Exhibitions;
using Museum.Application.Contracts.Tickets;

namespace Museum.Application.Contracts.Excursions;

/// <summary>
/// Интерйес сервиса для работы с экскурсиями
/// </summary>
public interface IExcursionService : ICrudService<ExcursionDto, ExcursionCreateUpdateDto>
{
    /// <summary>
    /// Вернуть список билетов на экскурсию
    /// </summary>
    /// <param name="excursionId">Идентификатор экскурсии</param>
    /// <returns>Список билетов на экскурсию</returns>
    public Task<IList<TicketDto>?> GetExcursionTickets(int excursionId);

    /// <summary>
    /// Вернуть список выставок, которые будут включены в экскурсию
    /// </summary>
    /// <param name="excursionId">Идентификатор экскурсии</param>
    /// <returns>Список выставок, которые будут включены в экскурсию</returns>
    public Task<IList<ExhibitionDto>?> GetExcursionExhibitions(int excursionId);
}