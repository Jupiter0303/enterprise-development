using System.ComponentModel.DataAnnotations;

namespace Museum.Application.Contracts;

public record ExcursionCreateUpdateDto
{
    public required DateTime StartsAt { get; init; }
    public required TimeSpan Duration { get; init; }

    [Required, MinLength(1, ErrorMessage = "В экскурсии должна быть хотя бы 1 выставка")]
    public required List<int> ExhibitionIds { get; init; }

}
