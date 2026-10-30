using System.ComponentModel.DataAnnotations;
using Museum.Domain;

namespace Museum.Application.Contracts.Exhibitions;

public record ExhibitionCreateUpdateDto : IValidatableObject
{
    [Required, StringLength(200)]
    public required string Name { get; init; }

    public required ExhibitionTheme Theme { get; init; }

    [Range(1, 999)]
    public required int HallNumber { get; init; }

    public required DateOnly StartDate { get; init; }

    public required DateOnly EndDate { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if ( EndDate < StartDate)
                    {
            errors.Add(new ValidationResult(
                "Дата окончания не может быть раньше даты начала",
                new[] { nameof(EndDate) }
                ));
        }

        return errors;
    }

}


