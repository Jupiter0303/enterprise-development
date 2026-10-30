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
    public async Task<IEnumerable<TDto>> GetAll()
    {
        var entities = await repository.ReadAll();
        return entities.Select(entity => ToDto(entity)).ToList();
    }
    public async Task<TDto> Create(TCreateUpdateDto dto)
    {
        var entity = await CreateEntity(dto);
        var created = await repository.Create(entity);
        return ToDto(created);
    }

    public async Task<TDto?> Update(int id, TCreateUpdateDto dto)
    {
        var entity = await repository.Read(id);
        if (entity is null)
            return default;

        await ApplyChanges(entity, dto);
        var updated = await repository.Update(entity);
        return updated is null ? default : ToDto(updated);
    }
    public Task<bool> Delete(int id) => repository.Delete(id);

}
