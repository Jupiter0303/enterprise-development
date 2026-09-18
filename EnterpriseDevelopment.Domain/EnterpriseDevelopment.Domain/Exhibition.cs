using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseDevelopment.Domain;



public class Exhibition
{
     
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required ExhibitionTheme Theme { get; init; }
    public required int HallNumber { get; init; }
    public required DateOnly StartDate { get; init; }
    public required DateOnly EndDate { get; init; }
}