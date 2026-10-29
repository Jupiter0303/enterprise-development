namespace Museum.Domain;

public interface IRepository<TEntity>
where TEntity : class, IEntity
{
    public Task<TEntity> Create(TEntity entity);
    public Task<TEntity?> Read(int id);
    public Task<IList<TEntity>> ReadAll();
    public Task<TEntity?> Update(TEntity entity);
    public Task<bool> Delete(int id);
}
