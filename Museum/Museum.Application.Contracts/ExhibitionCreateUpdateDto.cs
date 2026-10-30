using System.ComponentModel.DataAnnotations;

namespace Museum.Application.Contracts;

public class ExhibitionCreateUpdateDto
{
    [Required, StringLength(200)]
    public required string Name { get; set; } = string.Empty;

    [Required]
    public required Domain.ExhibitionTheme Theme { get; set; }

    [Required]
    public required DateOnly StartDate { get; set; }

    public required DateOnly EndDate { get; set; }
}
