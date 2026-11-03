using Microsoft.AspNetCore.Mvc;
using Museum.Application.Contracts.Analytics;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController
    (IAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    [HttpGet("top-exhibitions")]
    public async Task<IActionResult> GetTopExhibitions()
    {
        logger.LogInformation("HTTP GET запрос: топ 5 выставок по уникальным посетителям");
        return Ok(await analyticsService.GetTopExhibitions());
    }

    [HttpGet("min-visitors-excursions")]
    public async Task<IActionResult> GetMinVisitorsExcursions()
    {
        logger.LogInformation("HTTP GET запрос: экскурсии с минимальным количеством посетителей");
        return Ok(await analyticsService.GetExcursionsWithMinVisitors());
    }

    [HttpGet("theme-statistics")]
    public async Task<IActionResult> GetThemeStatistics([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate) 
    { 
        logger.LogInformation("HTTP GET запрос: статистика по темам выставок за период {StartDate} - {EndDate}", startDate, endDate);
        return Ok(await analyticsService.GetThemeStatistics(startDate, endDate)); 
    }

    [HttpGet("excursions-in-hall")]
    public async Task<IActionResult> GetExcursionsInHall([FromQuery] int hallNumber, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate) 
    { 
        logger.LogInformation("HTTP GET запрос: экскурсии в зале {HallNumber} за период {StartDate} - {EndDate}", hallNumber, startDate, endDate);
        return Ok(await analyticsService.GetExcursionsInHall(hallNumber, startDate, endDate)); 
    }

    [HttpGet("visitors-by-excursion/{excursionId}")]
    public async Task<IActionResult> GetVisitorsByExcursion(int excursionId) 
    { 
        logger.LogInformation("HTTP GET запрос: посетители экскурсии {ExcursionId}", excursionId);
        return Ok(await analyticsService.GetVisitorsByExcursion(excursionId));
    }
}