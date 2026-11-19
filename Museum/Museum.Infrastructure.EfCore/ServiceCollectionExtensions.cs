using Microsoft.Extensions.DependencyInjection;
using Museum.Domain;
using Museum.Infrastructure.EfCore.Repositories;

namespace Museum.Infrastructure.EfCore;

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
