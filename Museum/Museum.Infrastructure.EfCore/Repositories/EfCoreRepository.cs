using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Infrastructure.EfCore.Repositories;

public class EfCoreRepository<TEntity>(MuseumDbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected virtual IQueryable<TEntity> Query() => context.Set<TEntity>();

    public async Task<TEntity> Create(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> Read(int id) =>
        await Query().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IList<TEntity>> ReadAll() =>
        await Query().ToListAsync();

    public async Task<TEntity?> Update(TEntity entity)
    {
        var existingEntity = await context.Set<TEntity>().FindAsync(entity.Id);
        if (existingEntity == null)
            return null;

        context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existingEntity;
    }

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