using System.ComponentModel.DataAnnotations;
using Museum.Shared.Enums;

namespace Museum.Application.Contracts.Exhibitions;

/// <summary>
/// DTO для создания и обновления выставки
/// </summary>
public record ExhibitionCreateUpdateDto : IValidatableObject
{
    /// <summary>
    /// Название выставки
    /// </summary>
    [Required, StringLength(200)]
    public required string Name { get; init; }

    /// <summary>
    /// Тема выставки
    /// </summary>
    [EnumDataType(typeof(ExhibitionTheme))]
    public required ExhibitionTheme Theme { get; init; }

    /// <summary>
    /// Номер зала, в котором проходит выставка
    /// </summary>
    [Range(1, 999)]
    public required int HallNumber { get; init; }

    /// <summary>
    /// Дата начала выставки
    /// </summary>
    public required DateOnly StartDate { get; init; }

    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    public required DateOnly EndDate { get; init; }

    /// <summary>
    /// Валидация данных выставки вызывемая фреймворком
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (EndDate < StartDate)
        {
            errors.Add(new ValidationResult(
                "Дата окончания не может быть раньше даты начала",
                new[] { nameof(EndDate) }
                ));
        }

        return errors;
    }
}