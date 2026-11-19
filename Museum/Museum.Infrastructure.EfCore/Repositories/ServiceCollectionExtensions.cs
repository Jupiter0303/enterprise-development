using Microsoft.Extensions.DependencyInjection;
using Museum.Domain;

namespace Museum.Infrastructure.EfCore.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEfCoreRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Visitor>, EfCoreRepository<Visitor>>();
        services.AddScoped<IRepository<Exhibition>, EfCoreRepository<Exhibition>>();
        services.AddScoped<IRepository<Excursion>, ExcursionEfCoreRepository>();
        services.AddScoped<IRepository<Ticket>, TicketEfCoreRepository>();
        return services;
    }
}
