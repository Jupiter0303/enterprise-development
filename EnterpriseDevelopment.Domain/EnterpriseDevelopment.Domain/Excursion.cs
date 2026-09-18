using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseDevelopment.Domain;

public class Excursion
{
    public required int Id { get; init; }
    public required DateOnly Date { get; init; }
    public required TimeOnly StartTime { get; init; }
    public required TimeSpan Duration { get; init; }
    public required List<Exhibition> Exhibitions { get; init; }


}