using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseDevelopment.Domain;

public class Visitor
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init;  }
    public required DateOnly Birthday { get; init; }
    public string? PhoneNumber { get; set; }
    
}

