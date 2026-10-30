namespace Museum.Application.Contracts.Visitors;

public record VisitorDto
(
    int Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly Birthday,
    string? PhoneNumber
);