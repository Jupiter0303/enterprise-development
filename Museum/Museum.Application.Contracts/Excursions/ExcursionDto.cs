using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Exhibitions;

namespace Museum.Application.Contracts.Excursions;

/// <summary>
/// DTO для передачи данных об экскурсии
/// </summary>
/// <param name="Id">Идентификатор экскурсии</param>
/// <param name="StartsAt">Дата и время начала экскурсии</param>
/// <param name="Duration">Длительность экскурсии</param>
/// <param name="Exhibitions">Список выставок, которые будут включены в экскурсию</param>
public record ExcursionDto
(
    int Id,
    DateTime StartsAt,
    TimeSpan Duration,
    List<ExhibitionDto> Exhibitions
) : IHasId;