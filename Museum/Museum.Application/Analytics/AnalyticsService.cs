using Microsoft.Extensions.Logging;
using Museum.Application.Contracts.Analytics;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;
using Museum.Application.Mapping;
using Museum.Domain;


namespace Museum.Application.Analytics;

/// <summary>
/// Сервис для работы с аналитически запросами
/// </summary>
/// <param name="ticketRepository">Репозиторий билетов</param>
/// <param name="excursionRepository">Репозиторий экскурсий</param>
/// <param name="logger">Логгер</param>
public class AnalyticsService(
    IRepository<Ticket> ticketRepository,
    IRepository<Excursion> excursionRepository,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    /// <summary>
    /// Получить топ 5 выставок по количеству уникальных посетителей
    /// </summary>
    /// <returns>Список выставок с количеством уникальных посетителей</returns>
    public async Task<IList<ExhibitionVisitsDto>> GetTopExhibitions()
    {
        logger.LogInformation("Аналитика: топ 5 выставок по уникальным посетителям");
        var tickets = await ticketRepository.ReadAll();
        return tickets
            .SelectMany(t => t.Excursion.Exhibitions.Select(ex => new { Exhibition = ex, VisitorId = t.Visitor.Id }))
            .DistinctBy(x => new { x.Exhibition.Id, VisitorId = x.VisitorId })
            .GroupBy(x => x.Exhibition.Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => new ExhibitionVisitsDto(g.First().Exhibition.ToDto(), g.Count()))
            .ToList();
    }

    /// <summary>
    /// Получить экскурсии с минимальным количеством посетителей, включая те,
    /// на которые не взяли ни одного билета
    /// </summary>
    /// <returns>Список экскурсий с минимальным количеством посетителей</returns>
    public async Task<IList<ExcursionVisitsDto>> GetExcursionsWithMinVisitors()
    {
        logger.LogInformation("Аналитика: экскурсии с минимальным количеством уникальных посетителей");
        var tickets = await ticketRepository.ReadAll();
        var excursions = await excursionRepository.ReadAll();

        var query = tickets
            .Select(t => new { Excursion = t.Excursion, VisitorId = t.Visitor.Id })
            .DistinctBy(x => new { ExcursionId = x.Excursion.Id, VisitorId = x.VisitorId })
            .GroupBy(x => x.Excursion.Id)
            .Select(g => new { Excursion = g.First().Excursion, VisitCount = g.Count() })
            .Concat(
            excursions.Select(ex => new { Excursion = ex, VisitCount = 0 })
            )
            .DistinctBy(x => x.Excursion.Id)
            .ToList();

        if (!query.Any())
            return [];
        var minVisitors = query.Min(x => x.VisitCount);

        return query
            .Where(x => x.VisitCount == minVisitors)
            .OrderBy(x => x.Excursion.Id)
            .Select(x => new ExcursionVisitsDto(x.Excursion.ToDto(), x.VisitCount))
            .ToList();
    }

    /// <summary>
    /// Получить статистику посещаемости по темам выставок за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала периода</param>
    /// <param name="endDate">Дата окончания периода</param>
    /// <returns>Список статистик по темам выставок</returns>
    public async Task<IList<ThemeStatisticsDto>> GetThemeStatistics(DateOnly startDate, DateOnly endDate)
    {
        logger.LogInformation("Аналитика: статистика по темам выставок за период {StartDate} - {EndDate}", startDate, endDate);
        var tickets = await ticketRepository.ReadAll();

        return tickets
            .Where(t => DateOnly.FromDateTime(t.Excursion.StartsAt) >= startDate
                     && DateOnly.FromDateTime(t.Excursion.StartsAt) <= endDate)
            .GroupBy(t => DateOnly.FromDateTime(t.Excursion.StartsAt))
            .SelectMany(dateGroup => dateGroup
                .DistinctBy(t => new { VisitorId = t.Visitor.Id, ExcursionId = t.Excursion.Id })
                .GroupBy(t => t.Excursion.Id)
                .SelectMany(excursionGroup =>
                {
                    var excursion = excursionGroup.First().Excursion;
                    var uniqueThemes = excursion.Exhibitions
                    .Select(exh => exh.Theme)
                    .Distinct()
                    .ToList();
                    var totalExcursionPrice = excursionGroup.Sum(ex => ex.Price);
                    var priceShare = totalExcursionPrice / uniqueThemes.Count;

                    return uniqueThemes.Select(theme => new
                    {
                        Theme = theme,
                        VisitorCount = excursionGroup.Count(),
                        Price = priceShare
                    });
                })
                .GroupBy(x => x.Theme)
                .Select(themeGroup => new
                {
                    Theme = themeGroup.Key,
                    DailyVisitors = themeGroup.Sum(x => x.VisitorCount),
                    DailyPrice = themeGroup.Sum(x => x.Price)
                })
            )
            .GroupBy(x => x.Theme)
            .Select(themeGroup => new
            ThemeStatisticsDto(
                themeGroup.Key,
                themeGroup.Sum(x => x.DailyVisitors),
                Math.Round(themeGroup.Sum(x => x.DailyPrice), 2),
                themeGroup.Min(x => x.DailyVisitors),
                themeGroup.Max(x => x.DailyVisitors),
                themeGroup.Average(x => x.DailyVisitors)
            ))
            .OrderBy(t => t.Theme.ToString())
            .ToList();
    }

    /// <summary>
    /// Получить список экскурсий в указанном зале за указанный период
    /// </summary>
    /// <param name="hallNumber">Номер зала</param>
    /// <param name="startDate">Дата начала периода</param>
    /// <param name="endDate">Дата окончания периода</param>
    /// <returns>Список экскурсий в указанном зале за указанный период</returns>
    public async Task<IList<ExcursionDto>> GetExcursionsInHall(int hallNumber, DateOnly startDate, DateOnly endDate)
    {
        logger.LogInformation("Аналитика: экскурсии в зале {HallNumber} за период {StartDate} - {EndDate}",
            hallNumber,
            startDate,
            endDate);
        var excursions = await excursionRepository.ReadAll();

        return excursions
            .Where(exc => exc.Exhibitions.Any(exh => exh.HallNumber == hallNumber)
                       && DateOnly.FromDateTime(exc.StartsAt) >= startDate
                       && DateOnly.FromDateTime(exc.StartsAt) <= endDate)
            .OrderBy(exc => exc.Id)
            .Select(exc => exc.ToDto())
            .ToList();
    }

    /// <summary>
    /// Получить список посетителей, которые посетили указанную экскурсию
    /// </summary>
    /// <param name="excursionId">ID экскурсии</param>
    /// <returns>Список посетителей, которые посетили указанную экскурсию</returns>
    public async Task<IList<VisitorDto>> GetVisitorsByExcursion(int excursionId)
    {
        logger.LogInformation("Аналитика: уникальные посетители экскурсии {ExcursionId}", excursionId);
        var tickets = await ticketRepository.ReadAll();

        return tickets
            .Where(t => t.Excursion.Id == excursionId)
            .DistinctBy(t => t.Visitor.Id)
            .Select(t => t.Visitor.ToDto())
            .OrderBy(v => v.LastName)
            .ThenBy(v => v.FirstName)
            .ThenBy(v => v.Id)
            .ToList();
    }
}