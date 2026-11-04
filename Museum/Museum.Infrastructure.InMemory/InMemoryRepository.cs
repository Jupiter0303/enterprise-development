using Museum.Domain;

namespace Museum.Infrastructure.InMemory;

/// <summary>
/// Репозиторий хранилища
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <param name="seed">Стартовые данные</param>
public class InMemoryRepository<TEntity>(IReadOnlyCollection<TEntity> seed)
    : IRepository<TEntity>
    where TEntity : class, IEntity
{
    // lock для синхронизации доступа к коллекции
    private readonly Lock _lock = new();
    // коллекция для хранения сущностей
    private readonly List<TEntity> _items = [.. seed];
    // последний идентификатор сущности
    private int _lastId = seed.Select(e => e.Id).DefaultIfEmpty().Max();

    /// <summary>
    /// Добавить сущность и присвоить ей идентификатор
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <returns>Сущность с присвоенным идентификатором</returns>
    public Task<TEntity> Create(TEntity entity)
    {
        lock (_lock)
        {
            entity.Id = ++_lastId;
            _items.Add(entity);
        }
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Вернуть сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Сущность или null, если не найдена</returns>
    public Task<TEntity?> Read(int id)
    {
        lock (_lock)
        {
            var entity = _items.FirstOrDefault(e => e.Id == id);
            return Task.FromResult<TEntity?>(entity);
        }
    }
    /// <summary>
    /// Вернуть список всех сущностей
    /// </summary>
    /// <returns>Список сущностей</returns>
    public Task<IList<TEntity>> ReadAll()
    {
        lock (_lock)
        {
            return Task.FromResult<IList<TEntity>>([.. _items]);
        }
    }

    /// <summary>
    /// Обновить сущность
    /// </summary>
    /// <param name="entity">Обновляемая сущность</param>
    /// <returns>Обновленная сущность или null, если сущность не найдена</returns>
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

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>True, если сущность была удалена, иначе false</returns>
    public Task<bool> Delete(int id)
    {
        lock (_lock)
        {
            var delCount = _items.RemoveAll(e => e.Id == id);
            return Task.FromResult(delCount > 0);
        }
    }
}