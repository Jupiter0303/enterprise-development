using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Infrastructure.EfCore;

public class MuseumDbContext(DbContextOptions<MuseumDbContext> options) : DbContext(options)
{
    public DbSet<Visitor> Visitors => Set<Visitor>();
    public DbSet<Exhibition> Exhibitions => Set<Exhibition>();
    public DbSet<Excursion> Excursions => Set<Excursion>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

}
