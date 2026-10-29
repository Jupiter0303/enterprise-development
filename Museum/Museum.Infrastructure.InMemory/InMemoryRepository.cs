using Museum.Domain;

namespace Museum.Infrastructure.InMemory;

public class InMemoryRepository<TEntity>(IReadOnlyCollection<TEntity> seed)
    : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly Lock _lock = new();
    private readonly List<TEntity> _items = [.. seed];
    private int _lastId = seed.Select(e => e.Id).DefaultIfEmpty().Max();

    public Task<TEntity> Create(TEntity entity)
    {
        lock(_lock)
        {
            entity.Id = ++_lastId;
            _items.Add(entity);
        }
        return Task.FromResult(entity);

    }

    public Task<TEntity?> Read(int id)
    {
        lock (_lock)
        {
            var entity = _items.FirstOrDefault(e => e.Id == id);
            return Task.FromResult<TEntity?>(entity);
        }

    }
    public Task<IList<TEntity>> ReadAll()
    {
        lock (_lock)
        {
            return Task.FromResult<IList<TEntity>>([.. _items]);
        }
    }
    public Task<TEntity?> Update(TEntity entity)
    {
        lock (_lock)
        {
            var index = _items.FindIndex(e => e.Id == entity.Id);
            if (index == -1)
                return Task.FromResult<TEntity?>(null);

            _items[index] = entity;
            return Task.FromResult<TEntity?>(entity);
        }
    }
    public Task<bool> Delete(int id)
    {
        lock(_lock)
        {
            var delCount = _items.RemoveAll(e => e.Id == id);
            return Task.FromResult(delCount > 0);
        }
    }
}
