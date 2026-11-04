using Museum.Application.Contracts.Common;
using Museum.Domain;

namespace Museum.Application.Contracts.Exhibitions;

/// <summary>
/// DTO для передачи данных о выставке
/// </summary>
/// <param name="Id">Идентификатор выставки</param>
/// <param name="Name">Название выставки</param>
/// <param name="Theme">Тематика выставки</param>
/// <param name="HallNumber">Номер зала</param>
/// <param name="StartDate">Дата начала</param>
/// <param name="EndDate">Дата окончания</param>
public record ExhibitionDto
(
    int Id,
    string Name,
    ExhibitionTheme Theme,
    int HallNumber,
    DateOnly StartDate,
    DateOnly EndDate
) : IHasId;