using System.ComponentModel.DataAnnotations;

namespace Museum.Application.Contracts.Tickets;

public record TicketCreateUpdateDto
{

    public required int ExcursionId { get; init; }
    public required int VisitorId { get; init; }
    [EnumDataType(typeof(Domain.TicketType))]
    public required Domain.TicketType Type { get; init; }

    [Range(typeof(decimal), "0","10000", ErrorMessage = "Цена должна быть между 0 и 10000")]
    public required decimal Price { get; init; }
}
