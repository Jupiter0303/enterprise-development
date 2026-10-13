namespace EnterpriseDevelopment.Domain;

/// <summary>
/// Билет на экскурсию
/// </summary>
public class Ticket
{
    /// <summary>
    /// Идентификатор билета
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Экскурсия, на которую оформлен билет
    /// </summary>
    public required Excursion Excursion { get; set; }
    /// <summary>
    /// Посетитель, купивший билет
    /// </summary>
    public required Visitor Visitor { get; set; }
    /// <summary>
    /// Тип билета
    /// </summary>
    public required TicketType Type { get; set; }
    /// <summary>
    /// Цена билета
    /// </summary>
    public required decimal Price { get; set; }
}