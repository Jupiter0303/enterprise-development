namespace EnterpriseDevelopment.Domain;

/// <summary>
/// Экскурсия
/// </summary>
public class Excursion
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    public required int Id { get; init; }
    /// <summary>
    /// Дата проведения экскурсии
    /// </summary>
    public required DateOnly Date { get; init; }
    /// <summary>
    /// Время начала экскурсии
    /// </summary>
    public required TimeOnly StartTime { get; init; }
    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    public required TimeSpan Duration { get; init; }
    /// <summary>
    /// Список выставок, включенных в экскурсию
    /// </summary>
    public required List<Exhibition> Exhibitions { get; init; }


}