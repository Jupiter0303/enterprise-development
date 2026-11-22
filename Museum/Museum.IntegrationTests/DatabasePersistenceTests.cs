using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Museum.Application.Contracts.Visitors;
using Museum.Domain.Data;

namespace Museum.IntegrationTests;

/// <summary>
/// Интеграционные тесты, проверяющие корректную работу бд
/// </summary>
/// <param name="fixture">Фикстура</param>
public class DatabasePersistenceTests(MuseumAppFixture fixture)
{
    /// <summary>
    /// Тест на то, что созданный через API посетитель действительно
    /// сохранился в бд
    /// </summary>
    [Fact]
    public async Task Create_Visitor_IsPersistedInDatabase()
    {
        // arrange
        var ct = TestContext.Current.CancellationToken;
        var dto = new VisitorCreateUpdateDto
        {
            FirstName = "415 база..",
            LastName = "..ответьте",
            Birthday = new DateOnly(2000, 1, 1)
        };

        // act
        var response = await fixture.Client.PostAsJsonAsync("/api/visitor", dto, TestJson.Options, ct);
        response.EnsureSuccessStatusCode();
        var created = await response.Content.ReadFromJsonAsync<VisitorDto>(TestJson.Options, ct);
        Assert.NotNull(created);

        await using var db = fixture.CreateDbContext();
        var row = await db.Visitors.SingleOrDefaultAsync(v => v.Id == created.Id, ct);

        // assert
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
        // arrange
        var ct = TestContext.Current.CancellationToken;
        var seed = DataSeeder.Create();

        // act
        await using var db = fixture.CreateDbContext();
        var exhibitionsCount = await db.Exhibitions.CountAsync(ct);
        var excursionsCount = await db.Excursions.CountAsync(ct);
        var ticketsCount = await db.Tickets.CountAsync(ct);
        var visitorsCount = await db.Visitors.CountAsync(ct);

        // assert
        Assert.Equal(seed.Exhibitions.Count, exhibitionsCount);
        Assert.Equal(seed.Excursions.Count, excursionsCount);
        Assert.Equal(seed.Tickets.Count, ticketsCount);
        // другой тест мог добавить посетителя, поэтому >=
        Assert.True(visitorsCount >= seed.Visitors.Count);
    }

    /// <summary>
    /// Тест на то, что удаленный через API посетитель действительно
    /// исчез из бд
    /// </summary>
    [Fact]
    public async Task Delete_Visitor_IsRemovedFromDatabase()
    {
        // arrange
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

        // act
        var deleteResponse = await fixture.Client.DeleteAsync($"/api/visitor/{created.Id}", ct);
        deleteResponse.EnsureSuccessStatusCode();

        await using var db = fixture.CreateDbContext();
        var row = await db.Visitors.SingleOrDefaultAsync(v => v.Id == created.Id, ct);

        // assert
        Assert.Null(row);
    }

    /// <summary>
    /// Тест корректное заполнение связей "многие ко многим"
    /// в промежуточной таблице
    /// </summary>
    [Fact]
    public async Task Database_ManyToManyJoinTable_HasCorrectLinks()
    {
        // arrange
        var ct = TestContext.Current.CancellationToken;
        var seed = DataSeeder.Create();
        var expectedLinksCount = seed.Excursions.Sum(exc => exc.Exhibitions.Count);

        // act
        await using var db = fixture.CreateDbContext();
        var actualLinksCount = await db.Excursions.SelectMany(exc => exc.Exhibitions).CountAsync(ct);

        // assert
        Assert.Equal(expectedLinksCount, actualLinksCount);
    }

    /// <summary>
    /// Тест на snake_case формат названий таблиц в бд
    /// </summary>
    [Fact]
    public async Task Database_Tables_AreInSnakeCase()
    {
        // arrange
        var ct = TestContext.Current.CancellationToken;
        await using var db = fixture.CreateDbContext();
        var sqlQuery = "SELECT table_name FROM information_schema.tables WHERE table_schema = DATABASE()";

        // act
        var tables = await db.Database.SqlQueryRaw<string>(sqlQuery).ToListAsync(ct);

        // assert
        Assert.Contains("visitors", tables);
        Assert.Contains("exhibitions", tables);
        Assert.Contains("excursions", tables);
        Assert.Contains("tickets", tables);
        Assert.Contains("excursion_exhibitions", tables);
    }
}