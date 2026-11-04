namespace Museum.Application.Contracts.Common;

/// <summary>
/// Интерфейс для DTO, которые имеют идентификатор
/// Нужен для гарантии наличия id у DTO в контроллере 
/// </summary>
public interface IHasId
{
    /// <summary>
    /// Идентификатор DTO
    /// </summary>
    public int Id { get; }
}