namespace Museum.Domain;

/// <summary>
/// Интерфейс сущности с идентификатором, который выдает хранилище
/// </summary>
public interface IEntity
{
    /// <summary>
    /// идентификатор сущности
    /// </summary>
    public int Id { get; set; }
}