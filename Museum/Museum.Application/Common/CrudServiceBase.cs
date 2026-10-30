using Museum.Domain;
using Microsoft.Extensions.Logging;

namespace Museum.Application.Common;

public abstract class CrudServiceBase<TEntity, TDto, TCreateUpdateDto>(IRepository<TEntity> repository, ILogger logger) 
    where TEntity : class, IEntity
{
    protected abstract TDto ToDto(TEntity entity);
    protected abstract Task<TEntity> CreateEntity(TCreateUpdateDto dto);
    protected abstract Task ApplyChanges(TEntity entity, TCreateUpdateDto dto);

    public async Task<TDto?> Get(int id)
    {
        logger.LogInformation("Поиск {Entity} с Id: {Id}", typeof(TEntity).Name, id);
        var entity = await repository.Read(id);
        return entity is null ? default : ToDto(entity);
    }
    public async Task<IEnumerable<TDto>> GetAll()
    {
        logger.LogInformation("Получение списка всех {Entity}", typeof(TEntity).Name);
        var entities = await repository.ReadAll();
        return entities.Select(entity => ToDto(entity)).ToList();
    }
    public async Task<TDto> Create(TCreateUpdateDto dto)
    {
        logger.LogInformation("Создание новой сущности {Entity}", typeof(TEntity).Name);
        var entity = await CreateEntity(dto);
        var created = await repository.Create(entity);
        logger.LogInformation("Сущность {Entity} успешно создана с Id: {Id}", typeof(TEntity).Name, created.Id);
        return ToDto(created);
    }

    public async Task<TDto?> Update(int id, TCreateUpdateDto dto)
    {
        logger.LogInformation("Обновление {Entity} с Id: {Id}", typeof(TEntity).Name, id);
        var entity = await repository.Read(id);
        if (entity is null)
        {
            logger.LogWarning("{Entity} с Id: {Id} не найден для обновления", typeof(TEntity).Name, id);
            return default;
        }

        await ApplyChanges(entity, dto);
        var updated = await repository.Update(entity);
        return updated is null ? default : ToDto(updated);
    }
    public Task<bool> Delete(int id)
    {
        logger.LogInformation("Удаление {Entity} с Id: {Id}", typeof(TEntity).Name, id);
        return repository.Delete(id);
    }
}
