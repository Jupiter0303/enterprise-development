using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;

namespace Museum.Application.Contracts.Tickets;

public record TicketDto
(
    int Id,
    ExcursionDto Excursion,
    VisitorDto Visitor,
    Domain.TicketType Type,
    decimal Price
);
