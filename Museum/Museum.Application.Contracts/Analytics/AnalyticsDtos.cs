using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Exhibitions;
using Museum.Domain;

namespace Museum.Application.Contracts.Analytics;

public record ExhibitionVisitsDto (
    ExhibitionDto Exhibition,
    int VisitorCount
    );

public record ExcursionVisitsDto(
    ExcursionDto Excursion,
    int VisitorCount
);

public record ThemeStatisticsDto(
    ExhibitionTheme Theme,
    int TotalVisitors,
    decimal TotalPrice,
    int MinVisitorsPerDay,
    int MaxVisitorsPerDay,
    double AverageVisitorsPerDay
);
