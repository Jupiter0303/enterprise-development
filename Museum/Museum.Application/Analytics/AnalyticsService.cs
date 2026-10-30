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

}
