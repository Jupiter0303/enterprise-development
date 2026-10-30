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

    public required DateOnly Birthday { get; init; }
    [Phone]
    public string? Phone { get; init; }
}

