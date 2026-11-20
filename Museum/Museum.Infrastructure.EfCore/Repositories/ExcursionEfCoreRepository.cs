using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для работы с сущностями Excursion в базе данных через EF Core
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class ExcursionEfCoreRepository(MuseumDbContext context) : EfCoreRepository<Excursion>(context)
{
    /// <summary>
    /// Создать отложенный запрос к базе данных для
    /// получения сущностей Excursion с включением
    /// связанных сущностей Exhibition
    /// </summary>
    /// <returns>отложенный запрос к базе данных</returns>
    protected override IQueryable<Excursion> Query() => base.Query()
        .Include(e => e.Exhibitions);
}