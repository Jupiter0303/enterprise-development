using System.Net;
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
            .GetFromJsonAsync<List<VisitorDto>>("/api/visitor", TestJson.Options, ct);

        Assert.NotNull(visitors);
        Assert.True(visitors.Count >= 10);
    }
    [Fact]
    public async Task GetById_WhenExists_ReturnsVisitor()
    {
        var ct = TestContext.Current.CancellationToken;
        var response = await fixture.Client.GetAsync("/api/visitor/1", ct);

        response.EnsureSuccessStatusCode();
        var visitor = await response.Content.ReadFromJsonAsync<VisitorDto>(TestJson.Options, ct);

        Assert.NotNull(visitor);
        Assert.Equal(1, visitor.Id);
    }

    [Fact]
    public async Task Get_UnknownId_ReturnsNotFound()
    {
        var ct = TestContext.Current.CancellationToken;
        var response = await fixture.Client.GetAsync("/api/visitor/999999", ct);

        Assert.Equal(response.StatusCode, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_ValidVisitor_ReturnsCreatedAndCanBeRead()
    {
        var ct = TestContext.Current.CancellationToken;
        var dto = new VisitorCreateUpdateDto
        {
            FirstName = "Интеграционный",
            LastName = "Тест",
            Birthday = new DateOnly(2000, 1, 1)
        };

        var response = await fixture.Client.PostAsJsonAsync("/api/visitor", dto, TestJson.Options, ct);
        var created = await response.Content.ReadFromJsonAsync<VisitorDto>(TestJson.Options, ct);
        var fetched = await fixture.Client.GetFromJsonAsync<VisitorDto>($"/api/visitor/{created.Id}", TestJson.Options, ct);

        Assert.Equal(response.StatusCode, HttpStatusCode.Created);
        Assert.NotNull(created);
        Assert.Equal("Тест", fetched?.LastName);
    }

    [Fact]
    public async Task Create_EmptyFirstName_ReturnsBadRequest()
    {
        var ct = TestContext.Current.CancellationToken;
        var dto = new VisitorCreateUpdateDto
        {
            FirstName = "",
            LastName = "Тест",
            Birthday = new DateOnly(2000, 1, 1)
        };

        var response = await fixture.Client.PostAsJsonAsync("/api/visitor", dto, TestJson.Options, ct);

        Assert.Equal(response.StatusCode, HttpStatusCode.BadRequest);
    }
}