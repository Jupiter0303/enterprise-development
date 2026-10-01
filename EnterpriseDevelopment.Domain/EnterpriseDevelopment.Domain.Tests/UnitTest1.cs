using EnterpriseDevelopment.Domain;

namespace EnterpriseDevelopment.Domain.Tests;


public class VisitorTests : IClassFixture<SeedFixture>
{
    private readonly SeedFixture _fixture;

    public VisitorTests(SeedFixture fixture) => _fixture = fixture;

    [Fact]
    public void GetVisitors_ReturnsAtLeastTenVisitors()
    {
        //получение данных о посетителях из метода GetVisitors
        var visitors = _fixture.Visitors;
        //действие для проверки , что количество посетителей больше или равно 10
        var count = visitors.Count;
        //проверка услови я
        Assert.True(count >= 10);
    }
}

public class TicketsTests: IClassFixture<SeedFixture>
{
    private readonly SeedFixture _fixture;
    public TicketsTests(SeedFixture fixture) => _fixture = fixture;
    [Fact]
    public void GetTickets_ReturnsAtLeastTenTickets()
    {
        var tickets = _fixture.Tickets;
        var count = tickets.Count;
        Assert.True(count >= 10);
    }
}

public class ExhibitionsTests : IClassFixture<SeedFixture>

{
    private readonly SeedFixture _fixture;
    public ExhibitionsTests(SeedFixture fixture) => _fixture = fixture;

    [Fact]
    public void TopFiveExhibitions_ByUniqueVisitors_ReturnsCorrectRanking()
    {
        var tickets = _fixture.Tickets;

        var topFiveExhibitions = tickets
            .SelectMany(t => t.Excursion.Exhibitions.Select(ex => new { Exhibition = ex, VisitorId = t.Visitor.Id }))
            .DistinctBy(x => new { ExhibitionId = x.Exhibition.Id, VisitorId = x.VisitorId })
            .GroupBy(x => x.Exhibition.Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => new { Exhibition = g.First().Exhibition, VisitCount = g.Count() })
            .ToList();
        Assert.Equal(5, topFiveExhibitions.Count);
        Assert.Equal([7, 7, 5, 4, 3], topFiveExhibitions.Select(pair => pair.VisitCount));
        Assert.Equal([1, 5, 2, 7, 6], topFiveExhibitions.Select(pair => pair.Exhibition.Id));
    }
}

public class ExcursionTests : IClassFixture<SeedFixture>
{
    private readonly SeedFixture _fixture;
    public ExcursionTests(SeedFixture fixture) => _fixture = fixture;

    [Fact]
    public void TopThreeExcursions_ByUniqueVisitors_ReturnsCorrectRanking()
    {
        var tickets = _fixture.Tickets;
        var excursions = tickets
                    .Select(t => new { Excursion = t.Excursion, VisitorId = t.Visitor.Id })
                    .DistinctBy(x => new { ExcursionId = x.Excursion.Id, VisitorId = x.VisitorId })
                    .GroupBy(x => x.Excursion.Id)
                    .Select(g => new { Excursion = g.First().Excursion, VisitCount = g.Count() })
                    .Concat(
                                _fixture.Excursions
                            .Select(ex => new { Excursion = ex, VisitCount = 0 })
                            )
                    .DistinctBy(pair => pair.Excursion.Id)
                    .OrderBy(pair => pair.VisitCount)
                    .ThenBy(pair => pair.Excursion.Id)
                    .Take(3)
                    .ToList();
        Assert.Equal(3, excursions.Count);
        Assert.Equal([9, 3, 4], excursions.Select(x => x.Excursion.Id));
        Assert.Equal([0, 1, 1], excursions.Select(x => x.VisitCount));


    }

}

public class ThemesTests : IClassFixture<SeedFixture>
{
    private readonly SeedFixture _fixture;
    public ThemesTests(SeedFixture fixture) => _fixture = fixture;
    [Fact]
    public void ExhibitionAttendanceSummary_ByThemeAndPeriod_ReturnsCorrectStatistics()
    {
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 12, 31);
        var tickets = _fixture.Tickets;
        var themes = tickets
            .Where(t => t.Excursion.Date >= startDate && t.Excursion.Date <= endDate)
            .GroupBy(t => t.Excursion.Date)
            .SelectMany(dateGroup => dateGroup
                            .DistinctBy(t => new { t.Visitor, t.Excursion })
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
                    {
                        Theme = themeGroup.Key,
                        TotalVisitors = themeGroup.Sum(x => x.DailyVisitors),
                        TotalPrice = Math.Round(themeGroup.Sum(x => x.DailyPrice), 2),
                        MinVisitorsPerDay = themeGroup.Min(x => x.DailyVisitors),
                        MaxVisitorsPerDay = themeGroup.Max(x => x.DailyVisitors),
                        AverageVisitorsPerDay = themeGroup.Average(x => x.DailyVisitors)
                    })
                    .ToList();

        
    }
}

  
                                                                                                                                        
                                 