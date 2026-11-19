using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Infrastructure.EfCore;

public class MuseumDbContext(DbContextOptions<MuseumDbContext> options) : DbContext(options)
{
    public DbSet<Visitor> Visitors => Set<Visitor>();
    public DbSet<Exhibition> Exhibitions => Set<Exhibition>();
    public DbSet<Excursion> Excursions => Set<Excursion>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>(builder =>
        {
            builder.Property<int>("ExcursionId").HasColumnName("excursion_id");
            builder.Property<int>("VisitorId").HasColumnName("visitor_id");

            builder.HasOne(t => t.Excursion)
            .WithMany()
            .HasForeignKey("ExcursionId");

            builder.HasOne(t => t.Visitor)
            .WithMany()
            .HasForeignKey("VisitorId");
        });

        modelBuilder.Entity<Excursion>()
            .HasMany(e => e.Exhibitions)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
            "excursion_exhibitions",
            r => r.HasOne<Exhibition>().WithMany().HasForeignKey("exhibition_id"),
            l => l.HasOne<Excursion>().WithMany().HasForeignKey("excursion_id"));
    }
}
