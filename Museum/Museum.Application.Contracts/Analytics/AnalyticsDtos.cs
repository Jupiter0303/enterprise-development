using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Exhibitions;
using Museum.Shared.Enums;

namespace Museum.Application.Contracts.Analytics;

/// <summary>
/// DTO для результата топ 5 выставок по количеству уникальных посетителей
/// </summary>
/// <param name="Exhibition">DTO выставки</param>
/// <param name="VisitorCount">Количество уникальных посетителей</param>
public record ExhibitionVisitsDto (
    ExhibitionDto Exhibition,
    int VisitorCount);

/// <summary>
/// DTO для результата списка экскурсий с минимальным количеством посетителей
/// </summary>
/// <param name="Excursion">DTO экскурсии</param>
/// <param name="VisitorCount">Количество посетителей</param>
public record ExcursionVisitsDto(
    ExcursionDto Excursion,
    int VisitorCount);

/// <summary>
///  DTO для результата статистики по темам выставок
/// </summary>
/// <param name="Theme">Тема выставки</param>
/// <param name="TotalVisitors">Общее количество посетителей</param>
/// <param name="TotalPrice">Общая стоимость посещений</param>
/// <param name="MinVisitorsPerDay">Минимальное количество посетителей за день</param>
/// <param name="MaxVisitorsPerDay">Максимальное количество посетителей за день</param>
/// <param name="AverageVisitorsPerDay">Среднее количество посетителей за день</param>
public record ThemeStatisticsDto(
    ExhibitionTheme Theme,
    int TotalVisitors,
    decimal TotalPrice,
    int MinVisitorsPerDay,
    int MaxVisitorsPerDay,
    double AverageVisitorsPerDay);