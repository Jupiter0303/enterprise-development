namespace Museum.Application.Contracts.Exhibitions;

public record ExhibitionDto
(
    int Id,
    string Name,
    int HallNumber,
    Domain.ExhibitionTheme Theme,
    DateOnly StartDate,
    DateOnly EndDate
);


