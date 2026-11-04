namespace Museum.Domain;

/// <summary>
/// Интерфейс хранилища сущностей одного типа
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity>
where TEntity : class, IEntity
{
    /// <summary>
    /// Добавить сущность и присвоить ей идентификатор
    /// </summary>
    /// <param name="entity"> Добавляемая сущность</param>
    /// <returns> Сущность с присвоенным идентификатором</returns>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Вернуть сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Сущность или null, если не найдена</returns>
    public Task<TEntity?> Read(int id);

    /// <summary>
    /// Вернуть список всех сущностей
    /// </summary>
    /// <returns>Список сущностей</returns>
    public Task<IList<TEntity>> ReadAll();

    /// <summary>
    /// Обновить сущность
    /// </summary>
    /// <param name="entity">Обновляемая сущность</param>
    /// <returns>Обновленная сущность или null, если сущность не найдена</returns>
    public Task<TEntity?> Update(TEntity entity);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>True, если сущность была удалена, иначе false</returns>
    public Task<bool> Delete(int id);
}