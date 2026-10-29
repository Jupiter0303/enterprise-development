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
        var entity = default(TEntity);
        lock (_lock)
        {
            entity = _items.FirstOrDefault(e => e.Id == id);
        }
        return Task.FromResult<TEntity?>(entity);

    }
    public Task<IList<TEntity>> ReadAll()
    {
        var entities = default(IList<TEntity>);
        lock (_lock)
        {
            entities = _items.ToList();
        }
        return Task.FromResult<IList<TEntity>>(entities);


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
            if(_items.RemoveAll(e => e.Id == id) > 0)
            {

                return Task.FromResult(true);
            }
        }
        return Task.FromResult(false);
    }
}
