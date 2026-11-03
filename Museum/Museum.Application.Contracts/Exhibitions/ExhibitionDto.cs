using Museum.Application.Contracts.Common;
using Museum.Domain;
using System.ComponentModel.DataAnnotations;

namespace Museum.Application.Contracts.Exhibitions;

public record ExhibitionDto
(
    int Id,
    string Name,
    ExhibitionTheme Theme,
    int HallNumber,
    DateOnly StartDate,
    DateOnly EndDate
) : IHasId;


