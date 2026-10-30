using Museum.Application.Contracts.Common;
using Museum.Application.Contracts.Exhibitions;

namespace Museum.Application.Contracts.Excursions;

public record ExcursionDto
(
    int Id,
    DateTime StartsAt,
    TimeSpan Duration,
    List<ExhibitionDto> Exhibitions
) : IHasId;
