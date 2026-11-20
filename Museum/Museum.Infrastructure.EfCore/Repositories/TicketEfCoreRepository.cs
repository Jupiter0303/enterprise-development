using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для работы с сущностями Ticket в базе данных через EF Core
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class TicketEfCoreRepository(MuseumDbContext context) : EfCoreRepository<Ticket>(context)
{
    /// <summary>
    /// Создать отложенный запрос к базе данных для
    /// получения сущностей Ticket с включением 
    /// связанных сущностей Visitor и 
    /// Excursion с Exhibitions
    /// </summary>
    /// <returns>Отложенный запрос к базе данных</returns>
    protected override IQueryable<Ticket> Query() =>
        base.Query()
            .Include(t => t.Visitor)
            .Include(t => t.Excursion)
            .ThenInclude(e => e.Exhibitions);
}