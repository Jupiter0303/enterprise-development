using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseDevelopment.Domain;




public record Ticket
{
    public required int Id { get; init; }
    public required Excursion Excursion { get; init; }
    public required Visitor Visitor { get; init; }
    public required TicketType Type { get; init; }
    public required decimal Price { get; init; }
}