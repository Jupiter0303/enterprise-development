using Aspire.Hosting;
using Microsoft.EntityFrameworkCore;
using Museum.Infrastructure.EfCore;

[assembly: AssemblyFixture(typeof(Museum.IntegrationTests.MuseumAppFixture))]

namespace Museum.IntegrationTests;

public sealed class MuseumAppFixture : IAsyncLifetime
{
    private DistributedApplication? _app;
    public HttpClient Client { get; private set; } = null!;
    private string _connectionString = null!;
    public async ValueTask InitializeAsync()
    {
        var ct = TestContext.Current.CancellationToken;

        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Museum_AppHost>(ct);
        //Убирает проверку ssl сертификата для тестового клиента
        //На CI нет доверенного сертификата разработки и поэтому
        //запросы к API будут падать с UntrustedRoot
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