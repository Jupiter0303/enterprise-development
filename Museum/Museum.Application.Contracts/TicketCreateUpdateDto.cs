using System.ComponentModel.DataAnnotations;

namespace Museum.Application.Contracts;

public record TicketCreateUpdateDto
{

    public required int ExcursionId { get; init; }
    public required int VisitorId { get; init; }
    public required Domain.TicketType Type { get; init; }

    [Range(0,10000, ErrorMessage = "Цена должна быть между 0 и 10000")]
    public required decimal Price { get; init; }
}
