using System.ComponentModel.DataAnnotations;

namespace Museum.Application.Contracts;



public record VisitorCreateUpdateDto
{
    [Required, StringLength(100)]
    public required string FirstName { get; init; }

    [Required, StringLength(100)]
    public required string LastName { get; init; }

    [StringLength(100)]
    public string? MiddleName { get; init; }

    [Range(typeof(DateOnly), "1900-01-01", "2100-01-01")]
    public required DateOnly Birthday { get; init; }

    [Phone]
    public string? PhoneNumber { get; init; }
}

