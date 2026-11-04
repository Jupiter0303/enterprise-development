using System.ComponentModel.DataAnnotations;

namespace Museum.Application.Contracts.Tickets;

/// <summary>
/// DTO для создания и обновления билета
/// </summary>
public record TicketCreateUpdateDto
{
    /// <summary>
    /// Идентификатор экскурсии, на которую оформлен билет
    /// </summary>
    public required int ExcursionId { get; init; }

    /// <summary>
    /// Идентификатор посетителя, которому оформлен билет
    /// </summary>
    public required int VisitorId { get; init; }

    /// <summary>
    /// Тип билета
    /// </summary>
    [EnumDataType(typeof(Domain.TicketType))]
    public required Domain.TicketType Type { get; init; }

    /// <summary>  
    /// Цена билета
    /// </summary>
    [Range(typeof(decimal), "0","10000", ErrorMessage = "Цена должна быть между 0 и 10000")]
    public required decimal Price { get; init; }
}
