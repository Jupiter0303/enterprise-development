namespace Museum.Domain;

public interface IRepository<TEntity, TKey>
where TEntity : class, IEntity
{
    public Task<TEntity> Create(TEntity entity);
    public Task<TEntity?> Read(TKey id);
    public Task<IList<TEntity>> ReadAll();
    public Task<TEntity?> Update(TEntity entity);
    public Task<bool> Delete(TKey id);
}
