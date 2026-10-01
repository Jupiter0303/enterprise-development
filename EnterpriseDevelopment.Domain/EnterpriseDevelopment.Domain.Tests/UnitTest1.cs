using Xunit;
using EnterpriseDevelopment.Domain;

namespace EnterpriseDevelopment.Domain.Tests;



public class TicketsTests : IClassFixture<SeedFixture>
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

    [Fact]
    public void ExcursionsInHall_WithinPeriod_ReturnsMatchingExcursions()
    {
        var hallNumber = 101;
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 12, 31);

        var hallExcursions = _fixture.Excursions
                                            .Where(exc => exc.Exhibitions.Any(exh => exh.HallNumber == hallNumber) &&
                                                         exc.Date >= startDate && exc.Date <= endDate)
                                            .OrderBy(exc => exc.Id)
                                            .ToList();

        Assert.Equal(7, hallExcursions.Count);
        Assert.Equal([1, 4, 5, 6, 7, 9, 10], hallExcursions.Select(exc => exc.Id));

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
        var sortedThemes = themes.OrderBy(t => t.Theme.ToString()).ToList();

        Assert.Equal(4, sortedThemes.Count);
        var art = sortedThemes.Single(t => t.Theme == ExhibitionTheme.Art);
        Assert.Equal(14, art.TotalVisitors);
        Assert.Equal(3433.33m, art.TotalPrice);
        Assert.Equal(1, art.MinVisitorsPerDay);
        Assert.Equal(2, art.MaxVisitorsPerDay);
        Assert.Equal(1.4, art.AverageVisitorsPerDay, 2);

        var history = sortedThemes.Single(t => t.Theme == ExhibitionTheme.History);
        Assert.Equal(6, history.TotalVisitors);
        Assert.Equal(1308.33m, history.TotalPrice);
        Assert.Equal(1, history.MinVisitorsPerDay);
        Assert.Equal(2, history.MaxVisitorsPerDay);
        Assert.Equal(1.2, history.AverageVisitorsPerDay, 2);

        var science = sortedThemes.Single(t => t.Theme == ExhibitionTheme.Science);
        Assert.Equal(5, science.TotalVisitors);
        Assert.Equal(1033.33m, science.TotalPrice);
        Assert.Equal(1, science.MinVisitorsPerDay);
        Assert.Equal(2, science.MaxVisitorsPerDay);
        Assert.Equal(1.25, science.AverageVisitorsPerDay, 2);

        var natural = sortedThemes.Single(t => t.Theme == ExhibitionTheme.Natural);
        Assert.Equal(2, natural.TotalVisitors);
        Assert.Equal(425.00m, natural.TotalPrice);
        Assert.Equal(2, natural.MinVisitorsPerDay);
        Assert.Equal(2, natural.MaxVisitorsPerDay);
        Assert.Equal(2.0, natural.AverageVisitorsPerDay, 2);

    }

}

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

    [Fact]
    public void GetVisitors_ForSelectedExcursion_OrderedByFullName()
    {
        var excursionId = 1;

        var extraTicket = new List<Ticket>
        {
            new()
            {
                Id = 1001,
                Excursion = _fixture.Excursions[0],
                Visitor = _fixture.Visitors[2],
                Type = TicketType.Adult,
                Price = 500m
            },
        new()   {
            Id = 1002,
            Excursion = _fixture.Excursions[0],
            Visitor = _fixture.Visitors[8],
            Type = TicketType.Discounted,
            Price = 250m
            }
        };
        var allTickets = _fixture.Tickets.Concat(extraTicket).ToList();
        var visitorsExc = allTickets
                                .Where(t => t.Excursion.Id == excursionId)
                                .DistinctBy(t => t.Visitor.Id)
                                .Select(t => new
                                {
                                    t.Visitor.Id,
                                    t.Visitor.FirstName,
                                    t.Visitor.LastName,
                                    t.Visitor.MiddleName,
                                    t.Visitor.Birthday
                                })
                                .OrderBy(v => v.LastName)
                                .ThenBy(v => v.FirstName)
                                .ThenBy(v => v.Id)
                                .ToList();


        Assert.Equal(4, visitorsExc.Count);
        Assert.Equal(["Иванов", "Новиков", "Петров", "Попова"], visitorsExc.Select(v => v.LastName));
        Assert.Equal([3, 9, 1, 6], visitorsExc.Select(v => v.Id));


    }
}
