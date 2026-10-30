using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;
using Museum.Domain;

namespace Museum.Application.Contracts.Tickets;

public record TicketDto
(
    int Id,
    ExcursionDto Excursion,
    VisitorDto Visitor,
    TicketType Type,
    decimal Price
) : IHasId;
