using Museum.Domain;
using Microsoft.Extensions.Logging;
using Museum.Application.Contracts.Common;

namespace Museum.Application.Common;

/// <summary>
/// Базовый класс для сервисов с реализацией CRUD операций
/// Реализация маппинга остается за наследниками
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TDto">Тип DTO для чтения</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания и обновления</typeparam>
/// <param name="repository">Репозиторий для доступа к данным</param>
/// <param name="logger">Логгер</param>
public abstract class CrudServiceBase<TEntity, TDto, TCreateUpdateDto>(
    IRepository<TEntity> repository,
    ILogger logger)
    : ICrudService<TDto, TCreateUpdateDto>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Преобразовать сущность в DTO
    /// </summary>
    /// <param name="entity">Сущность для преобразования</param>
    /// <returns>DTO</returns>
    protected abstract TDto ToDto(TEntity entity);

    /// <summary>
    /// Создать сущность из DTO
    /// </summary>
    /// <param name="dto">DTO для создания</param>
    protected abstract Task<TEntity> CreateEntity(TCreateUpdateDto dto);

    /// <summary>
    /// Обновить сущность на основе DTO
    /// </summary>
    /// <param name="entity">Сущность для обновления</param>
    /// <param name="dto">DTO для обновления</param>
    protected abstract Task ApplyChanges(TEntity entity, TCreateUpdateDto dto);

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>DTO объект</returns>
    public async Task<TDto?> Get(int id)
    {
        logger.LogInformation("Поиск {Entity} с Id: {Id}", typeof(TEntity).Name, id);
        var entity = await repository.Read(id);
        return entity is null ? default : ToDto(entity);
    }

    /// <summary>
    /// Получить все объекты
    /// </summary>
    /// <returns>Список DTO объектов</returns>
    public async Task<IEnumerable<TDto>> GetAll()
    {
        logger.LogInformation("Получение списка всех {Entity}", typeof(TEntity).Name);
        var entities = await repository.ReadAll();
        return entities.Select(entity => ToDto(entity)).ToList();
    }

    /// <summary>
    /// Создать новый объект
    /// </summary>
    /// <param name="dto">DTO для создания</param>
    /// <returns>Созданный объект</returns>
    public async Task<TDto> Create(TCreateUpdateDto dto)
    {
        logger.LogInformation("Создание новой сущности {Entity}", typeof(TEntity).Name);
        var entity = await CreateEntity(dto);
        var created = await repository.Create(entity);
        logger.LogInformation("Сущность {Entity} успешно создана с Id: {Id}", typeof(TEntity).Name, created.Id);
        return ToDto(created);
    }

    /// <summary>
    /// Обновить существующий объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="dto">DTO для обновления</param>
    /// <returns>Обновленный объект</returns>
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

    /// <summary>
    /// Удалить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>True, если объект был удален, иначе False</returns>
    public Task<bool> Delete(int id)
    {
        logger.LogInformation("Удаление {Entity} с Id: {Id}", typeof(TEntity).Name, id);
        return repository.Delete(id);
    }
}