using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;


namespace Museum.Application.Contracts.Analytics;

public interface IAnalyticsService
{
    public Task<IList<ExhibitionVisitsDto>> GetTopExhibitions();
    public Task<IList<ExhibitionVisitsDto>> GetExcursionsWithMinVisitors();
    public Task<IList<ThemeStatisticsDto>> GetThemeStatistics(DateOnly startDate, DateOnly endDate);
    public Task<IList<ExcursionDto>> GetExcursionsInHall(int hallNumber, DateOnly startDate, DateOnly endDate);
    public Task<IList<VisitorDto>> GetVisitorsByExcursion(int excursionId);
}
