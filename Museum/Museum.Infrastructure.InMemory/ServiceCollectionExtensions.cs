using Microsoft.Extensions.DependencyInjection;
using Museum.Domain;
using Museum.Domain.Data;

namespace Museum.Infrastructure.InMemory;
/// <summary>
/// Расширение IServiceCollection для регистрации InMemory репозиториев в DI контейнере
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистритровать репозитории InMemory на основе общего DataSeeder
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов с зарегистрированными InMemory репозиториями</returns>
    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        var seed = DataSeeder.Create();
        services.AddSingleton<IRepository<Visitor>>(new InMemoryRepository<Visitor>(seed.Visitors));
        services.AddSingleton<IRepository<Exhibition>>(new InMemoryRepository<Exhibition>(seed.Exhibitions));
        services.AddSingleton<IRepository<Excursion>>(new InMemoryRepository<Excursion>(seed.Excursions));
        services.AddSingleton<IRepository<Ticket>>(new InMemoryRepository<Ticket>(seed.Tickets));
        return services;
    }
}