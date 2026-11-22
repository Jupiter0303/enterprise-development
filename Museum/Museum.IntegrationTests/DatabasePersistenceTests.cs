using Microsoft.EntityFrameworkCore;
using Museum.Application.Contracts.Visitors;
using Museum.Domain.Data;
using System.Net.Http.Json;

namespace Museum.IntegrationTests;

/// <summary>
/// Интеграционные тесты, проверяющие корректную работу бд
/// </summary>
/// <param name="fixture">Фикстура</param>
public class DatabasePersistenceTests(MuseumAppFixture fixture)
{
    /// <summary>
    /// Тест на то,что созданный через API посетитель действительно
    /// сохранился в бд
    /// </summary>
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

        var response = await fixture.Client.PostAsJsonAsync("/api/visitor", dto, TestJson.Options, ct);
        response.EnsureSuccessStatusCode();
        var created = await response.Content.ReadFromJsonAsync<VisitorDto>(TestJson.Options, ct);
        Assert.NotNull(created);

        await using var db = fixture.CreateDbContext();
        var row = await db.Visitors.SingleOrDefaultAsync(v => v.Id == created.Id, ct);

        Assert.NotNull(row);
        Assert.Equal("415 база..", row.FirstName);
    }

    /// <summary>
    /// Тест на то, что после миграций в бд находится ожидаемое 
    /// количество сидированных данных
    /// </summary>
    [Fact]
    public async Task Database_AfterMigration_ContainsSeededData()
    {
        var ct = TestContext.Current.CancellationToken;
        var seed = DataSeeder.Create();

        await using var db = fixture.CreateDbContext();

        Assert.Equal(seed.Exhibitions.Count, await db.Exhibitions.CountAsync(ct));
        Assert.Equal(seed.Excursions.Count, await db.Excursions.CountAsync(ct));
        Assert.Equal(seed.Tickets.Count, await db.Tickets.CountAsync(ct));

        // другой тест мог добавить посетителя, поэтому >=
        Assert.True(await db.Visitors.CountAsync(ct) >= seed.Visitors.Count);
    }

    /// <summary>
    /// Тест на то,что удаленный через API посетитель действительно
    /// исчез из бд
    /// </summary>
    [Fact]
    public async Task Delete_Visitor_IsRemovedFromDatabase()
    {
        var ct = TestContext.Current.CancellationToken;

        var dto = new VisitorCreateUpdateDto
        {
            FirstName = "удаляй..",
            LastName = "..при мне",
            Birthday = new DateOnly(2000, 1, 1)
        };

        var response = await fixture.Client.PostAsJsonAsync("/api/visitor", dto, TestJson.Options, ct);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<VisitorDto>(TestJson.Options, ct);
        Assert.NotNull(created);

        var deleteResponse = await fixture.Client.DeleteAsync($"/api/visitor/{created.Id}", ct);
        deleteResponse.EnsureSuccessStatusCode();

        await using var db = fixture.CreateDbContext();
        var row = await db.Visitors.SingleOrDefaultAsync(v => v.Id == created.Id, ct);

        Assert.Null(row);
    }

    /// <summary>
    /// Тест корректное заполнение связей "многие ко многим"
    /// в промежуточной таблице
    /// </summary>
    [Fact]
    public async Task Database_ManyToManyJoinTable_HasCorrectLinks()
    {
        var ct = TestContext.Current.CancellationToken;
        var seed = DataSeeder.Create();

        var expectedLinksCount = seed.Excursions.Sum(exc => exc.Exhibitions.Count);

        await using var db = fixture.CreateDbContext();

        var actualLinksCount = await db.Excursions.SelectMany(exc => exc.Exhibitions).CountAsync(ct);

        Assert.Equal(expectedLinksCount, actualLinksCount);
    }

    /// <summary>
    /// Тест на snake_case формат названий таблиц в бд
    /// </summary>
    [Fact]
    public async Task Database_Tables_AreInSnakeCase()
    {
        var ct = TestContext.Current.CancellationToken;

        await using var db = fixture.CreateDbContext();

        var sqlQuery = "SELECT table_name FROM information_schema.tables WHERE table_schema = DATABASE()";
        var tables = await db.Database.SqlQueryRaw<string>(sqlQuery).ToListAsync(ct);

        Assert.Contains("visitors", tables);
        Assert.Contains("exhibitions", tables);
        Assert.Contains("excursions", tables);
        Assert.Contains("tickets", tables);
        Assert.Contains("excursion_exhibitions", tables);
    }
}