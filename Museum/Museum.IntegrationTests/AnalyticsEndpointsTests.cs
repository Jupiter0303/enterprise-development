using Museum.Application.Contracts.Analytics;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;
using Museum.Shared.Enums;
using System.Net.Http.Json;

namespace Museum.IntegrationTests;

/// <summary>
/// Интеграционные тесты аналитических эндпоинтов
/// </summary>
/// <param name="fixture">Фикстура</param>
public class AnalyticsEndpointsTests(MuseumAppFixture fixture)
{
    /// <summary>
    /// Тест на получение топ-5 выставок по количеству уникальных посетителей
    /// </summary>
    [Fact]
    public async Task GetTopExhibitions_ByUniqueVisitors_ReturnsCorrectRanking()
    {
        // arrange
        int[] expectedExhibitionIds = [1, 5, 2, 7, 6];
        int[] expectedVisitCounts = [7, 7, 5, 4, 3];

        // act
        var result = await fixture.Client.GetFromJsonAsync<List<ExhibitionVisitsDto>>(
            "/api/analytics/top-exhibitions", TestJson.Options, fixture.Ct);

        // assert
        Assert.NotNull(result);
        Assert.Equal(expectedExhibitionIds, result.Select(x => x.Exhibition.Id));
        Assert.Equal(expectedVisitCounts, result.Select(x => x.VisitorCount));
    }

    /// <summary>
    /// Тест на экскурсии с минимальным количеством посетителей,
    /// включая не имеющие билетов
    /// </summary>
    [Fact]
    public async Task GetExcursionsWithMinVisitors_ReturnsExcursionsWithMinimumVisitors()
    {
        // arrange
        int[] expectedExcursionIds = [9];
        int[] expectedVisitorCounts = [0];

        // act
        var result = await fixture.Client.GetFromJsonAsync<List<ExcursionVisitsDto>>(
            "/api/analytics/min-visitors-excursions", TestJson.Options, fixture.Ct);

        // assert
        Assert.NotNull(result);
        Assert.Equal(expectedExcursionIds, result.Select(x => x.Excursion.Id));
        Assert.Equal(expectedVisitorCounts, result.Select(x => x.VisitorCount));
    }

    /// <summary>
    /// Тест на получение всех экскурсий, проходящих
    /// в определенном зале в заданный период времени
    /// </summary>
    [Fact]
    public async Task GetExcursionsInHall_WithinPeriod_ReturnsMatchingExcursions()
    {
        // arrange
        int[] expectedExcursionIds = [1, 4, 5, 6, 7, 9, 10];
        var url = "/api/analytics/excursions-in-hall?hallNumber=101&startDate=2024-01-01&endDate=2024-12-31";

        // act
        var result = await fixture.Client.GetFromJsonAsync<List<ExcursionDto>>(
            url, TestJson.Options, fixture.Ct);

        // assert
        Assert.NotNull(result);
        Assert.Equal(expectedExcursionIds, result.Select(x => x.Id));
    }

    /// <summary>
    /// Тест на получение списка посетителей для выбранной
    /// экскурсии, упорядоченного по полному имени
    /// </summary>
    [Fact]
    public async Task GetVisitors_ForSelectedExcursion_OrderedByFullName()
    {
        // arrange
        int[] expectedVisitorIds = [1, 6];

        // act
        var result = await fixture.Client.GetFromJsonAsync<List<VisitorDto>>(
            "/api/analytics/visitors-by-excursion/1", TestJson.Options, fixture.Ct);

        // assert
        Assert.NotNull(result);
        Assert.Equal(expectedVisitorIds, result.Select(x => x.Id));
    }

    /// <summary>
    /// Тест на получение статистики посещаемости
    /// и выручки выставок по темам и за определенный
    /// период времени
    /// </summary>
    [Fact]
    public async Task GetThemeStatistics_ByThemeAndPeriod_ReturnsCorrectStatistics()
    {
        // arrange
        var url = "/api/analytics/theme-statistics?startDate=2024-01-01&endDate=2024-12-31";
        var expectedStats = new Dictionary<ExhibitionTheme,
           (
               int TotalVisitors,
               decimal TotalPrice,
               int MinVisitorsPerDay,
               int MaxVisitorsPerDay,
               double AverageVisitorsPerDay
           )>
        {
            { ExhibitionTheme.Art, (14, 3433.33m, 1, 2, 1.4) },
            { ExhibitionTheme.History, (6, 1308.33m, 1, 2, 1.2) },
            { ExhibitionTheme.Science, (5, 1033.33m, 1, 2, 1.25) },
            { ExhibitionTheme.Natural, (2, 425.00m, 2, 2, 2.0) }
        };

        // act
        var result = await fixture.Client.GetFromJsonAsync<List<ThemeStatisticsDto>>(
            url, TestJson.Options, fixture.Ct);

        // assert
        Assert.NotNull(result);
        Assert.Equal(expectedStats.Count, result.Count);

        foreach (var expected in expectedStats)
        {
            var actual = result.Single(x => x.Theme == expected.Key);

            Assert.Equal(expected.Value.TotalVisitors, actual.TotalVisitors);
            Assert.Equal(expected.Value.TotalPrice, actual.TotalPrice);
            Assert.Equal(expected.Value.MinVisitorsPerDay, actual.MinVisitorsPerDay);
            Assert.Equal(expected.Value.MaxVisitorsPerDay, actual.MaxVisitorsPerDay);
            Assert.Equal(expected.Value.AverageVisitorsPerDay, actual.AverageVisitorsPerDay, 2);
        }
    }
}