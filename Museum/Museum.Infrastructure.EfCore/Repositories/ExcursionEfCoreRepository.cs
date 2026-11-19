using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Infrastructure.EfCore.Repositories;

internal class ExcursionEfCoreRepository(MuseumDbContext context) : EfCoreRepository<Excursion>(context)
{
    protected override IQueryable<Excursion> Query() => base.Query()
        .Include(e => e.Exhibitions);
}
