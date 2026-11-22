using System.Net;
using System.Net.Http.Json;
using Museum.Application.Contracts.Visitors;

namespace Museum.IntegrationTests;

/// <summary>
/// Интеграционные тесты эндпоинтов посетителей
/// </summary>
/// <param name="fixture">Фикстура</param>
public class VisitorEndpointsTests(MuseumAppFixture fixture)
{
    /// <summary>
    /// Тест на возврат от API списка посетителей после миграций и сидинга
    /// </summary>
    [Fact]
    public async Task GetAll_AfterMigration_ReturnsSeededVisitors()
    {
        var ct = TestContext.Current.CancellationToken;

        var visitors = await fixture.Client
            .GetFromJsonAsync<List<VisitorDto>>("/api/visitor", TestJson.Options, ct);

        Assert.NotNull(visitors);
        Assert.True(visitors.Count >= 10);
    }
    /// <summary>
    /// Тест на возврат 200 при get-запросе по существующему id
    /// </summary>
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
    /// <summary>
    /// Тест на возврат 404 при get-запросе по несуществующему id
    /// </summary>
    [Fact]
    public async Task Get_UnknownId_ReturnsNotFound()
    {
        var ct = TestContext.Current.CancellationToken;
        var response = await fixture.Client.GetAsync("/api/visitor/999999", ct);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Тест на 201 при добавлении посетителя при post-запросе
    /// и проверка, что данные сохранились
    /// </summary>
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
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<VisitorDto>(TestJson.Options, ct);
        Assert.NotNull(created);

        var fetched = await fixture.Client.GetFromJsonAsync<VisitorDto>($"/api/visitor/{created.Id}", TestJson.Options, ct);
        Assert.Equal("Тест", fetched?.LastName);
    }

    /// <summary>
    /// Тест на возврат 400 при post-запросе с невалидными данными
    /// </summary>
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

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    /// <summary>
    /// Тест для проверки обновления данных существующего посетителя
    /// при put-запросе
    /// </summary>
    [Fact]
    public async Task Update_ValidVisitor_ReturnsOkAndUpdatesData()
    {
        var ct = TestContext.Current.CancellationToken;

        var createDto = new VisitorCreateUpdateDto { FirstName = "Иван", LastName = "Иванов", Birthday = new DateOnly(2004, 10, 10) };
        var createResponse = await fixture.Client.PostAsJsonAsync("/api/visitor", createDto, TestJson.Options, ct);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<VisitorDto>(TestJson.Options, ct);

        Assert.NotNull(created);

        var updateDto = new VisitorCreateUpdateDto { FirstName = "Иван", LastName = "Наумов", Birthday = new DateOnly(2004, 10, 10) };
        var updateResponse = await fixture.Client.PutAsJsonAsync($"/api/visitor/{created.Id}", updateDto, TestJson.Options, ct);

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        var fetched = await fixture.Client.GetFromJsonAsync<VisitorDto>($"/api/visitor/{created.Id}", TestJson.Options, ct);
        Assert.Equal("Наумов", fetched?.LastName);
    }

    /// <summary>
    /// Тест на возврат 404 при put-запросе для несуществующего посетителя
    /// </summary>
    [Fact]
    public async Task Update_UnknownId_ReturnsNotFound()
    {
        var ct = TestContext.Current.CancellationToken;

        var updateDto = new VisitorCreateUpdateDto { FirstName = "Иван", LastName = "Наумов", Birthday = new DateOnly(2004, 10, 10) };
        var response = await fixture.Client.PutAsJsonAsync("/api/visitor/99999", updateDto, TestJson.Options, ct);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Тест на удаление посетителя при delete-запросе
    /// и возврат 404 после этого
    /// </summary>
    [Fact]
    public async Task Delete_ExistingVisitor_ReturnsNoContentAndRemovesData()
    {
        var ct = TestContext.Current.CancellationToken;

        var createDto = new VisitorCreateUpdateDto { FirstName = "Удалите", LastName = "Меня", Birthday = new DateOnly(2000, 1, 1) };
        var createResponse = await fixture.Client.PostAsJsonAsync("/api/visitor", createDto, TestJson.Options, ct);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<VisitorDto>(TestJson.Options, ct);

        Assert.NotNull(created);

        var deleteResponse = await fixture.Client.DeleteAsync($"/api/visitor/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await fixture.Client.GetAsync($"/api/visitor/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}