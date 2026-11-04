using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Analytics;
using Museum.Application.Contracts.Excursions;
using Museum.Application.Contracts.Visitors;

namespace Museum.Api.Controllers;

/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
/// <param name="analyticsService">Сервис аналитики</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController
    (IAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить топ 5 выставок по количеству уникальных посетителей
    /// </summary>
    /// <response code="200">Список топ 5 выставок</response>
    [HttpGet("top-exhibitions")]
    public async Task<ActionResult<IList<ExhibitionVisitsDto>>> GetTopExhibitions()
    {
        logger.LogInformation("HTTP GET запрос: топ 5 выставок по уникальным посетителям");
        return Ok(await analyticsService.GetTopExhibitions());
    }

    /// <summary>
    /// Получить экскурсии с минимальным количеством посетителей
    /// </summary>
    /// <response code="200">Список экскурсий с минимальным количеством посетителей</response>
    [HttpGet("min-visitors-excursions")]
    public async Task<ActionResult<IList<ExcursionVisitsDto>>> GetMinVisitorsExcursions()
    {
        logger.LogInformation("HTTP GET запрос: экскурсии с минимальным количеством посетителей");
        return Ok(await analyticsService.GetExcursionsWithMinVisitors());
    }

    /// <summary>
    /// Получить статистику по темам выставок за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала</param>
    /// <param name="endDate">Дата окончания</param>
    /// <response code="200">Список статистики по темам выставок</response>
    [HttpGet("theme-statistics")]
    public async Task<ActionResult<IList<ThemeStatisticsDto>>> GetThemeStatistics([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        logger.LogInformation("HTTP GET запрос: статистика по темам выставок за период {StartDate} - {EndDate}", startDate, endDate);
        return Ok(await analyticsService.GetThemeStatistics(startDate, endDate));
    }

    /// <summary>
    /// Получить список экскурсий в указанном зале за указанный период
    /// </summary>
    /// <param name="hallNumber">Номер зала</param>
    /// <param name="startDate">Дата начала</param>
    /// <param name="endDate">Дата окончания</param>
    /// <response code="200">Список экскурсий в указанном зале за указанный период</response>
    [HttpGet("excursions-in-hall")]
    public async Task<ActionResult<IList<ExcursionDto>>> GetExcursionsInHall([FromQuery] int hallNumber, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        logger.LogInformation("HTTP GET запрос: экскурсии в зале {HallNumber} за период {StartDate} - {EndDate}", hallNumber, startDate, endDate);
        return Ok(await analyticsService.GetExcursionsInHall(hallNumber, startDate, endDate));
    }

    /// <summary>
    /// Получить список посетителей, которые посетили указанную экскурсию
    /// </summary>
    /// <param name="excursionId">идентификтаор экскурсии</param>
    /// <response code="200">Список посетителей</response>
    [HttpGet("visitors-by-excursion/{excursionId}")]
    public async Task<ActionResult<IList<VisitorDto>>> GetVisitorsByExcursion(int excursionId)
    {
        logger.LogInformation("HTTP GET запрос: посетители экскурсии {ExcursionId}", excursionId);
        return Ok(await analyticsService.GetVisitorsByExcursion(excursionId));
    }
}