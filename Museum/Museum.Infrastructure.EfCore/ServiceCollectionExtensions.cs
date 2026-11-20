using Microsoft.Extensions.DependencyInjection;
using Museum.Domain;
using Museum.Infrastructure.EfCore.Repositories;

namespace Museum.Infrastructure.EfCore;

/// <summary>
/// Раcширение IServiceCollection для регистрации EF Core репозиториев в DI контейнере
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрировать репозитории EF Core
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов с зарегистрированными EF-репозиториями</returns>
    public static IServiceCollection AddEfCoreRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Visitor>, EfCoreRepository<Visitor>>();
        services.AddScoped<IRepository<Exhibition>, EfCoreRepository<Exhibition>>();
        services.AddScoped<IRepository<Excursion>, ExcursionEfCoreRepository>();
        services.AddScoped<IRepository<Ticket>, TicketEfCoreRepository>();
        return services;
    }
}