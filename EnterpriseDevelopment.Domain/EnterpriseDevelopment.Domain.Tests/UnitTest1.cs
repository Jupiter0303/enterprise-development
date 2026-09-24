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
        Assert.Equal([7, 7, 5, 4, 3], topFiveExhibitions.Select(pair => pair.VisitCount));
        Assert.Equal([1, 5, 2, 7, 6], topFiveExhibitions.Select(pair => pair.Exhibition.Id));
    }

    public class ExcursionTests
    {
        [Fact]
        public void TopThreeExcursions_ByUniqueVisitors_ReturnsCorrectRanking()
        {
            var tickets = Seed.Tickets;
            var excursions = tickets
                        .Select(t => new { Excursion = t.Excursion, VisitorId = t.Visitor.Id })
                        .DistinctBy(x => new { ExcursionId = x.Excursion.Id, VisitorId = x.VisitorId })
                        .GroupBy(x => x.Excursion.Id)
                        .Select(g => new { Excursion = g.First().Excursion, VisitCount = g.Count() })
                        .Concat(
                                 Seed.Excursions
                                .Select(ex => new { Excursion = ex, VisitCount = 0 })
                                )
                        .DistinctBy(pair => pair.Excursion.Id)
                        .OrderBy(pair => pair.VisitCount)
                        .ThenBy(pair => pair.Excursion.Id)
                        .Take(3)
                        .ToList();
            Assert.Equal(3, excursions.Count);
            Assert.Equal([9, 3, 4], excursions.Select(x => x.Excursion.Id));
            Assert.Equal([0, 1, 1], excursions.Select(x => x.VisitCount));


        }

    }
}
