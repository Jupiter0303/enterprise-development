using Aspire.Hosting;
using Microsoft.EntityFrameworkCore;
using Museum.Infrastructure.EfCore;

[assembly: AssemblyFixture(typeof(Museum.IntegrationTests.MuseumAppFixture))]

namespace Museum.IntegrationTests;

/// <summary>
/// Фикстура интеграционных тестов музея
/// Запускает Aspire-приложение, ожидает готовности API
/// и предоставляет http-клиент и контекст бд
/// </summary>
public sealed class MuseumAppFixture : IAsyncLifetime
{
    private DistributedApplication? _app;
    private string _connectionString = null!;

    /// <summary>
    /// http-клиент для обращения к API приложения
    /// </summary>
    public HttpClient Client { get; private set; } = null!;

    /// <summary>
    /// Инициализация фикстуры:запуск Aspire, ожидание готовности API
    /// и получение строки подключения к бд
    /// </summary>
    public async ValueTask InitializeAsync()
    {
        var ct = TestContext.Current.CancellationToken;

        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Museum_AppHost>(ct);
        // Убирает проверку ssl сертификата для тестового клиента
        // На CI нет доверенного сертификата разработки и поэтому
        // запросы к API будут падать с UntrustedRoot
        builder.Services.ConfigureHttpClientDefaults(http =>
            http.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            }));

        _app = await builder.BuildAsync(ct);
        await _app.StartAsync(ct);

        await _app.ResourceNotifications
            .WaitForResourceHealthyAsync("api", ct)
            .WaitAsync(TimeSpan.FromMinutes(3), ct);

        _connectionString = (await _app.GetConnectionStringAsync("museum", ct))!;

        Client = _app.CreateHttpClient("api");
    }

    /// <summary>
    /// Создать контекст базы данных для прямых запросов к ней
    /// </summary>
    public MuseumDbContext CreateDbContext() =>
        new(new DbContextOptionsBuilder<MuseumDbContext>()
            .UseMySql(_connectionString, new MySqlServerVersion(new Version(9, 0, 0)))
            .Options);
    public async ValueTask DisposeAsync()
    {
        Client?.Dispose();
        if (_app is not null)
            await _app.DisposeAsync();
    }
}