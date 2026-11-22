using Microsoft.EntityFrameworkCore;
using Museum.Domain;
using Museum.Domain.Data;

namespace Museum.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных для работы с сущностями музея
/// </summary>
public class MuseumDbContext(DbContextOptions<MuseumDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Набор сущностей посетителей
    /// </summary>
    public DbSet<Visitor> Visitors => Set<Visitor>();

    /// <summary>
    /// Набор сущностей выставок
    /// </summary>
    public DbSet<Exhibition> Exhibitions => Set<Exhibition>();

    /// <summary>
    /// Набор сущностей экскурсий
    /// </summary>
    public DbSet<Excursion> Excursions => Set<Excursion>();

    /// <summary>
    /// Набор сущностей билетов
    /// </summary>
    public DbSet<Ticket> Tickets => Set<Ticket>();

    /// <summary>
    /// Настроить модель базы данных и выполнить начальное заполнение данными
    /// </summary>
    /// <param name="modelBuilder">Строитель модели базы данных</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var seed = DataSeeder.Create();

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
                r => r.HasOne<Exhibition>()
                    .WithMany()
                    .HasForeignKey("exhibition_id"),
                l => l.HasOne<Excursion>()
                    .WithMany()
                    .HasForeignKey("excursion_id"),
                j => j.HasData(
                    seed.Excursions.SelectMany(exc =>
                            exc.Exhibitions.Select(exh =>
                            new Dictionary<string, object>
                            {
                                ["excursion_id"] = exc.Id,
                                ["exhibition_id"] = exh.Id
                            }))));

        modelBuilder.Entity<Visitor>().HasData(seed.Visitors);
        modelBuilder.Entity<Exhibition>().HasData(seed.Exhibitions);

        modelBuilder.Entity<Excursion>().HasData(
            seed.Excursions.Select(e => new
            {
                e.Id,
                e.StartsAt,
                e.Duration
            }));

        modelBuilder.Entity<Ticket>().HasData(
            seed.Tickets.Select(t =>
                new
                {
                    t.Id,
                    t.Type,
                    t.Price,
                    ExcursionId = t.Excursion.Id,
                    VisitorId = t.Visitor.Id
                }));
    }
}