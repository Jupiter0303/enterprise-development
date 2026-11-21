using System.Net.Http.Json;
using Museum.Application.Contracts.Visitors;

namespace Museum.IntegrationTests;

public class VisitorEndpointsTests(MuseumAppFixture fixture)
{
    [Fact]
    public async Task GetAll_AfterMigration_ReturnsSeededVisitors()
    {
        var ct = TestContext.Current.CancellationToken;

        var visitors = await fixture.Client
            .GetFromJsonAsync<List<VisitorDto>>("/api/visitor", ct);

        Assert.NotNull(visitors);
        Assert.True(visitors.Count >= 10);
    }
}