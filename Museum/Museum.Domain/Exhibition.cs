namespace Museum.Domain;

/// <summary>
/// Выставка
/// </summary>
public class Exhibition
{
    /// <summary>
    /// Идентификатор выставки
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Название выставки
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Тема выставки
    /// </summary>
    public required ExhibitionTheme Theme { get; set; }
    /// <summary>
    /// Номер зала
    /// </summary>
    public required int HallNumber { get; set; }
    /// <summary>
    /// Дата начала выставки
    /// </summary>
    public required DateOnly StartDate { get; set; }
    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    public required DateOnly EndDate { get; set; }
}