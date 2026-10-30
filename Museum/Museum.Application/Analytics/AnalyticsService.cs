using Museum.Application.Contracts.Analytics;
using Museum.Domain;
using Museum.Application.Mapping;


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
}
