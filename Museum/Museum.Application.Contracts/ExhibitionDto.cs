namespace Museum.Application.Contracts;

public record ExhibitionDto
(
    int Id,
    string Name,
    int HallNumber,
    Domain.ExhibitionTheme Theme,
    DateOnly StartDate,
    DateOnly EndDate
);


