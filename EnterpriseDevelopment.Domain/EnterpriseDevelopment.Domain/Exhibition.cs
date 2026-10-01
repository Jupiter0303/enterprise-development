namespace EnterpriseDevelopment.Domain;

/// <summary>
/// Выставка
/// </summary>
public class Exhibition
{
    /// <summary>
    /// Идентификатор выставки
    /// </summary>
    public required int Id { get; init; }
    /// <summary>
    /// Название выставки
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// Тема выставки
    /// </summary>
    public required ExhibitionTheme Theme { get; init; }
    /// <summary>
    /// Номер зала
    /// </summary>
    public required int HallNumber { get; init; }
    /// <summary>
    /// Дата начала выставки
    /// </summary>
    public required DateOnly StartDate { get; init; }
    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    public required DateOnly EndDate { get; init; }
}