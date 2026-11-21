using Museum.Application.Contracts.Analytics;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;
using Museum.Shared.Enums;
using System.Net.Http.Json;

namespace Museum.IntegrationTests;

public class AnalyticsEndpointsTests(MuseumAppFixture fixture)
{
    [Fact]
    public async Task GetTopExhibitions_ByUniqueVisitors_ReturnsCorrectRanking()
    {
        var ct = TestContext.Current.CancellationToken;

        var result = await fixture.Client.GetFromJsonAsync<List<ExhibitionVisitsDto>>(
            "/api/analytics/top-exhibitions", TestJson.Options, ct);

        Assert.NotNull(result);
        Assert.Equal([1, 5, 2, 7, 6], result.Select(x => x.Exhibition.Id));
        Assert.Equal([7, 7, 5, 4, 3], result.Select(x => x.VisitorCount));
    }

    [Fact]
    public async Task GetExcursionsWithMinVisitors_ReturnsExcursionsWithMinimumVisitors()
    {
        var ct = TestContext.Current.CancellationToken;

        var result = await fixture.Client.GetFromJsonAsync<List<ExcursionVisitsDto>>(
            "/api/analytics/min-visitors-excursions", TestJson.Options, ct);

        Assert.NotNull(result);
        Assert.Equal([9], result.Select(x => x.Excursion.Id));
        Assert.Equal([0], result.Select(x => x.VisitorCount));
    }

    [Fact]
    public async Task GetExcursionsInHall_WithinPeriod_ReturnsMatchingExcursions()
    {
        var ct = TestContext.Current.CancellationToken;

        var url = "/api/analytics/excursions-in-hall?hallNumber=101&startDate=2024-01-01&endDate=2024-12-31";

        var result = await fixture.Client.GetFromJsonAsync<List<ExcursionDto>>(
            url, TestJson.Options, ct);

        Assert.NotNull(result);
        Assert.Equal([1, 4, 5, 6, 7, 9, 10], result.Select(x => x.Id));
    }

    [Fact]
    public async Task GetVisitors_ForSelectedExcursion_OrderedByFullName()
    {
        var ct = TestContext.Current.CancellationToken;

        var result = await fixture.Client.GetFromJsonAsync<List<VisitorDto>>(
            "/api/analytics/visitors-by-excursion/1", TestJson.Options, ct);

        Assert.NotNull(result);
        Assert.Equal([1, 6], result.Select(x => x.Id));
    }

    [Fact]
    public async Task GetThemeStatistics_ByThemeAndPeriod_ReturnsCorrectStatistics()
    {
        var ct = TestContext.Current.CancellationToken;
        var url = "/api/analytics/theme-statistics?startDate=2024-01-01&endDate=2024-12-31";

        var result = await fixture.Client.GetFromJsonAsync<List<ThemeStatisticsDto>>(
            url, TestJson.Options, ct);

        Assert.NotNull(result);
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