using Museum.Domain;

namespace Museum.Application.Contracts.Exhibitions;

public record ExhibitionDto
(
    int Id,
    string Name,
    ExhibitionTheme Theme,
    int HallNumber,
    DateOnly StartDate,
    DateOnly EndDate
);


