namespace EnterpriseDevelopment.Domain;

/// <summary>
/// Билет на экскурсию
/// </summary>
public record Ticket
{
    /// <summary>
    /// Идентификатор билета
    /// </summary>
    public required int Id { get; init; }
    /// <summary>
    /// Экскурсия, на которую оформлен билет
    /// </summary>
    public required Excursion Excursion { get; init; }
    /// <summary>
    /// Посетитель, купивший билет
    /// </summary>
    public required Visitor Visitor { get; init; }
    /// <summary>
    /// Тип билета
    /// </summary>
    public required TicketType Type { get; init; }
    /// <summary>
    /// Цена билета
    /// </summary>
    public required decimal Price { get; init; }
}