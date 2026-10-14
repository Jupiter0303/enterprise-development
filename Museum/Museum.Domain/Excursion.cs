namespace Museum.Domain;

/// <summary>
/// Экскурсия
/// </summary>
public class Excursion
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Дата и время начала экскурсии
    /// </summary>
    public required DateTime StartsAt { get; set; }
    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    public required TimeSpan Duration { get; set; }
    /// <summary>
    /// Список выставок, включенных в экскурсию
    /// </summary>
    public required List<Exhibition> Exhibitions { get; set; }
}