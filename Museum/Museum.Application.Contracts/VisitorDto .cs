using System;
using System.Collections.Generic;
using System.Text;

namespace Museum.Application.Contracts;

public record VisitorDto
(
    int Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly Birthday,
    string? PhoneNumber
);