using Museum.Application.Contracts.Analytics;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Mapping;
using Museum.Domain;


namespace Museum.Application.Analytics;

public class AnalyticsService(
    IRepository<Ticket> ticketRepository,
    IRepository<Excursion> excursionRepository,
    IRepository<Visitor> visitorRepository) : IAnalyticsService
{

    public async Task<IList<ExhibitionVisitsDto>> GetTopExhibitions()
    {
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

    public async Task<IList<ExcursionVisitsDto>> GetExcursionsWithMinVisitors()
    {
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

        var minVisitors = query.Min(x => x.VisitCount);

        return query
            .Where(x => x.VisitCount == minVisitors)
            .OrderBy(x => x.Excursion.Id)
            .Select(x => new ExcursionVisitsDto(x.Excursion.ToDto(), x.VisitCount))
            .ToList();
    }

    public async Task<IList<ThemeStatisticsDto>> GetThemeStatistics(DateOnly startDate, DateOnly endDate)
    {
        var tickets = await ticketRepository.ReadAll();

        return tickets
            .Where(t => DateOnly.FromDateTime(t.Excursion.StartsAt) >= startDate
                     && DateOnly.FromDateTime(t.Excursion.StartsAt) <= endDate)
            .GroupBy(t => DateOnly.FromDateTime(t.Excursion.StartsAt))
            .SelectMany(dateGroup => dateGroup
                .DistinctBy(t => new { t.Visitor.Id, t.Excursion })
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
    public async Task<IList<ExcursionDto>> GetExcursionsInHall(int hallNumber, DateOnly startDate, DateOnly endDate)
    {
        var excursions = await excursionRepository.ReadAll();

        return excursions
            .Where(exc => exc.Exhibitions.Any(exh => exh.HallNumber == hallNumber)
                       && DateOnly.FromDateTime(exc.StartsAt) >= startDate
                       && DateOnly.FromDateTime(exc.StartsAt) <= endDate)
            .OrderBy(exc => exc.Id)
            .Select(exc => exc.ToDto())
            .ToList();
    }
}
