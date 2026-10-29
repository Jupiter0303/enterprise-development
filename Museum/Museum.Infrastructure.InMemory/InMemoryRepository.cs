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
}
