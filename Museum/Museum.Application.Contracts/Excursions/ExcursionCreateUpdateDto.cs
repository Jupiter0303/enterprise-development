using System.ComponentModel.DataAnnotations;

namespace Museum.Application.Contracts.Excursions;

/// <summary>
/// DTO для создания и обновления экскурсии
/// </summary>
public record ExcursionCreateUpdateDto
{
    /// <summary>
    /// Дата и время начала экскурсии
    /// </summary>
    public required DateTime StartsAt { get; init; }

    /// <summary>
    /// Длительность экскурсии
    /// </summary>
    public required TimeSpan Duration { get; init; }

    /// <summary>
    /// Список идентификаторов выставок, которые будут включены в экскурсию
    /// </summary>

    [Required, MinLength(1, ErrorMessage = "В экскурсии должна быть хотя бы 1 выставка")]
    public required List<int> ExhibitionIds { get; init; }
}