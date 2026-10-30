namespace Museum.Application.Contracts;

internal record ExhibitionDto
(
    int Id,
    string Name,
    int HallNumber,
    Domain.ExhibitionTheme Theme,
    DateOnly StartDate,
    DateOnly EndDate
);


