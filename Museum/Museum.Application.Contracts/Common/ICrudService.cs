namespace Museum.Application.Contracts.Common;

/// <summary>
/// Базовый интерфейс для CRUD сервисов
/// </summary>
/// <typeparam name="TDto">Тип DTO для чтения</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания и обновления</typeparam>
public interface ICrudService<TDto, TCreateUpdateDto>
{
    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns></returns>
    public Task<TDto?> Get(int id);

    /// <summary>
    /// Получить все объекты
    /// </summary>
    /// <returns> Список DTO объектов</returns>
    public Task<IEnumerable<TDto>> GetAll();

    /// <summary>
    /// Создать новый объект
    /// </summary>
    /// <param name="dto">DTO для создания</param>
    /// <returns>Созданный объект</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Обновить существующий объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="dto">DTO для обновления</param>
    /// <returns>Обновленный объект</returns>
    public Task<TDto?> Update(int id, TCreateUpdateDto dto);

    /// <summary>
    /// Удалить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>True, если объект был удален, иначе False</returns>
    public Task<bool> Delete(int id);
}