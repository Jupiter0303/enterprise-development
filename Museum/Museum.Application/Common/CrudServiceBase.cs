using Museum.Domain;

namespace Museum.Application.Common;

public abstract class CrudServiceBase<TEntity, TDto, TCreateUpdateDto>(IRepository<TEntity> repository)
    where TEntity : class, IEntity
{
    protected abstract TDto ToDto(TEntity entity);
    protected abstract Task<TEntity> CreateEntity(TCreateUpdateDto dto);
    protected abstract Task ApplyChanges(TEntity entity, TCreateUpdateDto dto);

    public async Task<TDto?> Get(int id)
    {
        var entity = await repository.Read(id);
        return entity is null ? default : ToDto(entity);
    }



}
