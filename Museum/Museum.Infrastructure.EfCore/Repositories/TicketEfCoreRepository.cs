using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Infrastructure.EfCore.Repositories;

internal class TicketEfCoreRepository(MuseumDbContext context) : EfCoreRepository<Ticket>(context)
{
    protected override IQueryable<Ticket> Query() =>
        base.Query()
        .Include(t => t.Visitor)
        .Include(t => t.Excursion)
        .ThenInclude(e => e.Exhibitions);
}
