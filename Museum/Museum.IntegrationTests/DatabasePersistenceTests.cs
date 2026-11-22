using Microsoft.EntityFrameworkCore;
using Museum.Application.Contracts.Visitors;
using Museum.Domain.Data;
using System.Net.Http.Json;

namespace Museum.IntegrationTests;


public class DatabasePersistenceTests(MuseumAppFixture fixture)
{
    [Fact]
    public async Task Create_Visitor_IsPersistedInDatabase()
    {
        var ct = TestContext.Current.CancellationToken;

        var dto = new VisitorCreateUpdateDto
        {
            FirstName = "415 база..",
            LastName = "..ответьте",
            Birthday = new DateOnly(2000, 1, 1)
        };

        var response = await fixture.Client.PostAsJsonAsync<VisitorCreateUpdateDto>("/api/visitor", dto, TestJson.Options, ct);
        response.EnsureSuccessStatusCode();
        var created = await response.Content.ReadFromJsonAsync<VisitorDto>(TestJson.Options, ct);
        Assert.NotNull(created);

        await using var db = fixture.CreateDbContext();
        var row = await db.Visitors.SingleOrDefaultAsync(v => v.Id == created.Id, ct);

        Assert.NotNull(row);
        Assert.Equal("415 база..", row.FirstName);
    }
}