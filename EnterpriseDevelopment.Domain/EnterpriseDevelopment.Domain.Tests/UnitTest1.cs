using EnterpriseDevelopment.Domain;

namespace EnterpriseDevelopment.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {


    }

}



public class VisitorTests
{
    [Fact]
    public void GetVisitors_ReturnsAtLeastTenVisitors()
    {
        //получение данных о посетителях из метода GetVisitors
        var visitors = Seed.Visitors;
        //действие для проверки , что количество посетителей больше или равно 10
        var count = visitors.Count;
        //проверка услови я
        Assert.True(count >= 10);
    }
}

public class TicketsTests
{
    [Fact]
    public void TopFiveExhibitions_ByUniqueVisitors_ReturnsCorrectRanking()
    {
        var tickets = Seed.Tickets;

        var topFiveExhibitions = tickets
            .SelectMany(t => t.Excursion.Exhibitions.Select(ex => new { Exhibition = ex, VisitorId = t.Visitor.Id }))
            .DistinctBy(x => new { ExhibitionId = x.Exhibition.Id, VisitorId = x.VisitorId })
            .GroupBy(x => x.Exhibition.Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => new { Exhibition = g.First().Exhibition, VisitCount = g.Count() })
            .ToList();
        Assert.Equal(5, topFiveExhibitions.Count);
        Assert.Equal([7, 7, 5, 4, 3], topFiveExhibitions.Select(x => x.VisitCount));
        Assert.Equal([1, 5, 2, 7, 6], topFiveExhibitions.Select(x => x.Exhibition.Id));
    }
}
