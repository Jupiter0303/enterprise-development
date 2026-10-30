using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;

namespace Museum.Application.Contracts.Tickets;

public record TicketDto
(
    int Id,
    VisitorDto VisitorId,
    ExcursionDto Excursion,
    Domain.TicketType Type,
    decimal Price
);
