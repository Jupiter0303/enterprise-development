using EnterpriseDevelopment.Domain;

namespace EnterpriseDevelopment.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {


    }

}


 
}

public class VisitorTests
{
    [Fact]
    public void GetVisitors_ReturnsAtLeastTenVisitors()
    {
        //получение данных о посетителях из метода GetVisitors
        var visitors = Seed.GetVisitors();
        //действие для проверки , что количество посетителей больше или равно 10
        var count = visitors.Count;
        //проверка услови я
        Assert.True(count >= 10);
    }
}

public class TicketsTests
{
    [Fact]
    public void GetTickets_ReturnsAtLargeFiveExhibitions()
    {
        var tickets = Seed.GetTickets();

        var topFiveExhibitions = tickets
            .SelectMany(t => t.Excursion.Exhibitions.Select(ex => new {Exhibition = ex, VisitorId = t.Visitor.Id} ))
            .DistinctBy(x => new {x.Exhibition, x.VisitorId })
            .GroupBy(x => x.Exhibition.Id)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new {Exhibition = g.First().Exhibition, VisitCount = g.Count() })
            .ToList();
    }
}
