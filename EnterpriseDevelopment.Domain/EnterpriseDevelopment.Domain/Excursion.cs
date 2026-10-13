namespace EnterpriseDevelopment.Domain;

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
    /// Дата проведения экскурсии
    /// </summary>
    public required DateOnly Date { get; set; }
    /// <summary>
    /// Время начала экскурсии
    /// </summary>
    public required TimeOnly StartTime { get; set; }
    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    public required TimeSpan Duration { get; set; }
    /// <summary>
    /// Список выставок, включенных в экскурсию
    /// </summary>
    public required List<Exhibition> Exhibitions { get; set; }
}