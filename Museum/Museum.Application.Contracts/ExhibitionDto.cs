namespace Museum.Application.Contracts;

internal record ExhibitionDto
(
    int Id,
    string Name,
    Domain.ExhibitionTheme Theme,
    DateOnly StartDate,
    DateOnly EndDate
);


