using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;


namespace Museum.Application.Contracts.Analytics;

/// <summary>
/// Интерфейс сервиса для аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить топ 5 выставок по количеству уникальных посетителей
    /// </summary>
    /// <returns>Список выставок с количеством уникальных посетителей</returns>
    public Task<IList<ExhibitionVisitsDto>> GetTopExhibitions();

    /// <summary>
    /// Получить экскурсии с минимальным количеством посетителей, включая те,
    /// на которые не взяли ни одного билета
    /// </summary>
    /// <returns>Список экскурсий с минимальным количеством посетителей</returns>
    public Task<IList<ExcursionVisitsDto>> GetExcursionsWithMinVisitors();

    /// <summary>
    /// Получить статистику посещаемости по темам выставок за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала периода</param>
    /// <param name="endDate">Дата окончания периода</param>
    /// <returns>Список статистик по темам выставок</returns>
    public Task<IList<ThemeStatisticsDto>> GetThemeStatistics(DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Получить список экскурсий в указанном зале за указанный период
    /// </summary>
    /// <param name="hallNumber">Номер зала</param>
    /// <param name="startDate">Дата начала периода</param>
    /// <param name="endDate">Дата окончания периода</param>
    /// <returns>Список экскурсий в указанном зале за указанный период</returns>
    public Task<IList<ExcursionDto>> GetExcursionsInHall(int hallNumber, DateOnly startDate, DateOnly endDate);
    
    /// <summary>
    /// Получить список посетителей, которые посетили указанную экскурсию
    /// </summary>
    /// <param name="excursionId">ID экскурсии</param>
    /// <returns>Список посетителей, которые посетили указанную экскурсию</returns>
    public Task<IList<VisitorDto>> GetVisitorsByExcursion(int excursionId);
}