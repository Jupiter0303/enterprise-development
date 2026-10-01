namespace EnterpriseDevelopment.Domain.Tests;

/// <summary>
/// Класс тестов для проверки функциональности музея
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными</param>
public class MuseumTests(SeedFixture fixture) : IClassFixture<SeedFixture>
{
    /// <summary>
    /// Тест для проверки наличия хотя бы десяти сущностей
    /// каждого типа в инициализированных данных
    /// </summary>
    [Fact]
    public void GetSeedData_WhenInitialized_ContainsAtLeastTenEntitiesOfEachType()
    {
        // arrange & act & assert
        Assert.True(fixture.Visitors.Count >= 10);
        Assert.True(fixture.Exhibitions.Count >= 10);
        Assert.True(fixture.Excursions.Count >= 10);
        Assert.True(fixture.Tickets.Count >= 10);
    }

    /// <summary>
    /// Тест на получение топ-5 выставок по количеству уникальных посетителей
    /// </summary>
    [Fact]
    public void GetTopFiveExhibitions_ByUniqueVisitors_ReturnsCorrectRanking()
    {
        //arrange
        var tickets = fixture.Tickets;
        int[] expectedExhibitionIds = [1, 5, 2, 7, 6];
        int[] expectedVisitCounts = [7, 7, 5, 4, 3];

        //act
        var topFiveExhibitions = tickets
            .SelectMany(t => t.Excursion.Exhibitions.Select(ex => new { Exhibition = ex, VisitorId = t.Visitor.Id }))
            .DistinctBy(x => new { ExhibitionId = x.Exhibition.Id, VisitorId = x.VisitorId })
            .GroupBy(x => x.Exhibition.Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => new { Exhibition = g.First().Exhibition, VisitCount = g.Count() })
            .ToList();

        // assert
        Assert.Equal(5, topFiveExhibitions.Count);
        Assert.Equal(expectedVisitCounts, topFiveExhibitions.Select(pair => pair.VisitCount));
        Assert.Equal(expectedExhibitionIds, topFiveExhibitions.Select(pair => pair.Exhibition.Id));
    }

    /// <summary>
    /// Тест на экскурсии с минимальным количеством посетителей, включая не имеющие билетов
    /// </summary>
    [Fact]
    public void GetTopThreeExcursions_ByUniqueVisitors_ReturnsCorrectRanking()
    {
        // arrange
        var tickets = fixture.Tickets;
        int[] expectedExcursionIds = [9, 3, 4];
        int[] expectedVisitorCounts = [0, 1, 1];

        // act
        var excursions = tickets
                    .Select(t => new { Excursion = t.Excursion, VisitorId = t.Visitor.Id })
                    .DistinctBy(x => new { ExcursionId = x.Excursion.Id, VisitorId = x.VisitorId })
                    .GroupBy(x => x.Excursion.Id)
                    .Select(g => new { Excursion = g.First().Excursion, VisitCount = g.Count() })
                    .Concat(
                        fixture.Excursions.Select(ex => new { Excursion = ex, VisitCount = 0 })
                    )
                    .DistinctBy(pair => pair.Excursion.Id)
                    .OrderBy(pair => pair.VisitCount)
                    .ThenBy(pair => pair.Excursion.Id)
                    .Take(3)
                    .ToList();

        // assert
        Assert.Equal(3, excursions.Count);
        Assert.Equal(expectedExcursionIds, excursions.Select(x => x.Excursion.Id));
        Assert.Equal(expectedVisitorCounts, excursions.Select(x => x.VisitCount));
    }

    /// <summary>
    /// Тест на получение всех экскурсий, проходящих в определенном зале в заданный период времени
    /// </summary>
    [Fact]
    public void GetExcursionsInHall_WithinPeriod_ReturnsMatchingExcursions()
    {
        //arrange
        var hallNumber = 101;
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 12, 31);
        int[] expectedExcursionIds = [1, 4, 5, 6, 7, 9, 10];

