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

    public class ThemesTests
    {
        [Fact]
        public void ExhibitionAttendanceSummary_ByThemeAndPeriod_ReturnsCorrectStatistics()
        {
            var startDate = new DateOnly(2023, 1, 1);
            var endDate = new DateOnly(2023, 12, 31);
            var tickets = Seed.Tickets;
            var themes = tickets
                .Where(t => t.Excursion.Date >= startDate && t.Excursion.Date <= endDate)
                .GroupBy(t => t.Excursion.Date)
                .SelectMany(dateGroup => dateGroup
                              .DistinctBy(t => new { t.Visitor, t.Excursion })
                              .GroupBy(t => t.Excursion.Id)
                              .SelectMany(excursionGroup => excursionGroup.First()
                                                                          .Excursion
                                                                          .Exhibitions
                                                                          .DistinctBy(exh => exh.Theme)
                                                                          .Select(exh => new
                                                                          {
                                                                              ExhibitionTheme = exh.Theme,
                                                                              VisitCount = excursionGroup.Count(),
                                                                              PriceExhibitionTheme = excursionGroup.Sum(ex => ex.Price) / excursionGroup.Count() / excursionGroup.First().Excursion
                                                                                                                                                                                         .Exhibitions
                                                                                                                                                                                         .Select(exh => exh.Theme)
                                                                                                                                                                                         .Distinct()
                                                                                                                                                                                         .Count()
                                                                          })

                              ).GroupBy(pairThemeVisit => pairThemeVisit.ExhibitionTheme)
                              .Select(themeGroup => new
                              {
                                  Theme = themeGroup.Key,
                                  TotalVisits = themeGroup.Sum(ThemeVisitsPriceInLocalExcurce => ThemeVisitsPriceInLocalExcurce.VisitCount),
                                  TotalPrice = themeGroup.Sum(x => x.PriceExhibitionTheme * x.VisitCount)
                              })
                  )
                  .GroupBy(ThemeVisitsPriceInLocalDay => ThemeVisitsPriceInLocalDay.Theme)
                  .Select(themeGroup => new
                  {
                      Theme = themeGroup.Key,
                      TotalPrice = themeGroup.Sum(ThemeVisitsPriceInLocalDay => ThemeVisitsPriceInLocalDay.TotalPrice),
                      MinimalCountVisits = themeGroup.Min(ThemeVisitsPriceInLocalDay => ThemeVisitsPriceInLocalDay.TotalVisits),
                      MaximalCountVisits = themeGroup.Max(x => x.TotalVisits),
                      AverageCountVisits = themeGroup.Average(ThemeVisitsPriceInLocalDay => ThemeVisitsPriceInLocalDay.TotalVisits)
                  })
                  .ToList();



        }
    }
}



  public void ExhibitionAttendanceSummary_ByThemeAndPeriod_ReturnsCorrectStatistics()
    {
        var startDate = new DateOnly(2023, 1, 1);
        var endDate = new DateOnly(2023, 12, 31);
        var tickets = Seed.Tickets;
        var themes = tickets
            .Where(t => t.Excursion.Date >= startDate && t.Excursion.Date <= endDate)
            .GroupBy(t => t.Excursion.Date)
            .Select(dateGroup => dateGroup
                          .DistinctBy(t => new { Visitor = t.Visitor, Excursion = t.Excursion })
                          .GroupBy(t => t.Excursion.Id)
                          .Select(excursionGroup => new
                          {
                              Excursion = excursionGroup.First().Excursion,
                              VisitCount = excursionGroup.Count(),
                              PriceExhibitionTheme = excursionGroup.Sum(ex => ex.Price) / excursionGroup.Count() / excursionGroup.First()
                                                                                                                                        .Excursion
                                                                                                                                        .Exhibitions
                                                                                                                                        .Select(exh => exh.Theme)
                                                                                                                                        .Distinct()
                                                                                                                                        .Count()
                          })
                          .SelectMany(pairExсVisit => pairExсVisit.Excursion.Exhibitions
                                                                .Select(ex => new
                                                                {
                                                                    ExhibitionTheme = ex.Theme,
                                                                    VisitCount = pairExсVisit.VisitCount,
                                                                    PriceExhibitionTheme = pairExсVisit.PriceExhibitionTheme
                                                                })
                                                                )
                          .GroupBy(pairThemeVisit => pairThemeVisit.ExhibitionTheme)
                          .Select(themesGroup => new
                          {
                              Theme = themesGroup.Key,
                              TotalVisits = themesGroup.Sum(x => x.VisitCount)
                          })
                          );