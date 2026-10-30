using Museum.Domain;

namespace Museum.Application.Common;

public abstract class CrudServiceBase<TEntity, TDto, TCreateUpdateDto>
    where TEntity : class, IEntity
{
    protected abstract TDto ToDto(TEntity entity);
    protected abstract Task<TEntity> CreateEntity(TCreateUpdateDto dto);
    protected abstract Task ApplyChanges(TEntity entity, TCreateUpdateDto dto);
}
