using System.Net.Http.Json;
using Museum.Application.Contracts.Analytics;

namespace Museum.IntegrationTests;

public class AnalyticsEndpointsTests(MuseumAppFixture fixture)
{
    [Fact]
    public async Task GetTopExhibitions_ReturnsFiveMostVisitedExhibitions()
    {
        var ct = TestContext.Current.CancellationToken;

        var result = await fixture.Client.GetFromJsonAsync<List<ExhibitionVisitsDto>>(
            "/api/analytics/top-exhibitions", TestJson.Options, ct);

        Assert.NotNull(result);
        Assert.Equal([1, 5, 2, 7, 6], result.Select(x => x.Exhibition.Id));
        Assert.Equal([7, 7, 5, 4, 3], result.Select(x => x.VisitorCount));
    }
}