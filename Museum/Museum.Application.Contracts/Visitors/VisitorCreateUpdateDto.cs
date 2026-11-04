using System.ComponentModel.DataAnnotations;

namespace Museum.Application.Contracts.Visitors;


/// <summary>
/// DTO для создания и обновления посетителя
/// </summary>
public record VisitorCreateUpdateDto
{
    /// <summary>
    /// Имя посетителя
    /// </summary>
    [Required, StringLength(100)]

    public required string FirstName { get; init; }
    /// <summary>
    /// Фамилия посетителя
    /// </summary>
    [Required, StringLength(100)]

    public required string LastName { get; init; }
    /// <summary>
    /// Отчество посетителя
    /// </summary>
    [StringLength(100)]
    public string? MiddleName { get; init; }

    /// <summary>
    /// День рождения посетителя
    /// </summary>
    [Range(typeof(DateOnly), "1900-01-01", "2100-01-01")]
    public required DateOnly Birthday { get; init; }

    /// <summary>
    /// Номер телефона посетителя
    /// </summary>
    [Phone]
    public string? PhoneNumber { get; init; }
}

