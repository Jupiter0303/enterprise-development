using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;
using Museum.Domain;

namespace Museum.Application.Contracts.Tickets;

/// <summary>
/// DTO для передачи данных о билете
/// </summary>
/// <param name="Id">Идентификатор билета</param>
/// <param name="Excursion">Экскурсия, на которую оформлен билет</param>
/// <param name="Visitor">Посетитель, которому оформлен билет</param>
/// <param name="Type">Тип билета</param>
/// <param name="Price"></param>
public record TicketDto
(
    int Id,
    ExcursionDto Excursion,
    VisitorDto Visitor,
    TicketType Type,
    decimal Price
) : IHasId;
