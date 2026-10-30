using Museum.Domain;

namespace Museum.Application.Contracts;

public record ExcursionDto
(
    int Id,
    DateTime StartsAt,
    TimeSpan Duration,
    List<Exhibition> Exhibitions
);
