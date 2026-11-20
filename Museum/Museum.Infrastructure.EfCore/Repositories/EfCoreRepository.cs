using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для работы с сущностями в базе данных через EF Core
/// </summary>
/// <typeparam name="TEntity">Сущность</typeparam>
/// <param name="context">Контекст базы данных</param>
public class EfCoreRepository<TEntity>(MuseumDbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Создать запрос к базе данных для получения сущностей
    /// Позволяет наследникам добавлять включения через Include перед операций чтения
    /// </summary>
    /// <returns>Запрос к базе данных</returns>
    protected virtual IQueryable<TEntity> Query() => context.Set<TEntity>();

    /// <summary>
    /// Создать новую сущность
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns>Созданная сущность</returns>
    public async Task<TEntity> Create(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Вернуть сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Сущность или null, если не найдена</returns>
    public async Task<TEntity?> Read(int id) =>
        await Query().FirstOrDefaultAsync(e => e.Id == id);

    /// <summary>
    /// Вернуть список всех сущностей
    /// </summary>
    /// <returns>Список сущностей</returns>
    public async Task<IList<TEntity>> ReadAll() =>
        await Query().ToListAsync();

    /// <summary>
    /// Обновить сущность в базе данных.
    /// Копируются только скалярные поля.
    /// Навигационные свойства обновляются 
    /// через уже отслеживаемую сущность
    /// </summary>
    /// <param name="entity">Обновляемая сущность</param>
    /// <returns>Обновленная сущность или null, если не найдена</returns>
    public async Task<TEntity?> Update(TEntity entity)
    {
        var existingEntity = await context.Set<TEntity>().FindAsync(entity.Id);
        if (existingEntity == null)
            return null;

        context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existingEntity;
    }

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>True, если сущность была удалена, иначе False</returns>
    public async Task<bool> Delete(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity == null)
            return false;

        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}