        //act
        var hallExcursions = fixture.Excursions
            .Where(exc => exc.Exhibitions.Any(exh => exh.HallNumber == hallNumber)
                           && exc.Date >= startDate
                           && exc.Date <= endDate)
            .OrderBy(exc => exc.Id)
            .ToList();

        //assert
        Assert.Equal(7, hallExcursions.Count);
        Assert.Equal(expectedExcursionIds, hallExcursions.Select(exc => exc.Id));
    }

    /// <summary>
    /// Тест на получение статистики посещаемости и выручки выставок по темам и за определенный период времени
    /// </summary>
    [Fact]
    public void GetExhibitionAttendanceSummary_ByThemeAndPeriod_ReturnsCorrectStatistics()
    {
        //arrange
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 12, 31);
        var tickets = fixture.Tickets;

        //act
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
                                var priceShare = totalExcursionPrice / uniqueThemes.Count;// цена за тематику = 
                                                                                          // сумма билетов на текущую экскурсию,
                                                                                          // поделенная на количество уникальных тематик
                                                                                          // выставок в этой экскурсии
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
                        .OrderBy(t => t.Theme.ToString())
                        .ToList();

        //assert
        Assert.Equal(4, themes.Count);

        var art = themes.Single(t => t.Theme == ExhibitionTheme.Art);
        Assert.Equal(14, art.TotalVisitors);
        Assert.Equal(3433.33m, art.TotalPrice);
        Assert.Equal(1, art.MinVisitorsPerDay);
        Assert.Equal(2, art.MaxVisitorsPerDay);
        Assert.Equal(1.4, art.AverageVisitorsPerDay, 2);

        var history = themes.Single(t => t.Theme == ExhibitionTheme.History);
        Assert.Equal(6, history.TotalVisitors);
        Assert.Equal(1308.33m, history.TotalPrice);
        Assert.Equal(1, history.MinVisitorsPerDay);
        Assert.Equal(2, history.MaxVisitorsPerDay);
        Assert.Equal(1.2, history.AverageVisitorsPerDay, 2);

        var science = themes.Single(t => t.Theme == ExhibitionTheme.Science);
        Assert.Equal(5, science.TotalVisitors);
        Assert.Equal(1033.33m, science.TotalPrice);
        Assert.Equal(1, science.MinVisitorsPerDay);
        Assert.Equal(2, science.MaxVisitorsPerDay);
        Assert.Equal(1.25, science.AverageVisitorsPerDay, 2);

        var natural = themes.Single(t => t.Theme == ExhibitionTheme.Natural);
        Assert.Equal(2, natural.TotalVisitors);
        Assert.Equal(425.00m, natural.TotalPrice);
        Assert.Equal(2, natural.MinVisitorsPerDay);
        Assert.Equal(2, natural.MaxVisitorsPerDay);
        Assert.Equal(2.0, natural.AverageVisitorsPerDay, 2);
    }

    /// <summary>
    /// Тест на получение списка посетителей для выбранной экскурсии, упорядоченного по полному имени
    /// </summary>
    [Fact]
    public void GetVisitors_ForSelectedExcursion_OrderedByFullName()
    {
        //arrange
        var excursionId = 1;
        string[] expectedLastNames = ["Петров", "Попова"];
        int[] expectedVisitorIds = [1, 6];
        var tickets = fixture.Tickets;

        // act
        var visitorsExc = tickets
                                .Where(t => t.Excursion.Id == excursionId)
                                .DistinctBy(t => t.Visitor.Id)
                                .Select(t => t.Visitor)
                                .OrderBy(v => v.LastName)
                                .ThenBy(v => v.FirstName)
                                .ThenBy(v => v.Id)
                                .ToList();

        //assert
        Assert.Equal(2, visitorsExc.Count);
        Assert.Equal(expectedLastNames, visitorsExc.Select(v => v.LastName));
        Assert.Equal(expectedVisitorIds, visitorsExc.Select(v => v.Id));
    }
}
