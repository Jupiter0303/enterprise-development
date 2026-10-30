namespace Museum.Application.Contracts;

public record TicketDto
(
    int Id,
    VisitorDto VisitorId,
    ExcursionDto Excursion,
    Domain.TicketType Type,
    decimal Price
);
